using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigram
{
    /// <summary>
    /// A generic Histogram class to maintain a histogram of objects. 
    /// Objects must implement IEquatable<T> to ensure correct reference by value equality.
    /// </summary>
    public class Histogram<T> where T : IEquatable<T>
    {
        private Dictionary<T, int> histogram = new Dictionary<T, int>();

        /// <summary>
        /// Indexer to retrieve the count of a given item in the histogram. 
        /// If the item does not exist in the histogram, the default value of 0 is returned.
        /// </summary>
        /// <param name="index">The key representing the item to retrieve the count for.</param>
        /// <returns>The count of the specified item in the histogram, or 0 if the item is not present.</returns>
        public int this[T index] { 
            get {
                if (!histogram.ContainsKey(index)) { 
                    return 0; 
                }

                return histogram[index]; 
            }
        }

        /// <summary>
        /// Adds an entry to the histogram. 
        /// If the entry already exists, increments its count by the specified value. 
        /// Otherwise, initializes the entry with the specified count.
        /// </summary>
        /// <param name="value">The item to add or update in the histogram.</param>
        /// <param name="count">The number by which to increment the count of the item. Defaults to 1.</param>
        public void Add(T value, int count = 1)
        {
            if (!histogram.ContainsKey(value))
            {
                histogram.Add(value, 0);
            }

            histogram[value] += count;
        }

        /// <summary>
        /// Adds a range of entries to the histogram. 
        /// Each item in the collection is added individually, incrementing its count by 1 if it already exists, 
        /// or initializing it with a count of 1 if it does not exist.
        /// </summary>
        /// <param name="values">A collection of items to add to the histogram.</param>
        public void AddRange(IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                Add(value);
            }
        }

        /// <summary>
        /// Combines two histograms by summing the counts of matching entries. 
        /// Entries present in only one histogram will be added to the result with their existing counts.
        /// </summary>
        /// <param name="left">The first histogram to combine.</param>
        /// <param name="right">The second histogram to combine.</param>
        /// <returns>A new histogram containing the combined counts of entries from both histograms.</returns>
        /// <remarks>
        /// This operator is particularly useful when processing an input that is divided into multiple sub-histograms, 
        /// allowing them to be merged into a single aggregated histogram.
        /// </remarks>
        public static Histogram<T> operator +(Histogram<T> left, Histogram<T> right)
        {
            Histogram<T> histogram = new Histogram<T>();

            foreach (KeyValuePair<T, int> entry in left.histogram)
            {
                histogram.Add(entry.Key, entry.Value);
            }

            foreach (KeyValuePair<T, int> entry in right.histogram)
            {
                histogram.Add(entry.Key, entry.Value);
            }

            return histogram;
        }

        public override string ToString()
        {
            return ToSortedString();
        }

        public string ToSortedString()
        {
            // Can be a heavy lift, consider use cases.
            return string.Join("\n", histogram.OrderBy(item => -item.Value)
                                              .Select(item => $"{item.Key}: {item.Value}".Replace("\n", "{newline}")));
        }
    }
}
