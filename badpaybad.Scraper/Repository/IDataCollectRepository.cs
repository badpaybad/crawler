using System.Collections.Generic;
using System.Linq;
using System.Text;
using badpaybad.Scraper.DTO;

namespace badpaybad.Scraper.Repository
{
    public interface IDataCollectRepository
    {
        bool IsExits(ExtractedInfo data);
        void Add(ExtractedInfo data);
        void AddOrUpdate(ExtractedInfo data);
        void Update(ExtractedInfo data);
        List<ExtractedInfo> SelectAll();
        ExtractedInfo Select(int id);
        void DeleteAll();
        ExtractedInfo GetInfo(int id);
       
    }

}
