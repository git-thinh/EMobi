namespace EBook
{
    partial class fMedia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMedia));
            this._media = new AxWMPLib.AxWindowsMediaPlayer();
            this._backGroundTransparent = new System.UiTransparent();
            ((System.ComponentModel.ISupportInitialize)(this._media)).BeginInit();
            this.SuspendLayout();
            // 
            // _media
            // 
            this._media.Dock = System.Windows.Forms.DockStyle.Fill;
            this._media.Enabled = true;
            this._media.Location = new System.Drawing.Point(0, 0);
            this._media.Name = "_media";
            this._media.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("_media.OcxState")));
            this._media.Size = new System.Drawing.Size(800, 450);
            this._media.TabIndex = 0;
            // 
            // _backGroundTransparent
            // 
            this._backGroundTransparent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._backGroundTransparent.BackColor = System.Drawing.Color.Transparent;
            this._backGroundTransparent.Location = new System.Drawing.Point(0, 0);
            this._backGroundTransparent.Name = "_backGroundTransparent";
            this._backGroundTransparent.Opacity = 100;
            this._backGroundTransparent.Size = new System.Drawing.Size(800, 450);
            this._backGroundTransparent.TabIndex = 1;
            this._backGroundTransparent.Text = "uiTransparent1";
            this._backGroundTransparent.Click += new System.EventHandler(this._backGroundTransparent_Click);
            // 
            // fMedia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._backGroundTransparent);
            this.Controls.Add(this._media);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fMedia";
            this.Text = "fMedia";
            this.Load += new System.EventHandler(this.fMedia_Load);
            ((System.ComponentModel.ISupportInitialize)(this._media)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer _media;
        private System.UiTransparent _backGroundTransparent;
    }
}