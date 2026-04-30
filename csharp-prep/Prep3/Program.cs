using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What is the magic number? ");
        Random randomGenerator = new Random();
        int num = randomGenerator.Next(1,11);
        int guess = 0;

        do
        {
            Console.Write("What is your guess? ");
            string input = Console.ReadLine();    
            guess = int.Parse(input);
            
            if (guess > num)
            {
                Console.WriteLine("Lower!");
            }
            else if (guess < num)
            {
                Console.WriteLine("Higher!");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }

        } while (num != guess);
        
    }
}