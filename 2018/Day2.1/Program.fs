// Learn more about F# at http://fsharp.org

open System

type Worker() = 
    let calculate (lines: string[]) =
        0

    member this.Work(input : string[]) =
        input
            |> calculate


[<EntryPoint>]
let main argv =
    let lines = System.IO.File.ReadAllLines("input.txt")
    let worker = new Worker()
    worker.Work lines
        |> Console.WriteLine
    0 
