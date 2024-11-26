using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bigram.Testing
{
    public class BigramTests
    {
        [Fact]
        public void TestHappyPath()
        {
            string input = "The quick brown fox jumps over the lazy dog.\n twice";

            Histogram<string> histogram = BigramHistogramGenerator.ProcessString(input);

            Assert.Equal(histogram["none"], 0);
            // case change
            Assert.Equal(histogram["the quick"], 1);
            Assert.Equal(histogram["lazy dog"], 1);
            Assert.Equal(histogram["fox jumps"], 1);
            Assert.Equal(histogram["dog ."], 1);
            Assert.Equal(histogram[". \n"], 1);
            Assert.Equal(histogram["\n twice"], 1);
        }

        [Fact]
        public void TestSpecialCharacters()
        {
            string input = "the t-shirt costs $5.99 @Walmart";

            Histogram<string> histogram = BigramHistogramGenerator.ProcessString(input);

            Assert.Equal(histogram["none"], 0);
            Assert.Equal(histogram["the t"], 1);
            Assert.Equal(histogram["t -"], 1);
            Assert.Equal(histogram["- shirt"], 1);
            Assert.Equal(histogram["$ 5"], 1);
            Assert.Equal(histogram[". 99"], 1);
            Assert.Equal(histogram["@ walmart"], 1);
        }

        [Fact]
        public void TestEdgeCases()
        {
            string input = "the t-shirt costs $5.99 @Walmart.. but at target... it's free!\n\n\nplus   shipping #%$#@#%!!!!!!!!!!!!!!!!!!!!!";

            Histogram<string> histogram = BigramHistogramGenerator.ProcessString(input);

            Assert.Equal(histogram["none"], 0);
            Assert.Equal(histogram["walmart .."], 1);
            Assert.Equal(histogram["... it"], 1);
            Assert.Equal(histogram["it '"], 1);
            Assert.Equal(histogram["' s"], 1);
            Assert.Equal(histogram["free !"], 1);
            Assert.Equal(histogram["\n \n"], 2);
            Assert.Equal(histogram["\n plus"], 1);
            Assert.Equal(histogram["# %"], 2);
            Assert.Equal(histogram["! !"], 20);
        }
    }
}
