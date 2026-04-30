using System;

class Program
{
    static void Main(string[] args)
    {
        static void DisplayWelcome()
        {
            Console.WriteLine("Welcome to the Program!");
        }

        static string PromptUserName()
        {
            Console.Write("What is your name? : ");
            string name = Console.ReadLine();
            return name;
        }

        static int PromptUserNumber()
        {
            Console.Write("What is your favorite number? : ");
            string userInput = Console.ReadLine();
            int number = int.Parse(userInput);
            return number;
        }

        static void PromptUserBirthYear(out int year)
        {
            Console.Write("What year were you born? : ");
            string userInput = Console.ReadLine();
            year = int.Parse(userInput);
        }

        static int SquareNumber(int number)
        {
            int squared = number * number;
            return squared;
        }

        static void DisplayResult(string name, int squared, int year)
        {
            Console.WriteLine($"{name}, the square of your number is {squared}");

            int howold = DateTime.Now.Year - year;
            Console.WriteLine($"{name}, you will turn {howold} this year.");
        }

        DisplayWelcome();

        string userName = PromptUserName();
        int userNumber = PromptUserNumber();

        int year;
        PromptUserBirthYear(out year);

        int squaredNumber = SquareNumber(userNumber);

        DisplayResult(userName, squaredNumber, year);
    }
}