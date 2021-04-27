using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace English
{
    public partial class fEditor : Form
    {
        readonly string m_word = string.Empty;
        public fEditor(string word)
        {
            InitializeComponent();
            m_word = word;
            _labelTitle.Text = m_word;
        }

        private void _buttonUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
