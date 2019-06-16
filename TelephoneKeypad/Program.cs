using System;
using System.Collections.Generic;

namespace TelephoneKeypad
{
   class Program
   {
      static Dictionary<int, char[]> _lookup;

      static void Main(string[] args)
      {
         var number = args[0];

         _lookup = new Dictionary<int, char[]>
         {
            {2, new char[] {'a', 'b', 'c'} },
            {3, new char[] {'d', 'e', 'f'} },
            {4, new char[] {'g', 'h', 'i'} },
            {5, new char[] {'j', 'k', 'l'} },
            {6, new char[] {'m', 'n', 'o'} },
            {7, new char[] {'p', 'q', 'r', 's'} },
            {8, new char[] {'t', 'u', 'v'} },
            {9, new char[] {'w', 'x', 'y', 'z' } }
         };

         var results = Solve(number);

         foreach (var item in results)
         {
            Console.WriteLine(item);
         }
      }

      static List<string> Solve(string digits)
      {
         var queue = new Queue<List<char>>();
         var results = new List<string>();

         var charArray = digits.ToCharArray();

         var chars = GetCharacters(charArray[0]);

         foreach (var character in chars)
         {
            var list = new List<char>();
            list.Add(character);

            queue.Enqueue(list);
         }

         while (queue.TryPeek(out _))
         {
            var nextItem = queue.Dequeue();

            if (nextItem.Count < charArray.Length)
            {
               var charsForDigit = GetCharacters(charArray[nextItem.Count]);

               foreach (var character in charsForDigit)
               {
                  var list = new List<char>(nextItem.Count * 2);
                  list.AddRange(nextItem);
                  list.Add(character);
                  queue.Enqueue(list);
               }
            }
            else
            {
               results.Add(new string(nextItem.ToArray()));
            }
         }

         return results;
      }

      static string GetCharacters(char digit)
      {
         var parsed = int.Parse(digit.ToString());
         return new string(_lookup[parsed]);
      }
   }
}
