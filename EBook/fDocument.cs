using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EBook
{
    public class fDocument: Form
    {
        public fDocument() {
            doc_Init();
        }

        #region [ MAIN ]

        void doc_Init() {
            this.Visible = false;
            this.Shown += (se, ev) =>
            {
                this.BackColor = Color.Black;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Left = 0;
                this.Top = 0;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                this.Width = 0;

                doc_LoadSetting();
            };
        }

        void doc_LoadSetting() {
            using (var sr = new StreamReader("setting.bin", Encoding.UTF8))
            using (var reader = new JsonTextReader(sr))
            {
                if (!reader.Read() || reader.TokenType != JsonToken.StartObject)
                {
                    //throw new Exception("Expected start of array");
                }
                else
                {
                    var ser = new JsonSerializer();
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.EndObject) break;
                        var item = ser.Deserialize<oSetting>(reader);
                    }
                }
            }
        }

        #endregion

    }
}
