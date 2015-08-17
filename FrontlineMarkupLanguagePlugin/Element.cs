using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontlineMarkupLanguagePlugin
{
    /// <summary>
    /// Represents an item.  May or may not have child groupings.
    /// </summary>
    internal class Element : IComparable
    {
        private string elementAsString;
        private int depthLevel = 0;

        private Grouping childGrouping;

        public Element (string elementAsString, int depthLevel)
        {
            this.elementAsString = elementAsString;
            this.depthLevel = depthLevel;
        }

        /// <summary>
        /// Attempts to add a child grouping based on the provided string.
        /// </summary>
        /// <param name="childGroupingAsString"></param>
        public void AddChildGrouping(string childGroupingAsString)
        {
            Grouping grouping;
            if (Grouping.TryParseGrouping(childGroupingAsString, this.depthLevel + 1, out grouping))
            {
                this.childGrouping = grouping;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = this.GeneratePrepender();

            builder.Append(this.elementAsString).AppendLine();

            if (this.childGrouping != null)
            {
                builder.Append(this.childGrouping.ToString());
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates text that is appended to the line.  The generated text depends on the depth level.
        /// </summary>
        /// <returns></returns>
        private StringBuilder GeneratePrepender()
        {
            StringBuilder appender = new StringBuilder();

            for (int i = 0; i < this.depthLevel; i++)
            {
                appender.Append("-");
            }

            if (this.depthLevel > 0)
            {
                appender.Append(" ");
            }

            return appender;
        }

        public int CompareTo(object obj)
        {
            int result = 0;

            if (obj is Element)
            {
                Element objectAsElement = (Element)obj;

                string thisString = this.elementAsString.Replace(" ", "");
                string thatString = objectAsElement.elementAsString.Replace(" ", "");

                result = thisString.CompareTo(thatString);
            }

            return result;
        }
    }
}
