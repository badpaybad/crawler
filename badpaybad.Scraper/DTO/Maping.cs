using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using badpaybad.Scraper.Utils;

namespace badpaybad.Scraper.DTO
{
    [Serializable]
    public class Maping : IDisposable
    {
        public string FieldInDb { get; set; }
        public string ElementById { get; set; }
        /// <summary>
        /// can combine TagName, ClassName, Index
        /// </summary>
        public string ElementByTagName { get; set; }
        public string ElementByClassName { get; set; }
        public int ElementByIndex { get; set; }

        public string ExtractedContent { get; set; }

        public string Extract(string source)
        {
            var temp = string.IsNullOrEmpty(ElementById) ? "" : HtmlExtractor.ContentByIdOrName(ElementById, source);

            if (string.IsNullOrEmpty(temp) && !string.IsNullOrEmpty(ElementByTagName) && !string.IsNullOrEmpty(ElementByClassName))
            {
                temp = HtmlExtractor.ContentByTagAndClassNameAndIndex(ElementByTagName, ElementByClassName, source,
                                                                      ElementByIndex);
            }
            if (string.IsNullOrEmpty(temp) && !string.IsNullOrEmpty(ElementByTagName))
            {
                temp = HtmlExtractor.ContentByTagNameAndIndex(ElementByTagName, ElementByIndex, source);
            }
            if (string.IsNullOrEmpty(temp) && !string.IsNullOrEmpty(ElementByClassName))
            {
                temp = HtmlExtractor.ContentByClassNameAndIndex(ElementByTagName, source, ElementByIndex);
            }
            if (string.IsNullOrEmpty(temp))
            {
                temp = HtmlExtractor.ContentByTagNameAndIndex("body", ElementByIndex, source);
            }
            ExtractedContent = temp;
            return temp;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", FieldInDb, ElementById,
               ElementByTagName, ElementByClassName, ElementByIndex, ExtractedContent);
        }

        public void Dispose()
        {

        }
    }
}
