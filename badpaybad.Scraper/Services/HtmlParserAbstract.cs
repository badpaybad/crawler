using System.Collections.Generic;
using System.Net;
using badpaybad.Scraper.DTO;
using badpaybad.Scraper.Utils;
using System.Linq;

namespace badpaybad.Scraper.Services
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public abstract class HtmlParserAbstract : IHtmlParser
    {
        private HttpRequest _request;
        private string _htmlSource;
        private Link _link;
        public Link Link { get { return _link; } private set { _link = value; } }
        public string HtmlSource { get { return _htmlSource; } }
        private Config _config;
        public void Init(Config config, HttpRequest request)
        {
            _config = config;
            _request = request;
        }

        bool IsValidExtension(string url, out string extension, out string contenttype)
        {
            extension = Files.GetFileExtension(url, "").ToLower();
            contenttype = "";
            if (_config.IncludeExtensions == null || _config.IncludeExtensions.Count == 0) return true;

            if (!string.IsNullOrEmpty(extension) && _config.IncludeExtensions.Contains(extension))
            {
                return true;
            }
            else
            {
                //string ct;
                //HttpWebResponse webResponse;
                //var httpRequest = new HttpRequest(url);
                //var x = httpRequest.TryParseResponseHeader(out webResponse, out ct);
                //if (x)
                //{
                //    contenttype = ct;
                //    extension = DownloaderAbstract.GetFileExtension(url, contenttype);
                //    if (_config.IncludeExtensions.Contains(extension.ToLower()))
                //    {
                //        return true;
                //    }
                //}
                //httpRequest.TryAbort();
            }

            return false;
        }

        public void Parse(Link link)
        {
            try
            {
                link.Id = link.Uri.UrlToHashCode();
                _link = link;

                Link.ParseInprogress = true;
                Link.ParseCompleted = false;
                ParseStarted(this);

                _htmlSource = _request.CreateNew(link.Uri).WithCookies(_request.CookieCollection).HtmlByGet();

                #region detect links
                new ThreadSafe(() =>
                {
                    var urls = ParseUrls(_htmlSource);
                    var validUrls = new List<Link>();
                    foreach (var l in urls)
                    {
                        l.ParseInprogress = false;
                        l.Deep = link.Deep + 1;
                        l.ParseCompleted = false;
                        l.UriParent = this.Link.Uri;
                        l.Id = l.Uri.UrlToHashCode();
                        if (l.Deep <= _config.Deep
                            && validUrls.FirstOrDefault(k => k.Id == l.Id) == null)
                        {
                            validUrls.Add(l);
                        }
                    }
                    GetLinksCompleted(this, validUrls);
         
                }).Start();
                #endregion

                #region detect files

                new ThreadSafe(() =>
                    {

                        var files = ParseFiles(_htmlSource);
                        var validFiles = new List<File>();
                        foreach (var f in files)
                        {
                            f.PathOnDisk = "";
                            f.DownloadCompleted = false;
                            f.MergerCompleted = false;
                            f.Owner = this.Link;
                            f.DownloadInprogress = false;
                            f.Id = f.PathOnWeb.UrlToHashCode();
                            string contentype = "";
                            string extension = "";
                            if (validFiles.FirstOrDefault(i => i.Id == f.Id) == null
                                && IsValidExtension(f.PathOnWeb, out extension, out contentype))
                            {
                                validFiles.Add(f);
                            }
                            f.ContentType = contentype;
                            f.Extension = extension;
                        }

                        GetFilesCompleted(this, validFiles);

                    }).Start();

                #endregion

                if (!string.IsNullOrEmpty(this.HtmlSource))
                {
                    Link.ParseInprogress = false;
                    Link.ParseCompleted = true;
                }
                else
                {
                    Link.ParseFail = true;
                }
                ParseCompleted(this);
             }
            catch
            {
                Link.ParseFail = true;
                ParseCompleted(this);
            }
        }

        public event FoundLinks GetLinksCompleted;
        public event FoundFiles GetFilesCompleted;
        public event ParseEvent ParseStarted;
        public event ParseEvent ParseCompleted;

        public abstract List<Link> ParseUrls(string htmlSource);
        public abstract List<File> ParseFiles(string htmlSource);

        public void Dispose()
        {

        }
    }
}