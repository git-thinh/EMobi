using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace EBook
{
    public class oSelection
    {
        public int X { set; get; }
        public int Y { set; get; }
        public int Width { set; get; }
        public int Height { set; get; }
    }

    public class oPage
    {
        public int Id { set; get; }
        public string Indexing { set; get; }
        public string Title { set; get; }
        public List<oSelection> Selections { set; get; }

        public oPage()
        {
            Id = -1;
            Indexing = string.Empty;
            Title = string.Empty;
            Selections = new List<oSelection>() { };
        }
    }

    static class App
    {
        static App()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (se, ev) =>
            {
                Assembly asm = null;
                string comName = ev.Name.Split(',')[0];
                string resourceName = @"DLL\" + comName + ".dll";
                var assembly = Assembly.GetExecutingAssembly();
                resourceName = typeof(App).Namespace + "." + resourceName.Replace(" ", "_").Replace("\\", ".").Replace("/", ".");
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        Debug.WriteLine(resourceName);
                    }
                    else
                    {
                        byte[] buffer = new byte[stream.Length];
                        using (MemoryStream ms = new MemoryStream())
                        {
                            int read;
                            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                                ms.Write(buffer, 0, read);
                            buffer = ms.ToArray();
                        }
                        asm = Assembly.Load(buffer);
                    }
                }
                return asm;
            };
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new fMedia());
            Application.Run(new fMain());
        }
    }

    public interface IMain
    {
        bool IS_SELECTION { get; set; }
        bool SELECTION_ATTACH_MEDIA { get; set; }
        bool AUTO_CROP_PAGE_SELECTED { get; set; }

        int PageNumber { get; set; }
        string DocumentFile { get; set; }
        string DocumentName { get; set; }


        string PASSWORD { get; }
        string FOLDER_DATA { get; }
        string PATH_DATA { get; }

        Dictionary<int, byte[]> m_pages { get; }
        Dictionary<int, byte[]> m_page_crops { get; }
        Dictionary<int, oPage> m_infos { get; set; }
    }

    public class oSetting
    {
        public bool IS_SELECTION { get; set; }
        public bool SELECTION_ATTACH_MEDIA { get; set; }
        public bool AUTO_CROP_PAGE_SELECTED { get; set; }

        public int PageNumber { get; set; }
        public string DocumentFile { get; set; }
        public string DocumentName { get; set; }

        public oSetting()
        {
            this.IS_SELECTION = false;
            this.SELECTION_ATTACH_MEDIA = false;
            this.AUTO_CROP_PAGE_SELECTED = false;

            this.PageNumber = 0;
            this.DocumentFile = string.Empty;
            this.DocumentName = string.Empty;
        }

        public oSetting(IMain main) : base()
        {
            this.IS_SELECTION = main.IS_SELECTION;
            this.SELECTION_ATTACH_MEDIA = main.SELECTION_ATTACH_MEDIA;
            this.AUTO_CROP_PAGE_SELECTED = main.AUTO_CROP_PAGE_SELECTED;

            this.PageNumber = main.PageNumber;
            this.DocumentFile = main.DocumentFile;
            this.DocumentName = main.DocumentName;
        }
    }



}
