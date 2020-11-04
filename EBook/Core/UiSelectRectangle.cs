using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System
{
    public class UiSelectRectangle : Control
    {
        const int BORDER_WIDTH = 1;
        Color BORDER_COLOR = Color.Blue;
        Label _labelIndex = null;
        //UiTransparent _bgOpacity = new UiTransparent() { Visible = false, Dock = DockStyle.Fill, BackColor = Color.Black, Opacity = 0 };

        public int Index {
            get { return int.Parse(_labelIndex.Text); }
            set
            {
                _labelIndex.Text = value.ToString();
            }
        }

        //public void SetOpacity(int opacity) {
        //    _bgOpacity.Opacity = opacity;
        //    _bgOpacity.Visible = true;
        //}

        public UiSelectRectangle(int index):base()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            UiSelectRectangle.CheckForIllegalCrossThreadCalls = false;

            _labelIndex = new Label()
            {
                Visible = false,
                Text = index.ToString(),
                BackColor = BORDER_COLOR,
                AutoSize = true,
                Dock = DockStyle.None,
                Location = new Point(0, 0),
                ForeColor = Color.White,
                Font = new Font("Arial", 11f, FontStyle.Bold)
            };

            this.Controls.AddRange(new Control[]{
                _labelIndex,
                new Label()
                {
                    Dock = DockStyle.Top,
                    AutoSize = false,
                    Text = string.Empty,
                    BackColor = BORDER_COLOR,
                    Height = BORDER_WIDTH
                },
                new Label()
                {
                    Dock = DockStyle.Right,
                    AutoSize = false,
                    Text = string.Empty,
                    BackColor = BORDER_COLOR,
                    Width = BORDER_WIDTH
                },
                new Label()
                {
                    Dock = DockStyle.Bottom,
                    AutoSize = false,
                    Text = string.Empty,
                    BackColor = BORDER_COLOR,
                    Height = BORDER_WIDTH
                },
                new Label()
                {
                    Dock = DockStyle.Left,
                    AutoSize = false,
                    Text = string.Empty,
                    BackColor = BORDER_COLOR,
                    Width = BORDER_WIDTH
                },
                //_bgOpacity
            });
        }

    }
}
