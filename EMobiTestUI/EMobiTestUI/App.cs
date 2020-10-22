using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace EMobiTestUI
{
    class App
    {
        const int REDIS_PORT = 6379;

        static Thread REDIS_THREAD = null;
        static bool REDIS_OPEN = false;
        static Process REDIS_PROCESS = null;
        static string PATH_DATA = Application.StartupPath[0] + @":\emobidata";
        static AutoResetEvent REDIS_SIGNAL = new AutoResetEvent(false);


        static int getFreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            Thread.Sleep(100);
            return port;
        }

        public static void exitRedis()
        {
            Process.Start("TASKKILL", @"/F /IM ""emobi-db.exe*""");
            Thread.Sleep(100);
            if (REDIS_THREAD != null) REDIS_THREAD.Abort();
        }

        [STAThread]
        static void Main()
        {
            exitRedis();

            if (!Directory.Exists(PATH_DATA)) Directory.CreateDirectory(PATH_DATA);
            string REDIS_PATH = Path.Combine(PATH_DATA, "emobi-db.exe");
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

            REDIS_THREAD = new Thread(() =>
            {
                REDIS_PROCESS = new Process();
                //REDIS_PROCESS.StartInfo.WorkingDirectory = @"dir";
                REDIS_PROCESS.StartInfo.Arguments = "--port " + REDIS_PORT.ToString() + " --bind 127.0.0.1";
                REDIS_PROCESS.StartInfo.FileName = REDIS_PATH;

                REDIS_PROCESS.StartInfo.UseShellExecute = false;
                REDIS_PROCESS.StartInfo.RedirectStandardOutput = true;
                REDIS_PROCESS.StartInfo.RedirectStandardError = true;
                REDIS_PROCESS.StartInfo.CreateNoWindow = true;
                REDIS_PROCESS.ErrorDataReceived += (se, ev) =>
                {
                    REDIS_OPEN = false;
                    REDIS_SIGNAL.Set();
                };
                REDIS_PROCESS.OutputDataReceived += (se, ev) =>
                {
                    Debug.WriteLine(ev.Data);
                    if (!string.IsNullOrEmpty(ev.Data) && ev.Data.Contains("Ready"))
                    {
                        REDIS_OPEN = true;
                        REDIS_SIGNAL.Set();
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
            REDIS_THREAD.Start();

            REDIS_SIGNAL.WaitOne();
            
            Thread.Sleep(100);

            if (REDIS_OPEN)
            {
                //////IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
                ////IntPtr h = REDIS_PROCESS.MainWindowHandle;
                ////ShowWindow(h, 0);

                //// active SSL 1.1, 1.2, 1.3 for WebClient request HTTPS
                ServicePointManager.DefaultConnectionLimit = 1000;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | (SecurityProtocolType)3072 | (SecurityProtocolType)0x00000C00 | SecurityProtocolType.Tls;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new fMain());
            }
            else {
                exitRedis();
            }
        }
    }
}
