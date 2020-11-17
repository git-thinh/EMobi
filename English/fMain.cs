using CefSharp;
using CefSharp.WinForms;
using System;
using System.Windows.Forms;

namespace English
{
    public partial class fMain : Form
    {
        private readonly WebView web_media;
        private readonly WebView web_tree;
        private readonly WebView web_main;

        public fMain()
        {
            InitializeComponent();

            var setting = new BrowserSettings()
            {

            };

            web_media = new WebView("local://media", setting) { Dock = DockStyle.Fill };
            web_tree = new WebView("local://tree", setting) { Dock = DockStyle.Fill };
            web_main = new WebView("local://main", setting) { Dock = DockStyle.Fill };

            _panelMedia.Controls.Add(web_media);
            _panelTree.Controls.Add(web_tree);
            _panelMain.Controls.Add(web_main);
        }

        private void fMain_Load(object sender, EventArgs e)
        {

        }
    }
}
