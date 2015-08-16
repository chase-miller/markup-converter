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
            return this.parseMarkupHelper(markupText);
        }

        /// <summary>
        /// Convert via syntax (id,created,employee(id,firstname,employeeType(id), lastname),location) to indented list.
        /// </summary>
        /// <param name="markupText"></param>
        /// <returns></returns>
        private string parseMarkupHelper(string markupText)
        {
            string result = "Could not parse markup.  Please try again.";

            if (this.isValidMarkup(markupText))
            {

            }

            return result;
        }

        /// <summary>
        /// Checks whether the supplied markup text is valid.  Specifically, does it have proper matching parenthesis? 
        /// </summary>
        /// <param name="markupText"></param>
        /// <returns></returns>
        private bool isValidMarkup(string markupText)
        {
            if (!markupText.StartsWith("("))
            {
                return false;
            }
            if (!markupText.EndsWith(")"))
            {
                return false;
            }

            return true;
        }
    }
}
