open System
let rec sumNums n =
    if n = 0 then 0
    else (n%10) + (sumNums (n / 10))

let rec sumNumsTail n acc =
    if n = 0 then acc
    else sumNumsTail (n/10) (acc + (n%10))

let sumNumsTail2 n =
    sumNumsTail n 0

let rec numDivisorsDown n del count =
    match del with
    | 0 -> count
    | _ when ((n % del) = 0) -> (numDivisorsDown n (del-1) (count+1))
    | _ -> numDivisorsDown n (del-1) count

let numDivisorsMainDown n =
    numDivisorsDown n n 0

//Возвращается функция
let returnFunc bool n =
    match bool with
    | true -> sumNumsTail2 n 
    | false -> numDivisorsMainDown n

//int -> int -> int два аргумента принимает функция
let rec cifrFold num (f: int -> int -> int) acc =
    let newAcc = f acc (num%10)
    match num with 
    | 0 -> acc
    | _ -> cifrFold (num/10) f newAcc

// p - предикат (условие)
let rec cifrFold_ex9 num (f: int -> int -> int) acc (p: int -> bool) =
    let newAcc = if (p num) then (f acc (num%10)) else acc
    match num with 
    | 0 -> acc
    | _ -> cifrFold_ex9 (num/10) f newAcc p
    

[<EntryPoint>]
let main args =
    let sum1 = sumNums 1230
    let sum2 = sumNumsTail2 1230
    let func = returnFunc false 12
    let sumcNew = cifrFold 123 (fun x y -> x+y) 0
    let minCifr = cifrFold 123 (fun x y -> if x<y then x else y) 10
    let colNums = cifrFold 123 (fun x y -> x+1) 0

    let sumcNew2 = cifrFold_ex9 123 (fun x y -> x+y) 0 (fun x -> (x%2) <> 0)
    let minCifr2 = cifrFold_ex9 123 (fun x y -> if x<y then x else y) 10 (fun x -> (x%2) > 0)
    let colNums2 = cifrFold_ex9 123 (fun x y -> x+1) 0 (fun x -> true)

    Console.WriteLine(func)
    Console.WriteLine(sumcNew)
    Console.WriteLine(minCifr)
    Console.WriteLine(colNums)

    Console.WriteLine(sumcNew2)
    Console.WriteLine(minCifr2)
    Console.WriteLine(colNums2)
    Console.WriteLine($"Рекурсия вверх: {sum1}")
    Console.WriteLine($"Хвостовая рекурсия (вниз): {sum2}")
    0