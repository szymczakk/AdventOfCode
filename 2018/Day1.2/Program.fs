open System

//type Counter() =

//    let calculate (lines: int[]) =

//    member this.Calculate(input : string[]) =
//        input
//            |> Array.map(int)
//            |> calculate

[<EntryPoint>]
let main argv =
    //let lines = IO.File.ReadAllLines("input.txt")
    //let c = new Counter()
    //c.Calculate(lines)
    //    |> Console.WriteLine

    //NOT MY CODE, 
    //"STOLEN" FROM arathunku gist :D

    let stopWatch = new System.Diagnostics.Stopwatch()

    stopWatch.Start()

    let input = 
        "input.txt"
        |> System.IO.File.ReadAllLines
        |> Seq.map int
        |> Seq.toList

    let calibrate input =
        let rec loop originalInput input lastSeenSum seenFrequencies =
            match input with
            | freq::tail ->      
              let newSum = lastSeenSum + freq
              if Set.contains newSum seenFrequencies then
                newSum
              else
                loop originalInput tail newSum  (Set.add newSum seenFrequencies)
            | [] -> 
                loop originalInput originalInput lastSeenSum seenFrequencies      
    
        loop input input 0 (Set.empty.Add 0)

    let result = calibrate input

    stopWatch.Stop()
    Console.WriteLine(stopWatch.ElapsedMilliseconds)

    0