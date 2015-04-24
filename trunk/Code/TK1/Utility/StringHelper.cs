using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Utility
{
    public class StringHelper
    {
        /// <summary>
        /// Converts the phrase to specified convention.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cases">The cases.</param>
        /// <returns>string</returns>
        public static string ConvertCaseString(string value, UpperCase upperCase)
        {
            char[] chars = value.ToCharArray();
            StringBuilder stringBuilder = new StringBuilder();

            bool toUpper = true;
            bool autoUpper = upperCase == UpperCase.UpperFirstWord;
            foreach (char c in chars)
            {
                if (toUpper)
                {
                    if (char.IsUpper(c))
                        stringBuilder.Append(c);
                    else
                        stringBuilder.Append(char.ToUpper(c));
                    toUpper = false;
                }
                else 
                {
                    if (char.IsUpper(c))
                        stringBuilder.Append(char.ToLower(c));
                    else
                        stringBuilder.Append(c);
                }
                if (c == ' ')
                    if(autoUpper)
                        toUpper = true;
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Converts the phrase to specified convention.
        /// </summary>
        /// <param name="phrase"></param>
        /// <param name="cases">The cases.</param>
        /// <returns>string</returns>
        public static string ConvertCaseString(string phrase, Case cases)
        {
            char splitter = ' ';//, '-', '.'

            string[] splittedPhrase = phrase.Split(splitter);
            var stringBuilder = new StringBuilder();

            if (cases == Case.CamelCase)
            {
                stringBuilder.Append(splittedPhrase[0].ToLower());
                splittedPhrase[0] = string.Empty;
            }
            else if (cases == Case.PascalCase)
                stringBuilder = new StringBuilder();

            foreach (String s in splittedPhrase)
            {
                char[] splittedPhraseChars = s.ToCharArray();
                if (splittedPhraseChars.Length > 0)
                {
                    splittedPhraseChars[0] = ((new String(splittedPhraseChars[0], 1)).ToUpper().ToCharArray())[0];
                }
                stringBuilder.Append(new String(splittedPhraseChars));
            }
            return stringBuilder.ToString();
        }

        public enum Case
        {
            PascalCase,
            CamelCase,

        }
        public enum UpperCase
        {
            UpperFirstWord,
            UpperFirstParagraph
        }

    }
}
