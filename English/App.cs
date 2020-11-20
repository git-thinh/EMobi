using CefSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace English
{
    public class oSelection
    {
        public int X { set; get; }
        public int Y { set; get; }
        public int Width { set; get; }
        public int Height { set; get; }
    }

    public class oTextOriginAI
    {
        public bool Ok { set; get; }
        public float Version { set; get; }
        public string TextEn { set; get; }
        public string TextVi { set; get; }
    }

    public class oPage
    {
        public int Id { set; get; }

        public int Width { set; get; }
        public int Height { set; get; }

        public oTextOriginAI TextAI { set; get; }

        public string Indexing { set; get; }
        public string Title { set; get; }
        public List<oSelection> Selections { set; get; }

        public oPage()
        {
            Id = -1;

            Width = 0;
            Height = 0;

            TextAI = new oTextOriginAI()
            {
                TextEn = string.Empty,
                TextVi = string.Empty,
                Version = 0,
                Ok = false
            };

            Indexing = string.Empty;
            Title = string.Empty;
            Selections = new List<oSelection>() { };
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}x{2}= {3}", Id, Width, Height, TextAI.TextEn);
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
            Settings settings = new Settings()
            {

            };

            if (CEF.Initialize(settings) == false) return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var f = new fMain();
            CEF.RegisterScheme("local", new SchemeHandlerFactory());
            CEF.RegisterScheme("img", new ImageHandlerFactory(f));
            CEF.RegisterJsObject("api", f);
            Application.Run(f);

            //model.Dispose();
            CEF.Shutdown();
            //Environment.Exit(0);
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
        public bool SHOW_PAGE_INFO { get; set; }

        public bool IS_SELECTION { get; set; }
        public bool SELECTION_ATTACH_MEDIA { get; set; }
        public bool AUTO_CROP_PAGE_SELECTED { get; set; }

        public int PageNumber { get; set; }
        public string DocumentFile { get; set; }
        public string DocumentName { get; set; }

        public string ImageBase64 { get; set; }

        public oSetting()
        {
            this.SHOW_PAGE_INFO = false;
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

    public class ImageHandlerFactory : ISchemeHandlerFactory
    {
        readonly ISchemeHandler main;
        public ImageHandlerFactory(ISchemeHandler _main) : base()
        {
            main = _main;
        }

        public ISchemeHandler Create()
        {
            return main;
        }
    }

    public interface IBoundObject
    {
        void mainInited();
        Int32 getPageTotal();
        string getPageInfo(int page);
    }
}
