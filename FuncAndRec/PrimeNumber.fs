open System

let rec gcd x y = 
    match y with
    | 0 -> x
    | _ -> gcd y (x % y)

let primeTraverse num (func :int->int->int) init =
    let rec primeTail num acc temp =
        match temp with
        | 0 -> acc
        | _ -> 
            let newAcc = if gcd num temp = 1 then (func acc temp) else acc
            primeTail num newAcc (temp-1)
    primeTail num init num

let EulerFunction num = 
    primeTraverse num (fun x y -> x+1) 0

let primeTraverseCondition num (func :int->int->int) (cond :int->bool) init = 
    let rec primeTail num acc temp = 
        match temp with
        | 0 -> acc
        | _ -> 
            let next = temp-1
            let isPrime = if gcd num temp = 1 then true else false
            let flag = cond temp
            match isPrime, flag with
            | true, true -> primeTail num (func acc temp) next
            | _ -> primeTail num acc next
    primeTail num init num
    

[<EntryPoint>]
let main args =
    let answ1 = primeTraverse 123 (fun x y -> x+y) 0
    let colNums = EulerFunction 123
    let countLargeCoprimes = primeTraverseCondition 123 (fun x y -> x + 1) (fun x -> x > 100) 0
    let sumOdd = primeTraverseCondition 123 (fun x y -> x+y) (fun x -> (x%2) <> 0) 0
    Console.WriteLine(answ1)
    Console.WriteLine(colNums)
    Console.WriteLine(countLargeCoprimes)
    Console.WriteLine(sumOdd)

    0