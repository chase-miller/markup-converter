using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupConverterServiceApi
{
    public interface IMarkupConverterService
    {
        IList<string> GetLanguages();

        string ParseMarkup(string markupLanguage, string markupText);

        string ConvertToMarkup(string markupLanguage, string markupText);
    }
}
