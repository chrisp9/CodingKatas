using System;
using System.IO;

namespace CodingKatas
{
   public struct Args
   {
      public string DictionaryPath { get; }
      public string SourceWord { get; }
      public string TargetWord { get; }

      public Args(
         string dictionaryPath,
         string sourceWord,
         string targetWord)
      {
         DictionaryPath = dictionaryPath;
         SourceWord = sourceWord;
         TargetWord = targetWord;
      }
   }

   class Program
   {
      static void Main(string[] args)
      {
         var parsedArgs = Parse(args);
         var fileContent = File.ReadAllLines(parsedArgs.DictionaryPath);

         var sourceWord = parsedArgs.SourceWord;
         var targetWord = parsedArgs.TargetWord;

         var wordDictionary = new WordDictionary();

         foreach (var line in fileContent)
         {
            if(line.Length == sourceWord.Length)
               wordDictionary.Insert(line);
         }

         //if (!wordDictionary.Contains(sourceWord) || !wordDictionary.Contains(targetWord))
         //{
         //   PrintError("Source word and Target word must both be valid words");
         //}

         var result = wordDictionary.FindPath(parsedArgs.SourceWord, parsedArgs.TargetWord);

         //var finder = new SolutionFinder(wordDictionary);
         //var list = finder.FindValidPermutations(parsedArgs.SourceWord, parsedArgs.TargetWord);
         //list.Reverse();

         //Console.WriteLine($"Found path from {parsedArgs.SourceWord} to {parsedArgs.TargetWord}:");

         foreach (var item in result)
         {
            Console.WriteLine(item);
         }

         Console.WriteLine("Press any key to exit");
         Console.ReadKey();

      }

      static void PrintError(string error)
      {
         Console.WriteLine(error);
         Console.WriteLine("Press any key to exit");
         Console.ReadKey();
         Environment.Exit(-1);
      }

      static Args Parse(string[] args)
      {
         var sourceWord = args[0];
         var targetWord = args[1];
         var dictionaryPath = args[2];

         return new Args(dictionaryPath, sourceWord, targetWord);
      }
   }
}
