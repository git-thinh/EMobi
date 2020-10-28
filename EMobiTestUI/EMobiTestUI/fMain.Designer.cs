namespace EMobiTestUI
{
    partial class fMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this._buttonSelection = new System.Windows.Forms.Button();
            this._buttonMini = new System.Windows.Forms.Button();
            this._buttonClose = new System.Windows.Forms.Button();
            this._panelLeft = new System.Windows.Forms.FlowLayoutPanel();
            this._panelBody = new System.Windows.Forms.Panel();
            this._labelPageNumber = new System.Windows.Forms.Label();
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this._splitterLeft = new System.Windows.Forms.Splitter();
            this._splitterRight = new System.Windows.Forms.Splitter();
            this._panelRight = new System.Windows.Forms.Panel();
            this._buttonNext = new System.Windows.Forms.Button();
            this._buttonPrev = new System.Windows.Forms.Button();
            this._menu = new System.Windows.Forms.MenuStrip();
            this._menuMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._menuDocNew = new System.Windows.Forms.ToolStripMenuItem();
            this._menuPageNew = new System.Windows.Forms.ToolStripMenuItem();
            this._menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this._menuAttach = new System.Windows.Forms.ToolStripMenuItem();
            this._menuMedia = new System.Windows.Forms.ToolStripMenuItem();
            this._buttonView = new System.Windows.Forms.Button();
            this._buttonPageOpen = new System.Windows.Forms.Button();
            this._buttonOpen = new System.Windows.Forms.Button();
            this._buttonSave = new System.Windows.Forms.Button();
            this._buttonClean = new System.Windows.Forms.Button();
            this._buttonReIndex = new System.Windows.Forms.Button();
            this._panelBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this._panelRight.SuspendLayout();
            this._menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonSelection
            // 
            this._buttonSelection.BackColor = System.Drawing.SystemColors.Control;
            this._buttonSelection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._buttonSelection.ForeColor = System.Drawing.Color.Black;
            this._buttonSelection.Location = new System.Drawing.Point(-1, 51);
            this._buttonSelection.Name = "_buttonSelection";
            this._buttonSelection.Size = new System.Drawing.Size(48, 23);
            this._buttonSelection.TabIndex = 5;
            this._buttonSelection.Text = "Select";
            this._buttonSelection.UseVisualStyleBackColor = false;
            this._buttonSelection.Click += new System.EventHandler(this._buttonSelection_Click);
            // 
            // _buttonMini
            // 
            this._buttonMini.BackColor = System.Drawing.Color.Blue;
            this._buttonMini.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._buttonMini.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this._buttonMini.Location = new System.Drawing.Point(0, 0);
            this._buttonMini.Name = "_buttonMini";
            this._buttonMini.Size = new System.Drawing.Size(23, 24);
            this._buttonMini.TabIndex = 8;
            this._buttonMini.Text = "-";
            this._buttonMini.UseVisualStyleBackColor = false;
            this._buttonMini.Click += new System.EventHandler(this._buttonMini_Click);
            // 
            // _buttonClose
            // 
            this._buttonClose.BackColor = System.Drawing.Color.Red;
            this._buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._buttonClose.Location = new System.Drawing.Point(23, 2);
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Size = new System.Drawing.Size(24, 23);
            this._buttonClose.TabIndex = 0;
            this._buttonClose.Text = "x";
            this._buttonClose.UseVisualStyleBackColor = false;
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _panelLeft
            // 
            this._panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this._panelLeft.Location = new System.Drawing.Point(0, 0);
            this._panelLeft.Name = "_panelLeft";
            this._panelLeft.Size = new System.Drawing.Size(200, 805);
            this._panelLeft.TabIndex = 0;
            // 
            // _panelBody
            // 
            this._panelBody.Controls.Add(this._labelPageNumber);
            this._panelBody.Controls.Add(this._pictureBox);
            this._panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelBody.Location = new System.Drawing.Point(200, 0);
            this._panelBody.Name = "_panelBody";
            this._panelBody.Size = new System.Drawing.Size(705, 805);
            this._panelBody.TabIndex = 2;
            // 
            // _labelPageNumber
            // 
            this._labelPageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._labelPageNumber.BackColor = System.Drawing.SystemColors.ScrollBar;
            this._labelPageNumber.Location = new System.Drawing.Point(3, 780);
            this._labelPageNumber.Name = "_labelPageNumber";
            this._labelPageNumber.Size = new System.Drawing.Size(45, 24);
            this._labelPageNumber.TabIndex = 1;
            this._labelPageNumber.Text = "1";
            this._labelPageNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _pictureBox
            // 
            this._pictureBox.BackColor = System.Drawing.Color.Black;
            this._pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this._pictureBox.Location = new System.Drawing.Point(3, 1);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(264, 225);
            this._pictureBox.TabIndex = 0;
            this._pictureBox.TabStop = false;
            this._pictureBox.DoubleClick += new System.EventHandler(this._pictureBox_DoubleClick);
            this._pictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._pictureBox_MouseDoubleClick);
            this._pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this._pictureBox_MouseDown);
            this._pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this._pictureBox_MouseMove);
            this._pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this._pictureBox_MouseUp);
            // 
            // _splitterLeft
            // 
            this._splitterLeft.BackColor = System.Drawing.Color.Black;
            this._splitterLeft.Location = new System.Drawing.Point(200, 0);
            this._splitterLeft.Name = "_splitterLeft";
            this._splitterLeft.Size = new System.Drawing.Size(3, 805);
            this._splitterLeft.TabIndex = 3;
            this._splitterLeft.TabStop = false;
            // 
            // _splitterRight
            // 
            this._splitterRight.BackColor = System.Drawing.Color.Black;
            this._splitterRight.Dock = System.Windows.Forms.DockStyle.Right;
            this._splitterRight.Location = new System.Drawing.Point(902, 0);
            this._splitterRight.Name = "_splitterRight";
            this._splitterRight.Size = new System.Drawing.Size(3, 805);
            this._splitterRight.TabIndex = 4;
            this._splitterRight.TabStop = false;
            // 
            // _panelRight
            // 
            this._panelRight.BackColor = System.Drawing.Color.Black;
            this._panelRight.Controls.Add(this._buttonNext);
            this._panelRight.Controls.Add(this._buttonPrev);
            this._panelRight.Controls.Add(this._menu);
            this._panelRight.Controls.Add(this._buttonView);
            this._panelRight.Controls.Add(this._buttonPageOpen);
            this._panelRight.Controls.Add(this._buttonOpen);
            this._panelRight.Controls.Add(this._buttonSave);
            this._panelRight.Controls.Add(this._buttonClean);
            this._panelRight.Controls.Add(this._buttonReIndex);
            this._panelRight.Controls.Add(this._buttonSelection);
            this._panelRight.Controls.Add(this._buttonClose);
            this._panelRight.Controls.Add(this._buttonMini);
            this._panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this._panelRight.Location = new System.Drawing.Point(905, 0);
            this._panelRight.Name = "_panelRight";
            this._panelRight.Size = new System.Drawing.Size(45, 805);
            this._panelRight.TabIndex = 5;
            // 
            // _buttonNext
            // 
            this._buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonNext.BackColor = System.Drawing.SystemColors.Control;
            this._buttonNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._buttonNext.ForeColor = System.Drawing.Color.Black;
            this._buttonNext.Location = new System.Drawing.Point(23, 758);
            this._buttonNext.Name = "_buttonNext";
            this._buttonNext.Size = new System.Drawing.Size(22, 23);
            this._buttonNext.TabIndex = 16;
            this._buttonNext.Text = ">";
            this._buttonNext.UseVisualStyleBackColor = false;
            this._buttonNext.Click += new System.EventHandler(this._buttonNext_Click);
            // 
            // _buttonPrev
            // 
            this._buttonPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonPrev.BackColor = System.Drawing.SystemColors.Control;
            this._buttonPrev.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._buttonPrev.ForeColor = System.Drawing.Color.Black;
            this._buttonPrev.Location = new System.Drawing.Point(-2, 758);
            this._buttonPrev.Name = "_buttonPrev";
            this._buttonPrev.Size = new System.Drawing.Size(23, 23);
            this._buttonPrev.TabIndex = 15;
            this._buttonPrev.Text = "<";
            this._buttonPrev.UseVisualStyleBackColor = false;
            this._buttonPrev.Click += new System.EventHandler(this._buttonPrev_Click);
            // 
            // _menu
            // 
            this._menu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._menu.Dock = System.Windows.Forms.DockStyle.None;
            this._menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuMenu});
            this._menu.Location = new System.Drawing.Point(-10, 784);
            this._menu.Name = "_menu";
            this._menu.Size = new System.Drawing.Size(58, 24);
            this._menu.TabIndex = 17;
            this._menu.Text = "menuStrip1";
            // 
            // _menuMenu
            // 
            this._menuMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuDocNew,
            this._menuPageNew,
            this._menuOpen,
            this._menuAttach,
            this._menuMedia});
            this._menuMenu.Name = "_menuMenu";
            this._menuMenu.Size = new System.Drawing.Size(50, 20);
            this._menuMenu.Text = "&Menu";
            // 
            // _menuDocNew
            // 
            this._menuDocNew.Name = "_menuDocNew";
            this._menuDocNew.Size = new System.Drawing.Size(157, 22);
            this._menuDocNew.Text = "&Document New";
            // 
            // _menuPageNew
            // 
            this._menuPageNew.Name = "_menuPageNew";
            this._menuPageNew.Size = new System.Drawing.Size(157, 22);
            this._menuPageNew.Text = "&Page New";
            // 
            // _menuOpen
            // 
            this._menuOpen.Name = "_menuOpen";
            this._menuOpen.Size = new System.Drawing.Size(157, 22);
            this._menuOpen.Text = "&Open";
            // 
            // _menuAttach
            // 
            this._menuAttach.Name = "_menuAttach";
            this._menuAttach.Size = new System.Drawing.Size(157, 22);
            this._menuAttach.Text = "&Attach";
            // 
            // _menuMedia
            // 
            this._menuMedia.Name = "_menuMedia";
            this._menuMedia.Size = new System.Drawing.Size(157, 22);
            this._menuMedia.Text = "&Media";
            // 
            // _buttonView
            // 
            this._buttonView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonView.BackColor = System.Drawing.SystemColors.Control;
            this._buttonView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._buttonView.ForeColor = System.Drawing.Color.Black;
            this._buttonView.Location = new System.Drawing.Point(-2, 733);
            this._buttonView.Name = "_buttonView";
            this._buttonView.Size = new System.Drawing.Size(49, 23);
            this._buttonView.TabIndex = 14;
            this._buttonView.Text = "View";
            this._buttonView.UseVisualStyleBackColor = false;
            this._buttonView.Click += new System.EventHandler(this._buttonView_Click);
            // 
            // _buttonPageOpen
            // 
            this._buttonPageOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonPageOpen.BackColor = System.Drawing.SystemColors.Control;
            this._buttonPageOpen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._buttonPageOpen.ForeColor = System.Drawing.Color.Black;
            this._buttonPageOpen.Location = new System.Drawing.Point(-2, 708);
            this._buttonPageOpen.Name = "_buttonPageOpen";
            this._buttonPageOpen.Size = new System.Drawing.Size(49, 23);
            this._buttonPageOpen.TabIndex = 13;
            this._buttonPageOpen.Text = "Page";
            this._buttonPageOpen.UseVisualStyleBackColor = false;
            this._buttonPageOpen.Click += new System.EventHandler(this._buttonPageOpen_Click);
            // 
            // _buttonOpen
            // 
            this._buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonOpen.BackColor = System.Drawing.SystemColors.Control;
            this._buttonOpen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._buttonOpen.ForeColor = System.Drawing.Color.Black;
            this._buttonOpen.Location = new System.Drawing.Point(-1, 682);
            this._buttonOpen.Name = "_buttonOpen";
            this._buttonOpen.Size = new System.Drawing.Size(47, 23);
            this._buttonOpen.TabIndex = 12;
            this._buttonOpen.Text = "Open";
            this._buttonOpen.UseVisualStyleBackColor = false;
            this._buttonOpen.Click += new System.EventHandler(this._buttonOpen_Click);
            // 
            // _buttonSave
            // 
            this._buttonSave.BackColor = System.Drawing.Color.Blue;
            this._buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this._buttonSave.Location = new System.Drawing.Point(-2, 24);
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Size = new System.Drawing.Size(48, 23);
            this._buttonSave.TabIndex = 11;
            this._buttonSave.Text = "Save";
            this._buttonSave.UseVisualStyleBackColor = false;
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _buttonClean
            // 
            this._buttonClean.BackColor = System.Drawing.SystemColors.Control;
            this._buttonClean.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._buttonClean.ForeColor = System.Drawing.Color.Black;
            this._buttonClean.Location = new System.Drawing.Point(-1, 75);
            this._buttonClean.Name = "_buttonClean";
            this._buttonClean.Size = new System.Drawing.Size(48, 23);
            this._buttonClean.TabIndex = 10;
            this._buttonClean.Text = "Clean";
            this._buttonClean.UseVisualStyleBackColor = false;
            this._buttonClean.Click += new System.EventHandler(this._buttonClean_Click);
            // 
            // _buttonReIndex
            // 
            this._buttonReIndex.BackColor = System.Drawing.SystemColors.Control;
            this._buttonReIndex.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._buttonReIndex.ForeColor = System.Drawing.Color.Black;
            this._buttonReIndex.Location = new System.Drawing.Point(-1, 100);
            this._buttonReIndex.Name = "_buttonReIndex";
            this._buttonReIndex.Size = new System.Drawing.Size(49, 23);
            this._buttonReIndex.TabIndex = 9;
            this._buttonReIndex.Text = "Index";
            this._buttonReIndex.UseVisualStyleBackColor = false;
            this._buttonReIndex.Click += new System.EventHandler(this._buttonReIndex_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(950, 805);
            this.Controls.Add(this._splitterRight);
            this.Controls.Add(this._splitterLeft);
            this.Controls.Add(this._panelBody);
            this.Controls.Add(this._panelLeft);
            this.Controls.Add(this._panelRight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._menu;
            this.Name = "fMain";
            this.Text = "English";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMain_FormClosing);
            this.Load += new System.EventHandler(this.fMain_Load);
            this._panelBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this._panelRight.ResumeLayout(false);
            this._panelRight.PerformLayout();
            this._menu.ResumeLayout(false);
            this._menu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel _panelLeft;
        private System.Windows.Forms.Panel _panelBody;
        private System.Windows.Forms.Splitter _splitterLeft;
        private System.Windows.Forms.Button _buttonClose;
        private System.Windows.Forms.Button _buttonMini;
        private System.Windows.Forms.Button _buttonSelection;
        private System.Windows.Forms.PictureBox _pictureBox;
        private System.Windows.Forms.Splitter _splitterRight;
        private System.Windows.Forms.Panel _panelRight;
        private System.Windows.Forms.Button _buttonReIndex;
        private System.Windows.Forms.Button _buttonClean;
        private System.Windows.Forms.Button _buttonOpen;
        private System.Windows.Forms.Button _buttonSave;
        private System.Windows.Forms.Button _buttonPageOpen;
        private System.Windows.Forms.Button _buttonView;
        private System.Windows.Forms.Button _buttonNext;
        private System.Windows.Forms.Button _buttonPrev;
        private System.Windows.Forms.Label _labelPageNumber;
        private System.Windows.Forms.MenuStrip _menu;
        private System.Windows.Forms.ToolStripMenuItem _menuMenu;
        private System.Windows.Forms.ToolStripMenuItem _menuDocNew;
        private System.Windows.Forms.ToolStripMenuItem _menuOpen;
        private System.Windows.Forms.ToolStripMenuItem _menuAttach;
        private System.Windows.Forms.ToolStripMenuItem _menuMedia;
        private System.Windows.Forms.ToolStripMenuItem _menuPageNew;
    }
}