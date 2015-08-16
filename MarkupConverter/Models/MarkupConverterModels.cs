using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarkupConverterWeb.Models
{
    public class MarkupConverterModel
    {
        public IList<string> Languages { get; private set; }

        public MarkupConverterModel()
        {
            Languages = new List<string>();
        }
    }

    public class MarkupConverterResultModel
    {
        public string Result { get; set; }
    }
}