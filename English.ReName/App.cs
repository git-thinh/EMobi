using EnglishModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace English.ReName
{
    class App
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

        static void Main(string[] args)
        {
            Console.Title = "Move & Rename MP3";

            var arr = new oSource[] { };
            string file = "source.json";
            if (File.Exists(file))
            {
                try
                {
                    arr = JsonConvert.DeserializeObject<oSource[]>(File.ReadAllText(file));
                }
                catch { return; }
            }
            if (arr.Length == 0) return;

            string[] types = arr
                .Where(x => x.types.Contains("MP3"))
                .Select(x => string.Format("{0}.{1}", x.id, x.name))
                .ToArray();

            if (types.Length == 0) return;
            Console.WriteLine(string.Join(Environment.NewLine, types));
            Console.WriteLine(Environment.NewLine);

            Console.Write("Input Path: ");
            string path = Console.ReadLine();
            
            if (!Directory.Exists(path)) return;

            Console.Write("Choose Type: ");
            string stype = Console.ReadLine();
            Console.WriteLine(Environment.NewLine);

            int type = 0;
            if (int.TryParse(stype, out type) && type > 0)
            {
                var a = Directory.GetFiles(path, "*.mp3");
                var a2 = a.Select(x => Path.GetFileName(x))
                .Select(x => x.Substring(0, x.Length - 4))
                .Select(x => string.Format("{0}\\{1}#{2}.mp3", oPATH.PATH_MP3_WORD, x, type))
                .ToArray();

                for (var i = 0; i < a.Length; i++)
                {
                    File.Copy(a[i], a2[i], true);
                    Console.WriteLine("MOVE [" + i.ToString() + "|" + a2.Length.ToString() + "] -> " + a2[i]);
                }

                //Console.WriteLine(string.Join(Environment.NewLine, a2));
                Console.Read();
            }

            //string s = "[{&#034;word&#034;:&#034;abet&#034;,&#034;hw&#034;:true,&#034;freq&#034;:0.2846189650896845,&#034;ffreq&#034;:1.579110690973208,&#034;size&#034;:12,&#034;type&#034;:0},{&#034;word&#034;:&#034;abetment&#034;,&#034;hw&#034;:true,&#034;parent&#034;:&#034;abet&#034;,&#034;freq&#034;:0.013537168375252533,&#034;ffreq&#034;:0.014214026794015159,&#034;size&#034;:2,&#034;type&#034;:3},{&#034;word&#034;:&#034;abets&#034;,&#034;parent&#034;:&#034;abet&#034;,&#034;freq&#034;:0.03519663777565658,&#034;ffreq&#034;:0.03519663777565658,&#034;size&#034;:1,&#034;type&#034;:1},{&#034;word&#034;:&#034;abettal&#034;,&#034;hw&#034;:true,&#034;parent&#034;:&#034;abet&#034;,&#034;freq&#034;:3.3842920938131335E-4,&#034;ffreq&#034;:3.3842920938131335E-4,&#034;size&#034;:1,&#034;type&#034;:3},{&#034;word&#034;:&#034;abetted&#034;,&#034;parent&#034;:&#034;abet&#034;,&#034;freq&#034;:0.5161045443065028,&#034;ffreq&#034;:0.5161045443065028,&#034;size&#034;:1,&#034;type&#034;:1},{&#034;word&#034;:&#034;abetter&#034;,&#034;hw&#034;:true,&#034;parent&#034;:&#034;abet&#034;,&#034;freq&#034;:0.008460730234532832,&#034;ffreq&#034;:0.01929046493473486,&#034;size&#034;:2,&#034;type&#034;:2},{&#034;word&#034;:&#034;abetting&#034;,&#034;parent&#034;:&#034;abet&#034;,&#034;freq&#034;:0.3377523509625507,&#034;ffreq&#034;:0.3377523509625507,&#034;size&#034;:1,&#034;type&#034;:1},{&#034;word&#034;:&#034;abettor&#034;,&#034;hw&#034;:true,&#034;parent&#034;:&#034;abet&#034;,&#034;freq&#034;:0.11472750198026521,&#034;ffreq&#034;:0.3712568426913007,&#034;size&#034;:2,&#034;type&#034;:5},{&#034;word&#034;:&#034;abetments&#034;,&#034;parent&#034;:&#034;abetment&#034;,&#034;freq&#034;:6.768584187626267E-4,&#034;ffreq&#034;:6.768584187626267E-4,&#034;size&#034;:1,&#034;type&#034;:1},{&#034;word&#034;:&#034;abetters&#034;,&#034;parent&#034;:&#034;abetter&#034;,&#034;freq&#034;:0.010829734700202027,&#034;ffreq&#034;:0.010829734700202027,&#034;size&#034;:1,&#034;type&#034;:1},{&#034;word&#034;:&#034;abettors&#034;,&#034;parent&#034;:&#034;abettor&#034;,&#034;freq&#034;:0.2565293407110355,&#034;ffreq&#034;:0.2565293407110355,&#034;size&#034;:1,&#034;type&#034;:1}]";
            //string s2 = System.Web.HttpUtility.HtmlDecode(s);
        }
    }
}
