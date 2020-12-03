module Year2020Day3

open System.IO

let private inputs =
    File.ReadLines "inputs/year2020day3-inputs.txt"
    |> Seq.map string

let private parse input =
    input |> List.ofSeq |> List.map List.ofSeq

type Square = { X: int; Y: int }

let private treesInTrajectory slope (topography: List<List<char>>) =
    let height = topography.Length
    let width = topography.Head.Length

    let rec slideDown square treeCount =
        match square with
        | s when s.Y > height - 1 -> treeCount
        | _ ->
            let newTreeCount =
                match topography.[square.Y].[square.X] with
                | '#' -> treeCount + 1 //# is a tree
                | '.' -> treeCount //. is open
                | c -> failwith $"Invalid input character '${c}'. Only '#'(=tree) and '.'(=open) are allowed"

            let nextSquare =
                { X = (square.X + slope.X) % width
                  Y = square.Y + slope.Y }

            slideDown nextSquare newTreeCount

    let startingSquare = { X = 0; Y = 0 }
    slideDown startingSquare 0

let SolveDay3Part1 =
    let slope = { X = 3; Y = 1 }

    inputs 
    |> parse 
    |> treesInTrajectory slope

let SolveDay3Part2 =
    let topography = inputs |> parse
    let slope1 = { X = 1; Y = 1 }
    let slope2 = { X = 3; Y = 1 }
    let slope3 = { X = 5; Y = 1 }
    let slope4 = { X = 7; Y = 1 }
    let slope5 = { X = 1; Y = 2 }

    (treesInTrajectory slope1 topography)
    * (treesInTrajectory slope2 topography)
    * (treesInTrajectory slope3 topography)
    * (treesInTrajectory slope4 topography)
    * (treesInTrajectory slope5 topography)
