open System

type Maybe<'a> =
    | Just of 'a
    | Nothing

//реализация fMap для Maybe
let fmapMaybe f p =
    match p with
        | Just a -> Just (f a)
        | Nothing -> Nothing

//Закон 1: Закон сохранения идентичности: map id = id
let id x = x
let law1 x =
    let left = fmapMaybe id x
    let right = id x
    left = right

//Закон 2: Закон композиции: map (g ∘ f) = map g ∘ map f
let law2 f g x =
    let ans_1 = fmapMaybe f x
    let right = fmapMaybe g ans_1
    let left = fmapMaybe (g << f) x
    // Или можно так let right = (fmapMaybe g << fmapMaybe f) x
    left = right

//Реализация Аппликативного функтора
let returnMaybe x = Just x

let applyMaybe lf p =
    match lf, p with
    | Just f, Just x -> Just (f x)
    | _ -> Nothing

//Закон 1: Закон идентичности: apply (return id) (return v) = id v
let lawAp1 v =
    applyMaybe (returnMaybe id) v = id v

//Закон 2: Если y=f(x), то подъем функции f и значения х и применение к ним функции apply приведет к такому же результату, что и подъем y.
let lawAp2 f x =   
    let up_x = returnMaybe x
    let up_f = returnMaybe f
    let up_y = applyMaybe up_f up_x
    let up_y2 = returnMaybe (f x)
    up_y = up_y2

//Закон 3: Аргументы apply можно менять местами.
let applyMaybe2 p lf =
    match p, lf with
    | Just x, Just f -> Just (f x)
    | _ -> Nothing

let lawAp3 x =
    let app_t_1 = applyMaybe (returnMaybe id) (returnMaybe x) 
    let app_t_2 = applyMaybe2 (returnMaybe x) (returnMaybe id)
    app_t_1 = app_t_2


//Реализация Монады
//Монада применяет к поднятому значению функцию от обычного аргумента, которая возвращает поднятое значение.
// bind определяется типов m<a> -> (a->m<b>) -> m<b>
let bind (m: Maybe<'a>) (f: 'a -> Maybe<'b>) : Maybe<'b> =
    match m with
    | Just x -> f x
    | Nothing -> Nothing

//Закон 1: Функция return может быть поднята с помощью функции bind. Если это сделать, то она должна быть эквивалентна функции id.
let lawM1 m =
    bind m returnMaybe = m

// Закон 2 похож на закон 1, но в нем изменен на обратный порядок вызовов функций bind и return. То есть 
// Если применить поднятую версию f к поднятой версии a, то получится поднятое значение b:
let lawM2 x f =
    bind (returnMaybe x) f = f x

//Закон 3 говорит о том, что после поднятия ассоциативность сохраняется:
let lawM3 m f g =
    bind (bind m f) g = bind m (fun x -> bind (f x) g)


[<EntryPoint>]
let main args =
    // Проверка законов функтора
    Console.WriteLine("Проверка законов функтора:")
    Console.WriteLine($"law1 (Just 5) = {law1 (Just 5)}")
    Console.WriteLine($"law1 Nothing = {law1 Nothing}")

    let f = fun x -> x + 1
    let g = fun x -> x * 2
    Console.WriteLine($"law2 f g (Just 3) = {law2 f g (Just 3)}")
    Console.WriteLine($"law2 f g Nothing = {law2 f g Nothing}")

    // Проверка законов аппликативного функтора
    Console.WriteLine("\nПроверка законов аппликативного функтора:")
    Console.WriteLine($"lawAp1 (Just 5) = {lawAp1 (Just 5)}")
    Console.WriteLine($"lawAp1 Nothing = {lawAp1 Nothing}")

    let app_f = fun x -> x + 1
    Console.WriteLine($"lawAp2 app_f 5 = {lawAp2 app_f 5}")

    Console.WriteLine($"lawAp3 5 = {lawAp3 5}")

    // Проверка законов монады
    Console.WriteLine("\nПроверка законов монады:")
    Console.WriteLine($"lawM1 (Just 5) = {lawM1 (Just 5)}")
    Console.WriteLine($"lawM1 Nothing = {lawM1 Nothing}")

    let funcM = fun x -> Just (x + 1)
    Console.WriteLine($"lawM2 5 funcM = {lawM2 5 funcM}")

    let funcM3 = fun x -> Just (x + 1)
    let gM3 = fun x -> Just (x * 2)
    Console.WriteLine($"lawM3 (Just 5) funcM3 gM3 = {lawM3 (Just 5) funcM3 gM3}")
    Console.WriteLine($"lawM3 Nothing funcM3 gM3 = {lawM3 Nothing funcM3 gM3}")

    0

