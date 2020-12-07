module Year2020Day5

open System.IO
open System

let inputs =
    File.ReadLines "inputs/year2020day5-inputs.txt"

let getSeat (bsp:string) =
    let toBinaryChar c = 
        match c with
        //rows
        | 'F' -> '0'
        | 'B' -> '1'
        //columns
        | 'L' -> '0'
        | 'R' -> '1'
        | _ -> failwith "Invalid char"
    
    let binaryString = bsp |> Seq.map toBinaryChar 
    // OR: let binaryStringAlternative = bsp.Replace('F','0').Replace('B','1').Replace('L', '0').Replace('R', '1')
    let row = Convert.ToInt32(binaryString|> Seq.take 7 |> Array.ofSeq |> String , 2)
    let column = Convert.ToInt32(binaryString |> Seq.skip 7 |> Array.ofSeq |> String , 2)
    
    (row, column)

let getSeatId (row, column) = row * 8 + column

let printSeat s =
    let seat = getSeat s
    let (row, column) = seat
    $"BSP:{s}, Seat:(row:{row},column:{column}), SeatId:{seat |> getSeatId}"

let SolveDay5Part1 =
    inputs
    |> Seq.map (getSeat >> getSeatId)
    |> Seq.max

// Displays all seat information for the solution of part 1 (BSP)
let DetailedSolveDay5Part1 =
    inputs
    |> Seq.maxBy (getSeat >> getSeatId)
    |> printSeat

let SolveDay5Part2 =
    let allSeats = Seq.allPairs [ 0 .. 127 ] [ 0 .. 7 ]
    let seatsTaken = inputs |> Seq.map getSeat

    let seatIdsPlusMinusOneAreTaken seatId =
        let seatsTakenId = seatsTaken |> Seq.map getSeatId

        seatsTakenId |> Seq.exists ((=) (seatId - 1))
        && (seatsTakenId |> Seq.exists ((=) (seatId + 1)))

    allSeats
    |> Seq.except seatsTaken
    |> Seq.map getSeatId
    |> Seq.find seatIdsPlusMinusOneAreTaken
