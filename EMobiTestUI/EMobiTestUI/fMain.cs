﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace EMobiTestUI
{
    public partial class fMain : Form, IMain
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        readonly IApp m_app;
        Dictionary<int, byte[]> m_images = new Dictionary<int, byte[]>() { };

        public fMain(IApp app) : base()
        {
            m_app = app;

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            //this.WindowState = FormWindowState.Normal;
            _panelLeft.Width = 0;
            _panelRight.Width = 45;
            _buttonSave.Enabled = false;

            _menuDocNew.Click += _menuDocNew_Click;
            _menuPageNew.Click += _menuPageNew_Click;
            _menuAttach.Click += _menuAttach_Click;
            _menuMedia.Click += _menuMedia_Click;
            _menuOpen.Click += _menuOpen_Click;

        }

        private void fMain_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyUp += form_KeyUp;
            this.Shown += (se, ev) =>
            {
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                this.Top = 0;
                this.Left = 0;

                try
                {
                    if (File.Exists("config.json"))
                    {
                        var app = JsonConvert.DeserializeObject<AppInfo>(File.ReadAllText("config.json"));
                        var dic = m_app.ebk_Read(app.DocumentFile);
                        doc_Open(dic, app.PageNumber);
                        //new fPageAttach(m_app, this).ShowDialog();
                    }
                }
                catch { }
            };
        }

        private void _menuOpen_Click(object sender, EventArgs e)
        {
        }

        private void _menuMedia_Click(object sender, EventArgs e)
        {
        }

        private void _menuAttach_Click(object sender, EventArgs e)
        {
        }

        private void _menuPageNew_Click(object sender, EventArgs e)
        {
            new fPageAttach(m_app, this).ShowDialog();
        }

        private void _menuDocNew_Click(object sender, EventArgs e)
        {
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            m_app.redis_writeFile();
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
            var f = new fOpen(m_app, this);
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
            m_app.close();
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

            m_app.DocumentFile = file;
        }

        private void _buttonNext_Click(object sender, EventArgs e)
        {
            int page = m_app.PageNumber + 1;
            pageOpen(page);
        }

        private void _buttonPrev_Click(object sender, EventArgs e)
        {
            int page = m_app.PageNumber - 1;
            pageOpen(page);
        }

        void openImage(Image img)
        {
            cleanAll();

            int w = 0, h = 0;

            //var img = Image.FromFile(file);
            w0 = img.Width;
            h0 = img.Height;

            const int _top = 10;
            const int _bottom = 30;

            h = this.Height + _top + _bottom;
            w = h * w0 / h0;

            //if (w0 > h0)
            //{
            //    h = _panelBody.Height;
            //    w = h * w0 / h0;
            //}
            //else
            //{
            //    w = _panelBody.Width;
            //    h = (w * h0) / w0;
            //    if (h > h0)
            //    {
            //        h = _panelBody.Height;
            //        w = h * w0 / h0;
            //    }
            //}

            _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            _pictureBox.Width = w;
            _pictureBox.Height = h;

            _pictureBox.Image = img;
            _pictureBox.Tag = new Point(w0, h0);

            _pictureBox.Top = (-1) * _top;
        }

        private void _pictureBox_DoubleClick(object sender, EventArgs e)
        {
            //if (_pictureBox.Tag != null)
            //{
            //    Point p0 = (Point)_pictureBox.Tag;
            //    int w0 = p0.X, h0 = p0.Y,
            //        w1 = _pictureBox.Width, h1 = _pictureBox.Height;

            //    var buf = m_images[m_app.PageNumber];
            //    Image img = null;
            //    using (MemoryStream ms = new MemoryStream(buf, 0, buf.Length))
            //    {
            //        ms.Write(buf, 0, buf.Length);
            //        img = Image.FromStream(ms, true);

            //        _pictureBox.Width = w0;
            //        _pictureBox.Height = h0;

            //        _pictureBox.Image = img;
            //    }
            //}
        }

        private void form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Right)
            {
                _buttonNext_Click(null, null);
            }
            else if (e.KeyData == Keys.Left)
            {
                _buttonPrev_Click(null, null);
            }
            else if (e.KeyData == Keys.PageUp)
            {
                pageOpen(0);
            }
            else if (e.KeyData == Keys.PageDown)
            {
                pageOpen(m_images.Count - 1);
            }
            else if (e.KeyData == Keys.Escape)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        // Standard high quality thumbnail generation from 
        // http://weblogs.asp.net/gunnarpeipman/archive/2009/04/02/resizing-images-without-loss-of-quality.aspx
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

        public void pageOpen(int page)
        {
            if (m_images.ContainsKey(page))
            {
                m_app.PageNumber = page;

                var buf = m_images[page];
                Image img = null;
                using (MemoryStream ms = new MemoryStream(buf, 0, buf.Length))
                {
                    ms.Write(buf, 0, buf.Length);
                    img = Image.FromStream(ms, true);
                    openImage(img);
                }

                _labelPageNumber.Text = (page + 1).ToString();
                _labelPageNumber.Left = _pictureBox.Width - 42;

                this.Text = string.Format("{0}|{1}", m_app.PageNumber, m_app.DocumentName);
                this.Width = _pictureBox.Width + 45;
            }
        }

        public void doc_Open(Dictionary<int, byte[]> dic, int page = 0)
        {
            if (dic.Count == 0) return;
            m_images = dic;
            pageOpen(page);
        }
    }

    public interface IMain
    {
        void doc_Open(Dictionary<int, byte[]> dic, int page = 0);
    }
}
