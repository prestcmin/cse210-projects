using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public int GetScore()
    {
        return _score;
    }

    public void DisplayGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet. Create one from the menu!");
        }
        else
        {
            Console.WriteLine("Your Goals:");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {_goals[i].GetDetailsString()}");
            }
        }
        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }

    public void CreateSimpleGoal()
    {
        Console.Write("Enter the name of the goal: ");
        string name = Console.ReadLine();

        Console.Write("Enter a short description: ");
        string description = Console.ReadLine();

        Console.Write("Enter the point value for completing this goal: ");
        int points = int.Parse(Console.ReadLine());

        _goals.Add(new SimpleGoal(name, description, points));
        Console.WriteLine($"Simple goal \"{name}\" created!");
    }

    public void CreateEternalGoal()
    {
        Console.Write("Enter the name of the goal: ");
        string name = Console.ReadLine();

        Console.Write("Enter a short description: ");
        string description = Console.ReadLine();

        Console.Write("Enter the point value for each time you record this goal: ");
        int points = int.Parse(Console.ReadLine());

        _goals.Add(new EternalGoal(name, description, points));
        Console.WriteLine($"Eternal goal \"{name}\" created!");
    }

    public void CreateChecklistGoal()
    {
        Console.Write("Enter the name of the goal: ");
        string name = Console.ReadLine();

        Console.Write("Enter a short description: ");
        string description = Console.ReadLine();

        Console.Write("Enter the point value for each time you record this goal: ");
        int points = int.Parse(Console.ReadLine());

        Console.Write("Enter the number of times required to complete this goal: ");
        int requiredTimes = int.Parse(Console.ReadLine());

        Console.Write("Enter the bonus point value for completing the goal: ");
        int bonusPoints = int.Parse(Console.ReadLine());

        _goals.Add(new ChecklistGoal(name, description, points, requiredTimes, bonusPoints));
        Console.WriteLine($"Checklist goal \"{name}\" created!");
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals to record. Create one first!");
            return;
        }
        Console.WriteLine("Your Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {_goals[i].GetDetailsString()}");
        }
        Console.Write("\nWhich goal did you accomplish? (enter number): ");
        string input = Console.ReadLine();
        int index = int.Parse(input);
        if (index < 1 || index > _goals.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }
        Goal selected = _goals[index - 1];
        int pointsEarned = selected.RecordEvent();
        if (pointsEarned > 0)
        {
            _score += pointsEarned;
            Console.WriteLine($"You earned {pointsEarned} points! Total score: {_score}");
        }
    }

    public void SaveGoals()
    {
        Console.Write("Enter the filename to save to: ");
        string filename = Console.ReadLine();
        StreamWriter writer = new StreamWriter(filename);
        writer.WriteLine(_score);
        foreach (Goal goal in _goals)
        {
            writer.WriteLine(goal.GetStringRepresentation());
        }
        writer.Close();
        Console.WriteLine($"Goals saved to \"{filename}\".");
    }

    public void LoadGoals()
    {
        Console.Write("Enter the filename to load from: ");
        string filename = Console.ReadLine();
        if (!File.Exists(filename))
        {
            Console.WriteLine($"File \"{filename}\" not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        _goals.Clear();
        _score = int.Parse(lines[0].Trim());
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (line == "")
            {
                continue;
            }
            string[] parts = line.Split('|');
            string type = parts[0];
            if (type == "SimpleGoal")
            {
                string name        = parts[1];
                string description = parts[2];
                int points         = int.Parse(parts[3]);
                bool isComplete    = bool.Parse(parts[4]);
                _goals.Add(new SimpleGoal(name, description, points, isComplete));
            }
            else if (type == "EternalGoal")
            {
                string name        = parts[1];
                string description = parts[2];
                int points         = int.Parse(parts[3]);
                _goals.Add(new EternalGoal(name, description, points));
            }
            else if (type == "ChecklistGoal")
            {
                string name        = parts[1];
                string description = parts[2];
                int points         = int.Parse(parts[3]);
                int requiredTimes  = int.Parse(parts[4]);
                int bonusPoints    = int.Parse(parts[5]);
                int timesCompleted = int.Parse(parts[6]);
                _goals.Add(new ChecklistGoal(name, description, points,
                                             requiredTimes, bonusPoints, timesCompleted));
            }
        }

        Console.WriteLine($"Goals loaded from \"{filename}\". Score restored to {_score}.");
    }
}