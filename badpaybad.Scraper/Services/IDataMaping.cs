using System;
using badpaybad.Scraper.DTO;

namespace badpaybad.Scraper.Services
{
    public interface IDataMaping:IDisposable
    {
        event DataCollect DataMapingComplete;
        void Init(Config config);
        void Do(Link link);
        void Do(File file);
    }
}