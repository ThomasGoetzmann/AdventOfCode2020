module Year2020Day7

open System.IO
open System.Text.RegularExpressions

let inputs =
    File.ReadLines "inputs/year2020day7-inputs.txt"

type Color = string
type Content = { Color: Color; Amount: int }

type Bag =
    { Color: Color
      Contents: seq<Content> }

let parse line =
    let bagPattern = "(?'color'.*) bags contain "
    let bagMatch = Regex.Match(line, bagPattern)

    let parseContent c =
        let contentPattern =
            @"((?'amount'\d*) (?'color'.*)|(?'none'no other)) bag"

        let m = Regex.Match(c, contentPattern)

        match m with
        | m when m.Groups.[1].Value = "no other" -> { Color = "none"; Amount = 0 }
        | m ->
            { Color = string m.Groups.["color"].Value
              Amount = int m.Groups.["amount"].Value }

    let subIndex = line.IndexOf(" bags contain ") + 14

    { Color = bagMatch.Groups.["color"].Value
      Contents =
          line.Substring(subIndex).Split(',')
          |> Seq.map ((fun x -> x.Trim()) >> parseContent) }

let contentColors (bag: Bag) =
    bag.Contents
    |> Seq.map (fun content -> content.Color)
    |> List.ofSeq

let rec findAllColorsWhichCanContain (color: Color) (bags: seq<Bag>) =
    let rec findColorsAcc (color: Color) (bags: seq<Bag>) (colors: List<string>) =
        let newColors =
            bags
            |> Seq.filter (fun elem ->
                elem.Contents
                |> Seq.exists (fun content -> content.Color = color))
            |> Seq.map (fun x -> x.Color)
            |> List.ofSeq

        if List.isEmpty newColors then
            colors
        else
            newColors
            |> List.fold (fun acc elem -> findColorsAcc (elem) (bags) (acc @ newColors)) colors
            |> List.distinct

    findColorsAcc color bags []

let bagsAmountInColor (color: Color) (bags: seq<Bag>) =
    let rec bagsInBag (color: Color) (bags: seq<Bag>) (amount: int) (multiplicator : int) =
        let bag =
            bags |> Seq.find (fun b -> b.Color = color)

        let isBagEmpty = 
            bag.Contents
            |> Seq.filter (fun c -> c.Color <> "none")
            |> Seq.isEmpty

        if isBagEmpty then
            amount
        else
            bag.Contents
            |> Seq.fold (fun acc elem -> bagsInBag elem.Color bags (acc + (multiplicator * elem.Amount)) (multiplicator * elem.Amount)) amount

    bagsInBag color bags 0 1 

let SolveDay7Part1 =
    inputs
    |> Seq.map parse
    |> findAllColorsWhichCanContain "shiny gold"
    |> List.length

let SolveDay7Part2 =
    inputs
    |> Seq.map parse
    |> bagsAmountInColor "shiny gold"
