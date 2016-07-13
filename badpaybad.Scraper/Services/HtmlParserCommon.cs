using System.Collections.Generic;
using badpaybad.Scraper.DTO;
using badpaybad.Scraper.Utils;

namespace badpaybad.Scraper.Services
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public sealed class HtmlParserCommon : HtmlParserAbstract
    {
        

        public override List<Link> ParseUrls(string htmlSource)
        {
            var domain = HtmlExtractor.GetDomainName(this.Link.Uri);
            var regxInclude = "[^>]*" + domain + "[^>]*";

            var temp = HtmlExtractor.UrlCollection(regxInclude, "", htmlSource, this.Link.Uri);
            var res = new List<Link>();
            foreach (var t in temp)
            {
                res.Add(new Link()
                            {
                                Deep = 0,
                                ParseCompleted = false,
                                Uri = t,
                                UriParent = this.Link.Uri,
                                ParseInprogress = false,
                                Id = t.UrlToHashCode()
                            });
            }
            return res;
        }

        public override List<File> ParseFiles(string htmlSource)
        {
            var domain = HtmlExtractor.GetDomainName(this.Link.Uri);
            var regxInclude = "[^>]*" + domain + "[^>]*";

            var temp = HtmlExtractor.UrlCollectionWithHref(regxInclude, "", htmlSource, this.Link.Uri);
            var res = new List<File>();
            foreach (var t in temp)
            {
                res.Add(new File()
                            {
                                DownloadCompleted = false,
                                MergerCompleted = false,
                                PathOnDisk = "",
                                PathOnWeb = t.Key,
                                Href = t.Value,
                                DownloadInprogress = false,
                                Id = t.Key.UrlToHashCode()
                            });
            }
            var src = HtmlExtractor.UrlImgCollectionWithSrc(htmlSource, this.Link.Uri);
            foreach (var s in src)
            {
                res.Add(new File()
                            {
                                DownloadCompleted = false,
                                MergerCompleted = false,
                                PathOnDisk = "",
                                PathOnWeb = s.Key,
                                Href = s.Value,
                                DownloadInprogress = false,
                                Id = s.Key.UrlToHashCode()
                            });
            }
            return res;
        }
    }
}