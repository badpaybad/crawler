using System;
using System.Collections.Generic;

namespace badpaybad.Scraper.DTO
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public class Config : IDisposable
    {
        public string PostAndSaveDataToUrl { get; set; }

        private Link _initLink;
        public Link InitLink
        {
            get { return _initLink; }
            set { _initLink = value; }
        }

        public int Deep { get; set; }

        public List<string> IncludeExtensions { get; set; }

        public List<Maping> Maps { get; set; }

        public Config()
        {
            IncludeExtensions = new List<string>();
            Maps = new List<Maping>();
        }

        public Config(bool withDefaultMaping)
        {
            IncludeExtensions = new List<string>();
            Maps = new List<Maping>();
            if (withDefaultMaping)
            {
                Maps = DefaultMaping();
            }
        }

        public void Dispose()
        {

        }

        public static List<Maping> DefaultMaping()
        {
            var m = new List<Maping>();
            m.Add(new Maping() { ElementByTagName = "title", FieldInDb = "title"});
            m.Add(new Maping() { ElementByTagName = "body", FieldInDb = "body"});
            return m;
        }



    }
}