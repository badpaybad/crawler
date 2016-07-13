using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using badpaybad.Scraper.Services;

namespace badpaybad.Scraper.Utils
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public sealed class LogError
    {
        private static string _root = DownloaderAbstract.RootDir;
        private static string _logDir;
        static object _lockDisk=new object();
        static LogError()
        {
            _logDir = _root.Trim('/') + "/LogError";
            if (!Directory.Exists(_logDir))
                Directory.CreateDirectory(_logDir);
        }

        public static void Write(Exception ex, string message)
        {
            lock (_lockDisk)
            {
                var file = _logDir.Trim('/') + "/" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".log";
                using (var sw = new StreamWriter(file, true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine(ex.Message);
                    sw.WriteLine(message);
                    sw.WriteLine(ex.StackTrace);
                    sw.Flush();
                    sw.Close();
                }
            }

        }
    }
}
