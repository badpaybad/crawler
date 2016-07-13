using System.Collections.Generic;
using badpaybad.Scraper.DTO;

namespace badpaybad.Scraper.Repository
{
    public interface IDocumentRepository
    {
        bool IsExits(Document doc);
        void Add(Document doc);
        List<Document> SelectAll();
        Document Select(int id);
        void DeleteAll();
        Document GetInfo(int id);
        Document SelectByOnwer(Link owner);
    }
}