using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using badpaybad.Scraper.DTO;
using badpaybad.Scraper.Services;
using badpaybad.Scraper.Utils;
using File = badpaybad.Scraper.DTO.File;

namespace badpaybad.Scraper.Repository
{
    sealed class LinkRepository : ILinkRepository
    {
        static object _sych = new object();
        static object _lockDisk = new object();
        static readonly List<Link> _data = new List<Link>();
        static readonly Dictionary<int, Link> _files = new Dictionary<int, Link>();

        static string _root { get { return DownloaderAbstract.RootDir; } }

        private static readonly string _indexedDir = "LinksIndexed/";

        static LinkRepository()
        {
            _indexedDir = _root.Trim('/') + "/" + _indexedDir.Trim('/') + "/";
            if (!Directory.Exists(_indexedDir))
            {
                Directory.CreateDirectory(_indexedDir);
            }

            var files = Directory.GetFiles(_indexedDir);
            new ThreadSafe(() =>
            {
                foreach (var f in files)
                {
                    var item = new Link();
                    lock (_sych)
                    {
                        item.Id = Files.GetIdFromFileIndexed(f);
                        item.LazyLoad();
                        _data.Add(item);
                        _files.Add(item.Id, item);
                    }
                }
            }).Start();
        }

        public bool IsExist(Link link)
        {
            lock (_sych)
            {

                return _files.ContainsKey(link.Id);
            }
        }

        public void Add(Link link)
        {
            var id = link.Uri.UrlToHashCode();
            link.Id = id;
            if (!_files.ContainsKey(id))
            {
                lock (_sych)
                {
                    if (!_files.ContainsKey(id))
                    {
                        _files.Add(id, link);
                        _data.Add(link);
                    }
                }
                SaveToDisk(link);
            }

        }

        void SaveToDisk(Link link)
        {
            new ThreadSafe(() =>
                {
                    lock (_lockDisk)
                    {
                        var saveTo = _indexedDir + link.Id.ToString() + ".dlnk";
                        using (var sw = new StreamWriter(saveTo, false))
                        {
                            var serialize = new JavaScriptSerializer().Serialize(link);
                            sw.Write(serialize);
                            sw.Flush();
                            sw.Close();
                        }
                    }
                }).Start();
        }

        public Link GetLinkToParse()
        {
            lock (_sych)
            {
                var xx = _data.FirstOrDefault(i => i.ParseCompleted == false && i.ParseInprogress == false && i.ParseFail == false && i.ParseTimes <= 2);
                if (xx != null)
                {
                    xx.ParseTimes++;
                    Update(xx);
                }
                return xx;
            }
        }

        public Link GetLinkToExtract()
        {
            lock (_sych)
            {
                var xx = _data.FirstOrDefault(i => i.ExtractSuccess == false 
                    && i.ExtractInprogress == false && i.ExtractFail == false && i.ParseCompleted==true);
                if (xx != null)
                {
                    xx.ParseTimes++;
                    Update(xx);
                }
                return xx;
            }
        }

        public void Update(Link link)
        {
            lock (_sych)
            {
                var f = _data.FirstOrDefault(i => i.Id == link.Id);
                f.Deep = link.Deep;
                f.ParseCompleted = link.ParseCompleted;
                f.ParseInprogress = link.ParseInprogress;
                f.Uri = link.Uri;
                f.UriParent = link.UriParent;
                f.ExtractFail = link.ExtractFail;
                f.ExtractInprogress = link.ExtractInprogress;
                f.ExtractSuccess = link.ExtractSuccess;
                _files[link.Id] = f;
            }
            SaveToDisk(link);
        }

        public List<Link> SelectAll()
        {
            List<Link> temp;
            lock (_sych)
            {
                temp = _data.ToList();
            }
            return temp;
        }

        public Link Select(int id)
        {
            Link xxx = null;
            _files.TryGetValue(id, out xxx);
            return xxx;
        }

        public Link GetInfo(int id)
        {
            try
            {
                if (id == 0) return null;

                var file = _indexedDir + id.ToString() + ".dlnk";
                var content = "";
                using (var sr = new StreamReader(file))
                {
                    content = sr.ReadToEnd();
                    sr.Close();
                }

                return string.IsNullOrEmpty(content) ? null :
                    new JavaScriptSerializer().Deserialize<Link>(content);
            }
            catch
            {
                return null;
            }
        }

        public void DeleteAll()
        {
            lock (_sych)
            {

                _data.Clear();
                _files.Clear();
            }
            lock (_lockDisk)
            {

                Directory.Delete(_indexedDir, true);

                if (!Directory.Exists(_indexedDir))
                {
                    Directory.CreateDirectory(_indexedDir);
                }
            }
        }
    }
}