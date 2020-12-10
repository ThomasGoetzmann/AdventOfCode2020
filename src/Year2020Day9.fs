module Year2020Day9

open System.IO

let inputs = 
    File.ReadLines "inputs/year2020day9-inputs.txt" 
    |> Seq.map uint64
    |> List.ofSeq

let pairEqualTo number (p1, p2) = p1 + p2 = number

let isSumOfPairsInPrevious previous list = 
    let rec getPairs acc collection = 
        match collection with
        | [] -> acc
        | head::tail -> 
            let pairs = tail |> Seq.map (fun x -> (head, x))
            getPairs (Seq.append acc pairs) tail
        
    let number = list |> Seq.skip previous |> Seq.head

    let isSumOfPairs = 
        list 
        |> List.take previous
        |> getPairs []
        |> Seq.exists (pairEqualTo number)

    (number, isSumOfPairs)

let firstNumberNotEqualToSumOfPairsInPrevious previous list =
    let rec loop list =
        match isSumOfPairsInPrevious previous list with
        | (number,isSum) when not isSum-> number
        | _ -> loop list.Tail

    loop list

let rec contiguousNumbersEqualTo (i: uint64) (list: List<uint64>) =
    let rec sumEqualTo i list = 
        match list with
        | [] -> []
        | head::tail -> if List.sum(tail) = i then tail else sumEqualTo i tail

    match sumEqualTo i list with
    | [] -> contiguousNumbersEqualTo i (list |> List.rev |> List.tail |> List.rev)
    | l when l.Length = 1 -> contiguousNumbersEqualTo i (list |> List.rev |> List.tail |> List.rev)
    | l -> l

let minMaxPair l = 
    (l |> Seq.min, l |> Seq.max)

let SolveDay9Part1 = 
    inputs
    |> firstNumberNotEqualToSumOfPairsInPrevious 25

let SolveDay9Part2 = 
    inputs
    |> contiguousNumbersEqualTo SolveDay9Part1
    |> minMaxPair
