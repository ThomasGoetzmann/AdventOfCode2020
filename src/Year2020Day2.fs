module Year2020Day2

open System.IO
open System.Text.RegularExpressions

let private inputs =
    File.ReadLines "inputs/year2020day2-inputs.txt"
    |> Seq.map string

type PasswordInput =
    { Min: int
      Max: int
      Letter: char
      Password: string }

let private pattern =
    @"(?'min'\d*)-(?'max'\d*) (?'letter'.): (?'password'.*)"

let parse line =
    let matches = Regex.Match(line, pattern)

    { Min = int matches.Groups.["min"].Value
      Max = int matches.Groups.["max"].Value
      Letter = char matches.Groups.["letter"].Value
      Password = string matches.Groups.["password"].Value }

let validCount p =
    let occurencesOfLetter =
        p.Password
        |> Seq.filter (fun x -> p.Letter = x)
        |> Seq.length

    p.Min <= occurencesOfLetter && occurencesOfLetter <= p.Max

let SolveDay2Part1 =
    inputs
    |> Seq.map parse
    |> Seq.filter validCount
    |> Seq.length
