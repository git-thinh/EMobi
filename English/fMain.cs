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

        private readonly WebView web_media;
        private readonly WebView web_tree;
        private readonly WebView web_main;

        public fMain()
        {
            InitializeComponent();

            var setting = new BrowserSettings()
            {
            };

            web_media = new WebView("local://media", setting) { Dock = DockStyle.Fill, AllowDrop = false };
            web_tree = new WebView("local://tree", setting) { Dock = DockStyle.Fill, AllowDrop = false };
            web_main = new WebView("local://main", setting) { Dock = DockStyle.Fill, AllowDrop = false };

            this.AllowDrop = false;

            _panelMedia.Controls.Add(web_media);
            _panelTree.Controls.Add(web_tree);
            _panelMain.Controls.Add(web_main);
        }

        private void main_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyUp += form_KeyUp;

            this.FormClosing += main_formClosing;
            this.Shown += (se, ev) =>
            {
                this.Top = 0;
                this.Left = 0;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;

                web_main.ShowDevTools();
            };

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
        private void form_KeyUp(object sender, KeyEventArgs e)
        {
            app_keyPress(e);
        }

        public Int32 getPageTotal() {
            return m_pages.Count;
        }

        public void mainInited()
        {
            new Thread(() =>
            {
                openFileEBK(@"D:\book.data\speakout.ebk");
                //openFileEBK(@"D:\book.data\english grammar in use 3rd edition.ebk");
                web_main.ExecuteScript("pageInit();");
            }).Start();
        }

        public string getPageInfo(int page) {
            string s = "{}";
            if (m_infos.ContainsKey(page)) {
                oPage p = null;
                m_infos.TryGetValue(page, out p);
                if (p != null)
                    s = JsonConvert.SerializeObject(p);
            }
            return s;
        }

        void app_keyPress(KeyEventArgs ev)
        {

            switch (ev.KeyData)
            {
                case Keys.Enter:
                    //if (IS_SELECTION && _menuAutoCropPageSelected.Checked && m_selections.Count > 0)
                    //{
                    //    cropPageUpdate();
                    //    IS_CROP_ENTERING = true;
                    //}
                    //pageOpen(PageNumber);
                    break;
                case Keys.F1:
                    //if (IS_SELECTION)
                    //    IS_SELECTION = false;
                    //else
                    //    IS_SELECTION = true;
                    break;
                case Keys.Right:
                    //pageOpen(PageNumber + 1);
                    break;
                case Keys.Left:
                    //pageOpen(PageNumber - 1);
                    break;
                case Keys.Up:
                    //if (IS_SELECTION && _pictureBox.Tag != null)
                    //{
                    //    var sel = (UiSelectRectangle)_pictureBox.Tag;
                    //    sel.Top = sel.Top - 1;
                    //    sel.Height = sel.Height + 1;

                    //    var rec = (Rectangle)sel.Tag;
                    //    rec.Y = rec.Y - 1;
                    //    rec.Height = rec.Height + 1;
                    //    sel.Tag = rec;
                    //}
                    break;
                case Keys.Down:
                    //if (IS_SELECTION && _pictureBox.Tag != null)
                    //{
                    //    var sel = (UiSelectRectangle)_pictureBox.Tag;
                    //    sel.Top = sel.Top + 1;
                    //    sel.Height = sel.Height - 1;

                    //    var rec = (Rectangle)sel.Tag;
                    //    rec.Y = rec.Y + 1;
                    //    rec.Height = rec.Height - 1;
                    //    sel.Tag = rec;
                    //}
                    break;
                case Keys.PageUp:
                    //pageOpen(0);
                    break;
                case Keys.PageDown:
                    //pageOpen(m_pages.Count - 1);
                    break;
                case Keys.Escape:
                    //if (_menuStrip.Visible)
                    //{
                    //    menuHide();
                    //}
                    //else
                    //{
                    //    this.WindowState = FormWindowState.Minimized;
                    //}
                    break;
            }
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
    }
}
