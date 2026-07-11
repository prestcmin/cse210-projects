using System;

public abstract class Transaction
{
    protected double _amount;
    protected DateTime _date;
    protected string _description;

    public Transaction(double amount, DateTime date, string description)
    {
        _amount = amount;
        _date = date;
        _description = description;
    }

    public double GetAmount()
    {
        return _amount;
    }

    public DateTime GetDate()
    {
        return _date;
    }

    public string GetDescription()
    {
        return _description;
    }

    public abstract string RecordTransaction();
}
