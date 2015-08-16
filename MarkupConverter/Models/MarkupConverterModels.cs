using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MarkupConverterWeb.Models
{
    public class MarkupConverterModel
    {
        /// <summary>
        /// The list of all languages available.
        /// </summary>
        [DisplayName("Languages")]
        public IList<string> Languages { get; private set; }

        [DisplayName("Markup Text")]
        public string MarkupText { get; set; }

        /// <summary>
        /// The user-selected markup language.
        /// </summary>
        [DisplayName("Lang")]
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