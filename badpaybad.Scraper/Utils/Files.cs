using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace badpaybad.Scraper.Utils
{
    public class Files
    {

        static Dictionary<string, string> _mimeType = new Dictionary<string, string>();

        static Files()
        {
            var s = CommonResouces.mimetype.Split('\n');

            foreach (var m in s)
            {
                var arr = m.Trim(new[] { '\r', '\n', ' ' }).ToLower().Split(' ');
                if (arr.Length > 1)
                {
                    var k = arr[1].Trim();
                    var v = arr[0].Trim();
                    if (!_mimeType.ContainsKey(k))
                    {
                        _mimeType.Add(k, v);
                    }
                }
            }

        }

        public static int GetIdFromFileIndexed(string file)
        {
            if (string.IsNullOrEmpty(file)) return 0;
            file = file.Replace("\\", "/");
            var arr = file.Split('/');
            var fn = arr[arr.Length - 1].Split('.');
            int id=0;
            int.TryParse(fn[0], out id);
            return id;
        }

        public static string GetFileExtension(string urlOrFilePath, string contentType)
        {
            if (string.IsNullOrEmpty(urlOrFilePath)) return "";
            urlOrFilePath.Replace("\\", "/");
            var indexOf = contentType.IndexOf(";");
            if (indexOf > 0)
            {
                contentType = contentType.Substring(0, indexOf).Trim(new[] { ';', ' ', ',' });
            }
            var x = contentType.ToLower().Trim().Trim(new[] { ';', ' ', ',' });

            var ext = "";
            if (string.IsNullOrEmpty(x) || !_mimeType.TryGetValue(x, out ext))
            {
                var startIndex = urlOrFilePath.LastIndexOf(".");
                if (startIndex > 0)
                {
                    var temp = urlOrFilePath.Substring(startIndex).Trim('.').Split(new[] { '.', '/', '#', '?' });
                    ext = "." + temp[0];
                }
            }
            return ext;
        }
    }
}
