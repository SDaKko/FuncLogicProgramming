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


//Задание 13. Дан целочисленный массив и интервал a..b. Необходимо проверить наличие максимального элемента массива в этом интервале.
// 1. Функция поиска максимального элемента 
let rec findMaxRec currentMax = function
    | [] -> currentMax
    | head :: tail ->
        let newMax = if head > currentMax then head else currentMax
        findMaxRec newMax tail

// 2. Функция проверки попадания числа в интервал
let isInRange number a b =
    a <= number && number <= b

// 3. Основная функция
let isMaxInInterval list a b =
    match list with
    | [] -> false
    | head::tail -> 
        let maxElement = findMaxRec head list
        isInRange maxElement a b

let isMaxInIntervalClassList list a b =
    if List.isEmpty list then
        false
    else
        let maxElement = List.max list
        isInRange maxElement a b

//Задание 14. Дан целочисленный массив. Необходимо вывести вначале его элементы с четными индексами, а затем - с нечетными.
let splitEvenOddIndices list =
    let rec collectInOrder evenAcc oddAcc index = function
        | [] -> (evenAcc, oddAcc)
        | head :: tail ->
            if index % 2 = 0 then
                collectInOrder (evenAcc @ [head]) oddAcc (index + 1) tail
            else
                collectInOrder evenAcc (oddAcc @ [head]) (index + 1) tail
    
    let (evens, odds) = collectInOrder [] [] 0 list
    evens @ odds

let splitEvenOddIndicesClassList list =
    let indexed = List.indexed list  // Преобразуем в список пар (индекс, значение)
    let evens = indexed |> List.filter (fun (i, _) -> i % 2 = 0) |> List.map snd //snd - функция возвращает второй элемент кортежа
    let odds = indexed |> List.filter (fun (i, _) -> i % 2 = 1) |> List.map snd
    List.append evens odds

//Задание 15. Для введенного списка положительных чисел построить список всех положительных простых делителей элементов списка без повторений.
let rec countDividers num temp count =
    match temp with
    | 0 -> count
    | _ -> if (num%temp=0) then countDividers num (temp-1) (count+1) 
           else countDividers num (temp-1) count

let isPrime num =
    let count = countDividers num num 0
    match count with
    | 2 -> true
    | _ -> false

// Получение простых делителей числа 
let getPrimeDivisors n =
    let rec findDivisors acc d =
        if d > n then acc
        elif n % d = 0 && isPrime d then
            findDivisors (acc @ [d]) (d + 1)
        else
            findDivisors acc (d + 1)
    findDivisors [] 2

// Проверка наличия элемента в списке
let rec exists predicate = function
    | [] -> false
    | head :: tail -> predicate head || exists predicate tail

// Объединение списков без дубликатов 
let rec mergeUnique acc lst =
    match lst with
    | [] -> acc
    | head :: tail ->
        if exists (fun y -> y = head) acc then
            mergeUnique acc tail
        else
            mergeUnique (acc @ [head]) tail

// Основная функция 
let getAllPrimeDivisors list =
    let rec collect acc = function
        | [] -> acc
        | head :: tail ->
            let divisors = getPrimeDivisors head
            let newAcc = mergeUnique acc divisors
            collect newAcc tail
    
    collect [] list

//Используя методы класса List
// Получение простых делителей с использованием List.filter
let getPrimeDivisorsClassList n =
    [2..n] 
    |> List.filter (fun d -> n % d = 0 && isPrime d)

// Основная функция с использованием List.collect и List.distinct
let getAllPrimeDivisorsClassList list =
    list
    |> List.collect getPrimeDivisors  // Собираем все делители
    |> List.distinct                  // Удаляем дубликаты
    |> List.sort                     // Сортируем



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
    let isMaxElemInterval = isMaxInInterval list 25 35
    Console.WriteLine("Есть ли маскимальный элемент в интервале:")
    Console.WriteLine(isMaxElemInterval)
    let isMaxElemIntervalClassList = isMaxInIntervalClassList list 25 35
    Console.WriteLine("Есть ли маскимальный элемент в интервале (методы List):")
    Console.WriteLine(isMaxElemIntervalClassList)
    let listEvenOdd = splitEvenOddIndices list
    Console.WriteLine("Список элементов сначала с четными, затем с нечетными индексами:")
    writeList listEvenOdd
    let listEvenOddClassList = splitEvenOddIndicesClassList list
    Console.WriteLine("Список элементов сначала с четными, затем с нечетными индексами (методы List):")
    writeList listEvenOddClassList
    let listDivisors = getAllPrimeDivisors list
    Console.WriteLine("Список уникальных простых делителей чисел:")
    writeList listDivisors
    let listDivisorsClassList = getAllPrimeDivisorsClassList list
    Console.WriteLine("Список уникальных простых делителей чисел (методы List):")
    writeList listDivisorsClassList

    0
