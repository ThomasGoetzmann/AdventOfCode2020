module Year2020Day1

open System.IO

let private inputs =
    File.ReadLines "inputs/year2020day1-inputs.txt"
    |> Seq.map int
    |> Seq.toList

let rec pairs list =
    match list with
    | h :: t ->
        [ for x in t do
            yield (h, x)
          yield! pairs t ]
    | _ -> []

let rec firstPairEqualTo2020 list =
    match list with
    | (a, b) :: t when a + b = 2020 -> (a, b)
    | _ :: t -> firstPairEqualTo2020 t
    | [] -> failwith "No pair is equal to 2020 or list is empty"

let findFirstPairEqualTo2020 (l: List<int>) =
    [ for i = 0 to l.Length - 2 do
        for j = i + 1 to l.Length - 1 do
            if (l.[i] + l.[j] = 2020) then yield (l.[i], l.[j]) ]
    |> Seq.head

let multiplyPair (a, b) = a * b

let findFirstTripletEqualTo2020 (l: List<int>) =
    [ for i = 0 to l.Length - 3 do
        for j = i + 1 to l.Length - 2 do
            for k = i + 2 to l.Length - 1 do
                if (l.[i] + l.[j] + l.[k] = 2020) then yield (l.[i], l.[j], l.[k]) ]
    |> Seq.head

let multiplyTriplet (a, b, c) = a * b * c

let SolveDay1Part1 =
    inputs 
    |> findFirstPairEqualTo2020 
    |> multiplyPair

// First approach I came up with. But I didn't like it for the Part 2
let SolveDay1Part1Bis =
    inputs
    |> pairs
    |> firstPairEqualTo2020
    |> multiplyPair

let SolveDay1Part2 =
    inputs
    |> findFirstTripletEqualTo2020
    |> multiplyTriplet
