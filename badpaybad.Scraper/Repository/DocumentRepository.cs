using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using badpaybad.Scraper.DTO;
using badpaybad.Scraper.Services;
using badpaybad.Scraper.Utils;
using File = badpaybad.Scraper.DTO.File;
using System.Linq;

namespace badpaybad.Scraper.Repository
{
    sealed class DocumentRepository : IDocumentRepository
    {
        static object _sych = new object();
        static readonly List<Document> _data = new List<Document>();
        static readonly Dictionary<int, Document> _files = new Dictionary<int, Document>();

        static string _root { get { return DownloaderAbstract.RootDir; } }

        private static readonly string _indexedDir = "DocumentsIndexed/";
        private static object _lockDisk = new object();

        static DocumentRepository()
        {
            _indexedDir = _root.Trim('/') + "/" + _indexedDir.Trim('/') + "/";
            if (!Directory.Exists(_indexedDir))
            {
                Directory.CreateDirectory(_indexedDir); 
            }
            new ThreadSafe(() =>
            {
                var files = Directory.GetFiles(_indexedDir);
                foreach (var f in files)
                {
                    var item = new Document();
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

        public bool IsExits(Document doc)
        {
            lock (_sych)
            {
                return _files.ContainsKey(doc.Id);
            }
        }

        public void Add(Document doc)
        {
            var id = doc.Owner.Uri.UrlToHashCode();
            doc.Id = id;
            if (!_files.ContainsKey(id))
            {
                lock (_sych)
                {
                    if (!_files.ContainsKey(id))
                    {
                        _data.Add(doc);
                        _files.Add(id, doc);
                    }
                }
                SaveToDisk(doc);
            }

        }

        void SaveToDisk(Document doc)
        {
            new ThreadSafe(() =>
            {
                lock (_lockDisk)
                {

                    var saveTo = _indexedDir + doc.Id.ToString() + ".dhtm";
                    using (var sw = new StreamWriter(saveTo, false))
                    {
                        var serialize = new JavaScriptSerializer().Serialize(doc);
                        sw.Write(serialize);
                        sw.Flush();
                        sw.Close();
                    }
                }
            }).Start();
        }


        public List<Document> SelectAll()
        {
            List<Document> temp;
            lock (_sych)
            {
                temp = _data.ToList();
            }
            return temp;
        }

        public Document Select(int id)
        {
            Document xxx = null;
            _files.TryGetValue(id, out xxx);
            return xxx;
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

        public  Document GetInfo(int id)
        {
            try
            {
                if (id == 0) return null;

                var file = _indexedDir + id.ToString() + ".dhtm";
                var content = "";
                using (var sr = new StreamReader(file))
                {
                    content = sr.ReadToEnd();
                    sr.Close();
                }

                return string.IsNullOrEmpty(content) ? null :
                    new JavaScriptSerializer().Deserialize<Document>(content);
            }
            catch { return null; }
        }

        public Document SelectByOnwer(Link owner)
        {
            if(owner==null || owner.Id==0) return new Document();
            lock (_sych)
            {
                return _data.FirstOrDefault(i => i.Owner.Id == owner.Id);
            }
        }
    }
}