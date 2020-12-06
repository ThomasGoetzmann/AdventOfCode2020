module Year2020Day4

open System
open System.IO
open System.Text.RegularExpressions

let inputs =
    File.ReadAllText "inputs/year2020day4-inputs.txt"

let parseLines (input: string) =
    let separator =
        Environment.NewLine + Environment.NewLine

    input.Split separator

type Passport =
    { BYR: Option<string>
      IYR: Option<string>
      EYR: Option<string>
      HGT: Option<string>
      HCL: Option<string>
      ECL: Option<string>
      PID: Option<string>
      CID: Option<string> }

let toPassports line =
    let matchPasswordValue key =
        let pattern = key + @":(\S*)( |\r\n?|\n|$)"
        Regex.Match(line, pattern)

    let matchBYR = matchPasswordValue "byr"
    let matchIYR = matchPasswordValue "iyr"
    let matchEYR = matchPasswordValue "eyr"
    let matchHGT = matchPasswordValue "hgt"
    let matchHCL = matchPasswordValue "hcl"
    let matchECL = matchPasswordValue "ecl"
    let matchPID = matchPasswordValue "pid"
    let matchCID = matchPasswordValue "cid"

    { BYR = if matchBYR.Success then Some(string matchBYR.Groups.[1].Value) else None
      IYR = if matchIYR.Success then Some(string matchIYR.Groups.[1].Value) else None
      EYR = if matchEYR.Success then Some(string matchEYR.Groups.[1].Value) else None
      HGT = if matchHGT.Success then Some(string matchHGT.Groups.[1].Value) else None
      HCL = if matchHCL.Success then Some(string matchHCL.Groups.[1].Value) else None
      ECL = if matchECL.Success then Some(string matchECL.Groups.[1].Value) else None
      PID = if matchPID.Success then Some(string matchPID.Groups.[1].Value) else None
      CID = if matchCID.Success then Some(string matchCID.Groups.[1].Value) else None }

let hasAllMandatoryFields p =
    p.BYR.IsSome
    && p.IYR.IsSome
    && p.EYR.IsSome
    && p.HGT.IsSome
    && p.HCL.IsSome
    && p.ECL.IsSome
    && p.PID.IsSome

let isValid (p: Passport) =
    let isBYRValid =
        let isInt, intBYR = Int32.TryParse p.BYR.Value
        isInt && 1920 <= intBYR && intBYR <= 2002

    let isIYRValid =
        let isInt, intIYR = Int32.TryParse p.IYR.Value
        isInt && 2010 <= intIYR && intIYR <= 2020

    let isEYRValid =
        let isInt, intEYR = Int32.TryParse p.EYR.Value
        isInt && 2020 <= intEYR && intEYR <= 2030

    let isHGTValid =
        let pattern = @"^(\d*)(cm|in)$"

        match Regex.Match(p.HGT.Value, pattern) with
        | m when m.Groups.[2].Value = "cm" ->
            let isInt, height = Int32.TryParse m.Groups.[1].Value
            isInt && 150 <= height && height <= 193
        | m when m.Groups.[2].Value = "in" ->
            let isInt, height = Int32.TryParse m.Groups.[1].Value
            isInt && 59 <= height && height <= 76
        | _ -> false

    let isHCLValid =
        let pattern = "^#[0-9a-f]{6}$"
        Regex.IsMatch(p.HCL.Value, pattern)

    let isECLValid =
        let pattern = "^(amb|blu|brn|gry|grn|hzl|oth)$"
        Regex.IsMatch(p.ECL.Value, pattern)

    let isPIDValid =
        let pattern = @"^\d{9}$"
        Regex.IsMatch(p.PID.Value, pattern)

    isBYRValid
    && isIYRValid
    && isEYRValid
    && isHGTValid
    && isHCLValid
    && isECLValid
    && isPIDValid


let SolveDay4Part1 =
    inputs
    |> parseLines
    |> Seq.map toPassports
    |> Seq.filter hasAllMandatoryFields
    |> Seq.length

let SolveDay4Part2 =
    inputs
    |> parseLines
    |> Seq.map toPassports
    |> Seq.filter hasAllMandatoryFields
    |> Seq.filter isValid
    |> Seq.length
