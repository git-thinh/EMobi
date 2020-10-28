namespace EMobiTestUI
{
    partial class fPageAttach
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._textTitle = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._labelDocumentName = new System.Windows.Forms.Label();
            this._labelPageNumber = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this._tabAttach = new System.Windows.Forms.TabPage();
            this._listAttach = new System.Windows.Forms.CheckedListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this._panelMedia = new System.Windows.Forms.Panel();
            this._checkRepeat = new System.Windows.Forms.CheckBox();
            this._buttonMediaPlayPause = new System.Windows.Forms.Button();
            this._buttonMediaNext = new System.Windows.Forms.Button();
            this._buttonMediaPrev = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this._buttonAddItem = new System.Windows.Forms.Button();
            this._buttonRemoveItem = new System.Windows.Forms.Button();
            this._buttonRemoveAll = new System.Windows.Forms.Button();
            this._buttonUpdate = new System.Windows.Forms.Button();
            this._textAttachTitle = new System.Windows.Forms.TextBox();
            this._buttonAttachBrowser = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this._tabContent = new System.Windows.Forms.TabPage();
            this._textContent = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this._tabAttach.SuspendLayout();
            this.panel3.SuspendLayout();
            this._panelMedia.SuspendLayout();
            this.panel2.SuspendLayout();
            this._tabContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Document";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Title Page";
            // 
            // _textTitle
            // 
            this._textTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textTitle.Location = new System.Drawing.Point(66, 42);
            this._textTitle.Name = "_textTitle";
            this._textTitle.Size = new System.Drawing.Size(358, 20);
            this._textTitle.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this._labelDocumentName);
            this.panel1.Controls.Add(this._labelPageNumber);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(436, 71);
            this.panel1.TabIndex = 6;
            // 
            // _labelDocumentName
            // 
            this._labelDocumentName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._labelDocumentName.BackColor = System.Drawing.SystemColors.Control;
            this._labelDocumentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelDocumentName.Location = new System.Drawing.Point(141, 7);
            this._labelDocumentName.Name = "_labelDocumentName";
            this._labelDocumentName.Size = new System.Drawing.Size(283, 30);
            this._labelDocumentName.TabIndex = 6;
            // 
            // _labelPageNumber
            // 
            this._labelPageNumber.BackColor = System.Drawing.SystemColors.Control;
            this._labelPageNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelPageNumber.Location = new System.Drawing.Point(66, 9);
            this._labelPageNumber.Name = "_labelPageNumber";
            this._labelPageNumber.Size = new System.Drawing.Size(53, 24);
            this._labelPageNumber.TabIndex = 5;
            this._labelPageNumber.Text = "0";
            this._labelPageNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this._tabAttach);
            this.tabControl1.Controls.Add(this._tabContent);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 71);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(436, 531);
            this.tabControl1.TabIndex = 7;
            // 
            // _tabAttach
            // 
            this._tabAttach.Controls.Add(this._listAttach);
            this._tabAttach.Controls.Add(this.panel3);
            this._tabAttach.Controls.Add(this.panel2);
            this._tabAttach.Location = new System.Drawing.Point(4, 22);
            this._tabAttach.Name = "_tabAttach";
            this._tabAttach.Padding = new System.Windows.Forms.Padding(3);
            this._tabAttach.Size = new System.Drawing.Size(428, 505);
            this._tabAttach.TabIndex = 1;
            this._tabAttach.Text = "Attach";
            this._tabAttach.UseVisualStyleBackColor = true;
            // 
            // _listAttach
            // 
            this._listAttach.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._listAttach.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listAttach.FormattingEnabled = true;
            this._listAttach.Location = new System.Drawing.Point(3, 60);
            this._listAttach.Name = "_listAttach";
            this._listAttach.ScrollAlwaysVisible = true;
            this._listAttach.Size = new System.Drawing.Size(422, 405);
            this._listAttach.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this._panelMedia);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 465);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(422, 37);
            this.panel3.TabIndex = 10;
            // 
            // _panelMedia
            // 
            this._panelMedia.Controls.Add(this._checkRepeat);
            this._panelMedia.Controls.Add(this._buttonMediaPlayPause);
            this._panelMedia.Controls.Add(this._buttonMediaNext);
            this._panelMedia.Controls.Add(this._buttonMediaPrev);
            this._panelMedia.Enabled = false;
            this._panelMedia.Location = new System.Drawing.Point(219, 5);
            this._panelMedia.Name = "_panelMedia";
            this._panelMedia.Size = new System.Drawing.Size(200, 29);
            this._panelMedia.TabIndex = 8;
            // 
            // _checkRepeat
            // 
            this._checkRepeat.AutoSize = true;
            this._checkRepeat.Location = new System.Drawing.Point(2, 7);
            this._checkRepeat.Name = "_checkRepeat";
            this._checkRepeat.Size = new System.Drawing.Size(61, 17);
            this._checkRepeat.TabIndex = 6;
            this._checkRepeat.Text = "Repeat";
            this._checkRepeat.UseVisualStyleBackColor = true;
            // 
            // _buttonMediaPlayPause
            // 
            this._buttonMediaPlayPause.Location = new System.Drawing.Point(107, 3);
            this._buttonMediaPlayPause.Name = "_buttonMediaPlayPause";
            this._buttonMediaPlayPause.Size = new System.Drawing.Size(52, 23);
            this._buttonMediaPlayPause.TabIndex = 4;
            this._buttonMediaPlayPause.Text = "Play";
            this._buttonMediaPlayPause.UseVisualStyleBackColor = true;
            this._buttonMediaPlayPause.Click += new System.EventHandler(this._buttonMediaPlayPause_Click);
            // 
            // _buttonMediaNext
            // 
            this._buttonMediaNext.Location = new System.Drawing.Point(165, 2);
            this._buttonMediaNext.Name = "_buttonMediaNext";
            this._buttonMediaNext.Size = new System.Drawing.Size(32, 23);
            this._buttonMediaNext.TabIndex = 5;
            this._buttonMediaNext.Text = ">";
            this._buttonMediaNext.UseVisualStyleBackColor = true;
            this._buttonMediaNext.Click += new System.EventHandler(this._buttonMediaNext_Click);
            // 
            // _buttonMediaPrev
            // 
            this._buttonMediaPrev.Location = new System.Drawing.Point(69, 3);
            this._buttonMediaPrev.Name = "_buttonMediaPrev";
            this._buttonMediaPrev.Size = new System.Drawing.Size(32, 23);
            this._buttonMediaPrev.TabIndex = 3;
            this._buttonMediaPrev.Text = "<";
            this._buttonMediaPrev.UseVisualStyleBackColor = true;
            this._buttonMediaPrev.Click += new System.EventHandler(this._buttonMediaPrev_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._buttonAddItem);
            this.panel2.Controls.Add(this._buttonRemoveItem);
            this.panel2.Controls.Add(this._buttonRemoveAll);
            this.panel2.Controls.Add(this._buttonUpdate);
            this.panel2.Controls.Add(this._textAttachTitle);
            this.panel2.Controls.Add(this._buttonAttachBrowser);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(422, 57);
            this.panel2.TabIndex = 8;
            // 
            // _buttonAddItem
            // 
            this._buttonAddItem.Location = new System.Drawing.Point(118, 31);
            this._buttonAddItem.Name = "_buttonAddItem";
            this._buttonAddItem.Size = new System.Drawing.Size(57, 23);
            this._buttonAddItem.TabIndex = 11;
            this._buttonAddItem.Text = "Add Item";
            this._buttonAddItem.UseVisualStyleBackColor = true;
            this._buttonAddItem.Click += new System.EventHandler(this._buttonAddItem_Click);
            // 
            // _buttonRemoveItem
            // 
            this._buttonRemoveItem.Location = new System.Drawing.Point(265, 31);
            this._buttonRemoveItem.Name = "_buttonRemoveItem";
            this._buttonRemoveItem.Size = new System.Drawing.Size(78, 23);
            this._buttonRemoveItem.TabIndex = 10;
            this._buttonRemoveItem.Text = "Remove Item";
            this._buttonRemoveItem.UseVisualStyleBackColor = true;
            this._buttonRemoveItem.Click += new System.EventHandler(this._buttonRemoveItem_Click);
            // 
            // _buttonRemoveAll
            // 
            this._buttonRemoveAll.Location = new System.Drawing.Point(181, 31);
            this._buttonRemoveAll.Name = "_buttonRemoveAll";
            this._buttonRemoveAll.Size = new System.Drawing.Size(78, 23);
            this._buttonRemoveAll.TabIndex = 9;
            this._buttonRemoveAll.Text = "Remove All";
            this._buttonRemoveAll.UseVisualStyleBackColor = true;
            this._buttonRemoveAll.Click += new System.EventHandler(this._buttonRemoveAll_Click);
            // 
            // _buttonUpdate
            // 
            this._buttonUpdate.BackColor = System.Drawing.Color.DodgerBlue;
            this._buttonUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._buttonUpdate.ForeColor = System.Drawing.Color.White;
            this._buttonUpdate.Location = new System.Drawing.Point(349, 31);
            this._buttonUpdate.Name = "_buttonUpdate";
            this._buttonUpdate.Size = new System.Drawing.Size(66, 23);
            this._buttonUpdate.TabIndex = 8;
            this._buttonUpdate.Text = "Update";
            this._buttonUpdate.UseVisualStyleBackColor = false;
            this._buttonUpdate.Click += new System.EventHandler(this._buttonUpdate_Click);
            // 
            // _textAttachTitle
            // 
            this._textAttachTitle.Location = new System.Drawing.Point(59, 7);
            this._textAttachTitle.Name = "_textAttachTitle";
            this._textAttachTitle.Size = new System.Drawing.Size(357, 20);
            this._textAttachTitle.TabIndex = 7;
            // 
            // _buttonAttachBrowser
            // 
            this._buttonAttachBrowser.Location = new System.Drawing.Point(55, 31);
            this._buttonAttachBrowser.Name = "_buttonAttachBrowser";
            this._buttonAttachBrowser.Size = new System.Drawing.Size(57, 23);
            this._buttonAttachBrowser.TabIndex = 5;
            this._buttonAttachBrowser.Text = "Browser";
            this._buttonAttachBrowser.UseVisualStyleBackColor = true;
            this._buttonAttachBrowser.Click += new System.EventHandler(this._buttonAttachBrowser_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(-1, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Title Attach";
            // 
            // _tabContent
            // 
            this._tabContent.Controls.Add(this._textContent);
            this._tabContent.Location = new System.Drawing.Point(4, 22);
            this._tabContent.Margin = new System.Windows.Forms.Padding(0);
            this._tabContent.Name = "_tabContent";
            this._tabContent.Size = new System.Drawing.Size(428, 505);
            this._tabContent.TabIndex = 0;
            this._tabContent.Text = "Content";
            this._tabContent.UseVisualStyleBackColor = true;
            // 
            // _textContent
            // 
            this._textContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._textContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._textContent.Location = new System.Drawing.Point(0, 0);
            this._textContent.Multiline = true;
            this._textContent.Name = "_textContent";
            this._textContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textContent.Size = new System.Drawing.Size(428, 505);
            this._textContent.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(118, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "-";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fPageAttach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 602);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this._textTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "fPageAttach";
            this.Text = "Attach Page";
            this.Load += new System.EventHandler(this.fPageAttach_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this._tabAttach.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this._panelMedia.ResumeLayout(false);
            this._panelMedia.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this._tabContent.ResumeLayout(false);
            this._tabContent.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _textTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage _tabContent;
        private System.Windows.Forms.TextBox _textContent;
        private System.Windows.Forms.TabPage _tabAttach;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button _buttonUpdate;
        private System.Windows.Forms.TextBox _textAttachTitle;
        private System.Windows.Forms.Button _buttonAttachBrowser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button _buttonRemoveAll;
        private System.Windows.Forms.Button _buttonRemoveItem;
        private System.Windows.Forms.Label _labelPageNumber;
        private System.Windows.Forms.Button _buttonAddItem;
        private System.Windows.Forms.Label _labelDocumentName;
        private System.Windows.Forms.CheckedListBox _listAttach;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox _checkRepeat;
        private System.Windows.Forms.Button _buttonMediaNext;
        private System.Windows.Forms.Button _buttonMediaPlayPause;
        private System.Windows.Forms.Button _buttonMediaPrev;
        private System.Windows.Forms.Panel _panelMedia;
        private System.Windows.Forms.Label label4;
    }
}