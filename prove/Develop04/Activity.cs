using System;
using System.Collections.Generic;
using System.Threading;

public class Activity
{
    protected string _name;
    protected string _description;
    protected int _activityLength;
    protected Random _randomChoice;

    public Activity(int activityLength)
    {
        _activityLength = activityLength;
        _randomChoice   = new Random();
    }

    public int GetActivityLength()
    {
        return _activityLength;
    }

    public void DisplayDescription()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}.\n");
        Console.WriteLine(_description);
        Console.WriteLine($"\nYou have chosen {_activityLength} seconds for this activity.");
        Console.WriteLine("\nGet ready to begin...");
        DisplaySpinner(3);
    }

    public void DisplayEnd()
    {
        Console.WriteLine();
        Console.WriteLine("Well done! ");
        DisplaySpinner(3);
        Console.Clear();
        Console.WriteLine($"You have completed {_activityLength} seconds of the {_name}.");
        DisplaySpinner(3);
    }

    public void DisplayTimer(int seconds)
    {
        for (int remaining = seconds; remaining > 0; remaining--)
        {
            Console.Write(remaining);
            Thread.Sleep(1000);

            int digits = remaining.ToString().Length;
            for (int d = 0; d < digits; d++)
            {
                Console.Write("\b \b");
            }
        }
    }

    public void DisplaySpinner(int seconds)
    {
        string[] frames = { "|", "/", "-", "\\" };
        int totalFrames = seconds * 8;

        for (int i = 0; i < totalFrames; i++)
        {
            Console.Write(frames[i % frames.Length]);
            Thread.Sleep(125);
            Console.Write("\b \b");
        }
    }

    public string GetRandomPrompt(List<string> prompts)
    {
        int index = _randomChoice.Next(prompts.Count);
        return prompts[index];
    }

    public string GetRandomQuestion(List<string> questions)
    {
        int index = _randomChoice.Next(questions.Count);
        return questions[index];
    }


}