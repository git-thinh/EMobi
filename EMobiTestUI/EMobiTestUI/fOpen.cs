using ICSharpCode.SharpZipLib.Zip;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using TeamDev.Redis;

namespace EMobiTestUI
{
    public partial class fOpen : Form
    {
        const int IMG_WIDTH_BIG = 2400;
        const int IMG_WIDTH_NORMAL = 1200;

        readonly IApp m_app;
        readonly IMain m_main;
        public fOpen(IApp app, IMain main) : base()
        {
            m_app = app;
            m_main = main;

            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void fOpen_Load(object sender, EventArgs e)
        {
            _labelMessage.Text = "";
            this.Shown += (se, ev) =>
            {
                this.Top = (Screen.FromControl(this).WorkingArea.Height - this.Height) / 2 - 100;
                this.Left = (Screen.FromControl(this).WorkingArea.Width - this.Width) / 2;
            };
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _buttonBrowser_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (!string.IsNullOrEmpty(m_app.PathBrowserLastest))
                    openFileDialog.InitialDirectory = m_app.PathBrowserLastest;
                else
                    openFileDialog.InitialDirectory = m_app.PATH_DATA;

                //openFileDialog.Filter = "Image files (*.png)|*.png|(*.jpg)|*.jpg|All files (*.*)|*.*";
                openFileDialog.Filter = "EBook Files (*.ebk)|*.ebk|PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string file = openFileDialog.FileName;
                    m_app.PathBrowserLastest = Path.GetDirectoryName(file);

                    //openImage(file);
                    _labelFile.Text = file;
                    _labelMessage.Text = "";

                    string docName = Path.GetFileName(file);
                    docName = docName.Substring(0, docName.Length - 4).Trim().ToLower();

                    var dic = new Dictionary<int, byte[]>();

                    string zipFile = Path.Combine(m_app.PATH_DATA, docName + ".ebk");
                    if (File.Exists(zipFile))
                    {
                        using (ZipInputStream zstream = new ZipInputStream(File.OpenRead(zipFile)))
                        {
                            ZipEntry entry;
                            while ((entry = zstream.GetNextEntry()) != null)
                            {
                                int i = int.Parse(entry.Name.Substring(0, entry.Name.Length - 4));
                                //int size = 0;
                                //var data = new byte[entry.Size];
                                //size = s.Read(data, 0, data.Length);
                                //dic.Add(i, data);

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
                                        else {
                                            dic.Add(i, ms.ToArray());
                                        }
                                    }
                                }
                            }
                        }

                        m_app.DocumentFile = file;
                        m_app.DocumentName = docName;
                        m_main.pageOpen(dic);
                        this.Close();
                        return;
                    }

                    _buttonBrowser.Enabled = false;
                    _buttonOpen.Enabled = false;
                    _buttonClose.Enabled = false;

                    using (var document = PdfDocument.Load(file))
                    {
                        int dpi = 150,
                            max = document.PageCount,
                            wi = Screen.PrimaryScreen.WorkingArea.Width,
                            hi = Screen.PrimaryScreen.WorkingArea.Height;
                        string time = DateTime.Now.ToString("yyMMdd.HHmmss");
                        int wp = 0, hp = 0, w = 0, h = 0;


                        for (int i = 0; i < max; i++)
                        {
                            //if (i != 5 && i != 7) continue;

                            wp = (int)document.PageSizes[i].Width;
                            hp = (int)document.PageSizes[i].Height;

                            if (wp > hp)
                            {
                                //w = wi;
                                w = IMG_WIDTH_BIG;
                                h = w * hp / wp;
                            }
                            else
                            {
                                //h = hi;
                                //w = h * wp / hp;
                                w = IMG_WIDTH_NORMAL;
                                h = w * hp / wp;
                            }

                            using (var image = document.Render(i, w, h, dpi, dpi, PdfRenderFlags.Annotations))
                            {
                                _labelMessage.Text = "Processing page " + (i + 1).ToString() + "...";
                                //image.Save(stream, ImageFormat.Jpeg);
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    image.Save(ms, ImageFormat.Jpeg);
                                    var buf = ms.ToArray();
                                    dic.Add(i, buf);
                                }
                            }
                        }//end for

                        _buttonBrowser.Enabled = true;
                        _buttonOpen.Enabled = true;
                        _buttonClose.Enabled = true;
                        _labelMessage.Text = "Process complete: " + max + " pages";

                        //m_redis.Hash[docName].Clear();
                        //m_redis.Hash[docName].Set(dic);
                        //m_redis.WaitComplete(m_redis.SendCommand(RedisCommand.BGSAVE));

                        using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipFile)))
                        {
                            zipStream.SetLevel(9); // 0 - store only to 9 - means best compression
                            foreach (var kv in dic)
                            {
                                var entry = new ZipEntry(kv.Key.ToString() + ".jpg");
                                entry.DateTime = DateTime.Now;
                                zipStream.PutNextEntry(entry);
                                zipStream.Write(kv.Value, 0, kv.Value.Length);
                            }
                        }

                        m_app.DocumentFile = file;
                        m_app.DocumentName = docName;
                        m_main.pageOpen(dic);
                        this.Close();
                    }
                }
            }
        }
    }
}
