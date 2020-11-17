using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace English
{
    class SchemeHandlerFactory : ISchemeHandlerFactory
    {
        public ISchemeHandler Create()
        {
            return new SchemeHandler();
        }
    }

    class SchemeHandler : ISchemeHandler
    {
        private readonly IDictionary<string, string> resources;

        public SchemeHandler()
        {
            resources = new Dictionary<string, string>
            {
                //{ "BindingTest.html", Resources.BindingTest },
                //{ "PopupTest.html", Resources.PopupTest },
                //{ "SchemeTest.html", Resources.SchemeTest },
                //{ "TooltipTest.html", Resources.TooltipTest },
            };
        }

        public bool ProcessRequest(IRequest request, ref string mimeType, ref Stream stream)
        {
            var uri = new Uri(request.Url);
            string file = uri.AbsolutePath;
            if (file == "/") file = "locales/" + uri.Host + ".html";

            if (File.Exists(file))
            {
                stream = new FileStream(file, FileMode.Open);
                mimeType = "text/html";
                return true;
            }

            return false;
        }
    }
}
