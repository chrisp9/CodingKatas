using System.Collections.Generic;
using System.Linq;

namespace CodingKatas
{
   public class SolutionFinder
   {
      private readonly WordDictionary _dictionary;

      private readonly HashSet<char> _alphabet = new HashSet<char>(new[] {
         'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
         'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }); 

      public SolutionFinder(WordDictionary dictionary)
      {
         _dictionary = dictionary;
      }

      public List<string> FindValidPermutations(
         string source,
         string target)
      {
         var accumulator = new List<string>();

         if (FindValidPermutations(source, target, _dictionary.Copy(), accumulator))
         {
            return accumulator;
         }

         return accumulator;
      }

      public bool FindValidPermutations(
         string source, 
         string target, 
         WordDictionary currentWords, 
         List<string> accumulator)
      {
         if (string.Equals(source, target))
         {
            accumulator.Add(target);
            return true;
         }

         var neighbours = new HashSet<string>();
         var characters = source.ToCharArray();

         for (int i = 0; i < characters.Length; i++)
         {
            var candidate = characters.ToArray();
            var charactersToTry = _alphabet.Except(new[] { characters[i] });

            foreach(var character in charactersToTry)
            {
               candidate[i] = character;
               var candidateString = new string(candidate);

               if(currentWords.Contains(candidateString))
               {
                  neighbours.Add(candidateString);
                  currentWords.Remove(candidateString);
               }
            }
         }

         foreach (var neighbour in neighbours)
         {
            if (FindValidPermutations(neighbour, target, currentWords, accumulator))
            {
               accumulator.Add(source);
               return true;
            }
         }

         return false;
      }
   }
}
