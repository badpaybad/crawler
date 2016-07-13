using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using badpaybad.Scraper.Repository;

namespace badpaybad.Scraper.DTO
{
    public class ExtractedInfo : ScraperObject
    {
        private Link _owner;
        private string _description;
        private bool _isComplete;
        private bool _isError;
        private List<Maping> _mapingResult;

        public ExtractedInfo()
        {
            _owner=new Link();
            _mapingResult=new List<Maping>();
        }

        public int Id { get; set; }
        public Link Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public bool IsComplete
        {
            get { return _isComplete; }
            set { _isComplete = value; }
        }

        public bool IsError
        {
            get { return _isError; }
            set { _isError = value; }
        }

        public List<Maping> MapingResult
        {
            get { return _mapingResult; }
            set { _mapingResult = value; }
        }

        protected override bool LazyLoadInternal()
        {
            // if (this.Id == 0) return false;
            var f = RepositoryContainer.DataCollectRepository.GetInfo(this.Id);
            if (f == null)
            {
                _owner = new Link();
                _description = "";

                return false;
            }
            else
            {
                this._owner = f.Owner;
                _description = f.Description;
                _isComplete = f.IsComplete;
                _isError = f.IsError;
                _mapingResult = f.MapingResult;

                return true;
            }
        }
    }
}
