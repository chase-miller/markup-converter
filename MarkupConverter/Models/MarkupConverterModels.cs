using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarkupConverterWeb.Models
{
    public class MarkupConverterModel
    {
        /// <summary>
        /// The list of all languages available.
        /// </summary>
        public IList<string> Languages { get; private set; }

        public string MarkupText { get; set; }

        /// <summary>
        /// The user-selected markup language.
        /// </summary>
        public string MarkupLanguage { get; set; }

        /// <summary>
        /// The markup converted to standard output.
        /// </summary>
        public string ConvertedMarkup { get; set; }

        public MarkupConverterModel()
        {
            Languages = new List<string>();
        }
    }
}