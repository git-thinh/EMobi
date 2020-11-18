
namespace English
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
            this._panelExplorer = new System.Windows.Forms.Panel();
            this._panelTree = new System.Windows.Forms.Panel();
            this._panelMedia = new System.Windows.Forms.Panel();
            this._panelMain = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this._panelExplorer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _panelExplorer
            // 
            this._panelExplorer.Controls.Add(this._panelTree);
            this._panelExplorer.Controls.Add(this._panelMedia);
            this._panelExplorer.Dock = System.Windows.Forms.DockStyle.Left;
            this._panelExplorer.Location = new System.Drawing.Point(0, 0);
            this._panelExplorer.Name = "_panelExplorer";
            this._panelExplorer.Size = new System.Drawing.Size(261, 740);
            this._panelExplorer.TabIndex = 0;
            // 
            // _panelTree
            // 
            this._panelTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelTree.Location = new System.Drawing.Point(0, 113);
            this._panelTree.Name = "_panelTree";
            this._panelTree.Size = new System.Drawing.Size(261, 627);
            this._panelTree.TabIndex = 0;
            // 
            // _panelMedia
            // 
            this._panelMedia.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelMedia.Location = new System.Drawing.Point(0, 0);
            this._panelMedia.Name = "_panelMedia";
            this._panelMedia.Size = new System.Drawing.Size(261, 113);
            this._panelMedia.TabIndex = 0;
            // 
            // _panelMain
            // 
            this._panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelMain.Location = new System.Drawing.Point(261, 0);
            this._panelMain.Name = "_panelMain";
            this._panelMain.Size = new System.Drawing.Size(747, 740);
            this._panelMain.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(261, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 740);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 740);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this._panelMain);
            this.Controls.Add(this._panelExplorer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fMain";
            this.Text = "English";
            this.Load += new System.EventHandler(this.fMain_Load);
            this._panelExplorer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _panelExplorer;
        private System.Windows.Forms.Panel _panelMain;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel _panelTree;
        private System.Windows.Forms.Panel _panelMedia;
    }
}