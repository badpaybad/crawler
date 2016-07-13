using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using badpaybad.Scraper.DTO;

namespace badpaybad.Scraper.Utils
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public static class Extensions
    {
        private const string InvalidSegmentNames = @"[^a-zA-Z0-9\-]+";
        private static readonly Regex RegexInvalidSegmentNames = new Regex(InvalidSegmentNames, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static string ToUrlSegment(this string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;
            var res = RegexInvalidSegmentNames.Replace(source, "-");
            while (res.IndexOf("--") >= 0)
            {
                res = res.Replace("--", "-");
            }
            return res;
        }

        private const string PathOnDisk = @"[^a-zA-Z0-9\-/]+";
        private static readonly Regex RegexPathOnDisk = new Regex(PathOnDisk, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static string UrlToFilePath(this Link source)
        {
            if (source == null || string.IsNullOrEmpty(source.Uri)) return string.Empty;

            return source.Uri.ToUrlSegment();
        }

        public static string UrlToFilePath(this string source)
        {
            if (source == null || string.IsNullOrEmpty(source)) return string.Empty;
            var temp = source;
            var res = RegexPathOnDisk.Replace(temp, "-");
            while (res.IndexOf("--") >= 0)
            {
                res = res.Replace("--", "-");
            }
            while (res.IndexOf("//") >= 0)
            {
                res = res.Replace("//", "/");
            }
            return res;
        }

        public static int UrlToHashCode(this string source)
        {
            return source.GetHashCode();
        }
    }
}
