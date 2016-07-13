using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using badpaybad.Scraper.DTO;
using badpaybad.Scraper.Utils;
using File = badpaybad.Scraper.DTO.File;
using HttpRequest = badpaybad.Scraper.Utils.HttpRequest;

namespace badpaybad.Scraper.Services
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public abstract class DownloaderAbstract : IDownloader,IDisposable
    {
        private HttpRequest _request;
        public static string RootDir
        {
            get
            {
                var temp = "";
                try
                {
                    if (HttpContext.Current != null && HttpContext.Current.Server != null)
                    {
                        temp = HttpContext.Current.Server.MapPath("~/");
                    }
                    if (string.IsNullOrEmpty(temp))
                    {
                        temp = AppDomain.CurrentDomain.BaseDirectory;
                    }
                }
                catch
                {
                    temp = "C:/";
                }
                return temp.Replace("\\", "/").Trim('/') + "/";
            }
        }

        private Config _config;

        static Dictionary<string, string> _mimeType = new Dictionary<string, string>();

        static DownloaderAbstract()
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

        public void Init(Config config,HttpRequest request)
        {
            _config = config;
            _request = request;
        }

        public string DownloadHtml(Link link)
        {
            return _request.CreateNew(link.Uri).HtmlByGet();
        }

        public void DownloadFile(File file)
        {
            file.DownloadCompleted = false;
            file.DownloadInprogress = true;
            DownloadInprogress(this, file);
            try
            {
                var req = new HttpRequest(file.PathOnWeb);
                var pathFile = "";
                HttpWebResponse response;
                Stream res = req.DownloadData(out response);
                if (response != null)
                {
                    var ext =Files.GetFileExtension(file.PathOnWeb, response.ContentType);
                    TryCreateDirPath(file.PathOnWeb, ext, out pathFile);
                    using (var f = System.IO.File.OpenWrite(pathFile))
                    {
                        CopyStream(res, f);
                        f.Flush();
                        f.Close();
                    }
                    file.PathOnDisk = pathFile;
                    file.DownloadCompleted = true;
                    file.DownloadInprogress = false;
                    req.TryAbort();
                    DownloadComplete(this, file);
                }
                else
                {
                    file.DownloadError = true;
                    DownloadComplete(this, file);
                }
            }
            catch
            {
                file.DownloadError = true;
                DownloadComplete(this, file);
            }
        }

        public static void TryCreateDirPath(string fileLink, string extension, out string fileSaveTo)
        {
            var dir = fileLink.UrlToFilePath().Trim('/');
            var x = dir.Split('/');
            x[x.Length - 1] = x[x.Length - 1] + extension;
            if (x.Length > 1)
            {
                var temp = x.Skip(0).Take(x.Length - 1).ToArray();
                var realDir = RootDir.Trim('/') + "/" + string.Join("/", temp).Trim('/');
                if (!Directory.Exists(realDir))
                {
                    Directory.CreateDirectory(realDir);
                }
            }
            fileSaveTo = RootDir.Trim('/') + "/" + string.Join("/", x).Trim('/');
        }

        //public static string GetFileExtension(string urlOrFilePath, string contentType)
        //{
        //    if (string.IsNullOrEmpty(urlOrFilePath)) return "";
        //    urlOrFilePath.Replace("\\", "/");
        //    var indexOf = contentType.IndexOf(";");
        //    if (indexOf > 0)
        //    {
        //        contentType = contentType.Substring(0, indexOf).Trim(new[] { ';', ' ', ',' });
        //    }
        //    var x = contentType.ToLower().Trim().Trim(new[] { ';', ' ', ',' });

        //    var ext = "";
        //    if (string.IsNullOrEmpty(x) || !_mimeType.TryGetValue(x, out ext))
        //    {
        //        var startIndex = urlOrFilePath.LastIndexOf(".");
        //        if (startIndex > 0)
        //        {
        //            var temp = urlOrFilePath.Substring(startIndex).Trim('.').Split(new[] { '.', '/', '#', '?' });
        //            ext = "." + temp[0];
        //        }
        //    }
        //    return ext;
        //}

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public event FileDownload DownloadComplete;
        public event FileDownload DownloadInprogress;
        public void Dispose()
        {
            
        }
    }
}
