module Year2020Day6

open System
open System.IO

let inputs = 
    File.ReadAllText "inputs/year2020day6-inputs.txt"

let parseLines (input: string) =
    let separator =
        Environment.NewLine + Environment.NewLine

    input.Split separator

let yesAnswers (s:string) = 
    s 
    |> Seq.distinct
    |> Seq.filter (fun x -> x <> '\r' && x <> '\n' && x <> ' ')


let SolveDay6Part1 =
    inputs 
    |> parseLines
    |> Seq.map (yesAnswers >> Seq.length)
    |> Seq.sum

let SolveDay6Part2 =
    0