using System;

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public override int RecordEvent()
    {
        if (_isComplete)
        {
            Console.WriteLine("This goal is already complete!");
            return 0;
        }
        _isComplete = true;
        return _points;
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetDetailsString()
    {
        string status;
        if (_isComplete)
        {
            status = "[X]";
        }
        else
        {
            status = "[ ]";
        }
        return $"{status} {_name} ({_description})";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal|{_name}|{_description}|{_points}|{_isComplete}";
    }

    public SimpleGoal(string name, string description, int points, bool isComplete) : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
        _isComplete = false;
    }
}