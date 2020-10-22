using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EMobiTestUI
{
    public partial class fOpen : Form
    {
        readonly IMain m_main;
        string m_file;
        public fOpen(IMain main) : base()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            m_main = main;
            m_file = main.FileDocument;
            InitializeComponent();
        }

        private void fOpen_Load(object sender, EventArgs e)
        {
            this.Shown += (se, ev) =>
            {
                this.Top = (Screen.FromControl(this).WorkingArea.Height - this.Height) / 2 - 100;
                this.Left = (Screen.FromControl(this).WorkingArea.Width - this.Width) / 2;
            };
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        static Image GetPageImage(int pageNumber, Size size, PdfDocument document, int dpi)
        {
            return document.Render(pageNumber - 1, size.Width, size.Height, dpi, dpi, PdfRenderFlags.Annotations);
        }

        static void RenderPage(string pdfPath, int pageNumber, Size size, string outputPath)
        {
            using (var document = PdfDocument.Load(pdfPath))
            {
                using (var stream = new FileStream(outputPath, FileMode.Create))
                {

                    using (var image = GetPageImage(pageNumber, size, document, 150))
                    {
                        image.Save(stream, ImageFormat.Jpeg);
                    }
                }
            }
        }

        private void _buttonBrowser_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(m_file);
                //openFileDialog.Filter = "Image files (*.png)|*.png|(*.jpg)|*.jpg|All files (*.*)|*.*";
                openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string file = openFileDialog.FileName;
                    //openImage(file);
                    _labelFile.Text = file;

                    using (var document = PdfDocument.Load(file))
                    {
                        int dpi = 150,
                            max = document.PageCount,
                            wi = Screen.PrimaryScreen.WorkingArea.Width,
                            hi = Screen.PrimaryScreen.WorkingArea.Height;
                        string time = DateTime.Now.ToString("yyMMdd.HHmmss");
                        int wp = 0, hp = 0, w = 0, h = 0;
                        for (int i = 0; i < max; i++)
                        {
                            wp = (int)document.PageSizes[i].Width;
                            hp = (int)document.PageSizes[i].Height;

                            //h = hi;
                            //w = h * wp / hp;
                            w = wi;
                            h = w * hp / wp;

                            string outputPath = @"C:\test\images\" + (i + 1).ToString() + "-" + time + ".jpg";
                            using (var stream = new FileStream(outputPath, FileMode.Create))
                            {
                                using (var image = document.Render(i, w, h, dpi, dpi, PdfRenderFlags.Annotations))
                                {
                                    _labelMessage.Text = "Processing page " + (i + 1).ToString() + "...";
                                    image.Save(stream, ImageFormat.Jpeg);
                                }
                            }
                        }
                        _labelMessage.Text = "Process complete: " + max + " pasges";
                    }

                }
            }
        }
    }
}
