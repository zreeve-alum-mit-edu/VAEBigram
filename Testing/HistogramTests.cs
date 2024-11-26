using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace Bigram.Testing
{
    public class HistogramTests
    {
        [Fact]
        public void TestHistogramHappyPath()
        {
            Histogram<string> histogram = new Histogram<string>();

            //test empty
            Assert.Equal(histogram["test"], 0);

            //up to 10
            for (int i = 1; i < 10; i++)
            {
                histogram.Add("test");
                Assert.Equal(histogram["test"], i);
                histogram.Add("test again");
                Assert.Equal(histogram["test again"], i);
            }

            //retest empty
            Assert.Equal(histogram["none"], 0);
        }

        [Fact]
        public void TestHistogramObject()
        {

            Histogram<Person> histogram = new Histogram<Person>();

            Person person1 = new Person { FirstName = "john", LastName = "doe" };
            Person person1copy = new Person { FirstName = "john", LastName = "doe" };
            Person person2 = new Person { FirstName = "jane", LastName = "doe" };

            //test empty
            Assert.Equal(histogram[person1], 0);

            //up to 10
            for (int i = 1; i < 10; i++)
            {
                histogram.Add(person1);
                Assert.Equal(histogram[person1copy], i);
                histogram.Add(person2);
                Assert.Equal(histogram[person2], i);
            }
        }

        [Fact]
        public void TestHistogramMerge()
        {

            Histogram<string> histogram1 = new Histogram<string>();
            Histogram<string> histogram2 = new Histogram<string>();

            histogram1.Add("unique1", 1);
            histogram2.Add("unique2", 2);
            histogram1.Add("shared1", 3);
            histogram2.Add("shared1", 4);

            Histogram<string> combined = histogram1 + histogram2;

            //test merge
            Assert.Equal(combined["none"], 0);
            Assert.Equal(combined["unique1"], 1);
            Assert.Equal(combined["unique2"], 2);
            Assert.Equal(combined["shared1"], 7);
        }
    }

    internal class Person : IEquatable<Person> {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool Equals(Person? other)
        {
            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode() + LastName.GetHashCode();
        }
    }

}
