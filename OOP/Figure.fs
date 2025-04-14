open System

type IPrint = interface
    abstract member Print: unit -> unit
    end

[<AbstractClass>]
type GeomFig() =
    abstract Square: unit -> float
    //default this.Square() = Console.WriteLine("Not implemented") //Можно без реализации по умолчанию, если она не требуется
    //Если есть default, то можно не писать abstract, иначе нужно

type Rectangle(width: float, length: float) =
    inherit GeomFig()
    //member this.Width: float = width
    member val Width: float = width with get, set // Свойство 
    member val Length: float = length with get, set
    override this.Square() = this.Width * this.Length
    override this.ToString() = $"Ширина: {this.Width}, Высота: {this.Length}, Площадь: {this.Square()}"
    interface IPrint with 
        member this.Print() = Console.WriteLine(this.ToString())

type SquareFigure(side: float) = 
    inherit Rectangle(side, side)
    override this.ToString() = $"Сторона: {side}, Площадь: {this.Square()}"
    interface IPrint with 
        member this.Print() = Console.WriteLine(this.ToString())

type Circle(radius: float) =
    inherit GeomFig()
    member val Radius: float = radius with get, set
    override this.Square() = 3.14 * this.Radius ** 2
    override this.ToString() = $"Радиус: {this.Radius}, Площадь: {this.Square()}"
    interface IPrint with 
        member this.Print() = Console.WriteLine(this.ToString())

[<EntryPoint>]
let main args =
    let rect = Rectangle(2, 4)
    let rectInterf = rect :> IPrint
    rectInterf.Print()

    let square = SquareFigure(4)
    let squareInterf = square :> IPrint
    squareInterf.Print()

    let circ = Circle(4)
    let circInterf = circ :> IPrint
    circInterf.Print()
    0

