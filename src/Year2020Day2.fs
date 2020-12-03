module Year2020Day2

open System.IO
open System.Text.RegularExpressions

let private inputs =
    File.ReadLines "inputs/year2020day2-inputs.txt"
    |> Seq.map string

type OldRentalShopPasswordPolicy =
    { Min: int
      Max: int
      Letter: char
      Password: string }

type TobboganPasswordPolicy =
    { FirstIndex: int
      SecondIndex: int
      Letter: char
      Password: string }

let private pattern =
    @"(?'num1'\d*)-(?'num2'\d*) (?'letter'.): (?'password'.*)"

let parse line =
    let matches = Regex.Match(line, pattern)

    { Min = int matches.Groups.["num1"].Value
      Max = int matches.Groups.["num2"].Value
      Letter = char matches.Groups.["letter"].Value
      Password = string matches.Groups.["password"].Value }

let parse2 line =
    let matches = Regex.Match(line, pattern)

    // The index is not zero based in the input file, therefore we do a -1
    { FirstIndex = int matches.Groups.["num1"].Value - 1
      SecondIndex = int matches.Groups.["num2"].Value - 1
      Letter = char matches.Groups.["letter"].Value
      Password = string matches.Groups.["password"].Value }

let validCount (p: OldRentalShopPasswordPolicy) =
    let occurencesOfLetter =
        p.Password
        |> Seq.filter (fun x -> p.Letter = x)
        |> Seq.length

    p.Min <= occurencesOfLetter
    && occurencesOfLetter <= p.Max

let validCount2 (p: TobboganPasswordPolicy) =
    (p.Password.[p.FirstIndex] = p.Letter)
    <> (p.Password.[p.SecondIndex] = p.Letter)

let SolveDay2Part1 =
    inputs
    |> Seq.map parse
    |> Seq.filter validCount
    |> Seq.length

let SolveDay2Part2 =
    inputs
    |> Seq.map parse2
    |> Seq.filter validCount2
    |> Seq.length
