namespace Day1._2.Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Program

[<TestClass>]
type TestClass () =
    let testCase1 = [|"+1" ; "-1"|]
    let testCase2 = [|"+3"; "+3"; "+4"; "-2"; "-4"|]
    let testCase3 = [|"-6"; "+3"; "+8"; "+5"; "-6"|]
    let testCase4 = [|"+7"; "+7"; "-2"; "-7"; "-4"|]
    let testCase5 = [|"+1"; "-2"; "+3"; "+1"|]

    [<TestMethod>]
    member this.TestCase1() =
        let c = new Counter()
        c.Calculate(testCase1)
            |> fun s -> Assert.AreEqual(0, s)

    [<TestMethod>]
    member this.TestCase2() =
        let c = new Counter()
        c.Calculate(testCase2)
            |> fun s -> Assert.AreEqual(10, s)

    [<TestMethod>]
    member this.TestCase3() =
        let c = new Counter()
        c.Calculate(testCase3)
            |> fun s -> Assert.AreEqual(5, s)

    [<TestMethod>]
    member this.TestCase4() =
        let c = new Counter()
        c.Calculate(testCase4)
            |> fun s -> Assert.AreEqual(14, s)

    [<TestMethod>]
    member this.TestCase5() =
        let c = new Counter()
        c.Calculate(testCase5)
            |> fun s -> Assert.AreEqual(2, s)