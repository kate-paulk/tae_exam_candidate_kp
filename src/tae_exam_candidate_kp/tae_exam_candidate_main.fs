namespace tae_exam_candidate_kp
open System
open canopy
open runner
open reporters

module main = 
    let loggedin() =
        browser.PageSource.Contains("Kate")
    
    [<EntryPoint>]
    let main args =
        start firefox
        reporter <- new ConsoleReporter() :> IReporter
        let login = new Library.LoginPage()
        login.LoginAs("katepaulk@gmail.com", "Test1234!")
        waitFor loggedin
        if login.IsLoggedIn() then 
            Tests.All()
            run()
        else printfn "Unable to log in"
        printfn "press [enter] to exit"
        System.Console.ReadLine() |> ignore
        quit()
        0