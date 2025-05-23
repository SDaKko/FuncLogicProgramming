open System

//139

let rec gcd a b =
    match b with
    | 0 -> a
    | _ -> gcd b (a % b)

let generatePythagoreanTriples maxPerimeter =
    let sqrtMax = int (sqrt (float (maxPerimeter / 2)))
    
    let rec processM m acc =
 
        let rec processN m n acc =
        
            let rec processK a b c k acc =
                match k with
                | 0 -> acc
                | _ -> processK a b c (k - 1) ((a*k, b*k, c*k) :: acc)
            
            match n with
            | 0 -> acc
            | _ ->
                match (m - n) % 2, gcd m n with
                | 1, 1 ->
                    let a = m*m - n*n
                    let b = 2*m*n
                    let c = m*m + n*n
                    match a + b + c <= maxPerimeter with
                    | true -> 
                        let kMax = maxPerimeter / (a + b + c)
                        let triplesForMN = processK a b c kMax []
                        processN m (n - 1) (triplesForMN @ acc)
                    | false -> processN m (n - 1) acc
                | _ -> processN m (n - 1) acc
        
        match m > sqrtMax with
        | true -> acc
        | false ->
            let triplesForM = processN m (m - 1) []
            processM (m + 1) (triplesForM @ acc)
    
    processM 2 []

let satisfiesCondition (a, b, c) =
    let gapSize = abs (a - b)
    gapSize <> 0 && c % gapSize = 0

let filterAndCount predicate list =
    let rec loop count remainingList =
        match remainingList with
        | [] -> count
        | head :: tail ->
            let newCount = 
                match predicate head with
                | true -> count + 1
                | false -> count
            loop newCount tail
    loop 0 list

let countSuitableTriples maxPerimeter =
    generatePythagoreanTriples maxPerimeter
    |> filterAndCount satisfiesCondition


[<EntryPoint>]
let main argv =
    let result = countSuitableTriples 100000000
    Console.WriteLine(result)
    0