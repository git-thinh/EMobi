using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace EMobiTestUI
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            UiSelectRectangle.CheckForIllegalCrossThreadCalls = false;
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            _buttonSave.Enabled = false;
            _panelLeft.Width = 0;
            _panelRight.Width = 45;
            IS_MODE_EDIT = true;
            //openImage(@"C:\EMobi\data\speackout elementary student book.bbc\36.jpg");
            openImage(@"D:\EMobi\data\speackout elementary student book.bbc\15.jpg");
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
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(_pictureBox.Tag.ToString());
                openFileDialog.Filter = "Image files (*.png)|*.png|(*.jpg)|*.jpg|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string file = openFileDialog.FileName;
                    openImage(file);
                }
            }
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

        void cleanAll() {
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
        }
    }
}
