using System;

public class Menu
{
    private GoalManager _manager;
    private bool _running;

    public Menu()
    {
        _manager = new GoalManager();
        _running = true;
    }

    public void Run()
    {
        while (_running)
        {
            Console.WriteLine();
            Console.WriteLine($"You have {_manager.GetScore()} points.\n");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create a new goal");
            Console.WriteLine("  2. List goals");
            Console.WriteLine("  3. Save goals");
            Console.WriteLine("  4. Load goals");
            Console.WriteLine("  5. Record event");
            Console.WriteLine("  6. Quit");
            Console.Write("\nSelect a choice from the menu: ");

            string choice = Console.ReadLine();
            Console.WriteLine();
            if (choice == "1")
            {
                ShowCreateGoalMenu();
            }
            else if (choice == "2")
            {
                _manager.DisplayGoals();
            }
            else if (choice == "3")
            {
                _manager.SaveGoals();
            }
            else if (choice == "4")
            {
                _manager.LoadGoals();
            }
            else if (choice == "5")
            {
                _manager.RecordEvent();
            }
            else if (choice == "6")
            {
                Console.WriteLine("Goodbye! Keep up the great work on your Eternal Quest!");
                _running = false;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter a number from 1 to 6.");
            }
        }
    }

    private void ShowCreateGoalMenu()
    {
        Console.WriteLine("What type of goal would you like to create?");
        Console.WriteLine("  1. Simple Goal    (completed once)");
        Console.WriteLine("  2. Eternal Goal   (never complete, earn points each time)");
        Console.WriteLine("  3. Checklist Goal (must be done N times for a bonus)");
        Console.Write("\nSelect a choice: ");

        string choice = Console.ReadLine();
        Console.WriteLine();
        if (choice == "1")
        {
            _manager.CreateSimpleGoal();
        }
        else if (choice == "2")
        {
            _manager.CreateEternalGoal();
        }
        else if (choice == "3")
        {
            _manager.CreateChecklistGoal();
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }
}