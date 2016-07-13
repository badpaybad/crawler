using System;
using System.Net;
using badpaybad.Scraper.DTO;
using badpaybad.Scraper.Utils;

namespace badpaybad.Scraper.Services
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public interface IHtmlParser:IDisposable
    {
        Link Link { get; }
        string HtmlSource { get; }
        void Init(Config config, HttpRequest request);
        void Parse(Link link);
        event FoundLinks GetLinksCompleted;
        event FoundFiles GetFilesCompleted;
        event ParseEvent ParseStarted;
        event ParseEvent ParseCompleted;
    }
}