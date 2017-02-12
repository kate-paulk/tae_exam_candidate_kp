namespace tae_exam_candidate_kp

open System
open System.Linq
open canopy
open OpenQA.Selenium
/// Documentation for my library
///
/// ## Example
///
///     let user = new Library.LoginPage()
///     user.LoginAs("username", "password")
///     print user.IsLoggedIn()
///
module Library = 
  
    /// Login page object
    ///
    type LoginPage() =
        /// Url - currently hard-coded
        member l.url = "https://app.scoir.com/signin"

        /// Login routine - uses Canopy to perform the login
        /// ## Parameters
        ///  - `username` 
        ///  - `password` 
        member l.LoginAs(username, password) = 
            url l.url
            "#user-email" << username
            "#user-password" << password
            let button = elements("button").Head
            click button
    
        /// Checks for whether the user is logged in.
        /// User name is hard-coded - make this data driven. 
        /// Note that normal !string.Equals() is not possible due to F# using ! as shorthand for a mutable value
        /// ## Returns
        /// - bool true if both conditions return true
        ///    
        member l.IsLoggedIn() :bool =
            (currentUrl().Equals("https://app.scoir.com/signin") = false) && (browser.PageSource.Contains("Kate"))

    /// Test scores page
    ///
    /// Handles test data entry and locating the test fields.
    type TestScoresPage() =
        /// Url - currently hard-coded
        member t.url = "https://app.scoir.com/student/profile/edit/testing"

        /// Add SAT Link)
        member t.SATLink = browser.FindElement(By.PartialLinkText "Add SAT Scores")

        /// Add SAT (2400) Link
        member t.SAT2400Link =  browser.FindElement(By.PartialLinkText "Add SAT (2400) Scores")

        /// Set test date
        /// ## Parameters
        ///   - `element` - IWebElement representing the date entry field
        ///   - `date` - Test date
        member t.SetDate(element: IWebElement, date) =
            try 
                // this will be a challenge
                let jsString = "arguments[0].setAttribute('readonly', 'false'); arguments[0].value=arguments[1];"  // this would be much nicer without the readonly attribute on the date element
                (browser :?> IJavaScriptExecutor).ExecuteScript(jsString, element, date) |> ignore
                element.Click()
                press tab
            with 
                | _ -> reporter.write ("Failure setting date") 


        /// Set test reading & writing score
        /// ## Parameters
        ///   - `element` - IWebElement representing the reading score entry field
        ///   - `reading` - reading score
        member t.SetReadingWriting(element: IWebElement, reading) =
            try
                element << reading
                element.Click()
                press tab
            with
                | _ -> reporter.write ("Failure setting reading/writing score")

        /// Set test reading score
        /// ## Parameters
        ///   - `element` - IWebElement representing the reading score entry field
        ///   - `reading` - reading score
        member t.SetReading(element: IWebElement, reading) =
            try
                element << reading
                element.Click()
                press tab
            with
                | _ -> reporter.write ("Failure setting reading score")

        /// Set test math score
        /// ## Parameters
        ///   - `element` - IWebElement representing the math score entry field
        ///   - `math` - math score
        member t.SetMath(element: IWebElement, math) =
            try
                element << math
                element.Click()
                press tab
            with
                | _ -> reporter.write ("Failure setting math score")

        /// Set test reading score
        /// ## Parameters
        ///   - `element` - IWebElement representing the reading score entry field
        ///   - `reading` - reading score
        member t.SetWriting(element: IWebElement, writing) =
            try
                element << writing
                element.Click()
                press tab
            with
                | _ -> reporter.write ("Failure setting writing score")

        /// Set test date and scores
        /// ## Parameters
        ///   - `testtype` - string used to locate the correct score entry
        ///   - `date` - date of test
        ///   - `reading` - reading score
        ///   - `math` - math score
        member t.SetFields (testtype, date, reading, math, writing) = 
            // first locate the correct H4 element.
            try
                let parentElem = element testtype |> parent
                let flds =  parentElem.FindElement(By.TagName "form").FindElements(By.TagName "input")
                for element in flds do
                    match element with 
                    | element when element.GetAttribute("data-ng-model") = "Test.TestDate" -> t.SetDate (element, date)
                    | element when element.GetAttribute("data-ng-model") = "Test.ReadingAndWritingScore" -> t.SetReadingWriting (element, reading) 
                    | element when element.GetAttribute("data-ng-model") = "Test.ReadingScore" -> t.SetReading(element, reading)   
                    | element when element.GetAttribute("data-ng-model") = "Test.MathScore" -> t.SetMath (element, math)
                    | element when element.GetAttribute("data-ng-model") = "Test.WritingScore" -> t.SetWriting (element, writing)
                    | _ -> ()
            with
                | _ -> reporter.write("Failure setting scores")

        /// Save test scores link
        /// ## Parameters
        ///   - `testtype` - string used to locate the correct score entry
        /// ## Returns
        ///   - `Option<IWebElement>` = if the Save link is found, returns the link element. Otherwise returns null/None
        member t.Save(testtype) :IWebElement = 
                let parentElem = element testtype |> parent
                let  save = parentElem.FindElement(By.PartialLinkText "Save")
                save
                
        /// Cancel test scores link
        /// ## Parameters
        ///   - `testtype` - string used to locate the correct score entry
        /// ## Returns
        ///   - `Option<IWebElement>` = if the Cancel link is found, returns the link element. Otherwise returns null/None
        member t.Cancel(testtype) :IWebElement = 
            let parentElem = element testtype |> parent
            try
                let cancel = parentElem.FindElements(By.TagName "span").Where(fun span -> span.Text.Equals("Cancel", StringComparison.OrdinalIgnoreCase)).First()
                cancel
            with
                | _ -> parentElem

        /// Delete test scores link
        /// ## Parameters
        ///   - `testtype` - string used to locate the correct score entry
        ///   - `testdate` - date used for the correct score entry
        /// ## Returns
        ///   - `IWebElement` = if the Delete link is found, returns the link element. Otherwise returns the parent row for the test type
        member t.Delete(testtype, testdate) :IWebElement  = 
            let parentElem = element testtype |> parent
            try
                let row = parentElem.FindElements(By.TagName "div").Where(fun div -> div.Text = testdate).First() |> parent
                let delete = row.FindElements(By.TagName "span").Where(fun span -> span.Text.Equals("Delete", StringComparison.OrdinalIgnoreCase)).First()
                delete

            with 
                | _ -> parentElem


        // TODO: Add Edit functionality.

        /// Save test scores
        /// If the Save link is None or disabled, logs an error to the test reporter. If the element is active, clicks it.
        /// ## Parameters
        ///   - `testtype` - string used to locate the correct score entry
        member t.SaveTest(testtype) =
            try
                let save = t.Save(testtype)
                match save with
                | save when save.Enabled = true -> click save
                | _ -> reporter.write("Save not available")
            with
            | _ -> reporter.write("Error in save")

        /// Cancel test scores entry
        /// If the Cancel link is None or disabled, logs an error to the test reporter. If the element is active, clicks it.
        /// ## Parameters
        ///   - `testtype` - string used to locate the correct score entry
        member t.CancelEntry(testtype) = 
            try
                match t.Cancel(testtype) with
                | element when element.ToString().Contains("disabled") = false -> click element
                | _ -> reporter.write("Cancel not available")
            with
                | _ -> reporter.write("Error in cancel")

        /// Delete test scores
        /// If the Delete link is None or disabled, logs an error to the test reporter. If the element is active, clicks it.
        /// ## Parameters
        ///   - `testtype` - string used to locate the correct score entry
        ///   - `testdate` - string used to located the correct row of the score entries
        member t.DeleteEntry(testtype, testdate) =
            try
                let element = t.Delete(testtype, testdate)
                match element with
                | element when element.Text.Contains("DELETE") -> element.Click()
                                                                  t.ConfirmDelete()
                | _ -> reporter.write("Delete not available")
            with
                | _ -> reporter.write("Error in delete")

        /// Confirm score deletion
        member t.ConfirmDelete() =
            try
                let confirmButton = element(".action-buttons").FindElement(By.LinkText "Yes")
                click confirmButton
            with
                | _ -> reporter.write("Error confirming delete")


        member t.HasInvalidScore(testtype) : bool = 
            try
                let form = (element testtype |> parent).FindElement(By.Name "editTestForm")
                form.FindElements(By.TagName "input").Where(fun l -> l.ToString().Contains("ng-invalid")).Count() > 0
            with
                | _ -> false
                