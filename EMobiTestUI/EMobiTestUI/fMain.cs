using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace EMobiTestUI
{
    public partial class fMain : Form, IMain
    {
        public string FileDocument { set; get; }

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public string PATH_DATA { get { return Application.StartupPath[0] + @":\emobidata"; } }
        string REDIS_PATH = string.Empty;
        int REDIS_PORT = 3456;
        bool REDIS_OPEN = false;
        Thread REDIS_THREAD = null;

        public fMain()
        {
            REDIS_PORT = getFreeTcpPort();
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            UiSelectRectangle.CheckForIllegalCrossThreadCalls = false;
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = REDIS_PORT.ToString();

            exitRedis();

            if (!Directory.Exists(PATH_DATA)) Directory.CreateDirectory(PATH_DATA);
            REDIS_PATH = Path.Combine(PATH_DATA, "emobi-db.exe");
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
                Process p = new Process();
                //p.StartInfo.WorkingDirectory = @"dir";
                p.StartInfo.Arguments = "--port " + REDIS_PORT.ToString() + " --bind 127.0.0.1";
                p.StartInfo.FileName = REDIS_PATH;

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.ErrorDataReceived += (se, ev) =>
                {
                    //Debug.WriteLine(ev.Data);
                };
                p.OutputDataReceived += (se, ev) =>
                {
                    //Debug.WriteLine(ev.Data);
                    if (!string.IsNullOrEmpty(ev.Data) && ev.Data.Contains("Ready"))
                    {
                        REDIS_OPEN = true;
                        //IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
                        IntPtr h = p.MainWindowHandle;
                        ShowWindow(h, 0);
                    }
                };
                p.EnableRaisingEvents = true;
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                p.WaitForExit();
                ;

                ////ProcessStartInfo r = new ProcessStartInfo(REDIS_PATH);
                ////r.UseShellExecute = false;
                //////r.CreateNoWindow = true;
                ////r.Arguments = "--port " + REDIS_PORT.ToString() + " --bind 127.0.0.1"; 
                ////var p = Process.Start(r);
                //////IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
                //////IntPtr h = p.MainWindowHandle;
                //////ShowWindow(h, 0);
                ////p.WaitForExit();
                ////;
            });
            REDIS_THREAD.Start();

            _buttonSave.Enabled = false;
            _panelLeft.Width = 0;
            _panelRight.Width = 45;

            //IS_MODE_EDIT = true;
            ////openImage(@"C:\EMobi\data\speackout elementary student book.bbc\36.jpg");
            //openImage(@"D:\EMobi\data\speackout elementary student book.bbc\15.jpg");

            _buttonOpen_Click(null, null);
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to exit program?",
                                     "Confirm Exit",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void _buttonMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void _buttonSelection_Click(object sender, EventArgs e)
        {
            if (IS_MODE_EDIT)
                IS_MODE_EDIT = false;
            else
                IS_MODE_EDIT = true;
        }

        private void _pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (IS_MODE_EDIT)
            {
                x = e.X;
                y = e.Y;

                var l = new UiSelectRectangle(mList.Count + 1)
                {
                    Width = 1,
                    Height = 1,
                    Location = new Point(x, y)
                };
                l.MouseDoubleClick += (sv, ev) =>
                {
                    if (IS_MODE_EDIT)
                    {
                        int i = mList.FindIndex(x => x == l);
                        if (i != -1) mList.RemoveAt(i);
                        _pictureBox.Controls.Remove(l);
                        if (mList.Count == 0)
                        {
                            _buttonSave.Enabled = false;
                        }
                    }
                };
                l.MouseClick += (sv, ev) =>
                {
                    if (IS_MODE_EDIT && IS_MODE_REINDEX)
                    {
                        COUNTER_REINDEX++;
                        l.Index = COUNTER_REINDEX;
                        if (mList.Count == COUNTER_REINDEX) IS_MODE_REINDEX = false;
                    }
                };
                mList.Add(l);
                index = mList.Count - 1;
                _pictureBox.Controls.Add(l);
                l.BringToFront();
            }
        }

        private void _pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (IS_MODE_EDIT && mList.Count > 0 && index != -1)
            {
                int w = Math.Abs(x - x1), h = Math.Abs(y - y1);

                Rectangle rec = new Rectangle(x, y, w, h);
                mList[index].Tag = rec;
                mList[index].Width = w;
                mList[index].Height = h;
                //mList[index].SetOpacity(50);

                index = -1;
                x = 0;
                y = 0;

                _buttonSave.Enabled = true;
            }
        }

        private void _pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (IS_MODE_EDIT)
            {
                x1 = e.X;
                y1 = e.Y;
                if (index != -1)
                {
                    int w = Math.Abs(x - x1), h = Math.Abs(y - y1);
                    mList[index].Width = w;
                    mList[index].Height = h;
                }
            }
        }

        int COUNTER_REINDEX = 0;
        int w0 = 0, h0 = 0;
        int x = 0, y = 0, x1 = 0, y1 = 0;
        int index = -1;
        List<UiSelectRectangle> mList = new List<UiSelectRectangle>() { };

        private void _pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void _buttonReIndex_Click(object sender, EventArgs e)
        {
            if (_pictureBox.Controls.Count == 0) return;

            if (IS_MODE_REINDEX)
            {
                IS_MODE_REINDEX = false;
            }
            else
            {
                IS_MODE_REINDEX = true;
            }
        }

        bool IS_MODE_EDIT
        {
            get
            {
                return _buttonSelection.BackColor == Color.Orange;
            }

            set
            {
                if (value)
                {
                    _buttonSelection.BackColor = Color.Orange;
                }
                else
                {
                    _buttonSelection.BackColor = SystemColors.Control;
                }
            }
        }

        private void _buttonClean_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to clean all item selected ?",
                                     "Confirm Clean All",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                cleanAll();
            }
        }

        private void _buttonOpen_Click(object sender, EventArgs e)
        {
            var f = new fOpen(this);
            f.ShowDialog();
        }

        private void _buttonPageOpen_Click(object sender, EventArgs e)
        {
            new fPage().ShowDialog();
        }

        private void _buttonView_Click(object sender, EventArgs e)
        {
            new fView().ShowDialog();
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            exitRedis();
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            if (mList.Count > 0)
            {
                for (var i = 0; i < mList.Count; i++)
                {
                    var tag = mList[i].Tag;
                    if (tag != null)
                    {
                        var rec = (Rectangle)tag;

                        int w1 = _pictureBox.Width,
                            h1 = _pictureBox.Height,

                            wc = rec.Width * w0 / w1,
                            hc = rec.Height * h0 / h1,

                            xc = rec.X * w0 / w1,
                            yc = rec.Y * h0 / h1;

                        var cropRec = new Rectangle(xc, yc, wc, hc);
                        var target = new Bitmap(wc, hc);

                        using (Graphics g = Graphics.FromImage(target))
                        {
                            g.DrawImage(_pictureBox.Image, new Rectangle(0, 0, wc, hc), cropRec, GraphicsUnit.Pixel);
                        }

                        target.Save(@"C:\test\s." + (i + 1) + ".jpg", ImageFormat.Jpeg);
                    }
                }
            }

            _buttonSave.Enabled = false;
        }

        bool IS_MODE_REINDEX
        {
            get
            {
                return _buttonReIndex.BackColor == Color.Orange;
            }

            set
            {
                if (value)
                {
                    foreach (var l in _pictureBox.Controls) (l as UiSelectRectangle).Index = 0;
                    _buttonReIndex.BackColor = Color.Orange;
                }
                else
                {
                    COUNTER_REINDEX = 0;
                    _buttonReIndex.BackColor = SystemColors.Control;
                }
            }
        }

        void cleanAll()
        {
            mList.Clear();
            _pictureBox.Controls.Clear();
            _buttonSave.Enabled = false;
            index = -1;
            COUNTER_REINDEX = 0;
            x = 0;
            y = 0;
        }

        void openImage(string file)
        {
            cleanAll();

            int w = 0, h = 0;

            var img = Image.FromFile(file);
            w0 = img.Width;
            h0 = img.Height;

            if (w0 > h0)
            {
                h = _panelBody.Height;
                w = h * w0 / h0;
            }
            else
            {
                w = _panelBody.Width;
                h = (w * h0) / w0;
                if (h > h0)
                {
                    h = _panelBody.Height;
                    w = h * w0 / h0;
                }
            }

            _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            _pictureBox.Width = w;
            _pictureBox.Height = h;
            _pictureBox.Image = Image.FromFile(file);
            _pictureBox.Tag = file;

            this.FileDocument = file;
        }

        void exitRedis()
        {
            Process.Start("TASKKILL", @"/F /IM ""emobi-db.exe*""");
            Thread.Sleep(100);
            if (REDIS_THREAD != null) REDIS_THREAD.Abort();
        }

        static int getFreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            Thread.Sleep(100);
            return port;
        }

        //Standard high quality thumbnail generation from http://weblogs.asp.net/gunnarpeipman/archive/2009/04/02/resizing-images-without-loss-of-quality.aspx
        static System.Drawing.Image ShrinkImage(System.Drawing.Image sourceImage, float scaleFactor)
        {
            int newWidth = Convert.ToInt32(sourceImage.Width * scaleFactor);
            int newHeight = Convert.ToInt32(sourceImage.Height * scaleFactor);

            var thumbnailBitmap = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(thumbnailBitmap))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                System.Drawing.Rectangle imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                g.DrawImage(sourceImage, imageRectangle);
            }
            return thumbnailBitmap;
        }
    }

    public interface IMain
    {
        string FileDocument { set; get; }
    }
}
