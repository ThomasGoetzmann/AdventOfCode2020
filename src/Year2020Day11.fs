module Year2020Day11

open System.IO

let inputs =
    File.ReadAllLines "inputs/year2020day11-inputs.txt"
    |> Array.map Array.ofSeq

let toArray2D (array: char [] []) =
    let width = array.[0].Length
    let height = array.Length

    Array2D.init height width (fun x y -> array.[x].[y])

let adjacent =
    [ (-1, -1); (0, -1); (1, -1);
      (-1, 0);  (*0,0*)  (1,  0);
      (-1, 1);  (0, 1);  (1, 1) ]

let tryGetPlaceFrom (array: char [,]) (x, y) =
    let width = array |> Array2D.length1
    let height = array |> Array2D.length2

    if 0 <= x && x < width && 0 <= y && y < height
    then Some array.[x, y]
    else None

let move (x,y) (xx,yy) = x + xx, y + yy

let evaluatePlace (x) (y) (c) (array: char [,]) =
    let adjacentOccupied =
        adjacent
        |> List.map ((fun (xx,yy) -> x + xx, y + yy) >> tryGetPlaceFrom array)
        |> Seq.filter ((=) (Some '#'))
        |> Seq.length

    // L -> Empty
    // # -> Occupied
    // . -> Floor
    match c with
    | 'L' -> if adjacentOccupied = 0 then '#' else 'L'
    | '#' -> if adjacentOccupied >= 4 then 'L' else '#'
    | c -> c

let rec runUntilStable array =
    let processedArray =
        array
        |> Array2D.mapi (fun x y c -> evaluatePlace x y c array)

    if processedArray <> array then runUntilStable processedArray else processedArray

let SolveDay11Part1 =
    inputs
    |> toArray2D
    |> runUntilStable
    |> Seq.cast<char>
    |> Seq.filter ((=) '#')
    |> Seq.length

let SolveDay11Part2 = 0
