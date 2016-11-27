# CapitalOneCodingExercise
## Introduction
This is CapitalOne coding exercise project which is written in C#. It is a typical windows console application.<br />
This project is developped using VS2015.<br />
This project implements all three additional features: "--ignore-donuts", "--crystal-ball", and "--ignore-cc-payments". And you can also use any combination of those three parameters.<br />

## How to Compile
1. Please make sure you have .Net 4.0 installed in your machine. If not, you can download it from https://www.microsoft.com/EN-US/DOWNLOAD/confirmation.aspx?id=17718
2. Checkout the project or download it as zip.
3. Open a cmd window under the directory which contains the CapitalOneCoddingExercise.sln file.
4. Type command: nuget.exe restore (restore all the nuget packages)
5. Type command: c:\windows\microsoft.net\framework\v4.0.30319\msbuild.exe (Using msbuild to build the c# project. Assuming you installed .Net 4.0 using default settings. You can change the directory of msbuild.exe to any path you see fit).
6. Go to the following directory: %sln file directory%\CapitalOneCodingExercise\bin\debug\
7. You will find several files including CapitalOneCodingExercise.exe file. This is the executable file you can use to run the application.
8. If you have trouble to build the project. I also included the compiled version in Compiled folder. You can find the CapitalOneCodingExercise.exe executable there too.

## How to use
1. Open a cmd window under the CapitalOneCodingExercise.exe excutable directory.
2. To get original average data, use command "CapitalOneCodingExercise.exe".
3. To get data with "--ignore-donuts", use command "CapitalOneCodingExercise.exe --ignore-donuts".
4. To get data with "--crystal-ball", use command "CapitalOneCodingExercise.exe --crystal-ball".
5. To get data with "--ignore-cc-payments", use command "CapitalOneCodingExercise.exe --ignore--cc-payments".
6. You can also use any combination of those parameters. For example:<br />
   To take account of both "--ignore-donuts" and "--crystal-ball", you can use "CapitalOneCodingExercise.exe --ignore-donuts --crystal-ball"<br />
   To take account of all three, you can just use "CapitalOneCodingExercise.exe --ignore-donuts --crystal-ball --ignore-cc-payments"
7. The order of parameters does not matter.
8. The command line in windows prompt will look like this
   c:\CapitalOneCodingExercice\bin\debug>CapitalOneCodingExercise.exe --ignore-donuts


## Assumptions
1. The average is defined as<br />
   spent average = total amount of all spend in this month/the total number of spend transactions happened in this month<br />
   income average = total amount of all income in this month/the total number of income transactions happened in this month
2. Two transactions of cc payments only happen within the same month.
3. When using --crystal-ball, the predicted transactions for the rest of this month are not included in the calculation of total average.
