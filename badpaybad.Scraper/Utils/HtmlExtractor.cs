using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace badpaybad.Scraper.Utils
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public sealed class HtmlExtractor
    {
        private const string HttpPatten = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";

        public void Dispose()
        {
        }

        public static HtmlDocument CreateDocumentByUrl(string url)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.Load(Request(url));
            return htmlDocument;
        }

        public static HtmlDocument CreateDocumentByHtml(string source)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(source);
            return htmlDocument;
        }

        public static string ContentByTagNameAndIndex(string tagName, int index, string source)
        {
            var contentByTagName = ContentByTagName(tagName, source);
            if (contentByTagName.Count < 1) return "";
            return contentByTagName[index];
        }

        public static string ContentByIdOrName(string idOrName, string source)
        {
            try
            {
                idOrName = TagClassNameUrlNomalization(idOrName);
                var res = "";
                var doc = CreateDocumentByHtml(source);
                HtmlNode elementbyId = doc.GetElementbyId(idOrName);
                if (elementbyId == null)
                {
                    var nodes = doc.DocumentNode.Descendants();
                    foreach (var htmlNode in nodes)
                    {
                        if (htmlNode.Attributes["name"].Value.ToLower() == idOrName.ToLower()
                            || htmlNode.Attributes["id"].Value.ToLower() == idOrName.ToLower())
                        {
                            elementbyId = htmlNode;
                            break;
                        }
                    }
                }

                res = elementbyId.InnerHtml;
                return res;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string SrcImgCollectionAndIndex(string source, string urlContainDomain, int index)
        {
            var srcImgCollection = UrlImgCollection(source, urlContainDomain);
            if (srcImgCollection.Count < 1) return "";
            return srcImgCollection[index];
        }

        public static string ContentByClassNameAndIndex(string className, string source, int index)
        {
            var contentByClassName = ContentByClassName(className, source);
            if (contentByClassName.Count < 1) return "";
            return contentByClassName[index];
        }

        public static string ContentByTagAndClassNameAndIndex(string tagName, string className, string source, int index)
        {
            var contentByTagAndClassName = ContentByTagAndClassName(tagName, className, source);
            if (contentByTagAndClassName.Count < 1) return "";
            return contentByTagAndClassName[index];
        }

        public static string UrlCollectionAndIndex(string regxInclude, string regxExclude, string source,
                                                   string urlContainDomain, int index)
        {
            var urlCollection = UrlCollection(regxInclude, regxExclude, source, urlContainDomain);
            if (urlCollection.Count < 1) return "";
            return urlCollection[index];
        }

        public static List<string> ContentByTagName(string tagName, string source)
        {
            tagName = TagClassNameUrlNomalization(tagName);
            var res = new List<string>();
            var doc = CreateDocumentByHtml(source);
            var nodes = doc.DocumentNode.Descendants(tagName);
            foreach (var n in nodes)
            {
                res.Add(n.InnerHtml);
            }

            return res;
        }

        public static List<string> ContentByName(string name, string source)
        {
            name = TagClassNameUrlNomalization(name);
            var res = new List<string>();
            var doc = CreateDocumentByHtml(source);
            var nodes = doc.DocumentNode.Descendants().ToList();

            foreach (var n in nodes)
            {
                if (n.HasAttributes && n.Attributes["name"] != null)
                {
                    if (n.Attributes["name"].Value.ToLower().Equals(name.ToLower()))
                    {
                        res.Add(n.InnerHtml);
                    }
                }
            }

            return res;
        }

        public static List<string> ContentByClassName(string className, string source)
        {
            className = TagClassNameUrlNomalization(className);
            var res = new List<string>();
            var doc = CreateDocumentByHtml(source);
            var nodes = doc.DocumentNode.Descendants().ToList();

            foreach (var n in nodes)
            {
                if (n.HasAttributes && n.Attributes["class"] != null)
                {
                    if (n.Attributes["class"].Value.ToLower().Equals(className.ToLower()))
                    {
                        res.Add(n.InnerHtml);
                    }
                }
            }

            return res;
        }

        public static List<string> ContentByTagAndClassName(string tagName, string className, string source)
        {
            tagName = TagClassNameUrlNomalization(tagName);
            className = TagClassNameUrlNomalization(className);
            var res = new List<string>();
            var doc = CreateDocumentByHtml(source);
            var nodes = doc.DocumentNode.Descendants(tagName);

            foreach (var n in nodes)
            {
                if (n.HasAttributes && n.Attributes["class"] != null)
                {
                    if (n.Attributes["class"].Value.ToLower().Equals(className.ToLower()))
                    {
                        res.Add(n.InnerHtml);
                    }
                }
            }

            return res;
        }

        public static List<KeyValuePair<string, string>> UrlCollectionWithHref(string regxInclude, string regxExclude, string source,
                                                 string urlContainDomain)
        {
            urlContainDomain = TagClassNameUrlNomalization(urlContainDomain);
            var res = new List<KeyValuePair<string, string>>();
            var doc = CreateDocumentByHtml(source);
            var nodes = doc.DocumentNode.Descendants("a");
            foreach (var re in nodes)
            {
                if (re.HasAttributes && re.Attributes["href"] != null)
                {
                    var href = re.Attributes["href"].Value;
                    var realLink = GetRealLink(href, urlContainDomain);
                    if (IsValidUrl(realLink, regxInclude, regxExclude))
                    {
                        res.Add(new KeyValuePair<string, string>(realLink, href));
                    }
                }
            }

            return res;
        }

        public static List<string> UrlCollection(string regxInclude, string regxExclude, string source,
                                                 string urlContainDomain)
        {
            urlContainDomain = TagClassNameUrlNomalization(urlContainDomain);
            var res = new List<string>();
            var doc = CreateDocumentByHtml(source);
            var nodes = doc.DocumentNode.Descendants("a");
            foreach (var re in nodes)
            {
                if (re.HasAttributes && re.Attributes["href"] != null)
                {
                    var realLink = GetRealLink(re.Attributes["href"].Value, urlContainDomain);
                    if (IsValidUrl(realLink, regxInclude, regxExclude))
                    {
                        res.Add(realLink);
                    }
                }
            }

            return res;
        }

        public static List<string> FullContentByTagAndClassName(string tagName, string className, string source)
        {
            tagName = TagClassNameUrlNomalization(tagName);
            className = TagClassNameUrlNomalization(className);
            var res = new List<string>();
            var doc = CreateDocumentByHtml(source);
            var nodes = doc.DocumentNode.Descendants(tagName);

            foreach (var n in nodes)
            {
                if (n.HasAttributes && n.Attributes["class"] != null)
                {
                    if (n.Attributes["class"].Value.ToLower().Equals(className.ToLower()))
                    {
                        res.Add(n.OuterHtml);
                    }
                }
            }

            return res;
        }

        public static List<string> FullContentByTagNameAndStyle(string tagName, string style, string source)
        {
            tagName = TagClassNameUrlNomalization(tagName);
            style = TagClassNameUrlNomalization(style);
            var res = new List<string>();
            var doc = CreateDocumentByHtml(source);
            var nodes = doc.DocumentNode.Descendants(tagName);
            string styelLower = style.Trim().ToLower();
            foreach (var n in nodes)
            {
                if (n.HasAttributes && n.Attributes["style"] != null)
                {

                    if (n.Attributes["style"].Value.ToLower().Trim().Equals(styelLower))
                    {
                        res.Add(n.OuterHtml);
                    }
                }
            }

            return res;
        }

        public static List<string> FullContentByTagName(string tagName, string source)
        {
            tagName = TagClassNameUrlNomalization(tagName);
            var res = new List<string>();
            var doc = CreateDocumentByHtml(source);
            var nodes = doc.DocumentNode.Descendants(tagName);
            foreach (var n in nodes)
            {
                res.Add(n.OuterHtml);
            }

            return res;
        }

        public static string FullContentByIdOrName(string idOrName, string source)
        {
            try
            {
                idOrName = TagClassNameUrlNomalization(idOrName);
                var res = "";
                var doc = CreateDocumentByHtml(source);
                HtmlNode elementbyId = doc.GetElementbyId(idOrName);
                if (elementbyId == null)
                {
                    var nodes = doc.DocumentNode.Descendants();
                    foreach (var htmlNode in nodes)
                    {
                        if (htmlNode.Attributes["name"].Value.ToLower() == idOrName.ToLower()
                            || htmlNode.Attributes["id"].Value.ToLower() == idOrName.ToLower())
                        {
                            elementbyId = htmlNode;
                            break;
                        }
                    }
                }
                res = elementbyId.OuterHtml;
                return res;
            }
            catch (Exception ex)
            {
                string e = ex.ToString();
                return "";
            }
        }
     
        public static List<KeyValuePair<string,string>> UrlImgCollectionWithSrc(string source, string urlContainDomain)
        {
            urlContainDomain = TagClassNameUrlNomalization(urlContainDomain);

            var res = new List<KeyValuePair<string,string>>();
            var doc = CreateDocumentByHtml(source);
            var nodes = doc.DocumentNode.Descendants("img");
            foreach (var re in nodes)
            {
                var src = re.Attributes["src"];
                if (re.HasAttributes && src != null)
                {
                    var realLink = GetRealLink(src.Value.ToString(), urlContainDomain);
                    res.Add(new KeyValuePair<string, string>(realLink,src.Value));
                }
            }
            return res;
        }

        public static List<string> UrlImgCollection(string source, string urlContainDomain)
        {
            urlContainDomain = TagClassNameUrlNomalization(urlContainDomain);

            List<string> res = new List<string>();
            var doc = CreateDocumentByHtml(source);
            var nodes = doc.DocumentNode.Descendants("img");
            foreach (var re in nodes)
            {
                if (re.HasAttributes && re.Attributes["src"] != null)
                {
                    res.Add(GetRealLink(re.Attributes["src"].Value.ToString(), urlContainDomain));
                }
            }
            return res;
        }

        public static List<string> SrcOnlyImgCollection(string source)
        {
            List<string> res = new List<string>();
            var doc = CreateDocumentByHtml(source);
            var nodes = doc.DocumentNode.Descendants("img");
            foreach (var re in nodes)
            {
                if (re.HasAttributes && re.Attributes["src"] != null)
                {
                    res.Add(re.Attributes["src"].Value.ToString());
                }
            }
            return res;
        }



        public static string HtmlSource(string url)
        {
            url = TagClassNameUrlNomalization(url);
            var request = new HttpRequest(url);
            return request.HtmlByGet();

        }

        public static Stream Request(string url)
        {
            url = TagClassNameUrlNomalization(url);
            var request = new HttpRequest(url);
            return request.DataByGet();
        }

        public static string GetDomainName(string url)
        {
            url = TagClassNameUrlNomalization(url);
            var regUrl = new Regex(HttpPatten);

            if (regUrl.IsMatch(url))
            {
                var arr = url.Split('/');
                url = arr[2].Trim('/');
            }
            else
            {
                url = "";
            }

            return url;
        }

        public static string GetRealLink(string href, string initLink)
        {
            var url = TagClassNameUrlNomalization(href);
            href = href.Replace("&amp;", "&");
            href = href.Trim('.');
            href = href.Trim('/');

            var regUrl = new Regex(HttpPatten);
            try
            {
                if (!regUrl.IsMatch(href))
                {
                    var arr = initLink.Split('/');

                    href = href.Replace("about", "").Replace(":", "");
                    href = (arr[0] + "//" + arr[2]).Trim('/') + "/" + href.Trim().Trim('/');
                    if (!regUrl.IsMatch(href))
                    {
                        href = "";
                    }
                }
            }
            finally
            {
                regUrl = null;
            }
            url = href;
            return url;
        }

        public static bool IsValidUrl(string url, string regxInclude, string regxExclude)
        {
            var res = false;
            var link = TagClassNameUrlNomalization(url);
            var regInclude = new Regex(regxInclude, RegexOptions.IgnoreCase);
            var regExclude = new Regex(regxExclude, RegexOptions.IgnoreCase);
            try
            {
                var mc = regInclude.Matches(link);
                if (mc.Count > 0)
                {
                    res = true;
                }
            }
            finally
            {
                regInclude = null;
                regExclude = null;
            }
            return res;
        }

        private static string TagClassNameUrlNomalization(string src)
        {
            src = src ?? "";
            char[] spl = { '\r', '\n', '\t' };
            src = src.Trim(spl);
            return src;
        }

        public static string DomainName(string initLink)
        {
            var regUrl = new Regex(HttpPatten);

            if (regUrl.IsMatch(initLink))
            {
                var arr = initLink.Split('/');
                initLink = arr[2].Trim('/');
            }
            else
            {
                initLink = "";
            }

            return initLink;
        }

        public static string RemoveHTML(string src)
        {
            //var reg = new Regex("<[^>]+>");
            //src = reg.Replace(src, "");
            var stripTagsHtml = StripTagsHtml(src);
            stripTagsHtml = Regex.Replace(stripTagsHtml, "&[0-9a-z#]{1,10};", " ");
            stripTagsHtml = Regex.Replace(stripTagsHtml, @"\s{2,}", " ");

            return stripTagsHtml.Trim();
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        static string StripTagsHtml(string source)
        {
            var array = new char[source.Length];
            var arrayIndex = 0;
            var inside = false;

            for (var i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}