using System;
using System.Threading;

public class BreathingActivity : Activity
{
    public BreathingActivity(int activityLength) : base(activityLength)
    {
        _name = "Breathing Activity";
        _description =
            "This activity will help you relax by walking you through breathing in and\n" +
            "out slowly. Clear your mind and focus on your breathing.";
    }

    public void Run()
    {
        DisplayDescription();

        DateTime endTime = DateTime.Now.AddSeconds(_activityLength);

        while (DateTime.Now < endTime)
        {
            Console.Write("\nBreathe in... ");
            DisplayTimer(4);

            if (DateTime.Now >= endTime) break;

            Console.Write("\nBreathe out... ");
            DisplayTimer(4);
        }

        DisplayEnd();
    }
}