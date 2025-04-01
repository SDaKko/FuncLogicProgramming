open System

let askUser answer =
    match answer with
    | "F#" | "Prolog" -> "Вы подлиза!"
    | _ -> "Хороший выбор!"


[<EntryPoint>]
let main args =
    let userInput = Console.ReadLine()
    let answer = askUser userInput
    Console.WriteLine(answer)

    (Console.WriteLine("Какой твой любимый язык программирования?"))
    |> fun _ -> System.Console.ReadLine()
    |> askUser
    |> Console.WriteLine
    0