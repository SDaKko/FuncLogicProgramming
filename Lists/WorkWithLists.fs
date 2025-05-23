﻿open System

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

//Задание 16. Дан список. Построить новый список из квадратов неотрицательных чисел, меньших 100 и встречающихся в массиве больше 2 раз.
// Функция подсчета вхождений числа в список
let countOccurrences num list =
    let rec count acc = function
        | [] -> acc
        | head :: tail when head = num -> count (acc + 1) tail
        | _ :: tail -> count acc tail
    count 0 list

// Функция проверки наличия элемента в списке
let rec exists_el value = function
    | [] -> false
    | head :: tail -> head = value || exists_el value tail

// Основная функция обработки
let filterAndSquare list =
    let rec processList acc = function
        | [] -> acc
        | head :: tail ->
            if head >= 0 && head < 100 then
                let cnt = countOccurrences head list
                let squared = head * head
                if cnt > 2 && not (exists_el squared acc) then
                    processList (acc @ [squared]) tail
                else
                    processList acc tail
            else
                processList acc tail
    
    processList [] list

//С использованием методов List
// Основная функция с использованием List-методов
let filterAndSquareClassList list =
    list
    |> List.filter (fun x -> x >= 0 && x < 100)  // Фильтрация по диапазону
    |> List.countBy id                            // Подсчет вхождений, функция id возвращает свой же аргумент
    |> List.filter (fun (x, count) -> count > 2)  // Фильтрация по количеству
    |> List.map (fun (x, _) -> x * x)            // Возведение в квадрат
    |> List.distinct                             // Удаление дубликатов

//Задание 17.
// Проверка, является ли число полным квадратом любого элемента списка
let isPerfectSquare n list =
    let rec check = function
        | [] -> false
        | head :: tail -> head * head = n || check tail
    check list

// Проверка, делится ли число на все предыдущие элементы
let divisibleByAllPrev n prevList =
    let rec check = function
        | [] -> true
        | head :: tail -> n % head = 0 && check tail
    check prevList

// Подсчет элементов списка, больших заданного числа
let countGreaterThan n list =
    let rec count acc = function
        | [] -> acc
        | head :: tail -> count (if head > n then acc + 1 else acc) tail
    count 0 list

// Основная функция обработки списка
let processList inputList =
    let rec loop acc prevSum prevElems = function
        | [] -> acc
        | head :: tail ->
            let isSquare = isPerfectSquare head inputList
            let divisible = divisibleByAllPrev head prevElems
            let greaterThanSum = head > prevSum
            let count = countGreaterThan head inputList
            
            let newAcc = 
                if isSquare && divisible && greaterThanSum then
                    acc @ [(head, prevSum, count)]
                else
                    acc
            
            loop newAcc (prevSum + head) (prevElems @ [head]) tail
    
    loop [] 0 [] inputList


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
    let listSquares = filterAndSquare list
    Console.WriteLine("Список квадратов чисел:")
    writeList listSquares
    let listSquaresClassList = filterAndSquareClassList list
    Console.WriteLine("Список квадратов чисел (методы List):")
    writeList listSquaresClassList
    let newList = processList list
    Console.WriteLine("Список кортежей:")
    writeList newList
    0
