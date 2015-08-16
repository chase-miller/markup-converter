using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkupConverterServiceApi;

namespace FrontlineMarkupLanguagePlugin
{
    public class FrontlineMarkupLanguage : IMarkupLanguage
    {
        public string ConvertToMarkup(string markupText)
        {
            throw new NotImplementedException();
        }

        public string GetLanguage()
        {
            return "Frontline Markup";
        }

        public string ParseMarkup(string markupText)
        {
            return "hard-coded parsing";
        }
    }
}
