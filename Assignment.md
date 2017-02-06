# Assignment
## What you need to do
Build a series of automated tests that would correctly identify a defect introduced by code change. Please read the following sections for details.

## Background
SCOIR has a student focused application where a student user can fill out their profile. This profile section has many facets but today's exercise is limited to recording standardized test results. We need a regression suite that will test whether a student user can add, update, and delete a "test result" record for the various SAT tests supported by the system.

Review [User_Stories.md](./User_Stories.md) for requirements details of the page.

You are only expected to build automated tests for "SAT" & "SAT (2400)" test types

## Before you begin - Create a test user
Automated tests need to be executed as a test user.

1. Navigate to the student user signup page: [https://sif.scoir.rocks/signup](https://sif.scoir.rocks/signup)
1. Sign up for an account
1. Confirm the account using the email provided.
1. In the confirmation process, be sure you are creating a "High School Student". Use any high school available in the typeahead. Pick any graduation year. Enter any valid zip code.

## How to get to the student user profile
1. Log in with a test user
1. On the upper right hand corner of the screen click the 3 stacked bars, also known as a [hamburger menu](https://en.wikipedia.org/wiki/Hamburger_button)
1. Click "My Profile"
1. On the left rail of the screen, click "Test Scores"
1. You should now see a list standardized test types
