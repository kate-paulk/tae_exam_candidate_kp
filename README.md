# To run:
1. Use Visual Studio and build. All the relevant dependencies should be present in the .paket and packages directories.
1. The project uses the Canopy framework https://lefthandedgoat.github.io/canopy/index.html for tests. Canopy is a wrapper over Selenium
1. The project uses Project Scaffold http://fsprojects.github.io/ProjectScaffold/index.html as the base. Project Scaffold builds in a number of useful tools including documentation
1. The project also manually installed the gecko driver for Firefox. The driver is included in source.
1. Run in debug mode. Output is to the console. 
1. While the executable in the included zip of the bin directory is theoretically runnable, it has not been tested.
1. Documentation has not been generated yet. The documentation shell gives an indication of what it can do.

## Notes:
1. Selenium is not the best tool for this application.
1. There are a lot of places where refactoring is needed.
1. I could spend a lot of time playing with this to get it 100%, but I've already spent far too much time playing around and fighting off the cats (who like to walk on keyboards)
1. I know the assertions aren't great. They're a starting point. 
1. There are a number of todos scattered through the code, mostly for repeated code and cleaning up.


# SCOIR Technical Interview for Test Automation Engineers
This repo contains an exercise intended for Test Automation Engineers.

## Instructions
1. Fork this repo.
1. Using technology of your choice, complete the assignment found in [Assignment.md](./Assignment.md).
1. Be sure you satisfy the exam acceptance criteria in [Exam_Acceptance_Criteria.md](./Exam_Acceptance_Criteria.md).
1. Before the deadline, submit a pull request with your solution.


## Expectations
1. This exercise should take participants no more than 3 hours. Please do not spend more than that amount of time.
1. This exercise is meant to showcase how you work. With consideration to the time limit, do your best to treat it like a production system.
