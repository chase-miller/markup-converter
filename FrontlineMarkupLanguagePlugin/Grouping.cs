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
        private int depthLevel = 0;

        /// <summary>
        /// The elements in this group.
        /// </summary>
        internal List<Element> Elements { get; private set; }

        internal Grouping(string stringRepresentation, int depthLevel)
        {
            this.stringRepresentation = stringRepresentation;
            this.depthLevel = depthLevel;
            this.Elements = new List<Element>();

            this.CreateInternalElements();
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

            stringToParse = stringToParse.Replace(" ", "");

            if (stringToParse.StartsWith("(") && stringToParse.EndsWith(")"))
            {
                // Strip off the opening and closing parens
                stringToParse = stringToParse.Remove(0, 1);
                stringToParse = stringToParse.Remove(stringToParse.Length - 1, 1);
            }

            // We can't end with comma (since it's used for listing).
            if (stringToParse.EndsWith(","))
            {
                return false;
            }

            result = new Grouping(stringToParse, depthLevel);

            return true;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (Element element in this.Elements)
            {
                result.Append(element.ToString()).AppendLine();
            }

            return result.ToString();
        }

        /// <summary>
        /// Checks the string passed in for internal elements and creates them if appropriate.
        /// </summary>
        private void CreateInternalElements()
        {
            foreach (Element element in this.SplitTopLevelElements())
            {
                this.Elements.Add(element);
            }
        }

        private IList<Element> SplitTopLevelElements()
        {
            List<Element> elements = new List<Element>();

            if (this.stringRepresentation.Contains(',') || this.stringRepresentation.Contains('('))
            {
                int currentStringStartingIndex = 0;
                int cursor = 0;

                while (cursor < stringRepresentation.Length)
                {
                    if (this.stringRepresentation[cursor] == ',')
                    {
                        string word = this.stringRepresentation.Substring(currentStringStartingIndex, (cursor - currentStringStartingIndex));
                        elements.Add(new Element(word, depthLevel));

                        cursor++;
                        currentStringStartingIndex = cursor;
                    }
                    else if (this.stringRepresentation[cursor] == '(')
                    {
                        // First add the element before this ( and move the cursor.
                        string wordBeforeParen = this.stringRepresentation.Substring(currentStringStartingIndex, (cursor - currentStringStartingIndex));
                        Element newElement = new Element(wordBeforeParen, depthLevel);
                        currentStringStartingIndex = cursor;

                        // Now, find the matching closing parenthesis, add everything in between, and update the cursor.
                        int matchingClosingIndex = this.FindMatchingClosingParenPosition(cursor);

                        string innerGroupString = this.stringRepresentation.Substring(currentStringStartingIndex, (matchingClosingIndex - currentStringStartingIndex));

                        newElement.AddChildGrouping(innerGroupString);

                        elements.Add(newElement);

                        cursor = matchingClosingIndex + 1;
                        currentStringStartingIndex = cursor;
                    }
                    else if (cursor == (this.stringRepresentation.Length - 1))
                    {
                        string finalWord = this.stringRepresentation.Substring(currentStringStartingIndex, (this.stringRepresentation.Length - currentStringStartingIndex));
                        if (finalWord.StartsWith(","))
                        {
                            finalWord = finalWord.Remove(0, 1);
                        }

                        elements.Add(new Element(finalWord, depthLevel));

                        cursor++;
                    }
                    else
                    {
                        cursor++;
                    }
                }
            }
            else
            {
                // It's possible for a grouping to be a single string without any children or peers.
                elements.Add(new Element(this.stringRepresentation, this.depthLevel));
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
    }
}
