
namespace English
{
    partial class fEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fEditor));
            this.panel1 = new System.Windows.Forms.Panel();
            this._textEdit = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this._labelTitle = new System.Windows.Forms.Label();
            this._buttonUpdate = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._labelPronunciation = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this._labelPronunciation);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this._buttonUpdate);
            this.panel1.Controls.Add(this._labelTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.panel1.Size = new System.Drawing.Size(789, 53);
            this.panel1.TabIndex = 0;
            // 
            // _textEdit
            // 
            this._textEdit.AllowDrop = true;
            this._textEdit.BackColor = System.Drawing.Color.Black;
            this._textEdit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._textEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textEdit.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._textEdit.ForeColor = System.Drawing.Color.White;
            this._textEdit.Location = new System.Drawing.Point(7, 299);
            this._textEdit.Multiline = true;
            this._textEdit.Name = "_textEdit";
            this._textEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textEdit.Size = new System.Drawing.Size(782, 464);
            this._textEdit.TabIndex = 1;
            this._textEdit.Text = resources.GetString("_textEdit.Text");
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this._textEdit);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 53);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(7, 7, 0, 0);
            this.panel2.Size = new System.Drawing.Size(789, 763);
            this.panel2.TabIndex = 2;
            // 
            // _labelTitle
            // 
            this._labelTitle.AutoSize = true;
            this._labelTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this._labelTitle.Font = new System.Drawing.Font("Consolas", 23F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelTitle.ForeColor = System.Drawing.Color.White;
            this._labelTitle.Location = new System.Drawing.Point(10, 10);
            this._labelTitle.Name = "_labelTitle";
            this._labelTitle.Size = new System.Drawing.Size(136, 37);
            this._labelTitle.TabIndex = 0;
            this._labelTitle.Text = "ability";
            // 
            // _buttonUpdate
            // 
            this._buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonUpdate.Location = new System.Drawing.Point(732, 16);
            this._buttonUpdate.Name = "_buttonUpdate";
            this._buttonUpdate.Size = new System.Drawing.Size(54, 23);
            this._buttonUpdate.TabIndex = 1;
            this._buttonUpdate.Text = "SAVE";
            this._buttonUpdate.UseVisualStyleBackColor = true;
            this._buttonUpdate.Click += new System.EventHandler(this._buttonUpdate_Click);
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(7, 7);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(782, 122);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "N = Khả năng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Example";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DimGray;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(7, 129);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(782, 21);
            this.panel3.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.AllowDrop = true;
            this.textBox2.BackColor = System.Drawing.Color.Black;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox2.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(7, 150);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(782, 128);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = "The system has the ability to run more than one program at the same time.\r\nThe TO" +
    "EIC generally reflects one’s ability to use English.";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DimGray;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(782, 21);
            this.panel4.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sentence";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DimGray;
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(7, 278);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(782, 21);
            this.panel5.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Using";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(146, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 28);
            this.label4.TabIndex = 2;
            this.label4.Text = "-";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _labelPronunciation
            // 
            this._labelPronunciation.AutoSize = true;
            this._labelPronunciation.Dock = System.Windows.Forms.DockStyle.Left;
            this._labelPronunciation.Font = new System.Drawing.Font("Consolas", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelPronunciation.ForeColor = System.Drawing.Color.White;
            this._labelPronunciation.Location = new System.Drawing.Point(171, 10);
            this._labelPronunciation.Name = "_labelPronunciation";
            this._labelPronunciation.Size = new System.Drawing.Size(207, 40);
            this._labelPronunciation.TabIndex = 3;
            this._labelPronunciation.Text = "əˈbɪl.ə.ti";
            // 
            // fEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(789, 816);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "fEditor";
            this.Text = "Editor";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox _textEdit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label _labelTitle;
        private System.Windows.Forms.Button _buttonUpdate;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label _labelPronunciation;
        private System.Windows.Forms.Label label4;
    }
}