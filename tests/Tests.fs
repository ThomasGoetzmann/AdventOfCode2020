module Tests

open System
open Xunit
open FsUnit.Xunit

open Year2020Day1
open Year2020Day2

[<Fact>]
let ``Day 1 Part 1: Mutiplied pair where the pair is equal to 2020`` () =
    SolveDay1Part1 |> should equal 539851  

[<Fact>]
let ``Day 1 Part 1 (second solution): Mutiplied pair where the pair is equal to 2020`` () =
    SolveDay1Part1Bis |> should equal 539851  

[<Fact>]
let ``Day 1 Part 2: Mutiplied triplet where the triplet is equal to 2020`` () =
    SolveDay1Part2 |> should equal 212481360

[<Fact>]
let ``Day 2 Part 1: Number of valid passwords (having right amount of characters)`` () =
    SolveDay2Part1 |> should equal 546