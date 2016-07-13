using System;
using System.Collections.Generic;
using badpaybad.Scraper.DTO;

namespace badpaybad.Scraper.Services
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public interface IScraperManager:IDisposable
    {
        void Init();
        void Add(IScraper scraper,Config config);
        void Remove(Guid scraperId);
        void Start(Guid scraperId);
        void Stop(Guid scraperId);
        void StartAll();
        void StopAll();
        List<IScraper> GetAll();
        void CleanBeforeDispose();

        event Action<IScraper> Added; 
        event Action<IScraper> Started;
        event Action<IScraper> Stoped;
        event Action<Reports> PushReport;
        event Action<string> ParseCurrentUrl;
        event Action<IScraperManager> RefreshInterval;
    }
}