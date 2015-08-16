using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontlineMarkupLanguagePlugin
{
    class Grouping
    {
        string StringRepresentation { get; set; }

        /// <summary>
        /// A grouping may or may not include groupings within itself.
        /// </summary>
        List<Grouping> InternalGroupings { get; set; }

        internal Grouping(string stringRepresentation)
        {
            this.StringRepresentation = stringRepresentation;
            this.InternalGroupings = new List<Grouping>();
        }

        /// <summary>
        /// Tries to parse the provided string into a <see cref="Grouping"/>.
        /// </summary>
        /// <param name="stringToParse">The string to try and parse.</param>
        /// <param name="result">The resulting <see cref="Grouping"/></param>
        /// <returns>True if the provided string is valid.</returns>
        internal static bool TryParseGrouping(string stringToParse, out Grouping result)
        {
            result = null;

            if (!stringToParse.StartsWith("("))
            {
                return false;
            }
            if (!stringToParse.EndsWith(")"))
            {
                return false;
            }

            StringBuilder innerText = new StringBuilder(stringToParse);

            // Remove first and last characters - ( and )
            innerText.Remove(0, 1);
            innerText.Remove(innerText.Length - 1, 1);

            // After stripping () we can't end with comma (since it's used for listing).
            if (innerText.ToString().EndsWith(","))
            {
                return false;
            }

            foreach (string nextString in innerText.ToString().Split(','))
            {
                // need to figure this out.
            }

            return true;
        }

        internal string ToFormattedString()
        {
            StringBuilder result = new StringBuilder();



            return result.ToString();
        }
    }
}
