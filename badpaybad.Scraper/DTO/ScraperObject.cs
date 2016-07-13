using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using badpaybad.Scraper.Utils;

namespace badpaybad.Scraper.DTO
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    [Serializable]
    public abstract class ScraperObject : IDisposable
    {
        object _sych = new object();
        private bool _isLoaded = false;

        public void LazyLoad()
        {
            //some thing wrong here
            //must solve 
           // new ThreadSafe(() =>
            // {
                 lock (_sych)
                 {
                     if (!_isLoaded)
                     {
                         _isLoaded = LazyLoadInternal();
                     }
                 }
           //  }).Start();
        }

        protected abstract bool LazyLoadInternal();

        public virtual void Dispose()
        {

        }
    }
}
