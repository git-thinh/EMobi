using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using TeamDev.Redis;

namespace EMobiTestUI
{
    static class Program 
    {
        public static App app;

        [STAThread]
        static void Main()
        {
            app = new App();

            app.redis_Exit();

            if (!Directory.Exists(app.PATH_DATA)) Directory.CreateDirectory(app.PATH_DATA);

            string REDIS_PATH = Path.Combine(app.PATH_DATA, "emobi-db.exe");
            if (!File.Exists(REDIS_PATH))
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "EMobiTestUI.DLL.emobi-db.exe";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (BinaryReader br = new BinaryReader(stream))
                {
                    var buf = br.ReadBytes((int)stream.Length);
                    File.WriteAllBytes(REDIS_PATH, buf);
                    Thread.Sleep(100);
                }
            }

            app.REDIS_THREAD = new Thread(() =>
            {
                app.REDIS_PROCESS = new Process();
                var REDIS_PROCESS = app.REDIS_PROCESS;

                REDIS_PROCESS.StartInfo.WorkingDirectory = app.PATH_DATA;
                REDIS_PROCESS.StartInfo.Arguments = "--port " + app.REDIS_PORT.ToString() + " --bind 127.0.0.1 --dbfilename _data.bin";
                REDIS_PROCESS.StartInfo.FileName = REDIS_PATH;

                REDIS_PROCESS.StartInfo.UseShellExecute = false;
                REDIS_PROCESS.StartInfo.RedirectStandardOutput = true;
                REDIS_PROCESS.StartInfo.RedirectStandardError = true;
                REDIS_PROCESS.StartInfo.CreateNoWindow = true;
                REDIS_PROCESS.ErrorDataReceived += (se, ev) =>
                {
                    app.REDIS_OPEN = false;
                    app.REDIS_SIGNAL.Set();
                };
                REDIS_PROCESS.OutputDataReceived += (se, ev) =>
                {
                    Debug.WriteLine(ev.Data);
                    if (!string.IsNullOrEmpty(ev.Data) && ev.Data.Contains("Ready"))
                    {
                        app.REDIS_OPEN = true;
                        app.REDIS_SIGNAL.Set();
                    }
                };
                REDIS_PROCESS.EnableRaisingEvents = true;
                REDIS_PROCESS.Start();
                REDIS_PROCESS.BeginOutputReadLine();
                REDIS_PROCESS.BeginErrorReadLine();
                REDIS_PROCESS.WaitForExit();

                ////ProcessStartInfo r = new ProcessStartInfo(REDIS_PATH);
                ////r.UseShellExecute = false;
                //////r.CreateNoWindow = true;
                ////r.Arguments = "--port " + REDIS_PORT.ToString() + " --bind 127.0.0.1"; 
                ////var p = Process.Start(r);
                //////IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
                //////IntPtr h = p.MainWindowHandle;
                //////ShowWindow(h, 0);
                ////p.WaitForExit();
            });
            app.REDIS_THREAD.Start();

            app.REDIS_SIGNAL.WaitOne();

            Thread.Sleep(100);

            if (app.REDIS_OPEN)
            {
                app.m_redis = new RedisDataAccessProvider("127.0.0.1", app.REDIS_PORT);

                //////IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
                ////IntPtr h = REDIS_PROCESS.MainWindowHandle;
                ////ShowWindow(h, 0);

                //// active SSL 1.1, 1.2, 1.3 for WebClient request HTTPS
                ServicePointManager.DefaultConnectionLimit = 1000;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | (SecurityProtocolType)3072 | (SecurityProtocolType)0x00000C00 | SecurityProtocolType.Tls;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new fMain(app));
            }
            else
            {
                app.redis_Exit();
                Thread.Sleep(300);
                Application.Restart();
                Environment.Exit(0);
            }
        }
    }
}
