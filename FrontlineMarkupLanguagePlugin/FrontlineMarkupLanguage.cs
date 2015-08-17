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

        /// <summary>
        /// Convert via syntax (id,created,employee(id,firstname,employeeType(id), lastname),location) to indented list.
        /// </summary>
        /// <param name="markupText"></param>
        /// <returns></returns>
        public string ParseMarkup(string markupText)
        {
            string result = "Could not parse markup.  Please try again.";

            // The MarkupText itself must be a grouping.
            Grouping grouping;
            if (Grouping.TryParseGrouping(markupText, out grouping))
            {
                result = grouping.ToString();
            }

            return result;
        }
    }
}
