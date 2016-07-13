using System.Collections.Generic;
using badpaybad.Scraper.DTO;
using badpaybad.Scraper.Utils;

namespace badpaybad.Scraper.Services
{
    public delegate void FoundLinks(IHtmlParser sender, List<Link> list);

    public delegate void FoundFiles(IHtmlParser sender, List<File> list);

    public delegate void ParseEvent(IHtmlParser parser);

    public delegate void FileDownload(IDownloader sender,File file);

    public delegate void DataCollect(IDataMaping sender, Link owner, Document doc, List<File> files, List<Maping> mapeds );
}
