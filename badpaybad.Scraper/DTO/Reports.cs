using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace badpaybad.Scraper.DTO
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public class Reports :IDisposable
    {
       public int TotalLinksFound { get; set; }
       public int TotalLinksDownloaded { get; set; }
       public int TotalLinksFail { get; set; }
       public int TotalDocDownloaded { get; set; }
       public int TotalFilesFound { get; set; }
       public int TotalFilesDownloaded { get; set; }
       public int TotalFilesFail { get; set; }

       public override string ToString()
       {
           var x = string.Format("TotalLinksFound: {0}\r\nTotalLinksDownloaded: {1}\r\nTotalLinksFail: {2}\r\n\r\nTotalDocDownloaded: {3}\r\n\r\n"
               + "TotalFilesFound: {4}\r\nTotalFilesDownloaded: {5}\r\nTotalFilesFail: {6}"
               , TotalLinksFound,TotalLinksDownloaded,TotalLinksFail,TotalDocDownloaded,TotalFilesFound, TotalFilesDownloaded,TotalFilesFail);
           return x;
       }

        public void Dispose()
        {
            
        }
    }
}
