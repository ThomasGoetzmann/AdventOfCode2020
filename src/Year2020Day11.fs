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

let isInsideBoundaries (x, y) (array: char[,]) = 
    let width = array |> Array2D.length1
    let height = array |> Array2D.length2
    0 <= x && x < width && 0 <= y && y < height

let tryGetPlaceFrom (array: char [,]) (x, y) =
    if isInsideBoundaries (x,y) array
    then Some array.[x, y]
    else None

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

let evaluatePlaceDirections (x) (y) (c) (array: char [,]) =
    let rec tryGetPlaceFromDirection (distance) (xx, yy) = 
        match (x + xx * distance, y + yy * distance)  |> tryGetPlaceFrom array with 
        | Some place when place = 'L' || place = '#' -> Some place
        | Some place when place = '.' -> tryGetPlaceFromDirection (distance + 1) (xx, yy)
        | _ -> None
    
    let occupiedPlaces = 
        adjacent
        |> List.map (tryGetPlaceFromDirection 1)
        |> Seq.filter ((=) (Some '#'))
        |> Seq.length

    match c with
    | 'L' -> if occupiedPlaces = 0 then '#' else 'L'
    | '#' -> if occupiedPlaces >= 5 then 'L' else '#'
    | c -> c

let rec runUntilStable evaluateFunction array =
    let processedArray =
        array
        |> Array2D.mapi (fun x y c -> evaluateFunction x y c array)

    if processedArray <> array then runUntilStable evaluateFunction processedArray else processedArray

let SolveDay11Part1 =
    inputs
    |> toArray2D
    |> runUntilStable evaluatePlace
    |> Seq.cast<char>
    |> Seq.filter ((=) '#')
    |> Seq.length

let SolveDay11Part2 =
    inputs
    |> toArray2D   
    |> runUntilStable evaluatePlaceDirections
    |> Seq.cast<char>
    |> Seq.filter ((=) '#')
    |> Seq.length
