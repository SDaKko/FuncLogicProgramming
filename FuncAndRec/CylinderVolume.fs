open System
let cylinderVolume (circleArea: float -> float) radius length =
    (circleArea radius) * length 
    
let circleArea radius =
    let pi = 3.14159
    pi * radius * radius

let cylinderVolumeComposition area length =
    length * area

let h = circleArea >> cylinderVolumeComposition

let cylinderVolumeCurry radius length = 
    (circleArea radius) * length


[<EntryPoint>]
let main argv =
    let square = circleArea 10.0
    let volume = cylinderVolume circleArea 10.0 12.0
    let answerComposition = h 10.0 12.0
    let answerCurry1 = cylinderVolumeCurry 10.0
    let answerCurry2 = answerCurry1 12.0

    Console.WriteLine("{0}", answerCurry2)
    Console.WriteLine("{0}", answerComposition)
    Console.WriteLine("{0}", volume)

    0

