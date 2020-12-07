module Year2020Day6

open System
open System.IO

let inputs = 
    File.ReadAllText "inputs/year2020day6-inputs.txt"

let parseLines (input: string) =
    let separator =
        Environment.NewLine + Environment.NewLine

    input.Split separator


let anyoneAnsweredYes (s:string) = 
    s 
    |> Seq.distinct
    |> Seq.filter (fun x -> x <> '\r' && x <> '\n' && x <> ' ')

let everyoneAnsweredYes (s:string) =
    s.Split Environment.NewLine 
    |> Seq.map Set.ofSeq
    |> Seq.reduce Set.intersect

let SolveDay6Part1 =
    inputs 
    |> parseLines
    |> Seq.map (anyoneAnsweredYes >> Seq.length)
    |> Seq.sum

let SolveDay6Part2 =
    inputs 
    |> parseLines
    |> Seq.map (everyoneAnsweredYes >> Seq.length)
    |> Seq.sum