using System;
using badpaybad.Scraper.DTO;

namespace badpaybad.Scraper.Services
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public interface  IScraper:IDisposable
    {
        Guid Id { get; set; }
      
        void Init(Config config);
        void Start();
        void Stop();
        event Action<string> CurrentParseUrl;
        event Action<Reports> PushReport;
        event Action<IScraper> StartComplete;
        event Action<IScraper> StopComplete;
    }
}