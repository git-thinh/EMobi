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
using TeamDev.Redis;

namespace EMobiTestUI
{
    public partial class fMain : Form, IMain
    {
        public string DocumentFile { set; get; }
        public string DocumentName { set; get; }

        public string PATH_DATA { get { return Application.StartupPath[0] + @":\emobidata"; } }
        public int REDIS_PORT { get; }
        public string REDIS_HOST { get; }
        public bool REDIS_OPEN { get; set; }

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        RedisDataAccessProvider m_redis;

        public fMain(string host = "127.0.0.1", int port = 6379) : base()
        {
            REDIS_HOST = host;
            REDIS_PORT = port;

            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            var _self = this;

            m_redis = new RedisDataAccessProvider(REDIS_HOST, REDIS_PORT);
            //m_redis.WaitComplete(m_redis.SendCommand(RedisCommand.FLUSHALL));
            //m_redis.WaitComplete(m_redis.SendCommand(RedisCommand.BGSAVE));

            _buttonSave.Enabled = false;
            _panelLeft.Width = 0;
            _panelRight.Width = 45;

            //IS_MODE_EDIT = true;
            ////openImage(@"C:\EMobi\data\speackout elementary student book.bbc\36.jpg");
            //openImage(@"D:\EMobi\data\speackout elementary student book.bbc\15.jpg");

            //_buttonOpen_Click(null, null);
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
            App.exitRedis();
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

            this.DocumentFile = file;
        }

        void openImage(Image img)
        {
            cleanAll();

            int w = 0, h = 0;

            //var img = Image.FromFile(file);
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

            _pictureBox.Image = img;
            //_pictureBox.Image = Image.FromFile(file);
            //_pictureBox.Tag = file;
            //this.DocumentFile = file;
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

        public void pageOpen(int page = 0)
        {
            if (m_redis.Key.Exists(DocumentName))
            {
                var buf = m_redis.Hash[DocumentName].GetData(page.ToString());
                if (buf != null)
                {
                    using (MemoryStream ms = new MemoryStream(buf, 0, buf.Length))
                    {
                        ms.Write(buf, 0, buf.Length);
                        var img = Image.FromStream(ms, true);
                        openImage(img);
                    }
                }
            }
        }
    }

    public interface IMain
    {
        string PATH_DATA { get; }
        string REDIS_HOST { get; }
        int REDIS_PORT { get; }
        bool REDIS_OPEN { get; set; }
        string DocumentFile { set; get; }
        string DocumentName { set; get; }

        void pageOpen(int page = 0);
    }
}
