﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkupConverterServiceApi;

namespace MarkupConverterServiceLocal
{
    public class MarkupConverterServiceLocal : IMarkupConverterService
    {
        private List<IMarkupLanguage> languages = new List<IMarkupLanguage>();

        public MarkupConverterServiceLocal()
        {

        }

        /*
        public MarkupConverterServiceLocal(IMarkupLanguage[] languages)
        {
            this.languages.AddRange(languages);
        }
        */

        IList<string> IMarkupConverterService.GetLanguages()
        {
            IList<string> languagesAsStrings = new List<string>();

            this.languages.ForEach(x => languagesAsStrings.Add(x.GetLanguage()));

            return languagesAsStrings;
        }

        string IMarkupConverterService.ParseMarkup(string markupLanguage, string markupText)
        {
            return "hard-coded result";
        }

        /// <summary>
        /// Converting to markup is not yet supported.
        /// </summary>
        /// <param name="markupLanguage"></param>
        /// <param name="markupText"></param>
        /// <returns></returns>
        string IMarkupConverterService.ConvertToMarkup(string markupLanguage, string markupText)
        {
            throw new NotImplementedException();
        }
    }
}
