open System

[<EntryPoint>]
let main argv =
   let lookup = dict[
      2, ['a'..'c'];
      3, ['d'..'f'];
      3, ['g'..'i'];
      4, ['j'..'l'];
      5, ['m'..'o'];
      6, ['p'..'s'];
      7, ['t'..'v'];
      8, ['w'..'z']
   ]

   let empty = [""] |> Seq.map(fun v -> v.ToCharArray() |> List.ofArray)
   let input = argv.[0] |> List.ofSeq
   let append x y = [x] @ y

   let rec find digits =
      match digits with
         | head :: tail -> 
            seq {
               let digit = head.ToString() |> Int32.Parse
               let lettersForDigit = lookup.[digit]
               
               let recursedDigits = find tail
               yield! recursedDigits 
                  |> Seq.collect(fun existing -> 
                     lettersForDigit 
                        |> Seq.map(fun added -> append added existing))
            }
         | [] -> empty
   
   let results = find input |> Array.ofSeq
   for item in results do
      printfn "%s" (new string(item |> Array.ofList))
   
   System.Console.ReadKey()
   0