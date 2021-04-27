using EnglishModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace English
{
    public partial class fOther : Form, IOther
    {
        #region [ VARIABLE ]

        List<string> m_search_history = new List<string>() { };
        Font ui_searchFont = new Font("Consolas", 15.0f);
        CheckedListBox ui_searchHistory = new CheckedListBox()
        {
            BorderStyle = BorderStyle.FixedSingle,
            Visible = false,
            Width = 200,
            Height = 500,
            Location = new Point(297, 72)
        };

        string[] m_words = new string[] { };
        string m_keyword = string.Empty;

        #endregion

        #region [ MAIN ]

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public fOther()
        {
            InitializeComponent();

            this.Controls.Add(ui_searchHistory);
            ui_searchHistory.BringToFront();
            _buttonSearchClose.BringToFront();
            ui_searchHistory.Font = ui_searchFont;

            ui_searchHistory.Visible = false;
            _buttonSearchClose.Visible = false;

            _panelMedia.MouseMove += _panel_MouseMove;
            _labelTitle.MouseMove += _panel_MouseMove;

            this.KeyPreview = true;
            this.KeyUp += form_KeyUp;
        }

        private void fOther_Load(object sender, System.EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            app_Init();
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

        #endregion

        void app_Init() {
            m_words = Directory.GetFiles(oPATH.PATH_MP3_WORD, "*.mp3")
                .Select(x => Path.GetFileName(x).Split('#')[0].ToLower())
                .OrderBy(x => x)
                .ToArray();
            ui_searchHistory.MouseClick += (se, ev) => { 
            
            };
        }

        #region [ SEARCH ]

        private void _textSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
            else {
                string keyWord = _textSearch.Text.Trim().ToLower();
                if (m_keyword != keyWord)
                {
                    var a = m_words.Where(x => x.Contains(keyWord)).Take(10).ToArray();
                    ui_searchHistory.Items.Clear();
                    ui_searchHistory.Items.AddRange(a);
                    m_keyword = keyWord;
                }
            }
        }

        private void _buttonSearchClose_Click(object sender, EventArgs e)
        {
            _buttonSearchClose.Visible = false;
            ui_searchHistory.Visible = false;
        }

        private void _textSearch_MouseClick(object sender, MouseEventArgs e)
        {
            _buttonSearchClose.Visible = true;
            ui_searchHistory.Visible = true;
        }

        private void _textSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _textSearch.Text = string.Empty;
        }




        #endregion

        #region [ WORD ]

        private void _menuCache_ReloadWords_Click(object sender, EventArgs e)
        {

        }

        private void _menuWord_AddnewFromSearchBox_Click(object sender, EventArgs e)
        {

        }

        private void _menuWord_ReloadContent_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void _labelSearch_BrowserTag_Click(object sender, EventArgs e)
        {

        }

        private void _labelSearch_BrowserAlbum_Click(object sender, EventArgs e)
        {

        }

        private void _labelSearch_BrowserSubject_Click(object sender, EventArgs e)
        {

        }

        private void _menuWord_updateArticleAboutUsing_Click(object sender, EventArgs e)
        {
            int index = ui_searchHistory.SelectedIndex;
            if (index != -1)
            {
                string word = ui_searchHistory.Items[index].ToString();
                var f = new fEditor(word);
                f.Show();
            }
        }
    }
}
