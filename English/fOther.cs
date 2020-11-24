using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace English
{
    public partial class fOther : Form, IOther
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public fOther()
        {
            InitializeComponent();

            _panelMedia.MouseMove += _panel_MouseMove;
            _labelTitle.MouseMove += _panel_MouseMove;

            this.KeyPreview = true;
            this.KeyUp += form_KeyUp;
        }

        private void _panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void form_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    this.WindowState = FormWindowState.Minimized;
                    break;
            }
        }

        public void show()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void fOther_Load(object sender, System.EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
        }

        private void _menuClode_Click(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void _menuWindowOnTop_Click(object sender, EventArgs e)
        {
            if (this.TopMost)
            {
                _menuWindowOnTop.Checked = false;
                this.TopMost = false;
            }
            else {
                _menuWindowOnTop.Checked = true;
                this.TopMost = true;
            }
        }

        private void _labelMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
