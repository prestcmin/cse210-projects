using System;
using System.Collections.Generic;
using System.Threading;

public class EnumerationActivity : Activity
{
    private List<string> _prompts;

    public EnumerationActivity(int activityLength) : base(activityLength)
    {
        _name = "Enumeration Activity";
        _description =
            "This activity will help you reflect on the good things in your life by\n" +
            "having you list as many things as you can in a certain area.";

        _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
    }

    public void Run()
    {
        DisplayDescription();

        Console.WriteLine($"\nList as many responses as you can to the following prompt:");
        Console.WriteLine($"  --- {GetRandomPrompt(_prompts)} ---");
        Console.WriteLine("\nYou will have a few seconds to think before you start...");
        DisplayTimer(5);

        Console.WriteLine("\nStart listing items (press Enter after each one):\n");

        int itemCount = 0;
        DateTime endTime = DateTime.Now.AddSeconds(_activityLength);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string entry = Console.ReadLine();

            if (DateTime.Now >= endTime)
            {
                if (!string.IsNullOrWhiteSpace(entry))
                    itemCount++;
                break;
            }

            if (!string.IsNullOrWhiteSpace(entry))
                itemCount++;
        }

        Console.WriteLine($"\nYou listed {itemCount} item(s). Great effort!");
        DisplayEnd();
    }
}