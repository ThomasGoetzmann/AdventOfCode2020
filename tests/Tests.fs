module Tests

open System
open Xunit
open FsUnit.Xunit

open Year2020Day1

[<Fact>]
let ``Day 1 Part 1: Mutiplied pair where the pair is equal to 2020`` () =
    SolveDay1Part1 |> should equal 539851  

[<Fact>]
let ``Day 1 Part 1 (second solution): Mutiplied pair where the pair is equal to 2020`` () =
    SolveDay1Part1Bis |> should equal 539851  

[<Fact>]
let ``Day 1 Part 2: Mutiplied triplet where the triplet is equal to 2020`` () =
    SolveDay1Part2 |> should equal 212481360
    