using System.Collections.Generic;
using badpaybad.Scraper.DTO;

namespace badpaybad.Scraper.Repository
{
    public interface ILinkRepository
    {
        bool IsExist(Link link);
        void Add(Link link);
        Link GetLinkToParse();
        Link GetLinkToExtract();
        void Update(Link link);
        List<Link> SelectAll();
        Link Select(int id);
        void DeleteAll();
        Link GetInfo(int id);
    }
}