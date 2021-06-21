module Keyboard

open System.Text.RegularExpressions

type Position = int * int
type KeyboardLayout = string [] []

type Keyboard =
    { KeyboardLayout: KeyboardLayout
      Position: Position
      SelectedKeys: string list }

type Instruction =
    | Left of steps: int
    | Up of steps: int
    | Right of steps: int
    | Down of steps: int
    | Space
    | NewLine
    | Select
    | Unknown

let private instructionPattern =
    Regex @"^(?<instruction>L|R|U|D|S|_|N)(:(?<count>\d*))?$"

let private keys: KeyboardLayout = [|
    [| "1"; "2"; "3"; "4"; "5"; "6"; "7"; "8"; "9"; "0" |];
    [| "Q"; "W"; "E"; "R"; "T"; "Y"; "U"; "I"; "O"; "P" |];
    [| "A"; "S"; "D"; "F"; "G"; "H"; "J"; "K"; "L"; ";" |];
    [| "Z"; "X"; "C"; "V"; "B"; "N"; "M"; ","; "."; "?" |];
|]


let private getCount (count: string) : int =
    match System.Int32.TryParse(count: string) with
    | (true, int) -> int
    | _ -> 1

let private stringToInstruction (step: string) : Instruction =
    let m = instructionPattern.Match(step)

    if m.Success then
        let instruction = m.Groups.["instruction"].Value
        let count = m.Groups.["count"].Value |> getCount

        match instruction with
        | "R" -> Right(count)
        | "L" -> Left(count)
        | "D" -> Down(count)
        | "U" -> Up(count)
        | "_" -> Space
        | "N" -> NewLine
        | "S" -> Select
        | _ -> Unknown
    else
        Unknown

let private updatePosition (position: Position) (keyboard: Keyboard) : Keyboard = { keyboard with Position = position }

let private updateSelectedKeys (key: string) (keyboard: Keyboard) : Keyboard =
    { keyboard with
          SelectedKeys = key :: keyboard.SelectedKeys }

let private execute (keyboard: Keyboard) (instruction: Instruction) : Keyboard =
    let (x, y) = keyboard.Position

    match instruction with
    | Left (count) -> updatePosition (x - count, y) keyboard
    | Up (count) -> updatePosition (x, y - count) keyboard
    | Right (count) -> updatePosition (x + count, y) keyboard
    | Down (count) -> updatePosition (x, y + count) keyboard
    | Space -> updateSelectedKeys " " keyboard
    | NewLine -> updateSelectedKeys "\n" keyboard
    | Select -> updateSelectedKeys keyboard.KeyboardLayout.[y].[x] keyboard
    | Unknown -> keyboard

let init : Keyboard =
    { KeyboardLayout = keys
      Position = (4, 2)
      SelectedKeys = [] }

let getSelectedKeys (keyboard: Keyboard) : string =
    keyboard.SelectedKeys
    |> List.rev
    |> String.concat ""

let runInstructions (instructions: string) (keyboard: Keyboard) =
    instructions.ToUpper().Split(",")
    |> Seq.map stringToInstruction
    |> Seq.fold execute keyboard
