// Learn more about F# at http://fsharp.org

open System

type Guard = {id: string;  fallASleepTime: DateTime; wakeUpTime: DateTime; startTime: DateTime; endTime: DateTime}

type Worker() =
    member this.ParseInfo line=
        line.ToString().Split([|'[';']'|], System.StringSplitOptions.RemoveEmptyEntries)
            |> Seq.cast

    member this.GetMostTimeAsleep input =
        input
            |> Array.map (fun row-> row.ToString().Split([|'[';']'|], System.StringSplitOptions.RemoveEmptyEntries) )
            |> Seq.cast<string[]>
            |> Seq.map (fun el -> Map.map(fun k v -> System.DateTime.Parse(k)))
            |>
        0;

[<EntryPoint>]
let main argv =
    let w = new Worker();
    "input.txt"
        |> IO.File.ReadAllLines
        |> w.GetMostTimeAsleep
        |> Console.WriteLine
    0 // return an integer exit code
