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
        public bool ProcessRequest(IRequest request, ref string mimeType, ref Stream stream)
        {
            var uri = new Uri(request.Url);
            string file = uri.AbsolutePath;
            if (file == "/") file = "locales/" + uri.Host + ".html";
            else file = "locales" + file;

            if (File.Exists(file))
            {
                string ext = Path.GetExtension(file);
                mimeType = MimeTypeMap.GetMimeType(ext);

                stream = new FileStream(file, FileMode.Open);
                return true;
            }

            return false;
        }
    }
}
