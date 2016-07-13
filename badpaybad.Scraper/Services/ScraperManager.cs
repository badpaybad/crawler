using System;
using System.Collections.Generic;
using System.Threading;
using badpaybad.Scraper.DTO;
using System.Linq;
using badpaybad.Scraper.Repository;
using badpaybad.Scraper.Utils;

namespace badpaybad.Scraper.Services
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public sealed class ScraperManager : IScraperManager
    {
        static readonly ILinkRepository _linkRepository;
        static readonly IFileRepository _fileRepository;
        static readonly IDocumentRepository _documentRepository;

        static List<IScraper> _workers = new List<IScraper>();
        static object _sych = new object();
        static Random _rnd = new Random();

        static ScraperManager()
        {
            _fileRepository = RepositoryContainer.FileRepository;
            _linkRepository = RepositoryContainer.LinkRepository;
            _documentRepository = RepositoryContainer.DocumentRepository;
        }

        public ScraperManager()
        {
        }

        private bool _isStop = false;
        private void LoopRefreshInterval()
        {
            new ThreadSafe(() =>
            {
                while (!_isStop)
                {
                    var docs = _documentRepository.SelectAll();
                    var files = _fileRepository.SelectAll();
                    var links = _linkRepository.SelectAll();
                    PushReport(new Reports()
                    {
                        TotalDocDownloaded = docs.Count(i => i.SaveCompleted),
                        TotalFilesDownloaded = files.Count(i => i.DownloadCompleted),
                        TotalFilesFound = files.Count,
                        TotalLinksDownloaded = links.Count(i => i.ParseCompleted),
                        TotalLinksFound = links.Count,
                        TotalLinksFail = links.Count(i => i.ParseFail),
                        TotalFilesFail = files.Count(i => i.DownloadError)
                    });
                    ThreadSafe.Sleep(_rnd.Next(5000, 10000));
                }
            }).Start();
            new ThreadSafe(() =>
               {
                   while (!_isStop)
                   {
                       RefreshInterval(this);
                       ThreadSafe.Sleep(_rnd.Next(5000, 10000));
                   }
               }).Start();
        }

        public void Init()
        {
            LoopRefreshInterval();
        }

        public void Add(IScraper scraper, Config config)
        {
            lock (_sych)
            {
                scraper.Id = Guid.NewGuid();
                scraper.Init(config);
                scraper.CurrentParseUrl += OnCurrentParseUrl;
                scraper.PushReport += OnPushReport;
                scraper.StartComplete += OnStartComplete;
                scraper.StopComplete += OnStopComplete;
                _workers.Add(scraper);
            }
            Added(scraper);
        }

        private void OnStopComplete(IScraper sender)
        {

            Stoped(sender);
        }

        private void OnStartComplete(IScraper sender)
        {
            Started(sender);
        }

        private void OnPushReport(Reports obj)
        {
            PushReport(obj);
        }

        private void OnCurrentParseUrl(string obj)
        {
            ParseCurrentUrl(obj);
        }

        public void Remove(Guid scraperId)
        {
            IScraper worker;
            lock (_sych)
            {
                worker = _workers.FirstOrDefault(i => i.Id == scraperId);
            }
            if (worker != null)
            {
                worker.Stop();
                lock (_sych)
                {
                    _workers.RemoveAll(i => i.Id == scraperId);
                }
            }
        }

        public void Start(Guid scraperId)
        {
            IScraper scraper;
            lock (_sych)
            {
                scraper = _workers.FirstOrDefault(i => i.Id == scraperId);
            }
            if (scraper != null) { scraper.Start(); }
        }

        public void Stop(Guid scraperId)
        {
            IScraper scraper;
            lock (_sych)
            {
                scraper = _workers.FirstOrDefault(i => i.Id == scraperId);
            }
            if (scraper != null) { scraper.Stop(); }
        }

        public void StartAll()
        {
            foreach (var scraper in _workers)
            {
                scraper.Start();
            }
        }

        public void StopAll()
        {
            foreach (var scraper in _workers)
            {
                scraper.Stop();
            }
        }

        public List<IScraper> GetAll()
        {
            List<IScraper> lst;
            lock (_sych)
            {
                lst = _workers.ToList();
            }
            return lst;
        }

        public void CleanBeforeDispose()
        {
            foreach (var scraper in _workers)
            {
                scraper.Stop();
            }
            _isStop = true;
        }

        public event Action<IScraper> Added;

        public event Action<IScraper> Started;
        public event Action<IScraper> Stoped;
        public event Action<Reports> PushReport;
        public event Action<string> ParseCurrentUrl;
        public event Action<IScraperManager> RefreshInterval;


        public void Dispose()
        {
            for (int i = 0; i < 30; i++)
            {
                if (!_isStop) ThreadSafe.Sleep(1000);
            }
        }
    }
}