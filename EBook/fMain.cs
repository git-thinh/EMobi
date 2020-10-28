﻿using Ionic.Zip;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EBook
{
    class fMain : Form
    {
        private int PageNumber = 0;
        private string DocumentFile = string.Empty;
        private string DocumentName = string.Empty;
        const string FOLDER_DATA = "Mr.Thinh's Gifts";
        private Dictionary<int, byte[]> m_images = new Dictionary<int, byte[]>() { };

        private MenuStrip _menuStrip;
        private ToolStripMenuItem _menuOpen;
        private ToolStripSeparator _menuHr;
        private ToolStripMenuItem _menuExit;
        private Label _labelPage;
        private PictureBox _pictureBox;
        private ToolStripMenuItem _menuCropMode;
        private ToolStripMenuItem _menuExtractTextSelection;
        private ToolStripMenuItem _menuMain;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._menuMain = new System.Windows.Forms.ToolStripMenuItem();
            this._menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this._menuCropMode = new System.Windows.Forms.ToolStripMenuItem();
            this._menuHr = new System.Windows.Forms.ToolStripSeparator();
            this._menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this._labelPage = new System.Windows.Forms.Label();
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this._menuExtractTextSelection = new System.Windows.Forms.ToolStripMenuItem();
            this._menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _menuStrip
            // 
            this._menuStrip.BackColor = System.Drawing.Color.White;
            this._menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuMain});
            this._menuStrip.Location = new System.Drawing.Point(-6, -8);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(156, 34);
            this._menuStrip.TabIndex = 0;
            this._menuStrip.Text = "menuStrip1";
            // 
            // _menuMain
            // 
            this._menuMain.AutoSize = false;
            this._menuMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuOpen,
            this._menuExtractTextSelection,
            this._menuCropMode,
            this._menuHr,
            this._menuExit});
            this._menuMain.Font = new System.Drawing.Font("Segoe UI", 15F);
            this._menuMain.ForeColor = System.Drawing.Color.Black;
            this._menuMain.Name = "_menuMain";
            this._menuMain.Padding = new System.Windows.Forms.Padding(0);
            this._menuMain.Size = new System.Drawing.Size(28, 30);
            this._menuMain.Text = "≡";
            this._menuMain.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this._menuMain.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // _menuOpen
            // 
            this._menuOpen.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._menuOpen.Name = "_menuOpen";
            this._menuOpen.Size = new System.Drawing.Size(267, 32);
            this._menuOpen.Text = "&Open";
            this._menuOpen.Click += new System.EventHandler(this._menuOpen_Click);
            // 
            // _menuCropMode
            // 
            this._menuCropMode.Name = "_menuCropMode";
            this._menuCropMode.Size = new System.Drawing.Size(267, 32);
            this._menuCropMode.Text = "Crop Mode";
            this._menuCropMode.Click += new System.EventHandler(this._menuCropMode_Click);
            // 
            // _menuHr
            // 
            this._menuHr.Name = "_menuHr";
            this._menuHr.Size = new System.Drawing.Size(264, 6);
            // 
            // _menuExit
            // 
            this._menuExit.Name = "_menuExit";
            this._menuExit.Size = new System.Drawing.Size(267, 32);
            this._menuExit.Text = "&Exit";
            this._menuExit.Click += new System.EventHandler(this._menuExit_Click);
            // 
            // _labelPage
            // 
            this._labelPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._labelPage.BackColor = System.Drawing.SystemColors.Control;
            this._labelPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelPage.ForeColor = System.Drawing.Color.Black;
            this._labelPage.Location = new System.Drawing.Point(718, 794);
            this._labelPage.Name = "_labelPage";
            this._labelPage.Size = new System.Drawing.Size(35, 23);
            this._labelPage.TabIndex = 3;
            this._labelPage.Text = "0";
            this._labelPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _pictureBox
            // 
            this._pictureBox.Location = new System.Drawing.Point(0, 0);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(623, 735);
            this._pictureBox.TabIndex = 4;
            this._pictureBox.TabStop = false;
            this._pictureBox.Click += new System.EventHandler(this._pictureBox_Click);
            // 
            // _menuExtractTextSelection
            // 
            this._menuExtractTextSelection.Name = "_menuExtractTextSelection";
            this._menuExtractTextSelection.Size = new System.Drawing.Size(267, 32);
            this._menuExtractTextSelection.Text = "Extract Text Selection";
            this._menuExtractTextSelection.Click += new System.EventHandler(this._menuExtractTextSelection_Click);
            // 
            // fMain
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(754, 819);
            this.Controls.Add(this._labelPage);
            this.Controls.Add(this._menuStrip);
            this.Controls.Add(this._pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._menuStrip;
            this.Name = "fMain";
            this.Load += new System.EventHandler(this.fMain_Load);
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, System.EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyUp += form_KeyUp;
            this.Shown += (se, ev) =>
            {
                this.Top = 0;
                this.Left = 0;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                IsCropMode = true;

                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System\" + FOLDER_DATA, false);
                if (key != null){
                    var file = key.GetValue("File") as string;
                    if (!string.IsNullOrEmpty(file)) {
                        if (File.Exists(file))
                        {
                            var page = key.GetValue("Page") as string;
                            int p = 0;
                            int.TryParse(page, out p);
                            openFile(file, p);
                        }
                    }
                    key.Close();
                }
            };
        }

        private void form_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F1:
                    if (IsCropMode)
                        IsCropMode = false;
                    else
                        IsCropMode = true;
                    break;
                case Keys.F5:
                    if (IsExtractTextSelection)
                        IsExtractTextSelection = false;
                    else
                        IsExtractTextSelection = true;
                    break;
                case Keys.Right:
                    pageOpen(PageNumber + 1);
                    break;
                case Keys.Left:
                    pageOpen(PageNumber - 1);
                    break;
                case Keys.PageUp:
                    pageOpen(0);
                    break;
                case Keys.PageDown:
                    pageOpen(m_images.Count - 1);
                    break;
                case Keys.Escape:
                    this.WindowState = FormWindowState.Minimized;
                    break;
            }
        }

        private void _menuOpen_Click(object sender, System.EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "EBook Files (*.ebk)|*.ebk";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    openFile(openFileDialog.FileName);
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
                    key = key.CreateSubKey(FOLDER_DATA);
                    key.SetValue("File", DocumentFile, RegistryValueKind.String);
                    key.Close();
                }
            }
        }

        void openFile(string file,int page = 0) {
            _pictureBox.Visible = false;

            DocumentFile = file;
            DocumentName = doc_formatName(Path.GetFileName(DocumentFile));
            m_images.Clear();

            using (ZipFile zip = ZipFile.Read(DocumentFile))
            {
                foreach (ZipEntry entry in zip)
                {
                    int i = int.Parse(entry.FileName.Substring(0, entry.FileName.Length - 4));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        entry.ExtractWithPassword(ms, FOLDER_DATA);
                        m_images.Add(i, ms.ToArray());
                    }
                }
            }

            pageOpen(page);
            _pictureBox.Visible = true;
        }

        void pageOpen(int page)
        {
            if (m_images.Count > 0 && m_images.ContainsKey(page))
            {
                PageNumber = page;

                var buf = m_images[page];
                Image img = null;
                using (MemoryStream ms = new MemoryStream(buf, 0, buf.Length))
                {
                    ms.Write(buf, 0, buf.Length);
                    img = Image.FromStream(ms, true);
                    openImage(img);
                }

                _labelPage.Text = (page + 1).ToString();
                //_labelPage.Left = _pictureBox.Width - _labelPage.Width;

                this.Text = string.Format("{0}|{1}", PageNumber, DocumentName);
                this.Width = _pictureBox.Width;// + 45;
            }
        }

        void openImage(Image img)
        {
            //cleanAll();

            int w = 0, h = 0;

            //var img = Image.FromFile(file);
            int w0 = img.Width, h0 = img.Height;

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

        private void _menuExit_Click(object sender, System.EventArgs e)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
            key = key.CreateSubKey(FOLDER_DATA);
            key.SetValue("Page", PageNumber.ToString(), RegistryValueKind.String);
            key.Close();
            this.Close();
        }

        private void _pictureBox_Click(object sender, System.EventArgs e)
        {
            if (DocumentName.Length == 0) return;

            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Right)
            {
                int page = PageNumber + 1;
                pageOpen(page);
            }
            else if (me.Button == MouseButtons.Left)
            {
                int page = PageNumber - 1;
                pageOpen(page);
            }
        }

        public bool IsCropMode
        {
            get { return _menuCropMode.Checked; }
            set
            {
                if (value)
                {
                    IsExtractTextSelection = false;
                    _menuCropMode.Checked = true;
                    _menuMain.BackColor = Color.Orange;
                }
                else
                {
                    _menuCropMode.Checked = false;
                    _menuMain.BackColor = Color.White;
                }
            }
        }

        public bool IsExtractTextSelection
        {
            get { return _menuExtractTextSelection.Checked; }
            set
            {
                if (value)
                {
                    IsCropMode = false;
                    _menuExtractTextSelection.Checked = true;
                    _menuMain.BackColor = Color.DeepSkyBlue;
                }
                else
                {
                    _menuExtractTextSelection.Checked = false;
                    _menuMain.BackColor = Color.White;
                }
            }
        }

        private void _menuCropMode_Click(object sender, System.EventArgs e)
        {
            if (IsCropMode)
            {
                IsCropMode = false;
            }
            else
            {
                IsCropMode = true;
            }
        }

        private void _menuExtractTextSelection_Click(object sender, System.EventArgs e)
        {
            if (IsExtractTextSelection)
            {
                IsExtractTextSelection = false;
            }
            else
            {
                IsExtractTextSelection = true;
            }
        }
    }
}
