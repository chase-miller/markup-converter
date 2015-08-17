using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontlineMarkupLanguagePlugin
{
    internal class Element
    {
        /// <summary>
        /// If this element is a string, this will be populated.
        /// </summary>
        internal string ElementString { get; set; }

        /// <summary>
        /// Otherwise, if this element is a grouping, this will be populated.
        /// </summary>
        internal Grouping ElementGrouping { get; set; }

        private int depthLevel = 0;

        public Element (int depthLevel)
        {
            this.depthLevel = depthLevel;
        }

        public override string ToString()
        {
            string result = string.Empty;

            string prepender = this.GeneratePrepender();

            if (!string.IsNullOrEmpty(this.ElementString))
            {
                result = prepender + this.ElementString;
            }
            else
            {
                result = prepender + this.ElementGrouping.ToString();
            }

            return result;
        }

        /// <summary>
        /// Generates text that is appended to the line.  The generated text depends on the depth level.
        /// </summary>
        /// <returns></returns>
        private string GeneratePrepender()
        {
            StringBuilder appender = new StringBuilder();

            for (int i = 1; i < this.depthLevel; i++)
            {
                appender.Append("-");
            }

            if (this.depthLevel > 1)
            {
                appender.Append(" ");
            }

            return appender.ToString();
        }
    }
}
