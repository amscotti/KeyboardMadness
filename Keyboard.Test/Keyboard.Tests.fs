module Keyboard.Tests

open FsUnit.Xunit
open Xunit

[<Fact>]
let ``Should select the starting points key`` () =
    Keyboard.init
    |> Keyboard.runInstructions "S"
    |> Keyboard.getSelectedKeys
    |> should equal "G"

[<Fact>]
let ``Should select the first letter to the left of the starting point`` () =
    Keyboard.init
    |> Keyboard.runInstructions "L,S"
    |> Keyboard.getSelectedKeys
    |> should equal "F"

[<Fact>]
let ``Should select the third letter to the left of the starting point`` () =
    Keyboard.init
    |> Keyboard.runInstructions "L:3,S"
    |> Keyboard.getSelectedKeys
    |> should equal "S"

[<Fact>]
let ``Should select the first letter to the right of the starting point`` () =
    Keyboard.init
    |> Keyboard.runInstructions "R,S"
    |> Keyboard.getSelectedKeys
    |> should equal "H"

[<Fact>]
let ``Should select the third letter to the right of the starting point`` () =
    Keyboard.init
    |> Keyboard.runInstructions "R:3,S"
    |> Keyboard.getSelectedKeys
    |> should equal "K"


[<Fact>]
let ``Should select the letter above of the starting point`` () =
    Keyboard.init
    |> Keyboard.runInstructions "U,S"
    |> Keyboard.getSelectedKeys
    |> should equal "T"

[<Fact>]
let ``Should select letter below of the starting point`` () =
    Keyboard.init
    |> Keyboard.runInstructions "D,S"
    |> Keyboard.getSelectedKeys
    |> should equal "B"


[<Fact>]
let ``Should add a space into the selected keys`` () =
    Keyboard.init
    |> Keyboard.runInstructions "S,_,S"
    |> Keyboard.getSelectedKeys
    |> should equal "G G"


[<Fact>]
let ``Should ignore any unknown instructions`` () =
    Keyboard.init
    |> Keyboard.runInstructions "S,Testing,Testing,Testing,S"
    |> Keyboard.getSelectedKeys
    |> should equal "GG"


[<Theory>]
[<InlineDataAttribute("R,S,R:2,U,S", "HI")>]
[<InlineDataAttribute("R,S,U,L:3,S,D,R:6,S,S,U,S", "HELLO")>]
[<InlineDataAttribute("L:3,S,U,R:5,S,R:3,S,D:2,S", "SUP?")>]
[<InlineDataAttribute("R,S,L,U,S,S,R:5,S,_,U:1,L:6,S,R:6,S,L:6,S", "HTTP 404")>]
let ``Should select the correct keys`` (input: string, expected: string) =
    Keyboard.init
    |> Keyboard.runInstructions input
    |> Keyboard.getSelectedKeys
    |> should equal expected
