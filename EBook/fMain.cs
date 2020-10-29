using Ionic.Zip;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tesseract;

namespace EBook
{
    class fMain : Form
    {
        private int PageNumber = 0;
        private string DocumentFile = string.Empty;
        private string DocumentName = string.Empty;
        const string PASSWORD = "Mr.Thinh's Gifts";
        const string FOLDER_DATA = "book.data";
        private string PATH_DATA = Application.StartupPath[0] + @":\" + FOLDER_DATA;
        private Dictionary<int, byte[]> m_pages = new Dictionary<int, byte[]>() { };
        private Dictionary<int, byte[]> m_page_crops = new Dictionary<int, byte[]>() { };

        #region [ MAIN ]

        private MenuStrip _menuStrip;
        private ToolStripMenuItem _menuOpen;
        private ToolStripSeparator _menuHr2;
        private ToolStripMenuItem _menuExit;
        private Label _labelPage;
        private PictureBox _pictureBox;
        private ToolStripMenuItem _menuSave;
        private ToolStripSeparator _menuHr1;
        private ToolStripSeparator _menuHr0;
        private CheckBox _checkSelection;
        private ToolStripMenuItem _menuSelection;
        private ToolStripMenuItem _menuCropSelection;
        private ToolStripMenuItem _menuExtracTextSelection;
        private ToolStripMenuItem _menuSetIndex;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem _menuAutoCropPageSelected;
        private ToolStripMenuItem _menuKeepSelectChangePage;
        private ToolStripMenuItem _menuDisplayPageCropping;
        private ToolStripMenuItem _menuBackupDocument;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem autoGeneralIndexTitleHeaderToolStripMenuItem;
        private ToolStripMenuItem _menuMain;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._menuMain = new System.Windows.Forms.ToolStripMenuItem();
            this._menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this._menuHr0 = new System.Windows.Forms.ToolStripSeparator();
            this._menuSelection = new System.Windows.Forms.ToolStripMenuItem();
            this._menuCropSelection = new System.Windows.Forms.ToolStripMenuItem();
            this._menuExtracTextSelection = new System.Windows.Forms.ToolStripMenuItem();
            this._menuSetIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._menuAutoCropPageSelected = new System.Windows.Forms.ToolStripMenuItem();
            this._menuKeepSelectChangePage = new System.Windows.Forms.ToolStripMenuItem();
            this._menuDisplayPageCropping = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.autoGeneralIndexTitleHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._menuHr1 = new System.Windows.Forms.ToolStripSeparator();
            this._menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this._menuHr2 = new System.Windows.Forms.ToolStripSeparator();
            this._menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this._menuBackupDocument = new System.Windows.Forms.ToolStripMenuItem();
            this._labelPage = new System.Windows.Forms.Label();
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this._checkSelection = new System.Windows.Forms.CheckBox();
            this._menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _menuStrip
            // 
            this._menuStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._menuStrip.BackColor = System.Drawing.Color.White;
            this._menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuMain});
            this._menuStrip.Location = new System.Drawing.Point(357, 276);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Padding = new System.Windows.Forms.Padding(0);
            this._menuStrip.Size = new System.Drawing.Size(150, 30);
            this._menuStrip.TabIndex = 0;
            this._menuStrip.Text = "menuStrip1";
            // 
            // _menuMain
            // 
            this._menuMain.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._menuMain.AutoSize = false;
            this._menuMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuOpen,
            this._menuHr0,
            this._menuSelection,
            this._menuHr1,
            this._menuBackupDocument,
            this._menuSave,
            this._menuHr2,
            this._menuExit});
            this._menuMain.Font = new System.Drawing.Font("Segoe UI", 15F);
            this._menuMain.ForeColor = System.Drawing.Color.Black;
            this._menuMain.Name = "_menuMain";
            this._menuMain.Padding = new System.Windows.Forms.Padding(0);
            this._menuMain.Size = new System.Drawing.Size(28, 30);
            this._menuMain.Text = "≡";
            this._menuMain.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this._menuMain.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            // 
            // _menuOpen
            // 
            this._menuOpen.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._menuOpen.Name = "_menuOpen";
            this._menuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this._menuOpen.Size = new System.Drawing.Size(243, 32);
            this._menuOpen.Text = "&Open";
            this._menuOpen.Click += new System.EventHandler(this._menuOpen_Click);
            // 
            // _menuHr0
            // 
            this._menuHr0.Name = "_menuHr0";
            this._menuHr0.Size = new System.Drawing.Size(240, 6);
            // 
            // _menuSelection
            // 
            this._menuSelection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuCropSelection,
            this._menuExtracTextSelection,
            this._menuSetIndex,
            this.toolStripSeparator1,
            this._menuAutoCropPageSelected,
            this._menuKeepSelectChangePage,
            this._menuDisplayPageCropping,
            this.toolStripSeparator2,
            this.autoGeneralIndexTitleHeaderToolStripMenuItem});
            this._menuSelection.Name = "_menuSelection";
            this._menuSelection.Size = new System.Drawing.Size(243, 32);
            this._menuSelection.Text = "Selection";
            // 
            // _menuCropSelection
            // 
            this._menuCropSelection.Name = "_menuCropSelection";
            this._menuCropSelection.Size = new System.Drawing.Size(402, 32);
            this._menuCropSelection.Text = "&Crop";
            this._menuCropSelection.Click += new System.EventHandler(this._menuCropSelection_Click);
            // 
            // _menuExtracTextSelection
            // 
            this._menuExtracTextSelection.Name = "_menuExtracTextSelection";
            this._menuExtracTextSelection.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this._menuExtracTextSelection.Size = new System.Drawing.Size(402, 32);
            this._menuExtracTextSelection.Text = "&Extract Text";
            this._menuExtracTextSelection.Click += new System.EventHandler(this._menuExtracTextSelection_Click);
            // 
            // _menuSetIndex
            // 
            this._menuSetIndex.Name = "_menuSetIndex";
            this._menuSetIndex.Size = new System.Drawing.Size(402, 32);
            this._menuSetIndex.Text = "Set &Index";
            this._menuSetIndex.Click += new System.EventHandler(this._menuSetIndex_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(399, 6);
            // 
            // _menuAutoCropPageSelected
            // 
            this._menuAutoCropPageSelected.Checked = true;
            this._menuAutoCropPageSelected.CheckState = System.Windows.Forms.CheckState.Checked;
            this._menuAutoCropPageSelected.Name = "_menuAutoCropPageSelected";
            this._menuAutoCropPageSelected.Size = new System.Drawing.Size(402, 32);
            this._menuAutoCropPageSelected.Text = "Auto Crop Page Selected";
            // 
            // _menuKeepSelectChangePage
            // 
            this._menuKeepSelectChangePage.Checked = true;
            this._menuKeepSelectChangePage.CheckState = System.Windows.Forms.CheckState.Checked;
            this._menuKeepSelectChangePage.Name = "_menuKeepSelectChangePage";
            this._menuKeepSelectChangePage.Size = new System.Drawing.Size(402, 32);
            this._menuKeepSelectChangePage.Text = "Keep Selection Change Page";
            this._menuKeepSelectChangePage.Click += new System.EventHandler(this._menuKeepSelectChangePage_Click);
            // 
            // _menuDisplayPageCropping
            // 
            this._menuDisplayPageCropping.Name = "_menuDisplayPageCropping";
            this._menuDisplayPageCropping.Size = new System.Drawing.Size(402, 32);
            this._menuDisplayPageCropping.Text = "Display Page Cropping";
            this._menuDisplayPageCropping.Click += new System.EventHandler(this._menuDisplayPageCropping_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(399, 6);
            // 
            // autoGeneralIndexTitleHeaderToolStripMenuItem
            // 
            this.autoGeneralIndexTitleHeaderToolStripMenuItem.Name = "autoGeneralIndexTitleHeaderToolStripMenuItem";
            this.autoGeneralIndexTitleHeaderToolStripMenuItem.Size = new System.Drawing.Size(402, 32);
            this.autoGeneralIndexTitleHeaderToolStripMenuItem.Text = "Auto General Index And Title Header";
            // 
            // _menuHr1
            // 
            this._menuHr1.Name = "_menuHr1";
            this._menuHr1.Size = new System.Drawing.Size(240, 6);
            // 
            // _menuSave
            // 
            this._menuSave.Name = "_menuSave";
            this._menuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this._menuSave.Size = new System.Drawing.Size(243, 32);
            this._menuSave.Text = "Save";
            this._menuSave.Click += new System.EventHandler(this._menuSave_Click);
            // 
            // _menuHr2
            // 
            this._menuHr2.Name = "_menuHr2";
            this._menuHr2.Size = new System.Drawing.Size(240, 6);
            // 
            // _menuExit
            // 
            this._menuExit.Name = "_menuExit";
            this._menuExit.Size = new System.Drawing.Size(243, 32);
            this._menuExit.Text = "&Exit";
            this._menuExit.Click += new System.EventHandler(this._menuExit_Click);
            // 
            // _menuBackupDocument
            // 
            this._menuBackupDocument.Name = "_menuBackupDocument";
            this._menuBackupDocument.Size = new System.Drawing.Size(243, 32);
            this._menuBackupDocument.Text = "&Backup Document";
            this._menuBackupDocument.Click += new System.EventHandler(this._menuBackupDocument_Click);
            // 
            // _labelPage
            // 
            this._labelPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._labelPage.BackColor = System.Drawing.Color.Transparent;
            this._labelPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelPage.ForeColor = System.Drawing.Color.Black;
            this._labelPage.Location = new System.Drawing.Point(-2, 283);
            this._labelPage.Name = "_labelPage";
            this._labelPage.Size = new System.Drawing.Size(35, 14);
            this._labelPage.TabIndex = 3;
            this._labelPage.Text = "0";
            this._labelPage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _pictureBox
            // 
            this._pictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._pictureBox.BackColor = System.Drawing.Color.White;
            this._pictureBox.Location = new System.Drawing.Point(-1, -234);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(295, 419);
            this._pictureBox.TabIndex = 4;
            this._pictureBox.TabStop = false;
            this._pictureBox.DoubleClick += new System.EventHandler(this._pictureBox_DoubleClick);
            this._pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this._pictureBox_MouseDown);
            this._pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this._pictureBox_MouseMove);
            this._pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this._pictureBox_MouseUp);
            // 
            // _checkSelection
            // 
            this._checkSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._checkSelection.AutoSize = true;
            this._checkSelection.BackColor = System.Drawing.Color.White;
            this._checkSelection.Location = new System.Drawing.Point(463, 283);
            this._checkSelection.Name = "_checkSelection";
            this._checkSelection.Size = new System.Drawing.Size(15, 14);
            this._checkSelection.TabIndex = 5;
            this._checkSelection.UseVisualStyleBackColor = false;
            // 
            // fMain
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(497, 297);
            this.Controls.Add(this._menuStrip);
            this.Controls.Add(this._checkSelection);
            this.Controls.Add(this._labelPage);
            this.Controls.Add(this._pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._menuStrip;
            this.Name = "fMain";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fMain_MouseClick);
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public fMain()
        {
            InitializeComponent();
            if (!Directory.Exists(PATH_DATA)) Directory.CreateDirectory(PATH_DATA);
        }

        private void fMain_Load(object sender, System.EventArgs e)
        {
            this.Width = 0;
            this.KeyPreview = true;
            this.KeyUp += form_KeyUp;
            this.Shown += (se, ev) =>
            {
                bool ok = false;
                string file = string.Empty;
                int p = 0;
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System\" + FOLDER_DATA, false);
                if (key != null)
                {
                    file = key.GetValue("File") as string;
                    if (!string.IsNullOrEmpty(file))
                    {
                        if (File.Exists(file))
                        {
                            var page = key.GetValue("Page") as string;
                            int.TryParse(page, out p);
                            ok = true;
                        }
                    }
                    key.Close();
                }

                this.Top = 0;
                this.Left = 0;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (ok) openFile(file, p);
                else this.Width = 235;
                IS_SELECTION = true;
            };
        }

        private void form_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F1:
                    if (IS_SELECTION)
                        IS_SELECTION = false;
                    else
                        IS_SELECTION = true;
                    break;
                case Keys.Right:
                    pageOpen(PageNumber + 1);
                    break;
                case Keys.Left:
                    pageOpen(PageNumber - 1);
                    break;
                case Keys.Up:
                    if (IS_SELECTION && _pictureBox.Tag != null)
                    {
                        var sel = (UiSelectRectangle)_pictureBox.Tag;
                        sel.Top = sel.Top - 1;
                        sel.Height = sel.Height + 1;

                        var rec = (Rectangle)sel.Tag;
                        rec.Y = rec.Y - 1;
                        rec.Height = rec.Height + 1;
                        sel.Tag = rec;
                    }
                    break;
                case Keys.Down:
                    if (IS_SELECTION && _pictureBox.Tag != null)
                    {
                        var sel = (UiSelectRectangle)_pictureBox.Tag;
                        sel.Top = sel.Top + 1;
                        sel.Height = sel.Height - 1;

                        var rec = (Rectangle)sel.Tag;
                        rec.Y = rec.Y + 1;
                        rec.Height = rec.Height - 1;
                        sel.Tag = rec;
                    }
                    break;
                case Keys.PageUp:
                    pageOpen(0);
                    break;
                case Keys.PageDown:
                    pageOpen(m_pages.Count - 1);
                    break;
                case Keys.Escape:
                    this.WindowState = FormWindowState.Minimized;
                    break;
            }
        }

        private void fMain_MouseClick(object sender, MouseEventArgs e)
        {
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

        #endregion

        #region [ PICTURE_BOX ]

        int COUNTER_REINDEX = 0;
        int x = 0, y = 0, x1 = 0, y1 = 0;
        List<UiSelectRectangle> m_selections = new List<UiSelectRectangle>() { };

        bool IS_MODE_REINDEX
        {
            get
            {
                return _menuSetIndex.Checked;
            }

            set
            {
                if (value)
                {
                    foreach (var l in _pictureBox.Controls) (l as UiSelectRectangle).Index = 0;
                    _menuSetIndex.Checked = true;
                }
                else
                {
                    COUNTER_REINDEX = 0;
                    _menuSetIndex.Checked = false;
                }
            }
        }

        private void _pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (IS_SELECTION)
            {
                x = e.X;
                y = e.Y;

                var sel = new UiSelectRectangle(m_selections.Count + 1)
                {
                    Width = 1,
                    Height = 1,
                    Location = new Point(x, y)
                };
                sel.MouseDoubleClick += (sv, ev) =>
                {
                    if (IS_SELECTION)
                    {
                        int i = m_selections.FindIndex(x => x == sel);
                        if (i != -1) m_selections.RemoveAt(i);
                        _pictureBox.Controls.Remove(sel);
                    }
                };
                sel.MouseClick += (sv, ev) =>
                {
                    if (IS_SELECTION && IS_MODE_REINDEX)
                    {
                        COUNTER_REINDEX++;
                        sel.Index = COUNTER_REINDEX;
                        if (m_selections.Count == COUNTER_REINDEX) IS_MODE_REINDEX = false;
                    }
                };
                _pictureBox.Tag = sel;
                _pictureBox.Controls.Add(sel);
                sel.BringToFront();
            }
        }

        private void _pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            bool IS_SELECTING = false;
            if (IS_SELECTION && _pictureBox.Tag != null)
            {
                int w = Math.Abs(x - x1),
                    h = Math.Abs(y - y1);
                Rectangle rec = new Rectangle(x, y, w, h);
                if (w > 99 && h > 32)
                {
                    var sel = (UiSelectRectangle)_pictureBox.Tag;
                    sel.Tag = rec;
                    sel.Width = w;
                    sel.Height = h;
                    //sel.SetOpacity(50);
                    m_selections.Add(sel);
                    IS_SELECTING = true;
                }

                x = 0;
                y = 0;
            }

            if (IS_SELECTING == false)
            {
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
        }

        private void _pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (IS_SELECTION && x > 0 && y > 0)
            {
                x1 = e.X;
                y1 = e.Y;

                if (_pictureBox.Tag != null)
                {
                    var sel = (UiSelectRectangle)_pictureBox.Tag;
                    int w = Math.Abs(x - x1), h = Math.Abs(y - y1);
                    sel.Width = w;
                    sel.Height = h;
                }
            }
        }

        private void _pictureBox_DoubleClick(object sender, EventArgs e)
        {

        }

        private void _pictureBox_Click(object sender, System.EventArgs e)
        {
            //if (DocumentName.Length == 0 || IS_SELECTION) return;

        }

        #endregion

        #region [ MENU ]

        private void _menuBackupDocument_Click(object sender, EventArgs e)
        {

        }

        private void _menuOpen_Click(object sender, System.EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = PATH_DATA;
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

        private void _menuSave_Click(object sender, EventArgs e)
        {
            updateDocument();
        }

        private void _menuCropSelection_Click(object sender, EventArgs e)
        {

        }

        private void _menuExtracTextSelection_Click(object sender, EventArgs e)
        {
            extractSelectionText();
        }

        private void _menuSetIndex_Click(object sender, EventArgs e)
        {

        }

        private void _menuExit_Click(object sender, System.EventArgs e)
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
                key = key.CreateSubKey(FOLDER_DATA);
                key.SetValue("Page", PageNumber.ToString(), RegistryValueKind.String);
                key.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            this.Close();
        }

        private void _menuDisplayPageCropping_Click(object sender, EventArgs e)
        {
            if (_menuDisplayPageCropping.Checked)
                _menuDisplayPageCropping.Checked = false;
            else
                _menuDisplayPageCropping.Checked = true;
        }

        private void _menuKeepSelectChangePage_Click(object sender, EventArgs e)
        {
            if (_menuKeepSelectChangePage.Checked)
                _menuKeepSelectChangePage.Checked = false;
            else
                _menuKeepSelectChangePage.Checked = true;
        }


        #endregion

        #region [ FUNCTIONS ]

        public bool IS_SELECTION
        {
            get { return _checkSelection.Checked; }
            set
            {
                if (value)
                {
                    _checkSelection.Checked = true;
                }
                else
                {
                    _checkSelection.Checked = false;
                }
            }
        }

        void extractSelectionText()
        {
            if (IS_SELECTION && _pictureBox.Tag != null)
            {
                var ui = (UiSelectRectangle)_pictureBox.Tag;
                var rec = (Rectangle)ui.Tag;
                var buf = getImageSelection(rec);
                if (buf != null && buf.Length > 0)
                {
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "tessdata");
                    using (var tesseract = new TesseractEngine(path, "eng"))//eng vie  
                    {
                        using (var img = Pix.LoadTiffFromMemory(buf))
                        {
                            using (var page = tesseract.Process(img))
                            {
                                var text = page.GetText();
                            }
                        }
                    }
                }
            }
        }

        void openFile(string file, int page = 0)
        {
            _pictureBox.Visible = false;

            DocumentFile = file;
            DocumentName = doc_formatName(Path.GetFileName(DocumentFile));

            m_pages.Clear();
            m_page_crops.Clear();

            using (ZipFile zip = ZipFile.Read(DocumentFile))
            {
                foreach (ZipEntry entry in zip)
                {
                    int i = int.Parse(entry.FileName.Substring(0, entry.FileName.Length - 4));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        entry.ExtractWithPassword(ms, PASSWORD);
                        m_pages.Add(i, ms.ToArray());
                    }
                }
            }

            pageOpen(page);
            _pictureBox.Visible = true;
        }

        void pageOpen(int page)
        {
            if (m_pages.Count > 0
                //&& PageNumber != page 
                && m_pages.ContainsKey(page))
            {
                if (IS_SELECTION && _menuAutoCropPageSelected.Checked && m_selections.Count > 0)
                    cropPageUpdate();

                PageNumber = page;

                byte[] buf;
                if (m_page_crops.ContainsKey(page) && _menuDisplayPageCropping.Checked)
                {
                    buf = m_page_crops[page];
                    _labelPage.BackColor = Color.Orange;
                }
                else
                {
                    buf = m_pages[page];
                    _labelPage.BackColor = Color.Transparent;
                }

                Image img = null;
                using (MemoryStream ms = new MemoryStream(buf, 0, buf.Length))
                {
                    ms.Write(buf, 0, buf.Length);
                    img = Image.FromStream(ms, true);
                    openImage(img);
                }

                string spage = (page + 1).ToString();
                _labelPage.Text = spage;

                this.Text = string.Format("{0}.{1}", spage, DocumentName);
            }
        }

        void openImage(Image img)
        {
            if (_menuKeepSelectChangePage.Checked == false)
            {
                m_selections.Clear();
                _pictureBox.Controls.Clear();
            }

            int w = 0, h = 0, _top = 0;

            int w0 = img.Width, h0 = img.Height;
            if (w0 < Screen.PrimaryScreen.WorkingArea.Width && h0 < this.Height)
            {
                w = w0;
                h = h0;
                _top = (this.Height - h) / 2;

                this.Width = w;

                _pictureBox.Width = w;
                _pictureBox.Height = h;
                _pictureBox.Location = new Point(0, _top);
                _pictureBox.Image = img;
                _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                _top = 3;
                int _bottom = 7, _left = 3, _right = 3;

                h = this.Height - (_top + _bottom);
                w = (h * w0 / h0);

                this.Width = w + _left + _right;

                _pictureBox.Anchor = AnchorStyles.None;
                _pictureBox.Width = w;
                _pictureBox.Height = h;
                _pictureBox.Location = new Point(_left, _top);
                _pictureBox.Image = img;
                _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                //this.BackColor = Color.Black;
            }


            this.Tag = new Size(w0, h0);
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

        byte[] getImageSelection(Rectangle rec)
        {
            byte[] buf = new byte[] { };
            var oriSize = (Size)this.Tag;
            int w0 = oriSize.Width, h0 = oriSize.Height;

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
            using (var ms = new MemoryStream())
            {
                target.Save(ms, System.Drawing.Imaging.ImageFormat.Tiff);
                buf = ms.ToArray();
            }
            return buf;
        }

        void cropPageUpdate()
        {
            if (m_selections.Count > 0)
            {
                for (var i = 0; i < m_selections.Count; i++)
                {
                    var tag = m_selections[i].Tag;
                    if (tag != null)
                    {
                        var rec = (Rectangle)tag;
                        var oriSize = (Size)this.Tag;
                        int w0 = oriSize.Width, h0 = oriSize.Height;

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

                        //target.Save(@"C:\test\" + i + ".jpg", ImageFormat.Jpeg);
                        using (var ms = new MemoryStream())
                        {
                            target.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            if (m_page_crops.ContainsKey(PageNumber))
                                m_page_crops[PageNumber] = ms.ToArray();
                            else m_page_crops.Add(PageNumber, ms.ToArray());
                        }
                    }
                }
            }

            //_buttonSave.Enabled = false;
        }

        void updateDocument()
        {
            if (m_page_crops.Count > 0)
            {
                using (ZipFile zip = ZipFile.Read(DocumentFile))
                {
                    foreach (int key in m_page_crops.Keys)
                        zip.UpdateEntry(key.ToString() + ".jpg", m_page_crops[key]);
                    zip.Save();
                }

                foreach (int key in m_page_crops.Keys)
                    m_pages[key] = m_page_crops[key];

                m_page_crops.Clear();
            }
        }

        #endregion
    }
}
