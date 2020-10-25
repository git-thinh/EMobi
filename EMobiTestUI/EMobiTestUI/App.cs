using ICSharpCode.SharpZipLib.Zip;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using TeamDev.Redis;

namespace EMobiTestUI
{
    class App : IApp
    {
        public App()
        {
            FOLDER_DATA = "Mr.Thinh's Gifts";
            PATH_DATA = Application.StartupPath[0] + @":\" + FOLDER_DATA;

            REDIS_PORT = 6379;
            //REDIS_PORT = getFreeTcpPort();
            REDIS_SIGNAL = new AutoResetEvent(false);
        }

        #region [ MEMEBR ]

        int _pageNumber = 0;
        public int PageNumber { 
            set 
            {
                _pageNumber = value;
                redis_Update("PAGE_NUMBER", value);
            }
            get { return _pageNumber; } 
        }

        string _documentFile = string.Empty;
        public string DocumentFile { 
            set {
                _documentFile = value;
                redis_Update("DOCUMENT_FILE", value, true);
            } 
            get { return _documentFile; } 
        }

        public string DocumentName { set; get; }
        public string PathBrowserLastest { set; get; }

        //------------------------------------------------------------------------------

        public string FOLDER_DATA { get; }
        public string PATH_DATA { get; }
        public int REDIS_PORT { get;  }
        public bool REDIS_OPEN { set; get; }

        #endregion

        #region [ METHOD ]

        public int redis_getInt(string key) {
            int v = 0;
            if (m_redis.Key.Exists(key))
            {
                string s = m_redis.Strings[key].Get();
                if (!string.IsNullOrEmpty(s)) int.TryParse(s, out v);
            }
            return v;
        }

        public string redis_getString(string key)
        {
            string s = string.Empty;
            if (m_redis.Key.Exists(key))
            {
                s = m_redis.Strings[key].Get();
                if (string.IsNullOrEmpty(s)) return s = string.Empty;
            }
            return s;
        }

        public void redis_Update(string key, int value, bool writeFile = false) => redis_Update(key, value.ToString(), writeFile);
        public void redis_Update(string key, string value, bool writeFile = false) {
            this.m_redis.Strings[key].Set(value);
            if (writeFile) redis_writeFile();
        }

        public void redis_writeFile()
        {
            if (m_redis != null)
            {
                m_redis.WaitComplete(m_redis.SendCommand(RedisCommand.BGSAVE));
            }
        }

        public void redis_Exit()
        {
            Process.Start("TASKKILL", @"/F /IM ""emobi-db.exe*""");
            Thread.Sleep(200);
            if (REDIS_THREAD != null) REDIS_THREAD.Abort();
        }

        public string doc_formatName(string name) {
            if (string.IsNullOrEmpty(name)) return string.Empty;
            name = name.Trim().ToLower();
            
            if (name.EndsWith(".pdf") || name.EndsWith(".ebk")) 
                name = name.Substring(0, name.Length - 4).Trim();

            name = name.Replace('-', ' ')
                .Replace('_', ' ')
                .Replace('.', ' ')
                .Replace(';', ' ');

            name = new Regex("[ ]{2,}", RegexOptions.None).Replace(name, " ");
            return name;
        }

        public Dictionary<int, byte[]> doc_recentOpen() {
            string recentFile = redis_getString("DOCUMENT_FILE"),
                docName = doc_formatName(Path.GetFileName(recentFile)),
                ebkFile = Path.Combine(PATH_DATA, docName + ".ebk");
            if (File.Exists(ebkFile)) {
                return ebk_Read(ebkFile);
            }
            return new Dictionary<int, byte[]>();
        }

        public Dictionary<int, byte[]> ebk_Read(string fileEBK)
        {
            string docName = doc_formatName(Path.GetFileName(fileEBK));
            var dic = new Dictionary<int, byte[]>() { };

            using (ZipInputStream zstream = new ZipInputStream(File.OpenRead(fileEBK)))
            {
                zstream.Password = FOLDER_DATA;

                ZipEntry entry;
                while ((entry = zstream.GetNextEntry()) != null)
                {
                    int i = int.Parse(entry.Name.Substring(0, entry.Name.Length - 4));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        byte[] data = new byte[4096];
                        int size = zstream.Read(data, 0, data.Length);
                        ms.Write(data, 0, size);
                        while (size > 0)
                        {
                            size = zstream.Read(data, 0, data.Length);
                            if (size > 0)
                                ms.Write(data, 0, size);
                            else
                            {
                                dic.Add(i, ms.ToArray());
                            }
                        }
                    }
                }
            }

            PageNumber = 0;
            DocumentFile = fileEBK;
            DocumentName = docName;

            return dic;
        }

        #endregion

        #region [ THREAD REDIS ]

        public Thread REDIS_THREAD = null;
        public Process REDIS_PROCESS = null;
        public AutoResetEvent REDIS_SIGNAL;
        public RedisDataAccessProvider m_redis;

        public int getFreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            Thread.Sleep(100);
            return port;
        }

        #endregion
    }

    public interface IApp
    {
        void redis_Exit();
        void redis_writeFile();
        int redis_getInt(string key);
        string redis_getString(string key);
        void redis_Update(string key, int value, bool writeFile = false);
        void redis_Update(string key, string value, bool writeFile = false);
        string doc_formatName(string name);

        Dictionary<int, byte[]> doc_recentOpen();
        Dictionary<int, byte[]> ebk_Read(string fileEBK);

        bool REDIS_OPEN { get; set; }
        int REDIS_PORT { get; }
        string FOLDER_DATA { get; }
        string PATH_DATA { get; }

        int PageNumber { set; get; }
        string DocumentFile { set; get; }
        string DocumentName { set; get; }
        string PathBrowserLastest { set; get; }
    }

}
