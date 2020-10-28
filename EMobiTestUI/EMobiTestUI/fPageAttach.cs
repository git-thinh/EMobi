using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EMobiTestUI
{
    public partial class fPageAttach : Form
    {
        readonly IApp m_app;
        readonly IMain m_main;
        public fPageAttach(IApp app, IMain main):base()
        {
            m_app = app;
            m_main = main;
            InitializeComponent();
        }

        private void fPageAttach_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Top = 125;

            //this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height - 220;

            _labelDocumentName.Text = m_app.DocumentName;
            _labelPageNumber.Text = (m_app.PageNumber + 1).ToString();
        }

         
        private void _buttonAttachBrowser_Click(object sender, EventArgs e)
        {

        }

        private void _buttonUpdate_Click(object sender, EventArgs e)
        {

        }

        private void _buttonRemoveItem_Click(object sender, EventArgs e)
        {

        }

        private void _buttonRemoveAll_Click(object sender, EventArgs e)
        {

        }

        private void _buttonAddItem_Click(object sender, EventArgs e)
        {

        }
         
        private void _buttonMediaPrev_Click(object sender, EventArgs e)
        {

        }

        private void _buttonMediaNext_Click(object sender, EventArgs e)
        {

        }

        private void _buttonMediaPlayPause_Click(object sender, EventArgs e)
        {

        }
    }
}
