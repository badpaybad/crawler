using badpaybad.Scraper.Repository;

namespace badpaybad.Scraper.DTO
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public class Link : ScraperObject
    {
        private string _uri;
        private string _uriParent;
        private int? _deep;
        private bool? _parseCompleted;
        private bool? _parseInprogress;
        private bool? _parseFail;
        private int? _parseTimes;
        private bool _extractInprogress;
        private bool _extractSuccess;
        private bool _extractFail;


        public Link()
        {
            //_uri = "";
            //_uriParent = "";
            _deep = 0;
            _parseCompleted = false;
            _parseInprogress = false;
            //_uriParent = "";
            _parseFail = false;
            _parseTimes = 0;
        }

        public int Id { get; set; }

        public int ParseTimes
        {
            get
            {
                //if (_parseTimes == null)
                //{ LazyLoadInternal(); }
                //  LazyLoad();
                return (int)_parseTimes;
            }
            set { _parseTimes = value; }
        }

        public string Uri
        {
            get
            {
                //  if (string.IsNullOrEmpty(_uri)) { LazyLoadInternal(); }
                //   LazyLoad();
                return _uri;
            }
            set { _uri = value; }
        }

        public string UriParent
        {
            get
            {
                // if (string.IsNullOrEmpty(_uri)) { LazyLoadInternal(); }
                // LazyLoad();
                return _uriParent;
            }
            set { _uriParent = value; }
        }

        public bool ParseCompleted
        {
            get
            {
                // if (_parseCompleted == null) { LazyLoadInternal(); }
                //   LazyLoad();
                return (bool)_parseCompleted;
            }
            set { _parseCompleted = value; }
        }

        public int Deep
        {
            get
            {
                //if (_deep == null) { LazyLoadInternal(); }
                //   LazyLoad();
                return (int)_deep;
            }
            set { _deep = value; }
        }

        public bool ParseFail
        {
            get
            {
                //if (_parseFail == null)
                //{
                //    LazyLoadInternal(LazyLoadInternal);
                //}
                //   LazyLoad();
                return (bool)_parseFail;
            }
            set { _parseFail = value; }
        }

        public bool ParseInprogress
        {
            get
            {
                //if (_parseInprogress == null) { LazyLoadInternal(); }
                //     LazyLoad();
                return (bool)_parseInprogress;
            }
            set { _parseInprogress = value; }
        }

        public bool ExtractInprogress
        {
            get { return _extractInprogress; }
            set { _extractInprogress = value; }
        }

        public bool ExtractSuccess
        {
            get { return _extractSuccess; }
            set { _extractSuccess = value; }
        }

        public bool ExtractFail
        {
            get { return _extractFail; }
            set { _extractFail = value; }
        }

        protected override bool LazyLoadInternal()
        {
            // if(this.Id==0) return false;
            var f =RepositoryContainer.LinkRepository.GetInfo(this.Id);
            if (f == null)
            {
                _uri = "";
                _uriParent = "";
                _deep = 0;
                _parseCompleted = false;
                _parseInprogress = false;
                _uriParent = "";
                _parseFail = false;
                _parseTimes = 0;
                return false;
            }
            else
            {
                _deep = f.Deep;
                _parseCompleted = f.ParseCompleted;
                _parseInprogress = f.ParseInprogress;
                _uri = f.Uri;
                _uriParent = f.UriParent;
                _parseFail = f.ParseFail;
                _parseTimes = f.ParseTimes;
                _extractFail = f.ExtractFail;
                _extractInprogress = f.ExtractInprogress;
                _extractSuccess = f.ExtractSuccess;
                return true;
            }

        }
        public override string ToString()
        {
            return string.Format("{0} {1}", Id, _uri);
        }
    }
}
