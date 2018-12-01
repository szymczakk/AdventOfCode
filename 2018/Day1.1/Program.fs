// Learn more about F# at http://fsharp.org

open System

let negate x = x * -1 

let calculate (n:string) =
    match n.[0] with 
    | '+' -> 0 + (n |> string |> int)
    | '-' -> 0 - (n |> string |> int |> negate)

let parse (x : string[]) = 
    x|> Array.map(calculate)
     |> Array.sum

[<EntryPoint>]
let main argv =
    let readLines = System.IO.File.ReadAllLines("input.txt")
    let result = parse(readLines)

    Console.WriteLine(result)
    0
