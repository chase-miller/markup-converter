using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrontlineMarkupLanguagePlugin
{
    class Grouping
    {
        private string stringRepresentation;
        int depthLevel = 0;

        /// <summary>
        /// A grouping may or may not include groupings within itself.
        /// </summary>
        List<Element> InternalElements { get; set; }

        internal Grouping(string stringRepresentation, int depthLevel)
        {
            this.stringRepresentation = stringRepresentation;
            this.depthLevel = depthLevel;
            this.InternalElements = new List<Element>();

            this.CreateInternalElements();
        }

        /// <summary>
        /// Checks the string passed in for internal elements and creates them if appropriate.
        /// </summary>
        private void CreateInternalElements()
        {
            int innerDepthLevel = depthLevel + 1;

            foreach (string element in this.SplitTopLevelElements())
            {
                Grouping group;
                if (TryParseGrouping(element, out group))
                {
                    this.InternalElements.Add(new Element(innerDepthLevel) { ElementGrouping = group });
                }
                else
                {
                    this.InternalElements.Add(new Element(innerDepthLevel) { ElementString = element });
                }
            }
        }

        private IList<string> SplitTopLevelElements()
        {
            List<string> elements = new List<string>();

            int currentStringStartingIndex = 0;
            int cursor = 0;

            while (cursor < stringRepresentation.Length)
            {
                if (this.stringRepresentation[cursor] == ',')
                {
                    string word = this.stringRepresentation.Substring(currentStringStartingIndex, (cursor - currentStringStartingIndex));
                    elements.Add(word);

                    cursor++;
                    currentStringStartingIndex = cursor;
                }
                else if (this.stringRepresentation[cursor] == '(')
                {
                    // Find the matching closing parenthesis, add the word, and update the cursor.
                    int matchingClosingIndex = this.FindMatchingClosingParenPosition(cursor);

                    string innerGroupString = this.stringRepresentation.Substring(currentStringStartingIndex, (matchingClosingIndex - currentStringStartingIndex));
                    elements.Add(innerGroupString);

                    cursor = matchingClosingIndex + 1;
                    currentStringStartingIndex = cursor;
                }
                else
                {
                    cursor++;
                }
            }

            return elements;
        }

        /// <summary>
        /// Finds the matching closing parenthesis based on the starting index. 
        /// </summary>
        /// <param name="openingParenPosition"></param>
        /// <returns></returns>
        private int FindMatchingClosingParenPosition(int openingParenPosition)
        {
            int result = 0;

            int remainingOpenParens = 0;

            for (int i = openingParenPosition; i < this.stringRepresentation.Length; i++)
            {
                if (this.stringRepresentation[i] == '(')
                {
                    remainingOpenParens++;
                }
                else if (remainingOpenParens > 0)
                {
                    if (this.stringRepresentation[i] == ')')
                    {
                        remainingOpenParens--;
                    }
                }
                else
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Tries to parse the provided string into a <see cref="Grouping"/>. Assumes a depth-level of 0.
        /// </summary>
        /// <param name="stringToParse">The string to try and parse.</param>
        /// <param name="result">The resulting <see cref="Grouping"/></param>
        /// <returns>True if the provided string is valid.</returns>
        internal static bool TryParseGrouping(string stringToParse, out Grouping result)
        {
            return TryParseGrouping(stringToParse, 0, out result);
        }

        /// <summary>
        /// Tries to parse the provided string into a <see cref="Grouping"/>.
        /// </summary>
        /// <param name="stringToParse">The string to try and parse.</param>
        /// <param name="depthLevel">How many levels deep this grouping is.</param>
        /// <param name="result">The resulting <see cref="Grouping"/></param>
        /// <returns>True if the provided string is valid.</returns>
        internal static bool TryParseGrouping(string stringToParse, int depthLevel, out Grouping result)
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

            result = new Grouping(innerText.ToString(), depthLevel);

            return true;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (Element element in this.InternalElements)
            {
                result.Append(element.ToString()).AppendLine();
            }

            return result.ToString();
        }
    }
}
