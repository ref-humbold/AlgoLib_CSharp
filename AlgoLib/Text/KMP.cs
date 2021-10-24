// Knuth-Morris-Pratt algorithm
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Text
{
    public static class KMP
    {
        /// <summary>
        /// Searches for pattern occurrences in given text using Knuth-Morris-Pratt algorithm.
        /// </summary>
        /// <param name="text">Text to search in</param>
        /// <param name="pattern">Pattern to search for</param>
        /// <returns>Enumerable of pattern occurrence positions</returns>
        public static IEnumerable<int> Kmp(this string text, string pattern)
        {
            if(text == null)
                throw new ArgumentNullException("Text is null");

            if(pattern == null)
                throw new ArgumentNullException("Pattern is null");

            if(pattern.Length == 0)
                return Enumerable.Empty<int>();

            var places = new List<int>();
            List<int> pi = prefixes(pattern);
            int position = 0;

            for(int i = 0; i < text.Length; ++i)
            {
                while(position > 0 && pattern[position] != text[i])
                    position = pi[position - 1];

                if(pattern[position] == text[i])
                    ++position;

                if(position == pattern.Length)
                {
                    places.Add(i - pattern.Length + 1);
                    position = pi[position - 1];
                }
            }

            return places;
        }

        // Counts values of Knuth's PI prefix function for given pattern.
        private static List<int> prefixes(string pattern)
        {
            var pi = new List<int>() { 0 };
            int position = 0;

            foreach(char letter in pattern)
            {
                while(position > 0 && pattern[position] != letter)
                    position = pi[position - 1];

                if(pattern[position] == letter)
                    ++position;

                pi.Add(position);
            }

            return pi;
        }
    }
}
