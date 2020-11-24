using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishModel
{
    public class oSource
    {
        public int id { set; get; }
        public string name { set; get; }
        public string[] types { set; get; }
        public string url { set; get; }

        public override string ToString()
        {
            return string.Format("{0}.{1}", id, name);
        }
    }
}
