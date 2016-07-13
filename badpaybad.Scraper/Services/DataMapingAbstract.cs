using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using badpaybad.Scraper.DTO;
using badpaybad.Scraper.Repository;
using badpaybad.Scraper.Utils;

namespace badpaybad.Scraper.Services
{
    public abstract class DataMapingAbstract : IDataMaping
    {
        private bool _isStop = true;
        private Config _config;
        private List<Maping> _maps;


        public void Init(Config config)
        {
            _config = config;
            _maps = config.Maps.ToList();
        }

        public void Do(Link link)
        {
            var doc = RepositoryContainer.DocumentRepository.SelectByOnwer(link);
            var files = RepositoryContainer.FileRepository.SelectByOwner(link);

            if (doc != null && files != null && !string.IsNullOrEmpty(doc.HtmlSource))
            {
                Merge(doc, files);
                foreach (var m in _config.Maps)
                {
                    m.Extract(doc.HtmlSource);
                }
                DataMapingComplete(this, link, doc, files, _maps);
            }
        }

        public void Do(File file)
        {
            if (file == null || file.Owner == null) return;

            var link = file.Owner;
            var doc = RepositoryContainer.DocumentRepository.SelectByOnwer(link);
            if (doc != null && !string.IsNullOrEmpty(doc.HtmlSource))
            {
                var files = new List<File>() { file };
                Merge(doc, files);
                foreach (var m in _maps)
                {
                    m.Extract(doc.HtmlSource);
                }
                DataMapingComplete(this, link, doc, files, _maps);
            }
        }


        private void Merge(Document doc, List<File> files)
        {
            if (string.IsNullOrEmpty(doc.HtmlSource) || files == null || files.Count == 0) return;
            foreach (var f in files)
            {
                if (!string.IsNullOrEmpty(f.Href) && !string.IsNullOrEmpty(f.PathOnDisk))
                {
                    //while (doc.HtmlSource.IndexOf(f.Href) > 0)
                    //{
                    //doc.HtmlSource = doc.HtmlSource.Replace(f.Href, f.PathOnDisk);
                    // }
                    var reg = new Regex(f.Href, RegexOptions.IgnoreCase);
                    doc.HtmlSource = reg.Replace(doc.HtmlSource, f.PathOnDisk);
                }

            }
        }

        public void Dispose()
        {

        }

        public event DataCollect DataMapingComplete;
    }
}
