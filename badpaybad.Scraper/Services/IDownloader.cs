using System;
using badpaybad.Scraper.DTO;
using badpaybad.Scraper.Utils;

namespace badpaybad.Scraper.Services
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public interface IDownloader:IDisposable
    {
        void Init(Config config,HttpRequest request);
        string DownloadHtml(Link link);
        void DownloadFile(File file);
        event FileDownload DownloadComplete;
        event FileDownload DownloadInprogress;
        
    }
}