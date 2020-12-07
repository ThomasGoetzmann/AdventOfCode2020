module Tests

open System
open Xunit
open FsUnit.Xunit

open Year2020Day1
open Year2020Day2
open Year2020Day3
open Year2020Day4
open Year2020Day5

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
let ``Day 3 Part 1: Number of trees hit with slope {X=3; Y=1}`` () =
    SolveDay3Part1 |> should equal 209

[<Fact>]
let ``Day 3 Part 2: Multiply treesHit when going down the five slopes {X=1; Y=1} {X=3; Y=1} {X=5; Y=1} {X=7; Y=1} {X=1; Y=2}`` () =
    SolveDay3Part2 |> should equal 1574890240

[<Fact>]
let ``Day 4 Part 1: Number of passwords with required fields`` () =
    SolveDay4Part1 |> should equal 170

[<Fact>]
let ``Day 4 Part 2: Number of valid passwords (fields + rules)`` () =
    SolveDay4Part2 |> should equal 103

[<Fact>]
let ``Day 5 Part 1: Highest seat id`` () =
    SolveDay5Part1 |> should equal 866

[<Fact>]
let ``Day 5 Part 2: Your seat id`` () =
    SolveDay5Part2 |> should equal 583
