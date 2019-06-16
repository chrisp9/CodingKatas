using System.Collections.Generic;
using System.Linq;

namespace CodingKatas
{
   public class WordChain
   {
      public readonly string Word;
      public readonly WordChain Parent;

      public WordChain(string word, WordChain parent)
      {
         Word = word;
         Parent = parent;
      }
   }

   public class WordDictionary
   {
      private readonly Dictionary<string, HashSet<string>> _lookupTable = new Dictionary<string, HashSet<string>>();

      private readonly HashSet<char> _alphabet = new HashSet<char>(new[] {
         'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
         'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' });

      public void Insert(string str)
      {
         var strArray = str.ToCharArray();
          
         for (int i = 0; i < strArray.Length; i++)
         {
            var copied = strArray.ToArray();
            copied[i] = '*';

            var lookupKey = new string(copied);

            if(!_lookupTable.TryGetValue(lookupKey, out var values))
            {
               values = new HashSet<string>();
               _lookupTable[lookupKey] = values;
            }

            values.Add(str);
         }
      }

      public List<string> FindPath(
         string source,
         string target)
      {
         var shortest = Traverse(source, target);

         if(shortest != null)
         {
            var current = shortest;
            var list = new List<string>();
            while(current != null)
            {
               list.Add(current.Word);
               current = current.Parent;
            }

            list.Reverse();
            return list;
         }

         return new List<string>();
      }

      private WordChain Traverse(
         string source,
         string target)
      {
         var candidates = new Queue<WordChain>();
         var considered = new HashSet<string>();

         considered.Add(source);

         candidates.Enqueue(new WordChain(source, null));

         while (candidates.TryPeek(out _))
         {
            var candidate = candidates.Dequeue();
            //System.Console.WriteLine("Considering " + candidate.Word);

            considered.Add(candidate.Word);

            if (candidate.Word == target)
            {
               return candidate;
            }

            var strArray = candidate.Word.ToCharArray();

            for (var i = 0; i < strArray.Length; i++)
            {
               var copied = strArray.ToArray();
               copied[i] = '*';

               var lookupKey = new string(copied);

               if (_lookupTable.TryGetValue(lookupKey, out var values))
               {
                  foreach (var value in values)
                  {
                     if (considered.Contains(value))
                        continue;

                     candidates.Enqueue(new WordChain(value, candidate));
                  }
               }
            }
         }

         return null;
      }

      private static void NewMethod()
      {
         List<string> shortest = null;
      }

      public bool Contains(string str)
      {
         return _lookupTable.ContainsKey(str);
      }
   }
}
