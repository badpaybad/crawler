using badpaybad.Scraper.Repository;
using badpaybad.Scraper.Services;

namespace badpaybad.Scraper.DTO
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public class File : ScraperObject
    {
        private Link _onwer;
        private string _pathOnDisk;
        private string _pathOnWeb;
        private bool? _downloadInprogress;
        private bool? _downloadCompleted;
        private bool? _mergerComplete;
        private bool? _downloadError;
        private string _extension;
        private string _contentType;
        private string _href;


        public File()
        {
            //_onwer = new Link();
            //_pathOnDisk = "";
            //_pathOnWeb = "";
            _downloadInprogress = false;
            _downloadCompleted = false;
            _mergerComplete = false;
            _downloadError = false;
            //_extension = "";
            //_contentType = "";
        }

        public int Id { get; set; }
        public Link Owner
        {
            get
            {
                //if (_onwer == null || _onwer.Id == 0)
                //{
                //    LazyLoadInternal();
                //}
                //   LazyLoad();
                return _onwer;
            }
            set { _onwer = value; }
        }

        public string Href
        {
            get { return _href; }
            set { _href = value; }
        }

        public string PathOnDisk
        {
            get
            {
                //if (string.IsNullOrEmpty(_pathOnWeb))
                //{
                //    LazyLoadInternal();
                //}
                //   LazyLoad();
                return _pathOnDisk;

            }
            set { _pathOnDisk = value; }
        }
        public string PathOnWeb
        {
            get
            {
                //if (string.IsNullOrEmpty(_pathOnWeb))
                //{
                //    LazyLoadInternal();
                //}
                //  LazyLoad();
                return _pathOnWeb;
            }
            set { _pathOnWeb = value; }
        }

        public string Extension
        {
            get
            {
                //if (string.IsNullOrEmpty(_pathOnWeb))
                //{
                //    LazyLoadInternal();
                //}
                //  LazyLoad();
                return _extension;
            }
            set { _extension = value; }
        }

        public string ContentType
        {
            get
            {
                //if (string.IsNullOrEmpty(_pathOnWeb))
                //{
                //    LazyLoadInternal();
                //}
                //    LazyLoad();
                return _contentType;
            }
            set { _contentType = value; }
        }

        public bool DownloadInprogress
        {
            get
            {
                //if (_downloadInprogress == null)
                //{
                //    LazyLoadInternal();
                //}
                //    LazyLoad();
                return (bool)_downloadInprogress;
            }
            set { _downloadInprogress = value; }
        }

        public bool DownloadError
        {
            get
            {
                //if (_downloadError == null)
                //{
                //    LazyLoadInternal();
                //}
                //      LazyLoad();
                return (bool)_downloadError;
            }
            set { _downloadError = value; }
        }

        public bool DownloadCompleted
        {
            get
            {
                //if (_downloadCompleted == null)
                //{
                //    LazyLoadInternal();
                //}
                //    LazyLoad();
                return (bool)_downloadCompleted;
            }
            set { _downloadCompleted = value; }
        }
        public bool MergerCompleted
        {
            get
            {
                //if (_mergerComplete == null)
                //{
                //    LazyLoadInternal();
                //}
                //     LazyLoad();
                return (bool)_mergerComplete;
            }
            set { _mergerComplete = value; }
        }

        protected override bool LazyLoadInternal()
        {
            // if (this.Id == 0) return false;
            var f = RepositoryContainer.FileRepository.GetInfo(this.Id);
            if (f == null)
            {
                _onwer = new Link();
                _pathOnDisk = "";
                _pathOnWeb = "";
                _downloadInprogress = false;
                _downloadCompleted = false;
                _mergerComplete = false;
                _downloadError = false;
                _extension = "";
                _contentType = "";
                return false;
            }
            else
            {
                this._onwer = f.Owner;
                _pathOnDisk = f.PathOnDisk;
                _pathOnWeb = f.PathOnWeb;
                _downloadCompleted = f.DownloadCompleted;
                _mergerComplete = f.MergerCompleted;
                _downloadError = f.DownloadError;
                _contentType = f.ContentType;
                _extension = f.Extension;
                _href = f.Href;
                return true;
            }

        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Id, _pathOnWeb, Href);
        }
    }
}