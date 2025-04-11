open System
//Задание 18. Напишите программу, заносящую в массив первые 100 натуральных чисел, делящихся на 13 или на 17, и печатающую его.
let numbers =
    Array.init 1000 (fun x -> 1 + x)
    |> Array.filter (fun x -> x % 13 = 0 || x % 17 = 0)
    |> Array.take 100


[<EntryPoint>]
let main args =
    Console.WriteLine("Результат:")
    Array.iteri (fun i x -> 
        Console.WriteLine("{0,3}: {1}", i+1, x)
    ) numbers

    0