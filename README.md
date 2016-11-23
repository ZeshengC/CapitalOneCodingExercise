# CapitalOneCodingExercise
## Introduction
This is CapitalOne coding exercise project which is written in C#. It is a typical windows console application.
This project is developped using VS2015.
This project implements all three additional features: "--ignore-donuts", "--crystal-ball", and "--ignore-cc-payments". And you can also use any combination of those three parameters.

## How to use
The zip file contains two folders, one is called src, the other one is called bin. The "src" folder contains all the source code and solution file which you can open using VS2015. The "bin" folder contains a compiled exe file which you can use to run the application.
Followings are the instructions to use the application
1. Open a window command prompt under the bin folder directory.
2. To get original average data, use command "CapitalOneCodingExercise.exe".
3. To get data with "--ignore-donuts", use command "CapitalOneCodingExercise.exe --ignore-donuts".
4. To get data with "--crystal-ball", use command "CapitalOneCodingExercise.exe --crystal-ball".
5. To get data with "--ignore-cc-payments", use command "CapitalOneCodingExercise.exe --ignore--cc-payments".
6. You can also use any combination of those parameters. For example:
   To take account of both "--ignore-donuts" and "--crystal-ball", you can use "CapitalOneCodingExercise.exe --ignore-donuts --crystal-ball"
   To take account of all three, you can just use "CapitalOneCodingExercise.exe --ignore-donuts --crystal-ball --ignore-cc-payments"
   The order of parameters does not matter.


## Note
The average is defined as
spent average = total amount of all spend in this month/the total number of spend transactions happened in this month

I assume the two transactions of cc payments only happen within the same month.
