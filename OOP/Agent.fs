open System

// Типы сообщений, которые может обрабатывать агент
type AgentMessage =
    | Greet of string
    | Calculate of int * int
    | GetRandom
    | Shutdown

// Создание и запуск агента
let agent = MailboxProcessor<AgentMessage>.Start(fun inbox ->
    let rec messageLoop() = async {
        let! msg = inbox.Receive()
        
        match msg with
        | Greet name ->
            Console.WriteLine($"Привет, {name}!")
        | Calculate (a, b) ->
            let sum = a + b
            Console.WriteLine($"Сумма {a} и {b} равна {sum}")
        | GetRandom ->
            let rnd = Random().Next(1, 100)
            Console.WriteLine($"Случайное число: {rnd}")
        | Shutdown ->
            Console.WriteLine("Агент завершает работу...")
            return () 
            
        return! messageLoop()
    }
    
    messageLoop()
)

let testAgent() =
    // Отправляем различные сообщения агенту
    agent.Post(Greet "Пользователь")
    agent.Post(Calculate(5, 7))
    agent.Post(GetRandom)
    agent.Post(GetRandom)
    agent.Post(Shutdown)
    
    Threading.Thread.Sleep(100)

[<EntryPoint>]
let main args =
    testAgent()
    0
