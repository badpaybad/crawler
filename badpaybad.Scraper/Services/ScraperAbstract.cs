using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using badpaybad.Scraper.DTO;
using badpaybad.Scraper.Repository;
using badpaybad.Scraper.Utils;
using System.Linq;

namespace badpaybad.Scraper.Services
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public abstract class ScraperAbstract : IScraper
    {
        public Guid Id { get; set; }

        public event Action<string> CurrentParseUrl;
        public event Action<Reports> PushReport;
        public event Action<IScraper> StartComplete;
        public event Action<IScraper> StopComplete;

      
        private Config _config;
        private bool _isStop = true;
        Random _rnd = new Random();
        static ScraperAbstract()
        {
        
        }
        public ScraperAbstract()
        {

        }

        public void Init(Config config)
        {
            _config = config;
            _config.IncludeExtensions =
                config.IncludeExtensions.Select(i => i.Trim()).Where(i => !string.IsNullOrEmpty(i)).ToList();
        }

        public void Start()
        {
            if (_isStop)
            {
                _isStop = false;
                _config.InitLink.Id = _config.InitLink.Uri.UrlToHashCode();
                RepositoryContainer.LinkRepository.Add(_config.InitLink);

                LoopThreadDownloadFile();
                LoopThreadParseLinks();
                LoopThreadPushReport();
                LoopThreadDataMaping();
                StartComplete(this);
            }
        }

        public void Stop()
        {
            if (!_isStop)
            {
                _isStop = true;
                StopComplete(this);
            }
        }

        #region thread loop
        private void LoopThreadParseLinks()
        {
            new ThreadSafe(() =>
                               {
                                   while (!_isStop)
                                   {
                                       var l = RepositoryContainer.LinkRepository.GetLinkToParse();
                                       if (l != null && !string.IsNullOrEmpty(l.Uri))
                                       {
                                           IHtmlParser parser = new HtmlParserCommon();
                                           parser.GetFilesCompleted += OnGetFilesCompleted;
                                           parser.GetLinksCompleted += OnGetLinksCompleted;
                                           parser.ParseStarted += OnParseStarted;
                                           parser.ParseCompleted += OnParseCompleted;
                                           parser.Init(_config, new HttpRequest(l.Uri));
                                           parser.Parse(l);
                                       }
                                       ThreadSafe.Sleep(_rnd.Next(1000, 10000));
                                   }
                               }).Start();
        }

        private void LoopThreadDownloadFile()
        {
            new ThreadSafe(() =>
                               {
                                   while (!_isStop)
                                   {
                                       var f = RepositoryContainer.FileRepository.GetFileToDownload();
                                       if (f != null && !string.IsNullOrEmpty(f.PathOnWeb))
                                       {
                                           IDownloader downloader = new DownloaderCommon();
                                           downloader.DownloadComplete += OnDownloadComplete;
                                           downloader.DownloadInprogress += OnDownloadInprogress;
                                           downloader.Init(_config, new HttpRequest(f.PathOnWeb));
                                           downloader.DownloadFile(f);
                                       }
                                       ThreadSafe.Sleep(_rnd.Next(1000, 10000));
                                   }
                               }).Start();
        }

        private void LoopThreadPushReport()
        {
            new ThreadSafe(() =>
            {
                while (!_isStop)
                {
                    
                    var docs = RepositoryContainer.DocumentRepository.SelectAll();
                    var files = RepositoryContainer.FileRepository.SelectAll();
                    var links = RepositoryContainer.LinkRepository.SelectAll();
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
        }

        private void LoopThreadDataMaping()
        {
            new ThreadSafe(() =>
                               {
                                   while (!_isStop)
                                   {
                                       var link = RepositoryContainer.LinkRepository.GetLinkToExtract();
                                       if (link != null)
                                       {
                                           IDataMaping dataMaping = new DataMapingCommon();
                                           dataMaping.DataMapingComplete += OnDataMapingComplete;
                                           dataMaping.Init(_config);
                                           dataMaping.Do(link);
                                       }
                                       ThreadSafe.Sleep(_rnd.Next(1000, 10000));
                                   }
                               }).Start();
        }

        private void OnDataMapingComplete(IDataMaping sender, Link owner, Document doc, List<File> files, List<Maping> mapeds)
        {
            if (!string.IsNullOrEmpty(doc.HtmlSource))
            {
                var extractedInfo = new ExtractedInfo();
                extractedInfo.Owner = owner;
                extractedInfo.MapingResult = mapeds;
                RepositoryContainer.DataCollectRepository.AddOrUpdate(extractedInfo);
                if (!string.IsNullOrEmpty(_config.PostAndSaveDataToUrl))
                {
                    new ThreadSafe(() =>
                    {
                        string serialize = new JavaScriptSerializer().Serialize(extractedInfo);
                        //var postBuffer = Encoding.UTF8.GetBytes(serialize);
                       // var str64 = Convert.ToBase64String(postBuffer);
                        var test = new HttpRequest(_config.PostAndSaveDataToUrl).HtmlByPostJson(
                            serialize);
                      
                    }).Start();
                }
            }
        }

        #endregion
        #region for downloader

        private void OnDownloadInprogress(IDownloader sender, File file)
        {
            RepositoryContainer.FileRepository.Update(file);
        }

        private void OnDownloadComplete(IDownloader sender, File file)
        {
            new ThreadSafe(() =>
                {
                    RepositoryContainer.FileRepository.Update(file);
                    //if(file.DownloadCompleted)
                    //{
                    //    IDataMaping dataMaping = new DataMapingCommon();
                    //    dataMaping.DataMapingComplete += OnDataMapingComplete;
                    //    dataMaping.Init(_config);
                    //    dataMaping.Do(file);
                    //}
                }).Start();
        }
        #endregion
        #region for document
        private void OnParseStarted(IHtmlParser parser)
        {
            CurrentParseUrl(parser.Link.Uri);
            RepositoryContainer.LinkRepository.Update(parser.Link);
        }

        private void OnParseCompleted(IHtmlParser parser)
        {
            new ThreadSafe(() =>
            {
                if (parser.Link.ParseCompleted)
                {
                    var doc = new Document()
                                  {
                                      Id = parser.Link.Id,
                                      HtmlSource = parser.HtmlSource,
                                      Owner = parser.Link,
                                      MergerCompleted = false,
                                      SaveCompleted = true,
                                  };
                    if (!RepositoryContainer.DocumentRepository.IsExits(doc))
                    {
                        RepositoryContainer.DocumentRepository.Add(doc);
                    }
                }
                RepositoryContainer.LinkRepository.Update(parser.Link);
               
            }).Start();
        }
        #endregion

        #region for parser detect links and files
        private void OnGetLinksCompleted(IHtmlParser sender, List<Link> list)
        {
            new ThreadSafe(() =>
            {
                foreach (var url in list)
                {
                    if (!RepositoryContainer.LinkRepository.IsExist(url))
                    {
                        RepositoryContainer.LinkRepository.Add(url);
                    }
                }
            }).Start();
        }

        private void OnGetFilesCompleted(IHtmlParser sender, List<File> list)
        {
            new ThreadSafe(() =>
            {
                foreach (var f in list)
                {
                    if (!RepositoryContainer.FileRepository.IsExits(f))
                    {
                        RepositoryContainer.FileRepository.Add(f);
                    }
                }
            }).Start();
        }
        #endregion

        public void Dispose()
        {

        }

        public override string ToString()
        {
            return string.Format("Stop: {0}; Identify: {1} {2}", _isStop, Id, _config.InitLink);
        }
    }
}