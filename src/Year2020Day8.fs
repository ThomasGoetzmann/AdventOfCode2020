module Year2020Day8

open System.IO
open System.Text.RegularExpressions

let inputs =
    File.ReadLines "inputs/year2020day8-inputs.txt"

type Operation =
    | Acc of int
    | Jmp of int
    | Nop

type Instruction = { Index:int; Operation:Operation}

type RunResult = { Count:int; LastIndex:int; InstructionsHistory:List<int>}

let parse line =
    let pattern = @"(?'operation'.*) (?'number'(-|\+)\d*)"
    let m = Regex.Match(line,pattern)
    match m.Groups.["operation"].Value with
    | "acc" -> Acc (int m.Groups.["number"].Value)
    | "jmp" -> Jmp (int m.Groups.["number"].Value)
    | "nop" -> Nop
    | _ -> failwith "Invalid operation in input file"

let runUntilFirstRepeat (ops:List<Operation>) = 
    let rec run (curr:Instruction) (count:int) (alreadyRun:List<int>) =
        if alreadyRun |> Seq.contains curr.Index then
            {Count= count; LastIndex = curr.Index; InstructionsHistory = alreadyRun}
        else
            match curr.Operation with
            | Acc a -> run { Index = curr.Index + 1 ; Operation = ops.[curr.Index + 1]} (count + a) (curr.Index::alreadyRun)
            | Jmp j -> run { Index = curr.Index + j ; Operation = ops.[curr.Index + j]} count (curr.Index::alreadyRun)
            | Nop -> run { Index = curr.Index + 1 ; Operation = ops.[curr.Index + 1] } count (curr.Index::alreadyRun)

    run { Index = 0; Operation = ops.Head } 0 List.empty

let SolveDay8Part1 = 
    inputs
    |> Seq.map parse
    |> List.ofSeq
    |> runUntilFirstRepeat
    |> fun runResult -> runResult.Count


let SolveDay8Part2 = 0
