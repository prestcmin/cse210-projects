using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        Console.Write("Enter number: ");
        string userInput = Console.ReadLine();

        int number = int.Parse(userInput);
        int sum = 0;
        double average = 0;
        int largest = 0; 
        List<int> numbers = new List<int>();

        while (number != 0)
        {
            Console.Write("Enter number: ");
            userInput = Console.ReadLine();
            numbers.Add(number);
            number = int.Parse(userInput);    
        }
        
        if (number == 0)
        {
            foreach (int num in numbers)
            {
                sum += num;
                if (num > largest)
                {
                    largest = num;
                }
            }

            average = (double)sum / numbers.Count;
            int count = numbers.Count;

            Console.WriteLine($"The sum is: {sum}");
            Console.WriteLine($"The average is: {average}");
            Console.WriteLine($"The largest number is: {largest}");
        }

    }
}