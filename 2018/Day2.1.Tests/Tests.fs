namespace Day2._1.Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Program

[<TestClass>]
type TestClass () =
    
    let test = [|"abcdef";"bababc";"abbcde";"abcccd";"aabcdd";"abcdee";"ababab"|]
   

    [<TestMethod>]
    member this.Test1 () =
        let worker = new Worker()
        worker.Work test
            |> fun f -> Assert.AreEqual(12, f)
