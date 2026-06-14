using System;
using System.Threading;

public class Menu
{
    public static void ChooseActivities()
    {
        Console.Clear();
        Console.WriteLine("=== Mindfulness Program ===\n");
        Console.WriteLine("Please choose an activity:");
        Console.WriteLine("  1. Breathing Activity");
        Console.WriteLine("  2. Reflection Activity");
        Console.WriteLine("  3. Enumeration Activity");
        Console.Write("\nSelect a choice from the menu: ");

        string choice = Console.ReadLine();

        int duration = AskForDuration();

        switch (choice)
        {
            case "1":
                BreathingActivity breathing = new BreathingActivity(duration);
                breathing.Run();
                break;
            case "2":
                ReflectionActivity reflection = new ReflectionActivity(duration);
                reflection.Run();
                break;
            case "3":
                EnumerationActivity enumeration = new EnumerationActivity(duration);
                enumeration.Run();
                break;
            default:
                Console.WriteLine("\nInvalid choice. Please restart and choose 1, 2, or 3.");
                Thread.Sleep(2500);
                break;
        }
    }

    private static int AskForDuration()
    {
        Console.Write("\nHow long, in seconds, would you like for your session? ");
        string raw = Console.ReadLine();
        int seconds;
        while (!int.TryParse(raw, out seconds) || seconds < 1)
        {
            Console.Write("Please enter a positive whole number: ");
            raw = Console.ReadLine();
        }
        return seconds;
    }
}