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
using System.Linq;

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
                    _labelPage.BackColor = Color.OrangeRed;
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
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem _menuIsSelectionMode;
        private ToolStripMenuItem _menuAlwaysOnTop;
        private ToolStripMenuItem _menuPageAttachFiles;
        private Panel _panelPageExtend;
        private ToolStripMenuItem _menuShowPageExtend;
        private TabControl _tabPageExtend;
        private TabPage _tabAttach;
        private TabPage _tabMedia;
        private ToolStrip _mediaPlayer;
        private PictureBox pictureBox1;
        private TextBox textBox1;
        private ToolStripButton _mediaPrev;
        private ToolStripButton _mediaNext;
        private ToolStripButton _mediaPause;
        private ToolStripButton _mediaPlay;
        private ToolStripButton _mediaRecordOn;
        private PictureBox _iconMenu;
        private ToolStripButton _mediaRepeatAll;
        private ToolStripButton _mediaRepeatOne;
        private ToolStripButton _mediaRecordOff;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Panel _panelMediaPlayer;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label6;
        private Label label7;
        private ToolStripButton _pageUnLock;
        private ToolStripButton _pageLock;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripSeparator toolStripSeparator9;
        private TabPage tabPage4;
        private ToolStripMenuItem _menuMain;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._menuMain = new System.Windows.Forms.ToolStripMenuItem();
            this._menuOpen = new System.Windows.Forms.ToolStripMenuItem();
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
            this._menuShowPageExtend = new System.Windows.Forms.ToolStripMenuItem();
            this._menuPageAttachFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
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
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this._menuAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this._menuBackupDocument = new System.Windows.Forms.ToolStripMenuItem();
            this._menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this._menuHr2 = new System.Windows.Forms.ToolStripSeparator();
            this._menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this._labelPage = new System.Windows.Forms.Label();
            this._panelPageExtend = new System.Windows.Forms.Panel();
            this._tabPageExtend = new System.Windows.Forms.TabControl();
            this._tabAttach = new System.Windows.Forms.TabPage();
            this._tabMedia = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this._panelMediaPlayer = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._iconMenu = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._mediaPlayer = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this._mediaNext = new System.Windows.Forms.ToolStripButton();
            this._mediaPause = new System.Windows.Forms.ToolStripButton();
            this._mediaPlay = new System.Windows.Forms.ToolStripButton();
            this._mediaPrev = new System.Windows.Forms.ToolStripButton();
            this._mediaRecordOff = new System.Windows.Forms.ToolStripButton();
            this._mediaRecordOn = new System.Windows.Forms.ToolStripButton();
            this._mediaRepeatAll = new System.Windows.Forms.ToolStripButton();
            this._mediaRepeatOne = new System.Windows.Forms.ToolStripButton();
            this._pageUnLock = new System.Windows.Forms.ToolStripButton();
            this._pageLock = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.label4 = new System.Windows.Forms.Label();
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this._menuStrip.SuspendLayout();
            this._panelPageExtend.SuspendLayout();
            this._tabPageExtend.SuspendLayout();
            this._panelMediaPlayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconMenu)).BeginInit();
            this._mediaPlayer.SuspendLayout();
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
            this._menuStrip.Location = new System.Drawing.Point(325, 16);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Padding = new System.Windows.Forms.Padding(0);
            this._menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this._menuStrip.Size = new System.Drawing.Size(28, 28);
            this._menuStrip.TabIndex = 0;
            // 
            // _menuMain
            // 
            this._menuMain.AutoSize = false;
            this._menuMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuOpen,
            this._menuHr0,
            this._menuIsSelectionMode,
            this._menuSelection,
            this._menuShowPageExtend,
            this._menuPageAttachFiles,
            this.toolStripSeparator7,
            this._menuMedia,
            this._menuGoPage,
            this.searchToolStripMenuItem,
            this.toolStripSeparator4,
            this._menuAlwaysOnTop,
            this._menuBackupDocument,
            this._menuSave,
            this._menuHr2,
            this._menuExit});
            this._menuMain.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._menuMain.ForeColor = System.Drawing.Color.Black;
            this._menuMain.Name = "_menuMain";
            this._menuMain.Padding = new System.Windows.Forms.Padding(0);
            this._menuMain.Size = new System.Drawing.Size(26, 28);
            this._menuMain.Text = "≡";
            this._menuMain.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this._menuMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this._menuMain_MouseDown);
            this._menuMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this._menuMain_MouseUp);
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
            // _menuShowPageExtend
            // 
            this._menuShowPageExtend.Name = "_menuShowPageExtend";
            this._menuShowPageExtend.Size = new System.Drawing.Size(199, 30);
            this._menuShowPageExtend.Text = "Show Page Extend";
            this._menuShowPageExtend.Click += new System.EventHandler(this._menuShowPageExtend_Click);
            // 
            // _menuPageAttachFiles
            // 
            this._menuPageAttachFiles.Name = "_menuPageAttachFiles";
            this._menuPageAttachFiles.Size = new System.Drawing.Size(199, 30);
            this._menuPageAttachFiles.Text = "Attach to Page";
            this._menuPageAttachFiles.Click += new System.EventHandler(this._menuPageAttachFiles_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(196, 6);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(196, 6);
            // 
            // _menuAlwaysOnTop
            // 
            this._menuAlwaysOnTop.Name = "_menuAlwaysOnTop";
            this._menuAlwaysOnTop.Size = new System.Drawing.Size(199, 30);
            this._menuAlwaysOnTop.Text = "Always On Top";
            this._menuAlwaysOnTop.Click += new System.EventHandler(this._menuAlwaysOnTop_Click);
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
            this._labelPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._labelPage.BackColor = System.Drawing.Color.Transparent;
            this._labelPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelPage.ForeColor = System.Drawing.Color.Silver;
            this._labelPage.Location = new System.Drawing.Point(0, 549);
            this._labelPage.Name = "_labelPage";
            this._labelPage.Size = new System.Drawing.Size(35, 14);
            this._labelPage.TabIndex = 3;
            this._labelPage.Text = "0";
            this._labelPage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _panelPageExtend
            // 
            this._panelPageExtend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._panelPageExtend.BackColor = System.Drawing.Color.White;
            this._panelPageExtend.Controls.Add(this._tabPageExtend);
            this._panelPageExtend.Controls.Add(this._panelMediaPlayer);
            this._panelPageExtend.Location = new System.Drawing.Point(428, -1);
            this._panelPageExtend.Margin = new System.Windows.Forms.Padding(0);
            this._panelPageExtend.Name = "_panelPageExtend";
            this._panelPageExtend.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this._panelPageExtend.Size = new System.Drawing.Size(269, 564);
            this._panelPageExtend.TabIndex = 7;
            // 
            // _tabPageExtend
            // 
            this._tabPageExtend.Controls.Add(this._tabAttach);
            this._tabPageExtend.Controls.Add(this._tabMedia);
            this._tabPageExtend.Controls.Add(this.tabPage1);
            this._tabPageExtend.Controls.Add(this.tabPage2);
            this._tabPageExtend.Controls.Add(this.tabPage3);
            this._tabPageExtend.Controls.Add(this.tabPage4);
            this._tabPageExtend.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabPageExtend.Location = new System.Drawing.Point(0, 143);
            this._tabPageExtend.Margin = new System.Windows.Forms.Padding(0);
            this._tabPageExtend.Multiline = true;
            this._tabPageExtend.Name = "_tabPageExtend";
            this._tabPageExtend.Padding = new System.Drawing.Point(0, 0);
            this._tabPageExtend.SelectedIndex = 0;
            this._tabPageExtend.Size = new System.Drawing.Size(269, 421);
            this._tabPageExtend.TabIndex = 0;
            // 
            // _tabAttach
            // 
            this._tabAttach.BackColor = System.Drawing.Color.White;
            this._tabAttach.Location = new System.Drawing.Point(4, 22);
            this._tabAttach.Margin = new System.Windows.Forms.Padding(0);
            this._tabAttach.Name = "_tabAttach";
            this._tabAttach.Size = new System.Drawing.Size(261, 395);
            this._tabAttach.TabIndex = 0;
            this._tabAttach.Text = "Attach";
            // 
            // _tabMedia
            // 
            this._tabMedia.BackColor = System.Drawing.Color.White;
            this._tabMedia.Location = new System.Drawing.Point(4, 22);
            this._tabMedia.Name = "_tabMedia";
            this._tabMedia.Padding = new System.Windows.Forms.Padding(3);
            this._tabMedia.Size = new System.Drawing.Size(261, 395);
            this._tabMedia.TabIndex = 1;
            this._tabMedia.Text = "Media";
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(261, 395);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "List";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(261, 395);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Note";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(261, 395);
            this.tabPage3.TabIndex = 4;
            this.tabPage3.Text = "Record";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(261, 395);
            this.tabPage4.TabIndex = 5;
            this.tabPage4.Text = "Task";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // _panelMediaPlayer
            // 
            this._panelMediaPlayer.Controls.Add(this.pictureBox1);
            this._panelMediaPlayer.Controls.Add(this._iconMenu);
            this._panelMediaPlayer.Controls.Add(this.textBox1);
            this._panelMediaPlayer.Controls.Add(this.label7);
            this._panelMediaPlayer.Controls.Add(this.label6);
            this._panelMediaPlayer.Controls.Add(this.label2);
            this._panelMediaPlayer.Controls.Add(this.label3);
            this._panelMediaPlayer.Controls.Add(this.label5);
            this._panelMediaPlayer.Controls.Add(this._mediaPlayer);
            this._panelMediaPlayer.Controls.Add(this.label4);
            this._panelMediaPlayer.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelMediaPlayer.Location = new System.Drawing.Point(0, 3);
            this._panelMediaPlayer.Name = "_panelMediaPlayer";
            this._panelMediaPlayer.Size = new System.Drawing.Size(269, 140);
            this._panelMediaPlayer.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::EBook.Properties.Resources.search_small;
            this.pictureBox1.Location = new System.Drawing.Point(190, 108);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 18);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // _iconMenu
            // 
            this._iconMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._iconMenu.BackColor = System.Drawing.Color.White;
            this._iconMenu.Image = global::EBook.Properties.Resources.app_small;
            this._iconMenu.Location = new System.Drawing.Point(240, -3);
            this._iconMenu.Name = "_iconMenu";
            this._iconMenu.Size = new System.Drawing.Size(32, 43);
            this._iconMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._iconMenu.TabIndex = 3;
            this._iconMenu.TabStop = false;
            this._iconMenu.Click += new System.EventHandler(this._iconMenu_Click);
            this._iconMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this._iconMenu_MouseDown);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(50, 107);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(160, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.OrangeRed;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Linen;
            this.label7.Location = new System.Drawing.Point(125, 47);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.label7.Size = new System.Drawing.Size(35, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "00:15";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.OrangeRed;
            this.label6.Location = new System.Drawing.Point(43, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 5);
            this.label6.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(43, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 5);
            this.label2.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(257, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "How are you today?";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(235, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "00:00";
            // 
            // _mediaPlayer
            // 
            this._mediaPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._mediaPlayer.AutoSize = false;
            this._mediaPlayer.BackColor = System.Drawing.Color.Transparent;
            this._mediaPlayer.Dock = System.Windows.Forms.DockStyle.None;
            this._mediaPlayer.GripMargin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this._mediaPlayer.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._mediaPlayer.ImageScalingSize = new System.Drawing.Size(32, 32);
            this._mediaPlayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator8,
            this._mediaNext,
            this._mediaPause,
            this._mediaPlay,
            this._mediaPrev,
            this._mediaRecordOff,
            this._mediaRecordOn,
            this._mediaRepeatAll,
            this._mediaRepeatOne,
            this._pageUnLock,
            this._pageLock,
            this.toolStripSeparator9});
            this._mediaPlayer.Location = new System.Drawing.Point(-65, -5);
            this._mediaPlayer.Name = "_mediaPlayer";
            this._mediaPlayer.Size = new System.Drawing.Size(313, 43);
            this._mediaPlayer.TabIndex = 4;
            this._mediaPlayer.Text = "toolStrip1";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 43);
            // 
            // _mediaNext
            // 
            this._mediaNext.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._mediaNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._mediaNext.Image = global::EBook.Properties.Resources.next_small;
            this._mediaNext.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._mediaNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mediaNext.Name = "_mediaNext";
            this._mediaNext.Size = new System.Drawing.Size(23, 40);
            this._mediaNext.Text = "toolStripButton3";
            this._mediaNext.Click += new System.EventHandler(this._mediaNext_Click);
            // 
            // _mediaPause
            // 
            this._mediaPause.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._mediaPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._mediaPause.Image = global::EBook.Properties.Resources.pause_;
            this._mediaPause.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._mediaPause.ImageTransparentColor = System.Drawing.Color.Transparent;
            this._mediaPause.Name = "_mediaPause";
            this._mediaPause.Size = new System.Drawing.Size(40, 40);
            this._mediaPause.Text = "toolStripButton2";
            this._mediaPause.Click += new System.EventHandler(this._mediaPause_Click);
            // 
            // _mediaPlay
            // 
            this._mediaPlay.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._mediaPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._mediaPlay.Image = global::EBook.Properties.Resources.play_arrow;
            this._mediaPlay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._mediaPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mediaPlay.Name = "_mediaPlay";
            this._mediaPlay.Size = new System.Drawing.Size(40, 40);
            this._mediaPlay.Text = "toolStripButton4";
            this._mediaPlay.Click += new System.EventHandler(this._mediaPlay_Click);
            // 
            // _mediaPrev
            // 
            this._mediaPrev.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._mediaPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._mediaPrev.Image = global::EBook.Properties.Resources.prev_small;
            this._mediaPrev.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._mediaPrev.ImageTransparentColor = System.Drawing.Color.Transparent;
            this._mediaPrev.Name = "_mediaPrev";
            this._mediaPrev.Size = new System.Drawing.Size(23, 40);
            this._mediaPrev.Text = "toolStripButton1";
            this._mediaPrev.Click += new System.EventHandler(this._mediaPrev_Click);
            // 
            // _mediaRecordOff
            // 
            this._mediaRecordOff.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._mediaRecordOff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._mediaRecordOff.Image = global::EBook.Properties.Resources.record_off_small;
            this._mediaRecordOff.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._mediaRecordOff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mediaRecordOff.Name = "_mediaRecordOff";
            this._mediaRecordOff.Size = new System.Drawing.Size(23, 40);
            this._mediaRecordOff.Text = "toolStripButton1";
            this._mediaRecordOff.Click += new System.EventHandler(this._mediaRecordOff_Click);
            // 
            // _mediaRecordOn
            // 
            this._mediaRecordOn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._mediaRecordOn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._mediaRecordOn.Image = global::EBook.Properties.Resources.record_on_small;
            this._mediaRecordOn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._mediaRecordOn.ImageTransparentColor = System.Drawing.Color.Transparent;
            this._mediaRecordOn.Name = "_mediaRecordOn";
            this._mediaRecordOn.Size = new System.Drawing.Size(23, 40);
            this._mediaRecordOn.Text = "toolStripButton5";
            this._mediaRecordOn.Click += new System.EventHandler(this._mediaRecordOn_Click);
            // 
            // _mediaRepeatAll
            // 
            this._mediaRepeatAll.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._mediaRepeatAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._mediaRepeatAll.Image = global::EBook.Properties.Resources.repeat_all_small;
            this._mediaRepeatAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._mediaRepeatAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mediaRepeatAll.Name = "_mediaRepeatAll";
            this._mediaRepeatAll.Size = new System.Drawing.Size(23, 40);
            this._mediaRepeatAll.Text = "toolStripButton4";
            this._mediaRepeatAll.Click += new System.EventHandler(this._mediaRepeatAll_Click);
            // 
            // _mediaRepeatOne
            // 
            this._mediaRepeatOne.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._mediaRepeatOne.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._mediaRepeatOne.Image = global::EBook.Properties.Resources.repeat_one_small;
            this._mediaRepeatOne.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._mediaRepeatOne.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mediaRepeatOne.Name = "_mediaRepeatOne";
            this._mediaRepeatOne.Size = new System.Drawing.Size(23, 40);
            this._mediaRepeatOne.Text = "toolStripButton5";
            this._mediaRepeatOne.Click += new System.EventHandler(this._mediaRepeatOne_Click);
            // 
            // _pageUnLock
            // 
            this._pageUnLock.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._pageUnLock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._pageUnLock.Image = global::EBook.Properties.Resources.unlock_small;
            this._pageUnLock.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._pageUnLock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._pageUnLock.Name = "_pageUnLock";
            this._pageUnLock.Size = new System.Drawing.Size(23, 40);
            this._pageUnLock.Text = "toolStripButton1";
            this._pageUnLock.Click += new System.EventHandler(this._pageUnLock_Click);
            // 
            // _pageLock
            // 
            this._pageLock.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._pageLock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._pageLock.Image = global::EBook.Properties.Resources.lock_small;
            this._pageLock.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._pageLock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._pageLock.Name = "_pageLock";
            this._pageLock.Size = new System.Drawing.Size(23, 40);
            this._pageLock.Text = "toolStripButton2";
            this._pageLock.Click += new System.EventHandler(this._pageLock_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 43);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "00:00";
            // 
            // _pictureBox
            // 
            this._pictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._pictureBox.BackColor = System.Drawing.Color.White;
            this._pictureBox.Location = new System.Drawing.Point(3, 2);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(19, 23);
            this._pictureBox.TabIndex = 4;
            this._pictureBox.TabStop = false;
            this._pictureBox.Visible = false;
            this._pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this._pictureBox_MouseDown);
            this._pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this._pictureBox_MouseMove);
            this._pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this._pictureBox_MouseUp);
            // 
            // fMain
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(696, 563);
            this.Controls.Add(this._labelPage);
            this.Controls.Add(this._panelPageExtend);
            this.Controls.Add(this._menuStrip);
            this.Controls.Add(this._pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._menuStrip;
            this.Name = "fMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMain_FormClosing);
            this.Load += new System.EventHandler(this.fMain_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fMain_MouseClick);
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this._panelPageExtend.ResumeLayout(false);
            this._tabPageExtend.ResumeLayout(false);
            this._panelMediaPlayer.ResumeLayout(false);
            this._panelMediaPlayer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconMenu)).EndInit();
            this._mediaPlayer.ResumeLayout(false);
            this._mediaPlayer.PerformLayout();
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
            menuHide();
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
            menuHide();
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
                var sel = (UiSelectRectangle)_pictureBox.Tag;
                if (w > 18 && h > 9)
                {
                    sel.Tag = rec;
                    sel.Width = w;
                    sel.Height = h;
                    //sel.SetOpacity(50);
                    m_selections.Add(sel);
                    IS_SELECTING = true;
                }
                else
                {
                    _pictureBox.Controls.Remove(sel);
                    _pictureBox.Tag = null;
                    if (w > 3 || h > 3) IS_SELECTING = true;
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

        #region [ MENU | BUTTON ]

        private void _buttonClosePageExtend_Click(object sender, EventArgs e)
        {
            pageExtendHide();
        }

        private void _menuAlwaysOnTop_Click(object sender, EventArgs e)
        {
            menuHide();
            if (_menuAlwaysOnTop.Checked)
            {
                _menuAlwaysOnTop.Checked = false;
                this.TopMost = false;
            }
            else
            {
                _menuAlwaysOnTop.Checked = true;
                this.TopMost = true;
            }
        }

        private void _menuPageAttachFiles_Click(object sender, EventArgs e)
        {
            menuHide();
            attachFilesToPage();
        }

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

        private void _menuGoPage_Click(object sender, EventArgs e)
        {
            menuHide();

            if (!string.IsNullOrEmpty(this.DocumentFile))
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Go to page", "Input number page you want to go?", string.Empty);
                int page = 0;
                int.TryParse(input, out page);
                if (page > 0)
                {
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
                openFileDialog.Filter = "EBook Files (*.ebk)|*.ebk|PDF Files (*.pdf)|*.pdf";
                //openFileDialog.FilterIndex = 2;
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

        private void _menuMain_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void _menuMain_MouseUp(object sender, MouseEventArgs e)
        {
            menuHide();
        }

        #endregion

        #region [ FUNCTIONS ]

        TesseractEngine m_tesseract;
        void app_Load()
        {
            string traineddata = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "tessdata");
            Environment.SetEnvironmentVariable("TESSDATA_PREFIX", traineddata);
            m_tesseract = new TesseractEngine(traineddata, "eng");//eng vie

            menuHide();

            this.BackgroundImageLayout = ImageLayout.Stretch;
            _panelPageExtend.Visible = false;
            _mediaPause.Visible = false;
            _mediaRepeatOne.Visible = false;
            _mediaRecordOn.Visible = false;
            _pageLock.Visible = false;


            this.Width = 0;
            this.KeyPreview = true;
            this.KeyUp += form_KeyUp;
            this.Shown += (se, ev) =>
            {
                _pictureBox.Location = new Point(0, 0);
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

                IS_SELECTION = setting.IS_SELECTION;
                _panelPageExtend.Visible = setting.SHOW_PAGE_INFO;

                //if (setting.DocumentFile.Length > 0)
                //    openFileEBK(setting.DocumentFile, setting.PageNumber);
                //else this.Width = 235;

                if (!string.IsNullOrEmpty(setting.ImageBase64))
                {
                    var img = Image.FromStream(new MemoryStream(Convert.FromBase64String(setting.ImageBase64)));
                    this.Width = img.Width;
                    this.BackgroundImage = img;
                }

                //if (_menuAutoCropPageSelected.Checked) _menuMain.BackColor = Color.Red;
            };
        }

        private bool IS_CROP_ENTERING = false;
        void app_keyPress(KeyEventArgs ev)
        {

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
                    if (_menuStrip.Visible)
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

        void app_click(MouseEventArgs ev)
        {
            MouseEventArgs e = (MouseEventArgs)ev;
            if (e.Button == MouseButtons.Right)
            {
                menuShow(e.X - 9, e.Y - 9);
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

        void menuShow(int x, int y)
        {
            _menuStrip.Location = new Point(x, y);
            _menuStrip.Visible = true;
            _menuMain.ShowDropDown();
        }

        void menuHide()
        {
            if (_menuStrip.Visible)
            {
                _menuMain.HideDropDown();
                _menuStrip.Visible = false;
            }
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

            //_pictureBox.Visible = false;

            DocumentFile = file;
            DocumentName = doc_formatName(Path.GetFileName(DocumentFile));

            bool hasUpdate = false;
            m_infos = new Dictionary<int, oPage>();
            using (ZipFile zip = ZipFile.Read(DocumentFile))
            {
                var info = zip.Entries.Where(x => x.FileName == "info.bin").Take(1).SingleOrDefault();
                if (info != null)
                {
                    try
                    {
                        using (var ms = new MemoryStream())
                        {
                            info.ExtractWithPassword(ms, PASSWORD);
                            ms.Seek(0, SeekOrigin.Begin);
                            //string json = Encoding.UTF8.GetString(ms.ToArray());
                            //m_infos = JsonConvert.DeserializeObject<Dictionary<int, oPage>>(json);

                            using (var sr = new StreamReader(ms))
                            using (var reader = new JsonTextReader(sr))
                            {
                                if (!reader.Read() || reader.TokenType != JsonToken.StartArray)
                                {
                                    //throw new Exception("Expected start of array");
                                }
                                else
                                {
                                    var ser = new JsonSerializer();
                                    while (reader.Read())
                                    {
                                        if (reader.TokenType == JsonToken.EndArray) break;
                                        var item = ser.Deserialize<oPage>(reader);
                                        if (item != null && m_infos.ContainsKey(item.Id) == false)
                                            m_infos.Add(item.Id, item);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        hasUpdate = true;
                    }
                }

                //m_infos.Clear();

                foreach (ZipEntry entry in zip)
                {
                    if (entry.FileName.EndsWith(".jpg"))
                    {
                        int i = int.Parse(entry.FileName.Substring(0, entry.FileName.Length - 4));

                        using (MemoryStream ms = new MemoryStream())
                        {
                            entry.ExtractWithPassword(ms, PASSWORD);
                            var buf = ms.ToArray();
                            m_pages.Add(i, buf);

                            Bitmap img = new Bitmap(ms);
                            //using (var tms = new MemoryStream())
                            //{
                            //    img.Save(tms, System.Drawing.Imaging.ImageFormat.Tiff);
                            //    tiffs = tms.ToArray();
                            //}

                            oPage p;
                            if (m_infos.ContainsKey(i)) p = m_infos[i];
                            else
                            {
                                p = new oPage() { Id = i, Width = img.Width, Height = img.Height };
                                m_infos.Add(i, p);

                                try
                                {
                                    string text = string.Empty;

                                    //using (var pix = Pix.LoadTiffFromMemory(tiffs))
                                    //using (var pro = m_tesseract.Process(pix))
                                    //    text = pro.GetText();

                                    //var bitmapConverter = new BitmapToPixConverter();
                                    //using (var pix = bitmapConverter.Convert(img))
                                    //using (var pro = m_tesseract.Process(pix))
                                    //    text = pro.GetText();

                                    p.TextAI.Ok = true;
                                    p.TextAI.TextEn = text;
                                }
                                catch (Exception et)
                                {
                                }
                                hasUpdate = true;
                            }

                        }
                    }
                }

                if (hasUpdate)
                {
                    //if (info != null)
                    //{
                    //    zip.RemoveEntry(info);
                    //    zip.Save();
                    //}

                    if (m_infos.Count > 0)
                    {
                        zip.Password = PASSWORD;
                        using (var ms = new MemoryStream())
                        {
                            using (var sw = new StreamWriter(ms))
                            using (var jw = new JsonTextWriter(sw))
                            {
                                //jw.Formatting = Formatting.Indented;
                                jw.WriteStartArray();
                                var ser = new JsonSerializer();
                                foreach (var kv in m_infos)
                                {
                                    ser.Serialize(jw, kv.Value);
                                    jw.Flush();
                                }
                                jw.WriteEndArray();
                            }
                            var buf = ms.ToArray();

                            //var zn = zip.AddEntry("info.bin", buf);
                            //zip.Save();

                            zip.UpdateEntry("info.bin", buf);
                            zip.Save();
                        }
                    }
                }
            }

            pageOpen(page);
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
                    IS_CROP_ENTERING = false;
                }
                else
                {
                    buf = m_pages[page];
                }

                //Image img = null;
                //using (MemoryStream ms = new MemoryStream(buf, 0, buf.Length))
                //{
                //    ms.Write(buf, 0, buf.Length);
                //    img = Image.FromStream(ms, true);
                //}

                if (buf != null)
                {
                    if (_menuKeepSelectChangePage.Checked == false)
                    {
                        m_selections.Clear();
                        _pictureBox.Controls.Clear();
                    }

                    int pageExtendWidth = 0, w = 0, h = 0;
                    if (_panelPageExtend.Visible) pageExtendWidth = _panelPageExtend.Width;

                    var img = Bitmap.FromStream(new MemoryStream(buf));

                    if (img.Height > this.Height) {
                        h = this.Height;
                        w = h * img.Width / img.Height;
                    }

                    this.Width = w + pageExtendWidth;
                    this.BackgroundImage = img;

                    ////int w = 0, h = 0, w0 = 0, h0 = 0, _top = 0;
                    //////int w0 = img.Width, h0 = img.Height,
                    ////int pageExtendWidth = 0;

                    //////if (_panelPageExtend.Visible) pageExtendWidth = _panelPageExtend.Width;

                    //////if (w0 < Screen.PrimaryScreen.WorkingArea.Width && h0 < this.Height)
                    //////{
                    //////    w = w0;
                    //////    this.Width = w + pageExtendWidth;

                    //////    h = h0;
                    //////    //////_top = (this.Height - h) / 2;

                    //////    //////_pictureBox.Width = w;
                    //////    //////_pictureBox.Height = h;
                    //////    //////_pictureBox.Location = new Point(0, _top);

                    //////    //_pictureBox.Image = img;
                    //////    //_pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

                    //////    this.BackgroundImage = Bitmap.FromStream(new MemoryStream(buf));
                    //////}
                    //////else
                    //////{
                    //////    ////_top = 3;
                    //////    ////int _bottom = 7, _left = 3, _right = 3;

                    //////    ////h = this.Height - (_top + _bottom);
                    //////    ////w = (h * w0 / h0);

                    //////    ////this.Width = w + _left + _right;

                    //////    ////_pictureBox.Anchor = AnchorStyles.None;
                    //////    ////_pictureBox.Width = w;
                    //////    ////_pictureBox.Height = h;
                    //////    ////_pictureBox.Location = new Point(_left, _top);
                    //////    ////_pictureBox.Image = img;
                    //////    ////_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    //////    this.BackgroundImage = Bitmap.FromStream(new MemoryStream(buf));
                    //////}


                    //////this.Tag = new Size(w0, h0);
                    ////////this.BackColor = Color.Black;
                    //////_pictureBox.Visible = true;
                }

                //string spage = (page + 1).ToString();
                //_labelPage.Text = spage;
                //_labelPage.Left = _pictureBox.Width - 28;

                //this.Text = string.Format("{0}.{1}", spage, DocumentName);

                //if (_panelPageExtend.Visible) pageExtendShow();
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

        void attachFilesToPage()
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = PATH_DATA;
                openFileDialog.Filter = "MP3 Files (*.mp3)|*.mp3|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                //openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var files = openFileDialog.FileNames;
                    if (files.Length > 0)
                    {

                    }
                }
            }
        }

        private void _menuShowPageExtend_Click(object sender, EventArgs e)
        {
            menuHide();

            if (_menuShowPageExtend.Checked)
            {
                pageExtendHide();
            }
            else
            {
                pageExtendShow();
            }
        }

        private void _iconMenu_MouseDown(object sender, MouseEventArgs e)
        {
            if (_menuStrip.Visible) menuHide();
            else
            {
                int x = this.Width - 24;
                menuShow(x, 1);
            }
        }

        private void _mediaPrev_Click(object sender, EventArgs e)
        {

        }

        private void _mediaPause_Click(object sender, EventArgs e)
        {
            mediaPlayOrPause(0);
        }

        private void _mediaPlay_Click(object sender, EventArgs e)
        {
            mediaPlayOrPause(1);
        }

        private void _mediaNext_Click(object sender, EventArgs e)
        {

        }

        private void _mediaRecord_Click(object sender, EventArgs e)
        {

        }

        private void _mediaRepeatOne_Click(object sender, EventArgs e)
        {
            mediaRepeatAllOrOne(0);
        }

        void mediaPlayOrPause(int type)
        {
            // Play click
            if (type == 1)
            {
                _mediaPlay.Visible = false;
                _mediaPause.Visible = true;
            }
            else
            {
                // Pause click
                _mediaPlay.Visible = true;
                _mediaPause.Visible = false;

            }
        }
        void mediaRepeatAllOrOne(int type)
        {

            // All click
            if (type == 1)
            {
                _mediaRepeatOne.Visible = true;
                _mediaRepeatAll.Visible = false;
            }
            else
            {
                // One click
                _mediaRepeatOne.Visible = false;
                _mediaRepeatAll.Visible = true;
            }
        }

        private void _mediaRepeatAll_Click(object sender, EventArgs e)
        {
            mediaRepeatAllOrOne(1);
        }

        private void _mediaRecordOn_Click(object sender, EventArgs e)
        {

        }

        private void _pageLock_Click(object sender, EventArgs e)
        {

        }

        private void _pageUnLock_Click(object sender, EventArgs e)
        {

        }

        private void _mediaRecordOff_Click(object sender, EventArgs e)
        {

        }

        private void _iconMenu_Click(object sender, EventArgs e)
        {

        }

        void pageExtendShow()
        {
            _menuShowPageExtend.Checked = true;
            int w = this.Width;
            this.Width = w + _panelPageExtend.Width;
            _pictureBox.Location = new Point(0, 0);
            _panelPageExtend.Visible = true;
        }

        void pageExtendHide()
        {
            _menuShowPageExtend.Checked = false;
            _panelPageExtend.Visible = false;
            this.Width = _pictureBox.Width;
            _pictureBox.Location = new Point(0, 0);
        }

        void closing()
        {
            if (m_pages.Count > 0)
            {
                var o = new oSetting(this);
                o.SHOW_PAGE_INFO = _panelPageExtend.Visible;
                var buf = m_pages[PageNumber];
                o.ImageBase64 = Convert.ToBase64String(buf);
                File.WriteAllText("setting.bin", JsonConvert.SerializeObject(o, Formatting.None));
            }
        }

        #endregion
    }
}
