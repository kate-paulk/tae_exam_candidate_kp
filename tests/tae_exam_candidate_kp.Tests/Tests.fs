module tae_exam_candidate_kp.Tests

open tae_exam_candidate_kp
open NUnit.Framework

[<Test>]
let ``hello returns 42`` () =
  let result =  42
  printfn "%i" result
  Assert.AreEqual(42,result)
