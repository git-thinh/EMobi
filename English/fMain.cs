using CefSharp;
using CefSharp.WinForms;
using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace English
{
    public partial class fMain : Form, ISchemeHandler
    {
        string PASSWORD = "Mr.Thinh's Gifts";
        string FOLDER_DATA = "book.data";
        string PATH_DATA = Application.StartupPath[0] + @":\book.data";
        ConcurrentDictionary<int, oPage> m_infos = new ConcurrentDictionary<int, oPage>();
        ConcurrentDictionary<int, byte[]> m_pages = new ConcurrentDictionary<int, byte[]>();

        private readonly WebView web_media;
        private readonly WebView web_tree;
        private readonly WebView web_main;

        public fMain()
        {
            InitializeComponent();

            var setting = new BrowserSettings()
            {

            };

            web_media = new WebView("local://media", setting) { Dock = DockStyle.Fill };
            web_tree = new WebView("local://tree", setting) { Dock = DockStyle.Fill };
            web_main = new WebView("local://main", setting) { Dock = DockStyle.Fill };

            _panelMedia.Controls.Add(web_media);
            _panelTree.Controls.Add(web_tree);
            _panelMain.Controls.Add(web_main);
        }

        private void main_Load(object sender, EventArgs e)
        {
            this.FormClosing += main_formClosing;
            this.Shown += (se, ev) =>
            {
                this.Top = 0;
                this.Left = 0;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            };
            openFileEBK(@"C:\book.data\speakout.ebk");
        }

        private void main_formClosing(object sender, FormClosingEventArgs e)
        {
            ((IWebBrowser)web_media).Dispose();
            ((IWebBrowser)web_tree).Dispose();
            ((IWebBrowser)web_main).Dispose();
        }

        public bool ProcessRequest(IRequest request, ref string mimeType, ref Stream stream)
        {
            var uri = new Uri(request.Url);
            int page = 0;
            if (int.TryParse(uri.Host.Substring(1), out page))
            {
                if (m_pages.ContainsKey(page))
                {
                    byte[] buf = null;
                    m_pages.TryGetValue(page, out buf);
                    if (buf != null)
                    {
                        stream = new MemoryStream(buf);
                        mimeType = "image/jpeg";
                        return true;
                    }
                }
            }

            return false;
        }

        string doc_formatName(string name)
        {
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

        void openFileEBK(string file, int page = 0)
        {
            m_infos.Clear();

            using (ZipFile zip = ZipFile.Read(file))
            {
                var info = zip.Entries.Where(x => x.FileName == "info.bin").Take(1).SingleOrDefault();
                if (info != null)
                {
                    try
                    {
                        using (var ms = new MemoryStream())
                        {
                            info.ExtractWithPassword(ms, PASSWORD);
                            ms.Seek(0, SeekOrigin.Begin);

                            using (var sr = new StreamReader(ms))
                            using (var reader = new JsonTextReader(sr))
                            {
                                if (!reader.Read() || reader.TokenType != JsonToken.StartArray)
                                {
                                    //throw new Exception("Expected start of array");
                                }
                                else
                                {
                                    var ser = new JsonSerializer();
                                    while (reader.Read())
                                    {
                                        if (reader.TokenType == JsonToken.EndArray) break;
                                        var item = ser.Deserialize<oPage>(reader);
                                        if (item != null && m_infos.ContainsKey(item.Id) == false)
                                            m_infos.TryAdd(item.Id, item);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }

                foreach (ZipEntry entry in zip)
                {
                    if (entry.FileName.EndsWith(".jpg"))
                    {
                        int i = int.Parse(entry.FileName.Substring(0, entry.FileName.Length - 4));

                        using (MemoryStream ms = new MemoryStream())
                        {
                            entry.ExtractWithPassword(ms, PASSWORD);
                            var buf = ms.ToArray();
                            m_pages.TryAdd(i, buf);
                        }
                    }
                }

            }
        }
    }
}
