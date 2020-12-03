module Tests

open System
open Xunit
open FsUnit.Xunit

open Year2020Day1
open Year2020Day2
open Year2020Day3

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
let ``Day 2 Part 1: Number of valid passwords (old rental shop password policy)`` () =
    SolveDay2Part1 |> should equal 546

[<Fact>]
let ``Day 2 Part 2: Number of valid passwords (toboggan corporate password policy)`` () =
    SolveDay2Part2 |> should equal 275

[<Fact>]
let ``Day 3 Part 1: Number of trees hit with slope (X= 3; Y= 1)`` () =
    SolveDay3Part1 |> should equal 209
