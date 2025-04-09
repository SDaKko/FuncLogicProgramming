open System

//Вариант 9
let rec readList n = 
    if n=0 then []
    else
    let Head = Convert.ToInt32(Console.ReadLine())
    let Tail = readList (n-1)
    Head::Tail

let readData = 
    Console.WriteLine("Введите количество:")
    let n=Convert.ToInt32(System.Console.ReadLine())
    Console.WriteLine("Введите элементы:")
    readList n

let rec writeList = function
    [] ->   let z = System.Console.ReadKey()
            0
    | (head : 'string)::tail -> 
                       System.Console.WriteLine(head)
                       writeList tail

let rec accCond list (f : int -> int -> int) p acc = 
    match list with
    | [] -> acc
    | h::t ->
                let newAcc = f acc h
                if p h then accCond t f p newAcc
                else accCond t f p acc

//Задание 10
let sortedByLength strings: string list = 
    List.sortBy (fun s -> s.Length) strings

//Задание 11. Дан целочисленный массив. Необходимо найти элементы, расположенные перед последним минимальным.
let rec findLastMinIndex currentIndex lastIndex minVal = function
    | [] -> lastIndex
    | head::tail ->
        let newMin = if head <= minVal then head else minVal
        let newLastIndex = if head = minVal then currentIndex else lastIndex
        findLastMinIndex (currentIndex + 1) newLastIndex newMin tail

let listMin list = 
    match list with 
    | [] -> 0
    | h::t -> accCond list (fun x y -> if x < y then x else y) (fun x -> true) h

let lastIndex list = findLastMinIndex 0 (-1) (listMin list) list

let rec elementsBeforeLastMinRec list lastInd =
    match list with 
    | [] -> []
    | head::tail -> 
        match lastInd with
        | _ when lastInd > 0 ->     
            let tail = elementsBeforeLastMinRec tail (lastInd-1)
            head::tail
        | _ -> []

let elementsBeforeLastMin list =
    elementsBeforeLastMinRec list (lastIndex list)

let elementsBeforeLastMinClassList (list: int list) =
    let minValue = List.min list
    let lastMinIndex = 
        list 
        |> List.rev
        |> List.findIndex (fun x -> x = minValue)
        |> fun i -> list.Length - 1 - i
    list |> List.take lastMinIndex
  
//Задание 12. Дан целочисленный массив. Необходимо осуществить циклический сдвиг элементов массива вправо на одну позицию.
// 1. Функция сбора элементов
let rec collectLast last acc = function
    | [] -> (last, acc)
    | [x] -> (x, acc)
    | head :: tail -> collectLast last (acc @ [head]) tail

// 2. Основная функция
let cyclicShiftRight list =
    match list with
    | [] -> []
    | [x] -> [x]
    | head :: tail -> 
        let (last, front) = collectLast head [head] tail
        last :: front

let cyclicShiftRightClassList list =
    match List.length list with
    | 0 -> []
    | 1 -> list
    | len ->
        let last = List.last list
        let front = List.take (len - 1) list
        last :: front




[<EntryPoint>]
let main args = 
    
    let list = readData
    Console.WriteLine("Исходный список:")
    writeList list
    let listBeforeMin = elementsBeforeLastMin list
    Console.WriteLine("Список до последнего минимального элемента:")
    writeList listBeforeMin
    let listBeforeMinClassList = elementsBeforeLastMinClassList list
    Console.WriteLine("Список до последнего минимального элемента (методы List):")
    writeList listBeforeMinClassList
    let listAfterShift = cyclicShiftRight list
    Console.WriteLine("Список после сдвига вправо:")
    writeList listAfterShift
    let listAfterShiftClass = cyclicShiftRightClassList list
    Console.WriteLine("Список после сдвига вправо (методы List):")
    writeList listAfterShiftClass

    0
