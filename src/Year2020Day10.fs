module Year2020Day10

open System.IO

let inputs = 
    File.ReadLines "inputs/year2020day10-inputs.txt" 
    |> Seq.map int
    |> List.ofSeq

type Jolts = {Jolt1 : int; Jolt3 : int}

let getJolts list =
    let rec jolts list prev acc = 
        match list with
        | [] -> acc
        | h::tail when h - prev <= 3 ->
            match h - prev with
            | 3 -> jolts tail h { acc with Jolt3= acc.Jolt3 + 1 }
            | 1 -> jolts tail h { acc with Jolt1= acc.Jolt1 + 1 }
            | _ -> jolts tail h acc
        | _ -> acc

    jolts list 0 {Jolt1 = 0; Jolt3 = 1 } //Don't forget to start Jolt3 at 1 because device is always Jolt3 and we need to include it

let SolveDay10Part1 = 
    inputs 
    |> List.sort
    |> getJolts
    |> fun x -> x.Jolt1 * x.Jolt3

let SolveDay10Part2 = 0
