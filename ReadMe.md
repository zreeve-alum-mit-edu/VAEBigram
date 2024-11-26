This code will take in a string and output a sorted histogram of its bigrams as a multiline string. 

The overall run complexity is `O(n log n)` in the average case, where `n` is the length of the string. The worst case is `O(n^2)`, which occurs when all the bigrams hash to the same value but are unique, though this is exceedingly rare.

### Assumptions Made:
Since this is an offline assessment, I had to make some assumptions to determine the best way to code my submission:

- **Bigrams are case-insensitive.**
- **Bigrams exist across sentences and include the period at the end, as well as newlines.**  
  This allows extra information to be obtained about how sentences/paragraphs begin and end.
- **Successive newlines will be treated uniquely; successive periods will be treated as one word.**  
  - Example: `"Hello\n\n"` &rarr; `"hello \n"`, `"\n \n"`  
  - Example: `"Hello..."` &rarr; `"hello ..."`
- **Special characters and punctuation will be treated as words.**  
  Some initial research suggests this is common in NLP bigram processing.  
  - Example: `"Hello, John"` &rarr; `"hello ,"`, `", john"`
- **Special characters include anything non-alphanumeric or space/newline.**  
  - Example: `"$3.15"` &rarr; `"$ 3"`, `"3 ."`, `". 15"`

### Notes:
- **Use case:**  
  The `ToString` method will only be called a few times per histogram creation. Based on this assumption, I chose faster processing of input with slower output generation.
