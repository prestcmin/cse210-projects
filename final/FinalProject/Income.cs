using System;

public class Income : Transaction
{
    private string _source;

    public Income(double amount, DateTime date, string description, string source)
        : base(amount, date, description)
    {
        _source = source;
    }

    public string GetSource()
    {
        return _source;
    }

    public override string RecordTransaction()
    {
        string dateText = _date.ToString("MM/dd/yyyy");
        string entry = string.Format("{0}  INCOME    ${1,8:0.00}   Source: {2,-15}   {3}", dateText, _amount, _source, _description);
        return entry;
    }
}
