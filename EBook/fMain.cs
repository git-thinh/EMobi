using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tesseract;
using Salar.Bois;
using PdfiumViewer;
using Newtonsoft.Json;

namespace EBook
{
    class fMain : Form, IMain
    {
        const int IMG_WIDTH_BIG = 2400;
        const int IMG_WIDTH_NORMAL = 1200;

        #region [ MAIN ]

        public bool IS_SELECTION
        {
            get { return _menuIsSelectionMode.Checked; }
            set
            {
                if (value)
                {
                    _menuIsSelectionMode.Checked = true;
                    _labelPage.BackColor = Color.Blue;
                    _labelPage.ForeColor = Color.White;
                }
                else
                {
                    _menuIsSelectionMode.Checked = false;
                    _labelPage.BackColor = Color.Transparent;
                    _labelPage.ForeColor = Color.Black;
                }
            }
        }
        //_menuSelectionAttachMedia.Checked;
        //_menuAutoCropPageSelected.Checked;
        public bool SELECTION_ATTACH_MEDIA { get; set; }
        public bool AUTO_CROP_PAGE_SELECTED { get; set; }

        public int PageNumber { get; set; }
        public string DocumentFile { get; set; }
        public string DocumentName { get; set; }

        public string PASSWORD { get; }
        public string FOLDER_DATA { get; }
        public string PATH_DATA { get; }

        public Dictionary<int, byte[]> m_pages { get; }
        public Dictionary<int, byte[]> m_page_crops { get; }
        public Dictionary<int, oPage> m_infos { get; set; }

        private MenuStrip _menuStrip;
        private ToolStripMenuItem _menuOpen;
        private ToolStripSeparator _menuHr2;
        private ToolStripMenuItem _menuExit;
        private Label _labelPage;
        private PictureBox _pictureBox;
        private ToolStripMenuItem _menuSave;
        private ToolStripSeparator _menuHr0;
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
        private ToolStripMenuItem _menuAutoRun;
        private ToolStripMenuItem menuAutoRun_updateIndex;
        private ToolStripMenuItem _menuCleanAllCacheBefore;
        private ToolStripSeparator toolStripSeparator3;
        private Label label1;
        private ToolStripMenuItem _menuCleanSelectionPageCurrent;
        private ToolStripMenuItem _menuMedia;
        private ToolStripMenuItem _menuGoPage;
        private ToolStripMenuItem _menuSelectionAttachMedia;
        private ToolStripMenuItem _menuOpenMediaWindow;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem searchToolStripMenuItem;
        private ToolStripMenuItem searchInPageToolStripMenuItem1;
        private ToolStripMenuItem searchInDocumentToolStripMenuItem1;
        private ToolStripMenuItem searchInAllDocumentToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem searchOnlyAudioToolStripMenuItem1;
        private ToolStripMenuItem searchOnlyAudioWordToolStripMenuItem1;
        private ToolStripMenuItem searchOnlyMovieToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem searchYoutubeOnlineToolStripMenuItem;
        private ToolStripMenuItem searchAdvancedToolStripMenuItem;
        private ToolStripMenuItem _menuHide;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem _menuIsSelectionMode;
        private ToolStripMenuItem _menuMain;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._menuMain = new System.Windows.Forms.ToolStripMenuItem();
            this._menuHide = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this._menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this._menuMedia = new System.Windows.Forms.ToolStripMenuItem();
            this._menuSelectionAttachMedia = new System.Windows.Forms.ToolStripMenuItem();
            this._menuOpenMediaWindow = new System.Windows.Forms.ToolStripMenuItem();
            this._menuGoPage = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchInPageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.searchInDocumentToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.searchInAllDocumentToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.searchOnlyAudioToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.searchOnlyAudioWordToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.searchOnlyMovieToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.searchYoutubeOnlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchAdvancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._menuHr0 = new System.Windows.Forms.ToolStripSeparator();
            this._menuIsSelectionMode = new System.Windows.Forms.ToolStripMenuItem();
            this._menuSelection = new System.Windows.Forms.ToolStripMenuItem();
            this._menuCleanAllCacheBefore = new System.Windows.Forms.ToolStripMenuItem();
            this._menuCleanSelectionPageCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._menuCropSelection = new System.Windows.Forms.ToolStripMenuItem();
            this._menuExtracTextSelection = new System.Windows.Forms.ToolStripMenuItem();
            this._menuSetIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._menuAutoCropPageSelected = new System.Windows.Forms.ToolStripMenuItem();
            this._menuKeepSelectChangePage = new System.Windows.Forms.ToolStripMenuItem();
            this._menuDisplayPageCropping = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._menuAutoRun = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAutoRun_updateIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this._menuBackupDocument = new System.Windows.Forms.ToolStripMenuItem();
            this._menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this._menuHr2 = new System.Windows.Forms.ToolStripSeparator();
            this._menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this._labelPage = new System.Windows.Forms.Label();
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this._menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _menuStrip
            // 
            this._menuStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._menuStrip.BackColor = System.Drawing.SystemColors.ControlDark;
            this._menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuMain});
            this._menuStrip.Location = new System.Drawing.Point(290, 212);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Padding = new System.Windows.Forms.Padding(0);
            this._menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this._menuStrip.Size = new System.Drawing.Size(150, 28);
            this._menuStrip.TabIndex = 0;
            // 
            // _menuMain
            // 
            this._menuMain.AutoSize = false;
            this._menuMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuHide,
            this.toolStripSeparator7,
            this._menuOpen,
            this._menuMedia,
            this._menuGoPage,
            this.searchToolStripMenuItem,
            this._menuHr0,
            this._menuIsSelectionMode,
            this._menuSelection,
            this.toolStripSeparator4,
            this._menuBackupDocument,
            this._menuSave,
            this._menuHr2,
            this._menuExit});
            this._menuMain.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._menuMain.ForeColor = System.Drawing.Color.Black;
            this._menuMain.Name = "_menuMain";
            this._menuMain.Padding = new System.Windows.Forms.Padding(0);
            this._menuMain.Size = new System.Drawing.Size(28, 28);
            this._menuMain.Text = "≡";
            this._menuMain.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this._menuMain.Click += new System.EventHandler(this._menuMain_Click);
            // 
            // _menuHide
            // 
            this._menuHide.Name = "_menuHide";
            this._menuHide.Size = new System.Drawing.Size(199, 30);
            this._menuHide.Text = "Hide Menu";
            this._menuHide.Click += new System.EventHandler(this._menuHide_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(196, 6);
            // 
            // _menuOpen
            // 
            this._menuOpen.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._menuOpen.Name = "_menuOpen";
            this._menuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this._menuOpen.Size = new System.Drawing.Size(199, 30);
            this._menuOpen.Text = "&Open";
            this._menuOpen.Click += new System.EventHandler(this._menuOpen_Click);
            // 
            // _menuMedia
            // 
            this._menuMedia.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuSelectionAttachMedia,
            this._menuOpenMediaWindow});
            this._menuMedia.Name = "_menuMedia";
            this._menuMedia.Size = new System.Drawing.Size(199, 30);
            this._menuMedia.Text = "Media";
            // 
            // _menuSelectionAttachMedia
            // 
            this._menuSelectionAttachMedia.Name = "_menuSelectionAttachMedia";
            this._menuSelectionAttachMedia.Size = new System.Drawing.Size(252, 24);
            this._menuSelectionAttachMedia.Text = "Selection To Attach Media";
            this._menuSelectionAttachMedia.Click += new System.EventHandler(this._menuSelectionAttachMedia_Click);
            // 
            // _menuOpenMediaWindow
            // 
            this._menuOpenMediaWindow.Name = "_menuOpenMediaWindow";
            this._menuOpenMediaWindow.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this._menuOpenMediaWindow.Size = new System.Drawing.Size(252, 24);
            this._menuOpenMediaWindow.Text = "Open Media Window";
            this._menuOpenMediaWindow.Click += new System.EventHandler(this._menuOpenMediaWindow_Click);
            // 
            // _menuGoPage
            // 
            this._menuGoPage.Name = "_menuGoPage";
            this._menuGoPage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this._menuGoPage.Size = new System.Drawing.Size(199, 30);
            this._menuGoPage.Text = "&Go Page";
            this._menuGoPage.Click += new System.EventHandler(this._menuGoPage_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchInPageToolStripMenuItem1,
            this.searchInDocumentToolStripMenuItem1,
            this.searchInAllDocumentToolStripMenuItem1,
            this.toolStripSeparator5,
            this.searchOnlyAudioToolStripMenuItem1,
            this.searchOnlyAudioWordToolStripMenuItem1,
            this.searchOnlyMovieToolStripMenuItem1,
            this.toolStripSeparator6,
            this.searchYoutubeOnlineToolStripMenuItem,
            this.searchAdvancedToolStripMenuItem});
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(199, 30);
            this.searchToolStripMenuItem.Text = "&Search";
            // 
            // searchInPageToolStripMenuItem1
            // 
            this.searchInPageToolStripMenuItem1.Name = "searchInPageToolStripMenuItem1";
            this.searchInPageToolStripMenuItem1.Size = new System.Drawing.Size(240, 24);
            this.searchInPageToolStripMenuItem1.Text = "Search In Page";
            // 
            // searchInDocumentToolStripMenuItem1
            // 
            this.searchInDocumentToolStripMenuItem1.Name = "searchInDocumentToolStripMenuItem1";
            this.searchInDocumentToolStripMenuItem1.Size = new System.Drawing.Size(240, 24);
            this.searchInDocumentToolStripMenuItem1.Text = "Search In Document";
            // 
            // searchInAllDocumentToolStripMenuItem1
            // 
            this.searchInAllDocumentToolStripMenuItem1.Name = "searchInAllDocumentToolStripMenuItem1";
            this.searchInAllDocumentToolStripMenuItem1.Size = new System.Drawing.Size(240, 24);
            this.searchInAllDocumentToolStripMenuItem1.Text = "Search In All Document";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(237, 6);
            // 
            // searchOnlyAudioToolStripMenuItem1
            // 
            this.searchOnlyAudioToolStripMenuItem1.Name = "searchOnlyAudioToolStripMenuItem1";
            this.searchOnlyAudioToolStripMenuItem1.Size = new System.Drawing.Size(240, 24);
            this.searchOnlyAudioToolStripMenuItem1.Text = "Search Only Audio";
            // 
            // searchOnlyAudioWordToolStripMenuItem1
            // 
            this.searchOnlyAudioWordToolStripMenuItem1.Name = "searchOnlyAudioWordToolStripMenuItem1";
            this.searchOnlyAudioWordToolStripMenuItem1.Size = new System.Drawing.Size(240, 24);
            this.searchOnlyAudioWordToolStripMenuItem1.Text = "Search Only Audio Word";
            // 
            // searchOnlyMovieToolStripMenuItem1
            // 
            this.searchOnlyMovieToolStripMenuItem1.Name = "searchOnlyMovieToolStripMenuItem1";
            this.searchOnlyMovieToolStripMenuItem1.Size = new System.Drawing.Size(240, 24);
            this.searchOnlyMovieToolStripMenuItem1.Text = "Search Only Movie";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(237, 6);
            // 
            // searchYoutubeOnlineToolStripMenuItem
            // 
            this.searchYoutubeOnlineToolStripMenuItem.Name = "searchYoutubeOnlineToolStripMenuItem";
            this.searchYoutubeOnlineToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.searchYoutubeOnlineToolStripMenuItem.Text = "Search Youtube Online";
            // 
            // searchAdvancedToolStripMenuItem
            // 
            this.searchAdvancedToolStripMenuItem.Name = "searchAdvancedToolStripMenuItem";
            this.searchAdvancedToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.searchAdvancedToolStripMenuItem.Text = "Search Advanced";
            // 
            // _menuHr0
            // 
            this._menuHr0.Name = "_menuHr0";
            this._menuHr0.Size = new System.Drawing.Size(196, 6);
            // 
            // _menuIsSelectionMode
            // 
            this._menuIsSelectionMode.Name = "_menuIsSelectionMode";
            this._menuIsSelectionMode.Size = new System.Drawing.Size(199, 30);
            this._menuIsSelectionMode.Text = "Selection Mode";
            this._menuIsSelectionMode.Click += new System.EventHandler(this._menuIsSelectionMode_Click);
            // 
            // _menuSelection
            // 
            this._menuSelection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuCleanAllCacheBefore,
            this._menuCleanSelectionPageCurrent,
            this.toolStripSeparator3,
            this._menuCropSelection,
            this._menuExtracTextSelection,
            this._menuSetIndex,
            this.toolStripSeparator1,
            this._menuAutoCropPageSelected,
            this._menuKeepSelectChangePage,
            this._menuDisplayPageCropping,
            this.toolStripSeparator2,
            this._menuAutoRun});
            this._menuSelection.Name = "_menuSelection";
            this._menuSelection.Size = new System.Drawing.Size(199, 30);
            this._menuSelection.Text = "Selection";
            // 
            // _menuCleanAllCacheBefore
            // 
            this._menuCleanAllCacheBefore.Name = "_menuCleanAllCacheBefore";
            this._menuCleanAllCacheBefore.Size = new System.Drawing.Size(268, 24);
            this._menuCleanAllCacheBefore.Text = "Clean All Cache Before";
            this._menuCleanAllCacheBefore.Click += new System.EventHandler(this._menuCleanAllCacheBefore_Click);
            // 
            // _menuCleanSelectionPageCurrent
            // 
            this._menuCleanSelectionPageCurrent.Name = "_menuCleanSelectionPageCurrent";
            this._menuCleanSelectionPageCurrent.Size = new System.Drawing.Size(268, 24);
            this._menuCleanSelectionPageCurrent.Text = "Clean Selection Page Current";
            this._menuCleanSelectionPageCurrent.Click += new System.EventHandler(this._menuCleanSelectionPageCurrent_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(265, 6);
            // 
            // _menuCropSelection
            // 
            this._menuCropSelection.Name = "_menuCropSelection";
            this._menuCropSelection.Size = new System.Drawing.Size(268, 24);
            this._menuCropSelection.Text = "&Crop";
            this._menuCropSelection.Click += new System.EventHandler(this._menuCropSelection_Click);
            // 
            // _menuExtracTextSelection
            // 
            this._menuExtracTextSelection.Name = "_menuExtracTextSelection";
            this._menuExtracTextSelection.Size = new System.Drawing.Size(268, 24);
            this._menuExtracTextSelection.Text = "&Extract Text";
            this._menuExtracTextSelection.Click += new System.EventHandler(this._menuExtracTextSelection_Click);
            // 
            // _menuSetIndex
            // 
            this._menuSetIndex.Name = "_menuSetIndex";
            this._menuSetIndex.Size = new System.Drawing.Size(268, 24);
            this._menuSetIndex.Text = "Set &Index";
            this._menuSetIndex.Click += new System.EventHandler(this._menuSetIndex_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(265, 6);
            // 
            // _menuAutoCropPageSelected
            // 
            this._menuAutoCropPageSelected.Checked = true;
            this._menuAutoCropPageSelected.CheckState = System.Windows.Forms.CheckState.Checked;
            this._menuAutoCropPageSelected.Name = "_menuAutoCropPageSelected";
            this._menuAutoCropPageSelected.Size = new System.Drawing.Size(268, 24);
            this._menuAutoCropPageSelected.Text = "Auto Crop Page Selected";
            this._menuAutoCropPageSelected.Click += new System.EventHandler(this._menuAutoCropPageSelected_Click);
            // 
            // _menuKeepSelectChangePage
            // 
            this._menuKeepSelectChangePage.Checked = true;
            this._menuKeepSelectChangePage.CheckState = System.Windows.Forms.CheckState.Checked;
            this._menuKeepSelectChangePage.Name = "_menuKeepSelectChangePage";
            this._menuKeepSelectChangePage.Size = new System.Drawing.Size(268, 24);
            this._menuKeepSelectChangePage.Text = "Keep Selection Change Page";
            this._menuKeepSelectChangePage.Click += new System.EventHandler(this._menuKeepSelectChangePage_Click);
            // 
            // _menuDisplayPageCropping
            // 
            this._menuDisplayPageCropping.Name = "_menuDisplayPageCropping";
            this._menuDisplayPageCropping.Size = new System.Drawing.Size(268, 24);
            this._menuDisplayPageCropping.Text = "Display Page Cropping";
            this._menuDisplayPageCropping.Click += new System.EventHandler(this._menuDisplayPageCropping_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(265, 6);
            // 
            // _menuAutoRun
            // 
            this._menuAutoRun.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAutoRun_updateIndex});
            this._menuAutoRun.Name = "_menuAutoRun";
            this._menuAutoRun.Size = new System.Drawing.Size(268, 24);
            this._menuAutoRun.Text = "Auto Run";
            // 
            // menuAutoRun_updateIndex
            // 
            this.menuAutoRun_updateIndex.Name = "menuAutoRun_updateIndex";
            this.menuAutoRun_updateIndex.Size = new System.Drawing.Size(171, 24);
            this.menuAutoRun_updateIndex.Text = "Update Index ";
            this.menuAutoRun_updateIndex.Click += new System.EventHandler(this.menuAutoRun_updateIndex_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(196, 6);
            // 
            // _menuBackupDocument
            // 
            this._menuBackupDocument.Name = "_menuBackupDocument";
            this._menuBackupDocument.Size = new System.Drawing.Size(199, 30);
            this._menuBackupDocument.Text = "&Backup Document";
            this._menuBackupDocument.Click += new System.EventHandler(this._menuBackupDocument_Click);
            // 
            // _menuSave
            // 
            this._menuSave.Name = "_menuSave";
            this._menuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this._menuSave.Size = new System.Drawing.Size(199, 30);
            this._menuSave.Text = "Save";
            this._menuSave.Click += new System.EventHandler(this._menuSave_Click);
            // 
            // _menuHr2
            // 
            this._menuHr2.Name = "_menuHr2";
            this._menuHr2.Size = new System.Drawing.Size(196, 6);
            // 
            // _menuExit
            // 
            this._menuExit.Name = "_menuExit";
            this._menuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this._menuExit.Size = new System.Drawing.Size(199, 30);
            this._menuExit.Text = "&Exit";
            this._menuExit.Click += new System.EventHandler(this._menuExit_Click);
            // 
            // _labelPage
            // 
            this._labelPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._labelPage.BackColor = System.Drawing.Color.Transparent;
            this._labelPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelPage.ForeColor = System.Drawing.Color.Silver;
            this._labelPage.Location = new System.Drawing.Point(462, 283);
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
            this._pictureBox.Size = new System.Drawing.Size(190, 422);
            this._pictureBox.TabIndex = 4;
            this._pictureBox.TabStop = false;
            this._pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this._pictureBox_MouseDown);
            this._pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this._pictureBox_MouseMove);
            this._pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this._pictureBox_MouseUp);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(157, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 46);
            this.label1.TabIndex = 6;
            this.label1.Text = "LOADING .....";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fMain
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(497, 297);
            this.Controls.Add(this._menuStrip);
            this.Controls.Add(this._labelPage);
            this.Controls.Add(this._pictureBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._menuStrip;
            this.Name = "fMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMain_FormClosing);
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

            PageNumber = 0;
            DocumentFile = string.Empty;
            DocumentName = string.Empty;
            PASSWORD = "Mr.Thinh's Gifts";
            FOLDER_DATA = "book.data";
            PATH_DATA = Application.StartupPath[0] + @":\" + FOLDER_DATA;
            if (!Directory.Exists(PATH_DATA)) Directory.CreateDirectory(PATH_DATA);

            m_pages = new Dictionary<int, byte[]>() { };
            m_page_crops = new Dictionary<int, byte[]>() { };
            m_infos = new Dictionary<int, oPage>() { };
        }

        private void fMain_Load(object sender, System.EventArgs e)
        {
            app_Load();
        }

        private void form_KeyUp(object sender, KeyEventArgs e)
        {
            app_keyPress(e);
        }

        private void fMain_MouseClick(object sender, MouseEventArgs e)
        {
            app_click(e);
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            closing();
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
                app_click(e);
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

        #endregion

        #region [ MENU ]

        private void _menuSelectionAttachMedia_Click(object sender, EventArgs e)
        {
            menuHide();

            if (_menuSelectionAttachMedia.Checked)
            {
                _menuSelectionAttachMedia.Checked = false;
            }
            else
            {
                _menuSelectionAttachMedia.Checked = true;
                _menuAutoCropPageSelected.Checked = false;
            }
        }

        private void _menuOpenMediaWindow_Click(object sender, EventArgs e)
        {
            menuHide();

            new fMedia(this).ShowDialog();
        }

        private void _menuHide_Click(object sender, EventArgs e)
        {
            menuHide();
        }

        private void _menuGoPage_Click(object sender, EventArgs e)
        {
            menuHide();

            if (!string.IsNullOrEmpty(this.DocumentFile)) {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Go to page", "Input number page you want to go?", string.Empty);
                int page = 0;
                int.TryParse(input, out page);
                if (page > 0) {
                    pageOpen(page);
                }
            }
        }

        private void _menuCleanAllCacheBefore_Click(object sender, EventArgs e)
        {
            menuHide();

            selection_cleanAllCache();
        }

        private void _menuBackupDocument_Click(object sender, EventArgs e)
        {
            menuHide();


        }

        private void _menuOpen_Click(object sender, System.EventArgs e)
        {
            menuHide();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = PATH_DATA;
                openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|EBook Files (*.ebk)|*.ebk";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string file = openFileDialog.FileName;
                    if (file.EndsWith(".ebk"))
                        openFileEBK(file);
                    else
                    {
                        openFilePdf(file);
                    }
                }
            }
        }

        private void _menuSave_Click(object sender, EventArgs e)
        {
            menuHide();

            updateDocument();
        }

        private void _menuCropSelection_Click(object sender, EventArgs e)
        {
            menuHide();


        }

        private void _menuExtracTextSelection_Click(object sender, EventArgs e)
        {
            menuHide();

            extractSelectionText();
        }

        private void _menuSetIndex_Click(object sender, EventArgs e)
        {
            menuHide();


        }

        private void _menuExit_Click(object sender, System.EventArgs e)
        {
            menuHide();

            this.Close();
        }

        private void _menuDisplayPageCropping_Click(object sender, EventArgs e)
        {
            menuHide();

            if (_menuDisplayPageCropping.Checked)
                _menuDisplayPageCropping.Checked = false;
            else
                _menuDisplayPageCropping.Checked = true;
        }

        private void _menuKeepSelectChangePage_Click(object sender, EventArgs e)
        {
            menuHide();

            if (_menuKeepSelectChangePage.Checked)
                _menuKeepSelectChangePage.Checked = false;
            else
                _menuKeepSelectChangePage.Checked = true;
        }

        private void menuAutoRun_updateIndex_Click(object sender, EventArgs e)
        {
            menuHide();

            autoRun_updateIndex();
        }

        private void _menuAutoCropPageSelected_Click(object sender, EventArgs e)
        {
            menuHide();

            if (_menuAutoCropPageSelected.Checked)
            {
                _menuMain.BackColor = Color.White;
                _menuAutoCropPageSelected.Checked = false;
            }
            else
            {
                _menuMain.BackColor = Color.Red;
                _menuAutoCropPageSelected.Checked = true;
            }
        }

        private void _menuCleanSelectionPageCurrent_Click(object sender, EventArgs e)
        {
            menuHide();

            _pictureBox.Tag = null;
            _pictureBox.Controls.Clear();

            m_selections.Clear();
            if (m_page_crops.ContainsKey(PageNumber))
            {
                m_page_crops.Remove(PageNumber);
            }
        }

        private void _menuIsSelectionMode_Click(object sender, EventArgs e)
        {
            menuHide();

            if (IS_SELECTION)
                IS_SELECTION = false;
            else
                IS_SELECTION = true;
        }

        private void _menuMain_Click(object sender, EventArgs e)
        {
            menuHide();
        }

        #endregion

        #region [ FUNCTIONS ]

        void app_Load() {
            menuHide();

            this.Width = 0;
            this.KeyPreview = true;
            this.KeyUp += form_KeyUp;
            this.Shown += (se, ev) =>
            {
                oSetting setting = null;
                try
                {
                    if (File.Exists("setting.bin"))
                        setting = JsonConvert.DeserializeObject<oSetting>(File.ReadAllText("setting.bin"));
                }
                catch { }

                this.Top = 0;
                this.Left = 0;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;

                if (setting == null) setting = new oSetting();
                if (string.IsNullOrEmpty(setting.DocumentFile) || File.Exists(setting.DocumentFile) == false)
                    setting.DocumentFile = string.Empty;
                if (setting.DocumentFile.Length > 0)
                    openFileEBK(setting.DocumentFile, setting.PageNumber);
                else this.Width = 235;

                IS_SELECTION = setting.IS_SELECTION;
                //if (_menuAutoCropPageSelected.Checked) _menuMain.BackColor = Color.Red;

            };
        }

        private bool IS_CROP_ENTERING = false;
        void app_keyPress(KeyEventArgs ev) {

            switch (ev.KeyData)
            {
                case Keys.Enter:
                    if (IS_SELECTION && _menuAutoCropPageSelected.Checked && m_selections.Count > 0)
                    {
                        cropPageUpdate();
                        IS_CROP_ENTERING = true;
                    }
                    pageOpen(PageNumber);
                    break;
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
                    if(_menuStrip.Visible)
                    {
                        menuHide();
                    }
                    else
                    {
                        this.WindowState = FormWindowState.Minimized;
                    }
                    break;
            }
        }

        void app_click(MouseEventArgs ev) {
            MouseEventArgs e = (MouseEventArgs)ev;
            if (e.Button == MouseButtons.Right)
            {
                _menuStrip.Location = new Point(e.X, e.Y);
                _menuStrip.Visible = true;
                _menuMain.ShowDropDown();
            }
            else if (e.Button == MouseButtons.Left)
            {
                int x = (this.Location.X + this.Width) / 2, 
                    page = 0;

                if (e.X > x)
                    page = PageNumber + 1;
                else
                    page = PageNumber - 1;
                    
                pageOpen(page);
            }
        }

        void menuHide() {
            _menuStrip.Visible = false;
        }

        void extractSelectionText()
        {
            if (IS_SELECTION && _pictureBox.Tag != null)
            {
                var ui = (UiSelectRectangle)_pictureBox.Tag;
                var rec = (Rectangle)ui.Tag;
                var buf = getImageSelection(_pictureBox.Image, rec);
                if (buf != null && buf.Length > 0)
                {
                    //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "tessdata");
                    string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    using (var tesseract = new TesseractEngine(path, "eng"))//eng vie  
                    {
                        using (var img = Pix.LoadTiffFromMemory(buf))
                        {
                            using (var page = tesseract.Process(img))
                            {
                                var text = page.GetText();
                                MessageBox.Show(text);
                            }
                        }
                    }
                }
            }
        }

        void openFilePdf(string file, int page = 0)
        {
            this.Enabled = false;
            _pictureBox.Visible = false;
            page_cleanAll();
            try
            {
                DocumentName = doc_formatName(Path.GetFileName(file));
                DocumentFile = Path.Combine(PATH_DATA, DocumentName + ".ebk");

                if (File.Exists(DocumentFile))
                {
                    openFileEBK(DocumentFile);
                    return;
                }

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
                            w = IMG_WIDTH_BIG;
                            h = w * hp / wp;
                        }
                        else
                        {
                            w = IMG_WIDTH_NORMAL;
                            h = w * hp / wp;
                        }

                        using (var image = document.Render(i, w, h, dpi, dpi, PdfRenderFlags.Annotations))
                        {
                            //image.Save(stream, ImageFormat.Jpeg);
                            using (MemoryStream ms = new MemoryStream())
                            {
                                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                var buf = ms.ToArray();
                                m_pages.Add(i, buf);
                            }
                        }
                    }//end for

                    using (var fs = File.Create(DocumentFile))
                    {
                        using (var zipStream = new ZipOutputStream(fs))
                        {
                            zipStream.CompressionLevel = Ionic.Zlib.CompressionLevel.Level9;
                            zipStream.Password = PASSWORD;

                            foreach (var kv in m_pages)
                            {
                                zipStream.PutNextEntry(kv.Key.ToString() + ".jpg");
                                zipStream.Write(kv.Value, 0, kv.Value.Length);
                            }
                        }
                    }

                    //using (var fileStream = File.Create(zipFile))
                    //{
                    //    using (ZipOutputStream zipStream = new ZipOutputStream(fileStream))
                    //    {
                    //        zipStream.SetLevel(9); // 0 - store only to 9 - means best compression
                    //        zipStream.Password = m_app.FOLDER_DATA;

                    //        foreach (var kv in dic)
                    //        {
                    //            var entry = new ZipEntry(kv.Key.ToString() + ".jpg");
                    //            entry.DateTime = DateTime.Now;
                    //            zipStream.PutNextEntry(entry);
                    //            zipStream.Write(kv.Value, 0, kv.Value.Length);
                    //        }
                    //    }
                    //}

                    pageOpen(0);

                    this.Enabled = true;
                }
            }
            catch (Exception err)
            {
                this.Enabled = true;
                _pictureBox.Visible = true;
                page_cleanAll();
                MessageBox.Show(err.Message);
            }
        }

        void openFileEBK(string file, int page = 0)
        {
            page_cleanAll();

            _pictureBox.Visible = false;

            DocumentFile = file;
            DocumentName = doc_formatName(Path.GetFileName(DocumentFile));

            using (ZipFile zip = ZipFile.Read(DocumentFile))
            {
                bool hasInfo = false;
                foreach (ZipEntry entry in zip)
                {
                    if (entry.FileName == "info.bin")
                    {
                        hasInfo = true;
                        m_infos.Clear();

                        using (var ms = new MemoryStream())
                        {
                            entry.ExtractWithPassword(ms, PASSWORD);
                            //_bois.Serialize(init, ms);
                            ms.Seek(0, SeekOrigin.Begin);
                            BoisSerializer _bois = new BoisSerializer();
                            m_infos = _bois.Deserialize<Dictionary<int, oPage>>(ms);
                        }
                    }
                    else if (entry.FileName.EndsWith(".jpg"))
                    {
                        int i = int.Parse(entry.FileName.Substring(0, entry.FileName.Length - 4));
                        using (MemoryStream ms = new MemoryStream())
                        {
                            entry.ExtractWithPassword(ms, PASSWORD);
                            m_pages.Add(i, ms.ToArray());
                            if (hasInfo == false) m_infos.Add(i, new oPage() { Id = i });
                        }
                    }
                }
            }

            pageOpen(page);
            _pictureBox.Visible = true;
        }

        void pageOpen(int page)
        {
            if (m_pages.Count > 0
                && m_pages.ContainsKey(page))
            {
                if (IS_SELECTION && _menuAutoCropPageSelected.Checked && m_selections.Count > 0)
                    cropPageUpdate();

                PageNumber = page;

                byte[] buf;
                if (m_page_crops.ContainsKey(page) &&
                    (_menuDisplayPageCropping.Checked || IS_CROP_ENTERING))
                {
                    buf = m_page_crops[page];
                    //_labelPage.BackColor = Color.Orange;
                    IS_CROP_ENTERING = false;
                }
                else
                {
                    buf = m_pages[page];
                    //_labelPage.BackColor = Color.Transparent;
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
                _pictureBox.SizeMode = PictureBoxSizeMode.Normal;
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
            }


            this.Tag = new Size(w0, h0);
            //this.BackColor = Color.Black;
            _pictureBox.Visible = true;
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

        void infoSave()
        {
            using (ZipFile zip = ZipFile.Read(DocumentFile))
            {
                using (var ms = new MemoryStream())
                {
                    BoisSerializer _bois = new BoisSerializer();
                    _bois.Serialize(m_infos, ms);
                    ms.Seek(0, SeekOrigin.Begin);

                    zip.Password = PASSWORD;
                    zip.UpdateEntry("info.bin", ms.ToArray());
                    zip.Save();
                }
            }
        }

        void autoRun_updateIndex()
        {
            if (IS_SELECTION && _pictureBox.Tag != null)
            {
                var ui = (UiSelectRectangle)_pictureBox.Tag;
                var rec = (Rectangle)ui.Tag;

                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "tessdata");
                var tesseract = new TesseractEngine(path, "eng");//eng vie  

                bool hasUpdate = false;

                foreach (var kv in m_pages)
                {
                    int page = kv.Key;
                    Image image = null;
                    string text = string.Empty;
                    oPage info = null;
                    if (m_infos.ContainsKey(page)) info = m_infos[page];

                    using (MemoryStream ms = new MemoryStream(kv.Value, 0, kv.Value.Length))
                        image = Image.FromStream(ms, true);

                    if (image != null)
                    {
                        var buf = getImageSelection(image, rec);
                        if (buf != null && buf.Length > 0)
                        {
                            using (var img = Pix.LoadTiffFromMemory(buf))
                            using (var pro = tesseract.Process(img))
                                text = pro.GetText();
                        }
                    }

                    if (text.Length > 0 && info != null && string.IsNullOrEmpty(info.Title))
                    {
                        info.Title = text;
                        hasUpdate = true;
                    }
                }//end for

                if (hasUpdate)
                    infoSave();
            }
        }

        void page_cleanAll()
        {
            selection_cleanAllCache();

            m_infos.Clear();
            m_pages.Clear();
            m_page_crops.Clear();
        }

        void selection_cleanAllCache()
        {
            m_page_crops.Clear();
            m_selections.Clear();
            _pictureBox.Controls.Clear();
        }

        byte[] getImageSelection(Image image, Rectangle rec)
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
                g.DrawImage(image, new Rectangle(0, 0, wc, hc), cropRec, GraphicsUnit.Pixel);
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

        void closing()
        {
            File.WriteAllText("setting.bin", JsonConvert.SerializeObject(new oSetting(this), Formatting.None));
        }

        #endregion
    }
}
