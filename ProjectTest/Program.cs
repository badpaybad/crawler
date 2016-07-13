using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using badpaybad.Scraper.Utils;

namespace ProjectTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += OnThreadException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if(e!=null && e.ExceptionObject!=null)
            LogError.Write((Exception) e.ExceptionObject, "ThreadException");
        }

        private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if(e!=null)
            LogError.Write(e.Exception,"ThreadException");
        }
    }
}
