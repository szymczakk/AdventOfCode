open System

[<EntryPoint>]
let main argv =
    let readLines = System.IO.File.ReadAllLines("input.txt")

    let result = 
        readLines
        |> Array.map(int)
        |> Array.sum

    Console.WriteLine(result)
    0
