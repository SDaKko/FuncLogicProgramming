open System
//Вариант 9

//1
let rec countDividers num temp count =
    match temp with
    | 0 -> count
    | _ -> if (num%temp=0) then countDividers num (temp-1) (count+1) else countDividers num (temp-1) count


let isPrime num =
    let count = countDividers num num 0
    match count with
    | 2 -> true
    | _ -> false
    
let rec maxDividerTail num temp (p1: int -> bool) (p2: int -> bool) =
    match (num%temp), p1 temp, p2 temp with
    | 0, true, true -> temp
    | _, _, _ -> maxDividerTail num (temp-1) p1 p2

let maxPrimeDivider num = 
    maxDividerTail num num isPrime (fun _ -> true)

//2
let isDivFive num =
    match (num%10)%5 with
    | 0 -> true
    | _ -> false

let rec multNumsTail num acc (p:int -> bool) =
    match num with
    | 0 -> acc
    | _ ->
        match p num with
        | true -> multNumsTail (num/10) acc p
        | false -> multNumsTail (num/10) (acc * (num%10)) p

let multNumsDivOnFive num = 
    multNumsTail num 1 isDivFive

//3
let rec gcd x y = 
    match y with
    | 0 -> x
    | _ -> gcd y (x % y)

let isOdd num =
    match (num%2) with
    | 0 -> false
    | _ -> true

let isNotPrime num =
    let count = countDividers num num 0
    match count with
    | 2 -> false
    | _ -> true

let find (gcdNum: int -> int  -> int) num (p1: int -> bool) (p2: int -> bool) (p3: int -> bool) =
    let x = maxDividerTail num num p1 p2
    let y = multNumsTail num 1 p3
    gcdNum x y 

let gcdFindPredicats  num =
    find gcd num isOdd isNotPrime (fun _ -> false)

let choice (num, arg) =
    match num with 
    | 1 -> Console.WriteLine($"Максимальный простой делитель числа = {maxPrimeDivider arg}")
    | 2 -> Console.WriteLine($"Произведение цифр числа, не делящихся на 5 = {multNumsDivOnFive arg}") 
    | 3 -> Console.WriteLine($"НОД максимального нечетного непростого делителя числа и прозведения цифр данного числа = {gcdFindPredicats arg}") 
    | _ -> Console.WriteLine("Введен неверный номер функции!")

[<EntryPoint>]
let main args =
    Console.WriteLine("Введите номер функции (1-3): ")
    let num = Console.ReadLine() |> int
    Console.WriteLine("Введите аргумент функции: ")
    let arg = Console.ReadLine() |> int
    let tuple = (num, arg)
    choice(tuple)

    0
