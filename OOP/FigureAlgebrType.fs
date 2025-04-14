open System
// Алгебраический тип или "Discriminated Unions"

type GeomFigure =
    | Rectangle of width: double * length: double //кортеж из двух double
    | SquareFigure of side: double
    | Circle of radius: double 

let findSquare (figure: GeomFigure) =
    match figure with
    | Rectangle (width, len) -> width * len
    | SquareFigure (side) -> side * side
    | Circle (radius) -> 3.14 * radius ** 2
    
[<EntryPoint>]
let main args =
    Console.WriteLine(findSquare(Rectangle (2, 4)))
    Console.WriteLine(findSquare(SquareFigure 4))
    Console.WriteLine(findSquare(Circle 4))

    0
