//****************************************************************************************************************
// This is an example calculator made in the F# language. It is not very complex but does lines of calculation
// without issue! 
//
// Author: Kyler Kupres
// Date: 04/19/2020
// Version: 1.0
//****************************************************************************************************************

open System;
open System.Collections.Generic
open System.Text.RegularExpressions

//This is the variable which controls the continuous loop
let mutable loop = true
//This is a variable that contains the running value of calculations
let mutable ansHist = 0.0

//The main loop of the calculator
while loop do  
    //List that will contain the calculating operators
    let operations = new List<string>()

    //List that will contain the total list of numbers
    let numbers = new List<float>()

    //This is the output of the calculation that we are doing
    let mutable output = ""

    //Ask the user to enter an equation into the console
    Console.Write("Enter the equation: ")

    //Read in the singular equation line from the console
    let inputText = Console.ReadLine()

    //Check to make sure that the input is not empty, null, and that it actually contains mathematical operators
    //so that there is something to actually calculate in the statement
    if inputText.Length = 0 || inputText = null || Regex.Replace(inputText,  @"\d+", "").Trim().Length = 0 
       || Regex.Replace(inputText,  @"\d+", "").Trim() = null then
        Console.WriteLine("Input was empty, null, or did not contain any mathematical symbols!")
    else
        //We trim down the string and parse it into only the mathematical symbols
        let mthSymbols = Regex.Replace(inputText,  @"\d+", "").Trim()
        
        //This matches every symbol with the correct one and then mapps them all
        //to the mathematical symbol list
        for mathSym in mthSymbols do
          match mathSym.ToString() with
          | "+" -> operations.Add(mathSym.ToString())
          | "-" -> operations.Add(mathSym.ToString())
          | "*" -> operations.Add(mathSym.ToString())
          | "/" -> operations.Add(mathSym.ToString())

        //We again parse the input string for only the numbers we want calculated
        let number = Regex.Matches(inputText, @"\d+")

        //We add the float value of all of these numbers to the number list. This allows us to calculate both 
        //whole numbers and decimal numbers which is what we wanted in this program.
        for num in number do
            numbers.Add(float num.Value)
     
        //The below function is for when there is only one operator. It checks for the operator, matches it with 
        //the correct action and then calculates the information and removes those numbers from the list. This also
        //catches divide by zero, and index issues, although for some reason it does not print to the console like 
        //I had hoped it would. I still attempted to contain this as much as I could. 
        let op = operations.[0].ToString()
        try
            match op with
                      | "+" -> let result = numbers.[0] + numbers.[1]
                               ignore(numbers.[0] <- result)
                               ignore(numbers.Remove(numbers.[1]))
                               ()
                      | "-" -> let result = numbers.[0] - numbers.[1]
                               ignore(numbers.[0] <- result)
                               ignore(numbers.Remove(numbers.[1]))
                               ()
                      | "/" -> let result = numbers.[0] / numbers.[1] 
                               ignore(numbers.[0] <- result)
                               ignore(numbers.Remove(numbers.[1]))
                               ()
                      | "*" -> let result = numbers.[0] * numbers.[1]
                               ignore(numbers.[0] <- result)
                               ignore(numbers.Remove(numbers.[1]))
                               ()
        with
        | :? System.DivideByZeroException -> Console.WriteLine("Tried to divide by zero!")
        | :? System.IndexOutOfRangeException -> Console.WriteLine("Index out of bounds exception")
    
        //The below function is for when there are multiple operators. It checks for the operator, matches it with 
        //the correct action and then calculates the information and removes those numbers from the list This also
        //catches divide by zero, and index issues, although for some reason it does not print to the console like 
        //I had hoped it would. I still attempted to contain this as much as I could. The difference between the one
        //above is that it relies on the position of the number list to do it's calculations properly. 
        let mutable position = 1

        while numbers.Count <>  1 do
            try
             match operations.[position] with
                       | "+" ->    let result = numbers.[0] + numbers.[1]
                                   ignore(numbers.[0] <- result)
                                   ignore(numbers.Remove(numbers.[1]))
                                   ()
                       | "-" ->    let result = numbers.[0] - numbers.[1]
                                   ignore(numbers.[0] <- result)
                                   ignore(numbers.Remove(numbers.[1]))
                                   ()
                       | "/" ->   
                                   let result = numbers.[0] / numbers.[1]
                                   ignore(numbers.[0] <- result)
                                   ignore(numbers.Remove(numbers.[1]))
                                   ()
                       | "*" ->    let result = numbers.[0] * numbers.[1]
                                   ignore(numbers.[0] <- result)
                                   ignore(numbers.Remove(numbers.[1]))
                                   ()
             position <- position + 1
            with
            | :? System.DivideByZeroException -> Console.WriteLine("Tried to divide by zero!")
            | :? System.IndexOutOfRangeException -> Console.WriteLine("Index out of bounds exception")

        //This is creating the output number of the calculations for display
        output <- output + "\n" + inputText + "= " + numbers.[numbers.Count-1].ToString()

        //This is adding the result to the previous value of the answer history. Note: This is just a total of all 
        //the calculations that have been done. I couldn't figure out how to manipulate this to create other calculations
        //from it. Technically you could add and sutract by doing 0-3 or 0+3 but it is not ideal.
        ansHist <- ansHist + numbers.[numbers.Count-1]

        //Clear the list of numbers
        numbers.Clear()
        //Clear the list of math operators
        operations.Clear()

        //Print the output to the user
        printfn "%s" output 
        //Pring the total of all the calculations to the user
        printfn "\nTotal Of All Calulations: %f" ansHist 

        //This section is where the user asks if they want to calculate anythign else or not. Saying no will close the 
        //application, and saying yes or typing anything else will keep the loop going. The total count is counted up
        //until the application closes. The app sets the ansHist to 0 upon close to keep it clean. 
        Console.WriteLine("\nWant to calculate more? yes/no")
        let ans = Console.ReadLine()
        if ans = "no" then
           ansHist = 0.0 
           loop <- false
        else Console.Clear()
  







