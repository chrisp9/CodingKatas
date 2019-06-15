using System.Collections.Generic;
using System.Linq;

namespace CodingKatas
{
   public class WordDictionary
   {
      private readonly HashSet<string> _words;

      public WordDictionary(string[] words)
      {
         _words = new HashSet<string>(words);
      }

      public bool Contains(string word)
      {
         return _words.Contains(word);
      }

      public void Remove(string word)
      {
         _words.Remove(word);
      }

      public WordDictionary Copy()
      {
         return new WordDictionary(_words.ToArray());
      }
   }
}
