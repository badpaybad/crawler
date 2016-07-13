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
    sealed class FileRepository : IFileRepository
    {
        static object _sych = new object();
        static readonly List<File> _data = new List<File>();
        static readonly Dictionary<int, File> _files = new Dictionary<int, File>();

        static string _root { get { return DownloaderAbstract.RootDir; } }

        private static readonly string _indexedDir = "FilesIndexed/";
        private static object _lockDisk = new object();

        static FileRepository()
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
                    var item = new File();
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

        public bool IsExits(File file)
        {
            lock (_sych)
            {
                return _files.ContainsKey(file.Id);
            }
        }

        public void Add(File file)
        {
            var id = file.PathOnWeb.UrlToHashCode();
            file.Id = id;
            if (!_files.ContainsKey(id))
            {
                lock (_sych)
                {
                    if (!_files.ContainsKey(id))
                    {
                        _data.Add(file);
                        _files.Add(id, file);
                    }
                }
                SaveToDisk(file);
            }

        }

        void SaveToDisk(File file)
        {
            new ThreadSafe(() =>
            {
                lock (_lockDisk)
                {
                    var saveTo = _indexedDir + file.Id.ToString() + ".dfil";
                    using (var sw = new StreamWriter(saveTo, false))
                    {
                        var serialize = new JavaScriptSerializer().Serialize(file);
                        sw.Write(serialize);
                        sw.Flush();
                        sw.Close();
                    }
                }
            }).Start();
        }


        public File GetFileToDownload()
        {
            lock (_sych)
            {
                return _data.FirstOrDefault(i => i.DownloadCompleted == false && i.DownloadInprogress == false && i.DownloadError == false);
            }
        }

        public void Update(File file)
        {
            lock (_sych)
            {
                var f = _data.FirstOrDefault(i => i.Id == file.Id);
                if (f != null)
                {
                    f.DownloadCompleted = file.DownloadCompleted;
                    f.DownloadInprogress = file.DownloadInprogress;
                    f.MergerCompleted = file.MergerCompleted;
                    f.Owner = file.Owner;
                    f.PathOnDisk = file.PathOnDisk;
                    f.PathOnWeb = file.PathOnWeb;
                    f.Href = file.Href;
                    _files[file.Id] = f;
                }
            }
            SaveToDisk(file);
        }

        public List<File> SelectAll()
        {
            List<File> temp;
            lock (_sych)
            {
                temp = _data.ToList();
            }
            return temp;
        }

        public File Select(int id)
        {
            File xxx = null;
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

        public  File GetInfo(int id)
        {
            try
            {
                if (id == 0) return null;

                var file = _indexedDir + id.ToString() + ".dfil";
                var content = "";
                using (var sr = new StreamReader(file))
                {
                    content = sr.ReadToEnd();
                    sr.Close();
                }

                return string.IsNullOrEmpty(content) ? null :
                    new JavaScriptSerializer().Deserialize<File>(content);
            }
            catch
            {
                return null;
            }
        }

        public List<File> SelectByOwner(Link owner)
        {
            if (owner == null || owner.Id == 0) return new List<File>();
            lock (_sych)
            {
                return _data.Where(i => i.Owner.Id == owner.Id).ToList();
            }
        }
    }
}