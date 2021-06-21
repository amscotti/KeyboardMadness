open Keyboard


[<EntryPoint>]
let main argv =
    match argv with
    | [| instructions |] ->
        Keyboard.init
        |> Keyboard.runInstructions instructions
        |> Keyboard.getSelectedKeys
        |> printfn "%s"

        0
    | _ ->
        printfn "No instructions given"
        -1
