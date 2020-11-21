using CefSharp;
using CefSharp.WinForms;
using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace English
{
    public partial class fMain : Form, ISchemeHandler, IBoundObject
    {
        string PASSWORD = "Mr.Thinh's Gifts";
        string FOLDER_DATA = "book.data";
        string PATH_DATA = Application.StartupPath[0] + @":\book.data";
        ConcurrentDictionary<int, oPage> m_infos = new ConcurrentDictionary<int, oPage>();
        ConcurrentDictionary<int, byte[]> m_pages = new ConcurrentDictionary<int, byte[]>();

        int m_page_current = 0;
        string m_file_name = string.Empty;
        string m_file = string.Empty;

        readonly WebView m_web;

        public fMain()
        {
            InitializeComponent();

            fMain.CheckForIllegalCrossThreadCalls = true;
            Control.CheckForIllegalCrossThreadCalls = true;

            var setting = new BrowserSettings()
            {
                UniversalAccessFromFileUrlsAllowed = true,
                DragDropDisabled = true,
                FileAccessFromFileUrlsAllowed = true,
                WebSecurityDisabled = true
            };
            m_web = new WebView("local://main", setting) { Dock = DockStyle.Fill, AllowDrop = false };
            m_web.ContextMenuStrip = null;

            this.AllowDrop = false;
            this.Controls.Add(m_web);
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

        }

        private void main_formClosing(object sender, FormClosingEventArgs e)
        {
            ((IWebBrowser)m_web).Dispose();
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

        public Int32 getPageTotal()
        {
            return m_pages.Count;
        }

        public void mainInited()
        {
            //////m_web.ShowDevTools();
            //new Thread(() =>
            //{
            //    //openFileEBK(@"D:\book.data\speakout.ebk");
            //    openFileEBK(@"D:\book.data\murphy r english grammar in use 2012 4 ed .ebk");
            //    m_web.ExecuteScript("pageInit();");
            //}).Start();
        }

        public void setAppWidth(int width, int height)
        {
            if (width > Screen.PrimaryScreen.WorkingArea.Width)
            {
                width = Screen.PrimaryScreen.WorkingArea.Width;
                this.Left = 0;
            }
            else width += 25;

            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    this.Width = width;
                }));
                return;
            }

            this.Width = width;
        }

        public string getPageInfo(int page)
        {
            string s = "{}";
            if (m_infos.ContainsKey(page))
            {
                oPage p = null;
                m_infos.TryGetValue(page, out p);
                if (p != null)
                    s = JsonConvert.SerializeObject(p);
            }
            return s;
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
            m_pages.Clear();

            using (ZipFile zip = ZipFile.Read(file))
            {
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

                            Bitmap img = new Bitmap(ms);
                            m_infos.TryAdd(i, new oPage() { Id = i, Height = img.Height, Width = img.Width });
                        }
                    }
                }

            }
        }

        public Int32 getScreenWidth()
        {
            return Screen.PrimaryScreen.WorkingArea.Width;
        }

        bool openDailogBrowserDocument()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = PATH_DATA;
                openFileDialog.Filter = "EBook Files (*.ebk)|*.ebk|PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
                //openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string file = openFileDialog.FileName;
                    if (file.EndsWith(".ebk"))
                    {
                        m_file = file;
                        m_file_name = Path.GetFileName(file);
                        return true;
                    }
                }
            }
            return false;
        }

        public void js_open()
        {
            bool ok = false;
            jsInvoke(() => { ok = openDailogBrowserDocument(); });
            if (ok)
            {
                m_web.ExecuteScript("loading(true);");
                new Thread(() =>
                {
                    openFileEBK(m_file);
                    m_web.ExecuteScript("pageInit();");
                }).Start();
            }
        }

        public void js_exit()
        {
            jsInvoke(() =>
            {
                this.Close();
            });
        }

        public void js_open_devtool()
        {
            jsInvoke(() =>
            {
                m_web.ShowDevTools();
            });
        }

        public void js_page_set_current(int page)
        {
            m_page_current = page;
            jsInvoke(() =>
            {
                this.Text = string.Format("[{0}] - {1}", m_page_current + 1, m_file_name);
            });
        }

        void jsInvoke(Action action)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(action));
                return;
            }
            action();
        }
    }
}
