namespace tae_exam_candidate_kp

module Tests =
    open System
    open canopy
    open runner
    open Library

    /// Define test conditions: valid/invalid conditions. These are the same for both test types.
    /// This avoids needing to code the conditions in more than one place.
    ///
    /// TODO: Refactor to clean up all the repetitive entry
    ///
    /// SAT test type
    let sat = "SAT"
    /// SAT (2400) test type
    let sat2400 = "SAT (2400)"


    ///SAT test entry
    /// TODO: Add less simplistic tests that check for specific conditions.
    /// TODO: Data drive invalid tests to use one invalid condition at a time
    /// TODO: Data drive valid tests for more coverage of the valid date range
    let SATTests() = 
        context "SAT Valid score entry"
        "Valid score with valid date can save" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SATLink
            testscore.SetFields (sat, "02/10/2017", "300", "780", "")
            testscore.SaveTest(sat)
            enabled (testscore.Delete(sat, "02/10/2017"))
        
        context "SAT Invalid score entry"
        "Valid scores with invalid date unable to save" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let baddate = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SATLink
            testscore.SetFields (sat, baddate, "300", "780", "")
            testscore.SaveTest(sat)
            enabled (testscore.Cancel(sat))
        
        "Reading below 200 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SATLink
            testscore.SetFields (sat, date, "100", "780", "")
            testscore.SaveTest(sat)
            enabled (testscore.Cancel(sat))
        
        "Math below 200 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SATLink
            testscore.SetFields (sat, date, "300", "110", "")
            testscore.SaveTest(sat)
            enabled (testscore.Cancel(sat))
         
        "Reading above 800 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SATLink
            testscore.SetFields (sat, date, "900", "780", "")
            testscore.SaveTest(sat)
            enabled (testscore.Cancel(sat))
        
        "Math above 800 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SATLink
            testscore.SetFields (sat, date, "300", "910", "")
            testscore.SaveTest(sat)
            enabled (testscore.Cancel(sat))
        
        "Reading not divisible by 10 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SATLink
            testscore.SetFields (sat, date, "871", "780", "")
            testscore.SaveTest(sat)
            enabled (testscore.Cancel(sat))
        
        "Math not divisible by 10 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SATLink
            testscore.SetFields (sat, date, "300", "812", "")
            testscore.SaveTest(sat)
            enabled (testscore.Cancel(sat))
        
        "Only one score with the same date allowed" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SATLink
            testscore.SetFields (sat, "02/10/2017", "300", "780", "")
            testscore.SaveTest(sat)
            enabled (testscore.Cancel(sat))

        context "SAT Cancel entry"
        "Cancel score entry removes line" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SATLink
            testscore.SetFields (sat, "02/09/2017", "300", "780", "")
            testscore.CancelEntry(sat)

        context "SAT Delete entry"
        "Delete after save" &&& fun _ ->
            // for convenience, using the current date already saved - change this when data driving
            let testscore = new Library.TestScoresPage()
            url testscore.url
            waitForElement "#profile-rail"
            testscore.DeleteEntry(sat, "02/10/2017")

    ///SAT (2400) test entry
    /// TODO: Add less simplistic tests that check for specific conditions.
    /// TODO: Data drive invalid tests to use one invalid condition at a time
    /// TODO: Data drive valid tests for more coverage of the valid date range
           
    let SAT2400Tests() = 
        context "SAT (2400) Valid score entry"
        "Valid score with valid date can save" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SAT2400Link
            testscore.SetFields (sat2400, "02/10/2017", "300", "780", "250")
            testscore.SaveTest(sat2400)
            enabled (testscore.Delete(sat2400, "02/10/2017"))
        
        context "SAT (2400) Invalid score entry"
        "Valid score with invalid date unable to save" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let baddate = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SAT2400Link
            testscore.SetFields (sat2400, baddate, "300", "780", "400")
            testscore.SaveTest(sat2400)
            enabled (testscore.Cancel(sat2400))
        
        "Reading below 200 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SAT2400Link
            testscore.SetFields (sat2400, date, "100", "780", "400")
            testscore.SaveTest(sat2400)
            enabled (testscore.Cancel(sat2400))
        
        "Math below 200 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SAT2400Link
            testscore.SetFields (sat2400, date, "300", "110", "400")
            testscore.SaveTest(sat2400)
            enabled (testscore.Cancel(sat2400))
         
        "Reading above 800 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SAT2400Link
            testscore.SetFields (sat2400, date, "900", "780", "400")
            testscore.SaveTest(sat2400)
            enabled (testscore.Cancel(sat2400))
        
        "Math above 800 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SAT2400Link
            testscore.SetFields (sat2400, date, "300", "910", "400")
            testscore.SaveTest(sat2400)
            enabled (testscore.Cancel(sat2400))
        
        "Reading not divisible by 10 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SAT2400Link
            testscore.SetFields (sat2400, date, "871", "780", "400")
            testscore.SaveTest(sat2400)
            enabled (testscore.Cancel(sat2400))
        
        "Math not divisible by 10 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SAT2400Link
            testscore.SetFields (sat2400, date, "300", "812", "400")
            testscore.SaveTest(sat2400)
            enabled (testscore.Cancel(sat2400))
        
        
        "Writing not divisible by 10 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SAT2400Link
            testscore.SetFields (sat2400, date, "871", "780", "405")
            testscore.SaveTest(sat2400)
            enabled (testscore.Cancel(sat2400))
        
        "Writing less than 200 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SAT2400Link
            testscore.SetFields (sat2400, date, "871", "780", "100")
            testscore.SaveTest(sat2400)
            enabled (testscore.Cancel(sat2400))
        
        "Writing more than 800 marked invalid" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            let date = DateTime.Today.AddDays(1.0).Date.ToString("MM/dd/yyyy")
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SAT2400Link
            testscore.SetFields (sat2400, date, "871", "780", "900")
            testscore.SaveTest(sat2400)
            enabled (testscore.Cancel(sat2400))

        "Only one score with the same date allowed" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SAT2400Link
            testscore.SetFields (sat2400, "02/10/2017", "300", "780", "500")
            testscore.SaveTest(sat2400)
            enabled (testscore.Cancel(sat2400))

        context "SAT (2400) Cancel entry"
        "Cancel removes row" &&& fun _ ->
            let testscore = new Library.TestScoresPage()
            url testscore.url
            waitForElement "#profile-rail"
            click testscore.SAT2400Link
            testscore.SetFields (sat2400, "02/09/2017", "300", "780", "500")
            testscore.CancelEntry(sat2400)

        context "SAT (2400) Delete entry"
        "Delete after save" &&& fun _ ->
            // for convenience, using the current date already saved - change this when data driving
            let testscore = new Library.TestScoresPage()
            url testscore.url
            waitForElement "#profile-rail"
            testscore.DeleteEntry(sat2400, "02/10/2017")


    /// Run all Canopy UI tests
    let All() =
        /// Run the SAT tests
        SATTests()
        /// Run the SAT (2400) tests
        SAT2400Tests()


