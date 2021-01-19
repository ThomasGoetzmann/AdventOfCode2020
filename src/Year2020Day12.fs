module Year2020Day12

open System.IO

let inputs =
    File.ReadAllLines "inputs/year2020day12-inputs.txt" 

type Direction =
    | East
    | West
    | North
    | South

type Action =
    | NorthMove
    | SouthMove
    | EastMove
    | WestMove
    | LeftTurn
    | RightTurn
    | Forward

type Instruction = { Action: Action; Value: int }

type Ship =
    { Direction: Direction
      X: int
      Y: int }

type Waypoint = { X: int; Y: int }

type ShipWithWaypoint = 
    { Waypoint: Waypoint
      X: int
      Y: int}

let parse inputs = 
    let parseInstruction (i:string) = 
        let instructionValue = i.Substring(1) |> int

        match i with 
        | x when x.[0] = 'N' -> {Action= NorthMove; Value = instructionValue}
        | x when x.[0] = 'S' -> {Action= SouthMove; Value = instructionValue}
        | x when x.[0] = 'E' -> {Action= EastMove; Value = instructionValue}
        | x when x.[0] = 'W' -> {Action= WestMove; Value = instructionValue}
        | x when x.[0] = 'L' -> {Action= LeftTurn; Value = instructionValue}
        | x when x.[0] = 'R' -> {Action= RightTurn; Value = instructionValue}
        | x when x.[0] = 'F' -> {Action= Forward; Value = instructionValue}
        | _ -> failwith "Invalid instruction for parsing"

    inputs
    |> Seq.map parseInstruction
    |> List.ofSeq

let rec rotate ship angle =
    let rotate90Anticlockwise dir =
        match dir with 
        | North -> West
        | West -> South
        | South -> East
        | East -> North
    
    let rotate90Clockwise dir =
        match dir with 
        | North -> East
        | East -> South
        | South -> West
        | West -> North

    match angle with
    | a when a < 0 -> rotate {ship with Direction = (rotate90Clockwise ship.Direction)} (angle + 90)
    | a when a > 0 -> rotate {ship with Direction = (rotate90Anticlockwise ship.Direction)} (angle - 90)
    | _ -> ship

let rec move (ship: Ship) (m:Instruction) =
    match m.Action with 
    | NorthMove -> { ship with Y = ship.Y + m.Value }
    | SouthMove -> { ship with Y = ship.Y - m.Value}
    | EastMove -> { ship with X = ship.X + m.Value}
    | WestMove -> { ship with X = ship.X - m.Value}
    | LeftTurn -> rotate ship m.Value
    | RightTurn -> rotate ship (-m.Value)
    | Forward -> 
        match ship.Direction with
        | North -> move ship { m with Action = NorthMove }
        | South -> move ship { m with Action = SouthMove }
        | East -> move ship { m with Action = EastMove }
        | West -> move ship { m with Action = WestMove }

let rec followInstructionsForPart1 (ship:Ship) (moves:List<Instruction>) = 
    match moves with 
    | m::mvs -> followInstructionsForPart1 (move ship m) mvs
    | _ -> ship
    
let manhattanDistance (x: int , y:int) = (abs x) + (abs y)

let rec rotateWaypointAroundShip (ship:ShipWithWaypoint) (angle:int) =
    let rotateWaypointClockwise (ship: ShipWithWaypoint) =
        //Clockwise rotation of (X; Y) is (Y; -X)
        {ship with Waypoint = { X = ship.Waypoint.Y; Y = -ship.Waypoint.X }}
        
    let rotateWaypointAnticlockwise (ship: ShipWithWaypoint) =
        //Anticlockwise rotation of (X; Y) is (-Y; X)
        {ship with Waypoint = { X = -ship.Waypoint.Y; Y = ship.Waypoint.X }}

    match angle with
    | a when a < 0 -> rotateWaypointAroundShip (rotateWaypointClockwise ship) (angle+90)
    | a when a > 0 -> rotateWaypointAroundShip (rotateWaypointAnticlockwise ship) (angle-90)
    | _ -> ship

let moveWithWaypoint (ship:ShipWithWaypoint) (m:Instruction) =
    match m.Action with
    | NorthMove -> { ship with Waypoint = { X = ship.Waypoint.X; Y = ship.Waypoint.Y + m.Value}}
    | SouthMove -> { ship with Waypoint = { X = ship.Waypoint.X; Y = ship.Waypoint.Y - m.Value}}
    | EastMove -> { ship with Waypoint = { X = ship.Waypoint.X + m.Value; Y = ship.Waypoint.Y}}
    | WestMove -> { ship with Waypoint = { X = ship.Waypoint.X - m.Value; Y = ship.Waypoint.Y}}
    | LeftTurn -> rotateWaypointAroundShip ship m.Value
    | RightTurn -> rotateWaypointAroundShip ship (-m.Value)
    | Forward -> { ship with X = ship.X + (m.Value * ship.Waypoint.X); Y = ship.Y + (m.Value * ship.Waypoint.Y)}

let rec followInstructionsForPart2 (ship:ShipWithWaypoint) (moves:List<Instruction>) = 
    match moves with
    | m::mvs ->  followInstructionsForPart2 (moveWithWaypoint ship m) mvs
    | _ -> ship

let SolveDay12Part1 = 
    let shipOrigin = { Direction = East; X = 0; Y = 0}
 
    let shipEndPosition =
        inputs 
        |> parse
        |> followInstructionsForPart1 shipOrigin

    manhattanDistance (shipEndPosition.X, shipEndPosition.Y)

let SolveDay12Part2 = 
    let shipOrigin = { Waypoint = {X = 10; Y=1}; X = 0; Y = 0 }

    let shipEndPosition = 
        inputs
        |> parse
        |> followInstructionsForPart2 shipOrigin

    manhattanDistance(shipEndPosition.X, shipEndPosition.Y)
