using badpaybad.Scraper.Repository;

namespace badpaybad.Scraper.DTO
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public class Document : ScraperObject
    {
        private Link _owner;
        private string _htmlSource;
        private bool? _saveCompleted;
        private bool? _mergerCompleted;


        public Document()
        {
            //_owner = new Link();
            //_htmlSource = "";
            _saveCompleted = false;
            _mergerCompleted = false;
        }

        public int Id { get; set; }

        public Link Owner
        {
            get
            {
                //if (_owner == null || _owner.Id == 0) { LazyLoadInternal(); }
                // LazyLoad();
                return _owner;
            }
            set { _owner = value; }
        }

        public string HtmlSource
        {
            get
            {
                //if (string.IsNullOrEmpty(_htmlSource)) { LazyLoadInternal(); }
                //  LazyLoad();
                return _htmlSource;
            }
            set { _htmlSource = value; }
        }

        public bool SaveCompleted
        {
            get
            {
                //if (_saveCompleted == null) { LazyLoadInternal(); }
                //  LazyLoad();
                return (bool)_saveCompleted;
            }
            set { _saveCompleted = value; }
        }

        public bool MergerCompleted
        {
            get
            {
                //if (_mergerCompleted == null)
                //{
                //    LazyLoadInternal();
                //}
                //  LazyLoad();
                return (bool)_mergerCompleted;
            }
            set { _mergerCompleted = value; }
        }
        public override string ToString()
        {
            return string.Format("{0} {1}", Id, _owner);
        }

        protected override bool LazyLoadInternal()
        {
            //  if (this.Id == 0) return false;

            var f = RepositoryContainer.DocumentRepository.GetInfo(this.Id);
            if (f == null)
            {
                _owner = new Link();
                _htmlSource = "";
                _saveCompleted = false;
                _mergerCompleted = false;
                return false;
            }
            else
            {
                _owner = f.Owner;
                _htmlSource = f.HtmlSource;
                _mergerCompleted = f.MergerCompleted;
                _saveCompleted = f.SaveCompleted;
                return true;
            }

        }
    }
}