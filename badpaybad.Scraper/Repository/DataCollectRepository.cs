using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using badpaybad.Scraper.DTO;
using badpaybad.Scraper.Services;
using badpaybad.Scraper.Utils;

namespace badpaybad.Scraper.Repository
{
    public class DataCollectRepository : IDataCollectRepository
    {
        static object _sych = new object();
        static readonly List<ExtractedInfo> _data = new List<ExtractedInfo>();
        static readonly Dictionary<int, ExtractedInfo> _files = new Dictionary<int, ExtractedInfo>();

        static string _root { get { return DownloaderAbstract.RootDir; } }

        private static readonly string _indexedDir = "DataCollectIndexed/";
        private static object _lockDisk = new object();

        static DataCollectRepository()
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
                    var item = new ExtractedInfo();
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

        public bool IsExits(ExtractedInfo data)
        {
            lock (_sych)
            {
                return _files.ContainsKey(data.Id);
            }
        }

        public void Add(ExtractedInfo data)
        {
            var id = data.Owner.Uri.UrlToHashCode();
            data.Id = id;
            if (!_files.ContainsKey(id))
            {
                lock (_sych)
                {
                    if (!_files.ContainsKey(id))
                    {
                        _data.Add(data);
                        _files.Add(id, data);
                    }
                }
                SaveToDisk(data);
            }
           
        }

        public void AddOrUpdate(ExtractedInfo data)
        {
            var id = data.Owner.Uri.UrlToHashCode();
            data.Id = id;
            if (!_files.ContainsKey(id))
            {
                lock (_sych)
                {
                    if (!_files.ContainsKey(id))
                    {
                        _data.Add(data);
                        _files.Add(id, data);
                    }
                }


                SaveToDisk(data);
            }
            else
            {
                Update(data);
            }
        }

        void SaveToDisk(ExtractedInfo doc)
        {
            new ThreadSafe(() =>
            {
                lock (_lockDisk)
                {

                    var saveTo = _indexedDir + doc.Id.ToString() + ".ddat";
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



        public void Update(ExtractedInfo data)
        {
            lock (_sych)
            {
                var f = _data.FirstOrDefault(i => i.Id == data.Id);
                if (f != null)
                {
                    f.Description = data.Description;
                    f.Owner = data.Owner;
                    f.IsComplete = data.IsComplete;
                    f.IsError = data.IsError;
                    f.MapingResult = data.MapingResult;
                    _files[data.Id] = f;
                }
            }
            SaveToDisk(data);
        }

        public List<ExtractedInfo> SelectAll()
        {
            List<ExtractedInfo> temp;
            lock (_sych)
            {
                temp = _data.ToList();
            }
            return temp;
        }

        public ExtractedInfo Select(int id)
        {
            ExtractedInfo xxx = null;
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

        public ExtractedInfo GetInfo(int id)
        {
            try
            {
                if (id == 0) return null;

                var file = _indexedDir + id.ToString() + ".ddat";
                var content = "";
                using (var sr = new StreamReader(file))
                {
                    content = sr.ReadToEnd();
                    sr.Close();
                }

                return string.IsNullOrEmpty(content) ? null :
                    new JavaScriptSerializer().Deserialize<ExtractedInfo>(content);
            }
            catch { return null; }
        }
    }
}