using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Bigram
{
    public static class BigramHistogramGenerator
    {
        // tokenizing regex: alpha numeric, elipses, special characters, newline
        private static string REGEX_PATTERN = @"[a-zA-Z0-9]+|\.{2,}|[^\w\s]|\.|\n";

        /// <summary>
        /// Generates a histogram of strings from the contents of a text file.
        /// Reads the entire file into memory and processes it to create a histogram.
        /// </summary>
        /// <param name="filePath">The path to the text file to be processed.</param>
        /// <returns>A <see cref="Histogram{T}"/> </returns>
        /// <exception cref="OutOfMemoryException">
        /// Thrown if the file is too large to be read into memory. 
        /// Consider splitting the file into smaller chunks and combining the resulting histograms.
        /// </exception>
        public static Histogram<string> GenerateStringHistogram(string filePath)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    return ProcessString(reader.ReadToEnd());
                }
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("OutOfMemoryException, file too large to process. Consider splitting into multiple files and combining the histograms");
                // needs to fail
                throw;
            }
        }

        /// <summary>
        /// Processes a string to generate a histogram of bigrams.
        /// Converts the input to lowercase and uses a regular expression to extract words and special characters.
        /// </summary>
        /// <param name="input">The string to be processed into a histogram.</param>
        /// <returns>
        /// A <see cref="Histogram{T}"/> containing the frequency of each bigram in the input string.
        /// </returns>
        /// <remarks>
        /// This method uses a regular expression defined by <c>REGEX_PATTERN</c> to match words and special characters. 
        /// Bigrams are formed by pairing consecutive matches and added to the histogram. 
        /// The method ensures case-insensitivity by converting the input string to lowercase before processing.
        /// </remarks>

        public static Histogram<string> ProcessString(string input)
        {
            var matches = Regex.Matches(input.ToLower(), REGEX_PATTERN);

            string left = null;
            string right = null;

            Histogram<string> histogram = new Histogram<string>();

            foreach (Match match in matches)
            {
                right = match.Value;

                if (left != null)
                {
                    histogram.Add($"{left} {right}");
                }

                left = right;
            }

            return histogram;
        }
    }
}
