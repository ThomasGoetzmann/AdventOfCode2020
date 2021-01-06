module Tests

open System
open Xunit
open FsUnit.Xunit

open Year2020Day1
open Year2020Day2
open Year2020Day3
open Year2020Day4
open Year2020Day5
open Year2020Day6
open Year2020Day7
open Year2020Day8
open Year2020Day9
open Year2020Day10
open Year2020Day11

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

[<Fact>]
let ``Day 6 Part 1: Sum of questions where ANYONE answered yes`` () =
    SolveDay6Part1 |> should equal 6714

[<Fact>]
let ``Day 6 Part 2: Sum of questions where EVERYONE answered yes`` () =
    SolveDay6Part2 |> should equal 3435

[<Fact>]
let ``Day 7 Part 1: All bag colors which can contain a 'shiny gold' bag`` () =
    SolveDay7Part1 |> should equal 372

[<Fact>]
let ``Day 8 Part 1: Accumulated value just before first instruction repeat`` () =
    SolveDay8Part1.Count |> should equal 1487

[<Fact>]
let ``Day 8 Part 2: Accumulated value for fixed program (1 Nop modified to Jmp or 1 Jmp modified to Nop)`` () =
    SolveDay8Part2.Count |> should equal 1607

[<Fact>]
let ``Day 9 Part 1: Number for which the sum of 2 numbers in the 25 preceding numbers is not equal`` () =
    SolveDay9Part1 |> should equal 466456641UL

[<Fact>]
let ``Day 9 Part 2: Sum of Min and Max of continuous numbers equal to Day9Part1 Answer`` () =
    SolveDay9Part2 |> fun (a,b) -> a + b |> should equal 55732936UL

[<Fact>]
let ``Day 10 Part 1: Jolt1 * Jolt3`` () =
    SolveDay10Part1 |> should equal 2760

[<Fact>]
let ``Day 11 Part 1: Occupied seats after no more changes occurs`` () =
    SolveDay11Part1 |> should equal 2494

[<Fact>]
let ``Day 11 Part 2: Occupied seats after no more changes with new rules`` () =
    SolveDay11Part2 |> should equal 2306
