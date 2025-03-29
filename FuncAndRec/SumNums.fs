open System
let rec sumNums n =
    if n = 0 then 0
    else (n%10) + (sumNums (n / 10))

let rec sumNumsTail n acc =
    if n = 0 then acc
    else sumNumsTail (n/10) (acc + (n%10))

let sumNumsTail2 n =
    sumNumsTail n 0

[<EntryPoint>]
let main args =
    let sum1 = sumNums 1230
    let sum2 = sumNumsTail2 1230
    Console.WriteLine($"Рекурсия вверх: {sum1}")
    Console.WriteLine($"Хвостовая рекурсия (вниз): {sum2}")
    0