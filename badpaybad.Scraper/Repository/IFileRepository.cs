using System.Collections.Generic;
using badpaybad.Scraper.DTO;

namespace badpaybad.Scraper.Repository
{
    public interface IFileRepository
    {
        bool IsExits(File file);
        void Add(File file);
        File GetFileToDownload();
        void Update(File file);
        List<File> SelectAll();
        File Select(int id);
        void DeleteAll();
        File GetInfo(int id);
        List<File> SelectByOwner(Link owner);
    }
}