using Bigram;

// Replace with text file of your choosing, may need to use absolute path
Histogram<string> histogram = BigramHistogramGenerator.GenerateStringHistogram(@"..\..\..\Testing\artifacts\mobydickfull.txt");

Console.WriteLine(histogram);
Console.ReadLine();