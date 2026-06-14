using System;
using System.Collections.Generic;
using System.Threading;

public class ReflectionActivity : Activity
{
    private List<string> _prompts;
    private List<string> _questions;

    public ReflectionActivity(int activityLength) : base(activityLength)
    {
        _name = "Reflection Activity";
        _description =
            "This activity will help you reflect on times in your life when you have\n" +
            "shown strength and resilience. This will help you recognize the power you\n" +
            "have and how you can use it in other aspects of your life.";

        _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };
    }

    public void Run()
    {
        DisplayDescription();

        Console.WriteLine("\nConsider the following prompt:\n");
        Console.WriteLine($"  --- {GetRandomPrompt(_prompts)} ---");
        Console.WriteLine("\nWhen you have a moment in mind, press Enter to begin.");
        Console.ReadLine();

        DateTime endTime = DateTime.Now.AddSeconds(_activityLength);

        while (DateTime.Now < endTime)
        {
            Console.Write($"\n> {GetRandomQuestion(_questions)} ");
            DisplaySpinner(5);
        }

        DisplayEnd();
    }
}