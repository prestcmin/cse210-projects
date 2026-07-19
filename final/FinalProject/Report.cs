using System;

public abstract class Report
{
    protected Account _account;
    protected string _title;

    public Report(Account account, string title)
    {
        _account = account;
        _title   = title;
    }

    public void PrintHeader()
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 60));
        Console.WriteLine("  " + _title);
        Console.WriteLine(new string('=', 60));
    }

    public void PrintFooter()
    {
        Console.WriteLine(new string('=', 60));
    }

    public abstract void Generate();
}