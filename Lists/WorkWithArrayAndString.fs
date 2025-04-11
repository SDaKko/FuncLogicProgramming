open System
//Задание 18. Напишите программу, заносящую в массив первые 100 натуральных чисел, делящихся на 13 или на 17, и печатающую его.
let numbers =
    Array.init 1000 (fun x -> 1 + x)
    |> Array.filter (fun x -> x % 13 = 0 || x % 17 = 0)
    |> Array.take 100

//Задание 19. Дана строка. Необходимо проверить образуют ли строчные символы латиницы палиндром.
let isPalindrome (str: string) =
    // Рекурсивная функция для фильтрации строчных букв
    let rec filterLowercase acc = function
        | [] -> acc
        | head :: tail when head >= 'a' && head <= 'z' -> filterLowercase (acc @ [head]) tail
        | _ :: tail -> filterLowercase acc tail
    
    // Рекурсивная проверка палиндрома
    let rec checkPalindrome = function
        | [] | [_] -> true
        | head :: tail ->
            let last = List.last tail
            let middle = List.take (List.length tail - 1) tail
            head = last && checkPalindrome middle
    
    str 
    |> List.ofSeq  // Конвертируем строку в список символов
    |> filterLowercase []  // Фильтруем строчные буквы
    |> checkPalindrome
 

[<EntryPoint>]
let main args =
    Console.WriteLine("Результат:")
    Array.iteri (fun i x -> 
        Console.WriteLine("{0,3}: {1}", i+1, x)
    ) numbers

    Console.WriteLine("Проверка на палиндром:")
    let test = "race car"
    Console.WriteLine(isPalindrome test)

    0