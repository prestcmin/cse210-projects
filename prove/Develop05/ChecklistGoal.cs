using System;

public class ChecklistGoal : Goal
{
    private int _timesCompleted;
    private int _requiredTimes;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int requiredTimes, int bonusPoints) : base(name, description, points)
    {
        _timesCompleted = 0;
        _requiredTimes  = requiredTimes;
        _bonusPoints    = bonusPoints;
    }

    public ChecklistGoal(string name, string description, int points, int requiredTimes, int bonusPoints, int timesCompleted) : base(name, description, points)
    {
        _timesCompleted = timesCompleted;
        _requiredTimes  = requiredTimes;
        _bonusPoints    = bonusPoints;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            Console.WriteLine("This goal is already complete!");
            return 0;
        }
        _timesCompleted++;

        if (_timesCompleted == _requiredTimes)
        {
            Console.WriteLine($"Congratulations! You completed the checklist goal and earned a {_bonusPoints} point bonus!");
            return _points + _bonusPoints;
        }

        return _points;
    }

    public override bool IsComplete()
    {
        return _timesCompleted >= _requiredTimes;
    }

    public override string GetDetailsString()
    {
        string status;
        if (IsComplete())
        {
            status = "[X]";
        }
        else
        {
            status = "[ ]";
        }
        return $"{status} {_name} ({_description}) -- Completed {_timesCompleted}/{_requiredTimes} times";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{_name}|{_description}|{_points}|{_requiredTimes}|{_bonusPoints}|{_timesCompleted}";
    }
}