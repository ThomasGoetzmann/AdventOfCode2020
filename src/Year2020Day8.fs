module Year2020Day8

open System.IO
open System.Text.RegularExpressions

let inputs =
    File.ReadLines "inputs/year2020day8-inputs.txt"
    |> List.ofSeq

type Operation =
    | Acc of int
    | Jmp of int
    | Nop of int

type Instruction = { Index: int; Operation: Operation }

type InstructionResult =
    { NextIndex: int
      Count: int
      History: List<int> }

type RunResult =
    { Count: int
      LastIndex: int
      InstructionsHistory: List<int>
      IsEndlessLoop: bool
      ModifiedOperationIndex: Option<int> }

let parse line =
    let pattern = @"(?'operation'.*) (?'number'(-|\+)\d*)"
    let m = Regex.Match(line, pattern)

    match m.Groups.["operation"].Value with
    | "acc" -> Acc(int m.Groups.["number"].Value)
    | "jmp" -> Jmp(int m.Groups.["number"].Value)
    | "nop" -> Nop(int m.Groups.["number"].Value)
    | _ -> failwith "Invalid operation in input file"

let runUntilFirstInstructionRepeat (ops: List<Operation>) =
    let rec run (curr: Instruction) (count: int) (alreadyRun: List<int>) =
        if alreadyRun |> Seq.contains curr.Index then
            { Count = count
              LastIndex = curr.Index
              InstructionsHistory = alreadyRun
              IsEndlessLoop = true
              ModifiedOperationIndex = None }
        else
            match curr.Operation with
            | Acc a ->
                run
                    { Index = curr.Index + 1
                      Operation = ops.[curr.Index + 1] }
                    (count + a)
                    (curr.Index :: alreadyRun)
            | Jmp j ->
                run
                    { Index = curr.Index + j
                      Operation = ops.[curr.Index + j] }
                    count
                    (curr.Index :: alreadyRun)
            | Nop _ ->
                run
                    { Index = curr.Index + 1
                      Operation = ops.[curr.Index + 1] }
                    count
                    (curr.Index :: alreadyRun)

    run { Index = 0; Operation = ops.Head } 0 List.empty

let runAllPossibleFixes (operations: List<Operation>) =
    let rec run (curr: Instruction, count: int, history: List<int>, ops: List<Operation>, modifiedIndex: int) =
        let execute (i: Instruction) =
            let result =
                { NextIndex = i.Index + 1
                  Count = count
                  History = i.Index :: history }

            match i.Operation with
            | Acc a -> { result with Count = count + a }
            | Jmp j -> { result with NextIndex = i.Index + j }
            | Nop _ -> result

        let runResult =
            { Count = count
              LastIndex = curr.Index
              InstructionsHistory = history
              IsEndlessLoop = true
              ModifiedOperationIndex = Some modifiedIndex }

        match curr.Index with
        | index when history |> Seq.contains index -> runResult
        | _ ->
            let ir = execute curr

            if ir.NextIndex < ops.Length then
                run
                    ({ Index = ir.NextIndex
                       Operation = ops.[ir.NextIndex] },
                     ir.Count,
                     ir.History,
                     ops,
                     modifiedIndex)
            else
                { runResult with
                      Count = ir.Count
                      InstructionsHistory = ir.History
                      IsEndlessLoop = false }

    let modifyOneOperation index _ =
        let modifyOperation o =
            match o with
            | Acc value -> Acc value
            | Jmp value -> Nop value
            | Nop value -> Jmp value

        let modifiedOperations =
            operations
            |> List.mapi (fun i o -> if i = i then modifyOperation o else o)

        (modifiedOperations, index)

    operations
    |> List.mapi modifyOneOperation
    |> List.map (fun (modifiedOperations, modifiedIndex) ->
        run
            ({ Index = 0
               Operation = modifiedOperations.Head },
             0,
             List.empty,
             modifiedOperations,
             modifiedIndex))

let SolveDay8Part1 =
    inputs
    |> List.map parse
    |> runUntilFirstInstructionRepeat

let SolveDay8Part2 =
    inputs
    |> List.map parse
    |> runAllPossibleFixes
    |> List.find (fun x -> not x.IsEndlessLoop)
