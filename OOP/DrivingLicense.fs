open System
open System.Text.RegularExpressions

type DrivingLicense(series: string, number: string, issueDate: DateTime, expiryDate: DateTime, categories: string) =
    // Валидация полей
    let validateSeries (series: string) =
        let pattern = @"^\d{4}$"
        if Regex.IsMatch(series, pattern) then series
        else failwith "Серия должна состоять из 4 цифр" // выбрасывает исключение failwith
    
    let validateNumber (number: string) =
        let pattern = @"^\d{6}$"
        if Regex.IsMatch(number, pattern) then number
        else failwith "Номер должен состоять из 6 цифр"
    
    let validateCategories (categories: string) =
        let pattern = @"^[A-Z](,\s*[A-Z])*$"
        if Regex.IsMatch(categories, pattern) then categories
        else failwith "Категории должны быть в формате 'A,B,C'"
    
    let validatedSeries = validateSeries series
    let validatedNumber = validateNumber number
    let validatedCategories = validateCategories categories
    
    // Проверка даты окончания действия с явной проверкой результата
    let dateCheck = 
        if expiryDate <= issueDate then
            failwith "Дата окончания действия должна быть позже даты выдачи"
        else
            Console.WriteLine("Проверка дат прошла успешно") 
            true
    
    /// Серия прав (4 цифры)
    member this.Series = validatedSeries
    
    /// Номер прав (6 цифр)
    member this.Number = validatedNumber
    
    /// Дата выдачи
    member this.IssueDate = issueDate
    
    /// Дата окончания действия
    member this.ExpiryDate = expiryDate
    
    /// Категории прав (например, "A,B,C")
    member this.Categories = validatedCategories
    
    /// Проверка, действительны ли права на указанную дату
    member this.IsValidOn(date: DateTime) =
        date >= issueDate && date <= expiryDate
    
    /// Проверка, действительны ли права сейчас
    member this.IsCurrentlyValid =
        this.IsValidOn(DateTime.Today)
    
    /// Сравнение по серии и номеру
    interface IComparable with
        member this.CompareTo(other) =
            match other with
            | :? DrivingLicense as otherLicense ->
                let seriesCompare = this.Series.CompareTo(otherLicense.Series)
                if seriesCompare <> 0 then seriesCompare
                else this.Number.CompareTo(otherLicense.Number)
            | _ -> invalidArg "other" "Сравнение возможно только с объектами DrivingLicense"
    
    override this.Equals(other) =
        match other with
        | :? DrivingLicense as otherLicense ->
            this.Series = otherLicense.Series && this.Number = otherLicense.Number
        | _ -> false
    
    override this.GetHashCode() =
        hash (this.Series, this.Number)
    
    // Вывод информации о правах 
    member this.PrintInfo() =
        let categoriesList = this.Categories.Split([|','|], StringSplitOptions.RemoveEmptyEntries)
                            |> Array.map (fun s -> s.Trim())
                            |> String.concat ", "
        Console.WriteLine("Водительское удостоверение {0} {1}", this.Series, this.Number)
        Console.WriteLine("Дата выдачи: {0:dd.MM.yyyy}", this.IssueDate)
        Console.WriteLine("Действительно до: {0:dd.MM.yyyy}", this.ExpiryDate)
        Console.WriteLine("Категории: {0}", categoriesList)
        Console.WriteLine("Статус: {0}", if this.IsCurrentlyValid then "Действительны" else "Недействительны")

let testDrivingLicense() =
    try
        Console.WriteLine("\nСоздаем права 1...")
        let license1 = DrivingLicense("1234", "654321", DateTime(2020, 1, 15), DateTime(2030, 1, 15), "A,B,C")
        
        Console.WriteLine("\nСоздаем права 2...")
        let license2 = DrivingLicense("1234", "123456", DateTime(2019, 5, 20), DateTime(2029, 5, 20), "B,C")
        
        Console.WriteLine("\nСоздаем права 3...")
        let license3 = DrivingLicense("1234", "654321", DateTime(2021, 3, 10), DateTime(2031, 3, 10), "A,D")
        
        Console.WriteLine("\nТестирование вывода документа:")
        license1.PrintInfo()
        
        Console.WriteLine("\nТестирование сравнения:")
        Console.WriteLine($"license1 = license2: {license1 = license2}")
        Console.WriteLine($"license1 = license3: {license1 = license3}")
        Console.WriteLine("license1.CompareTo(license2): {0}", (license1 :> IComparable).CompareTo(license2))
        Console.WriteLine("license2.CompareTo(license1): {0}", (license2 :> IComparable).CompareTo(license1))
        
        // Тест невалидных данных
        Console.WriteLine("\nПопытка создать невалидные права:")
        try
            let invalidLicense = DrivingLicense("12", "123456", DateTime.Now, DateTime.Now.AddYears(10), "A,B")
            Console.WriteLine("Не должно быть выведено")
        with
        | ex -> Console.WriteLine("Ошибка: {0}", ex.Message)
        
    with
    | ex -> Console.WriteLine("Ошибка при тестировании: {0}", ex.Message) // Шаблон ex - любое исключение

[<EntryPoint>]
let main argv =
    Console.WriteLine("Тестирование класса DrivingLicense")
    testDrivingLicense()
    Console.WriteLine("\nТестирование завершено")
    0 