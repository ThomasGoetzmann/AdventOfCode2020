module Year2020Day3

open System.IO

let private inputs =
    File.ReadLines "inputs/year2020day3-inputs.txt"
    |> Seq.map string
    |> List.ofSeq

type Square = { X: int; Y: int }

let private treesInTrajectory (angle:Square) (environment: char list list) = 
    let height = environment.Length
    let width  = environment.Head.Length

    let rec slideDown square treeCount =
        match square with
        | s when s.Y > height - 1 -> treeCount
        | _ -> 
            let newTreeCount = 
                match environment.[square.Y].[square.X] with
                | '#' -> treeCount + 1 //# is a tree
                | '.' -> treeCount  //. is open
                | c -> failwith $"Invalid input character '${c}'. Only '#'(=tree) and '.'(=open) are allowed"
            let nextSquare = { X = (square.X + angle.X) % width; Y = square.Y + angle.Y}
            slideDown nextSquare newTreeCount

    let startingSquare = { X = 0; Y = 0}
    slideDown startingSquare 0

let SolveDay3Part1 =
    let angle = {X = 3; Y = 1}

    inputs
    |> List.map List.ofSeq
    |> treesInTrajectory angle
