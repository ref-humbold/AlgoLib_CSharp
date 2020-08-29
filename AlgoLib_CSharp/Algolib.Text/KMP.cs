// Knuth-Morris-Pratt algorithm
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Text
{
    public sealed class KMP
    {
        /// <summary>
        /// Searches for pattern occurrences in specified text using Knuth-Morris-Pratt algorithm.
        /// </summary>
        /// <param name="text">a text</param>
        /// <param name="pattern">a pattern to search for</param>
        /// <returns>enumerable of pattern occurrence positions</returns>
        public static IEnumerable<int> Kmp(string text, string pattern)
        {
            if(text == null)
                throw new ArgumentNullException("Text is null");

            if(pattern == null)
                throw new ArgumentNullException("Pattern is null");

            if(pattern.Length == 0)
                return Enumerable.Empty<int>();

            List<int> places = new List<int>();
            List<int> pi = prefixes(pattern);
            int pos = 0;

            for(int i = 0; i < text.Length; ++i)
            {
                while(pos > 0 && pattern[pos] != text[i])
                    pos = pi[pos - 1];

                if(pattern[pos] == text[i])
                    ++pos;

                if(pos == pattern.Length)
                {
                    places.Add(i - pattern.Length + 1);
                    pos = pi[pos - 1];
                }
            }

            return places;
        }

        // Counts values of Knuth's PI prefix function for specified pattern.
        private static List<int> prefixes(string pattern)
        {
            List<int> pi = new List<int>() { 0 };
            int pos = 0;

            foreach(char letter in pattern)
            {
                while(pos > 0 && pattern[pos] != letter)
                    pos = pi[pos - 1];

                if(pattern[pos] == letter)
                    ++pos;

                pi.Add(pos);
            }

            return pi;
        }
    }
}
