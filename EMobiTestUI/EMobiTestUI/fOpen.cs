using PdfiumViewer;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace EMobiTestUI
{
    public partial class fOpen : Form
    {
        const int IMG_WIDTH_BIG = 2400;
        const int IMG_WIDTH_NORMAL = 1200;

        readonly IMain m_main;
        string m_file;
        readonly Redis m_redis;
        public fOpen(IMain main) : base()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            m_main = main;
            m_file = main.DocumentFile;
            m_redis = new Redis("127.0.0.1", main.REDIS_PORT);
            m_redis.Db = 1;
            //Test.test(main);
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
                openFileDialog.InitialDirectory = Path.GetDirectoryName(m_file);
                //openFileDialog.Filter = "Image files (*.png)|*.png|(*.jpg)|*.jpg|All files (*.*)|*.*";
                openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string file = openFileDialog.FileName;
                    //openImage(file);
                    _labelFile.Text = file;
                    _labelMessage.Text = "";

                    string docName = Path.GetFileName(file);
                    docName = docName.Substring(0, docName.Length - 4).Trim().ToLower();

                    if (m_redis.ContainsKey(docName))
                    {
                        m_main.DocumentFile = file;
                        m_main.DocumentName = docName;
                        m_main.pageOpen();
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

                            //string outputPath = @"C:\test\images\" + (i + 1).ToString() + "." + time + ".jpg";
                            //string outputPath = @"C:\test\images\" + (i + 1).ToString() + ".jpg";
                            //using (var stream = new FileStream(outputPath, FileMode.Create))
                            //{
                            using (var image = document.Render(i, w, h, dpi, dpi, PdfRenderFlags.Annotations))
                            {
                                _labelMessage.Text = "Processing page " + (i + 1).ToString() + "...";
                                //image.Save(stream, ImageFormat.Jpeg);
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    image.Save(ms, ImageFormat.Jpeg);
                                    var buf = ms.ToArray();
                                    m_redis.RightPush(docName, buf);
                                }
                            }
                            //}
                        }//end for
                        
                        _buttonBrowser.Enabled = true;
                        _buttonOpen.Enabled = true;
                        _buttonClose.Enabled = true;
                        _labelMessage.Text = "Process complete: " + max + " pages";

                        m_redis.BackgroundSave();

                        m_main.DocumentFile = file;
                        m_main.DocumentName = docName;
                        m_main.pageOpen();
                        this.Close();
                    }
                }
            }
        }
    }
}
