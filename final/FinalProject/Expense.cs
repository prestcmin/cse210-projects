using System;

public class Expense : Transaction
{
    private string _category;

    public Expense(double amount, DateTime date, string description, string category)
        : base(amount, date, description)
    {
        _category = category;
    }

    public string GetCategory()
    {
        return _category;
    }

    public override string RecordTransaction()
    {
        string dateText = _date.ToString("MM/dd/yyyy");
        string entry = string.Format("{0}  EXPENSE   ${1,8:0.00}   Category: {2,-15}   {3}", dateText, _amount, _category, _description);
        return entry;
    }
}
