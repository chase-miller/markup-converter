﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupConverterServiceApi
{
    public interface IMarkupLanguage
    {
        string GetLanguage();

        string ParseMarkup(string markupText);

        string ConvertToMarkup(string markupText);
    }
}
