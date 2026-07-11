using System;

public class Budget
{
    private string _categoryName;
    private double _monthlyLimit;
    private double _amountSpent;

    public Budget(string categoryName, double monthlyLimit)
    {
        _categoryName = categoryName;
        _monthlyLimit = monthlyLimit;
        _amountSpent = 0;
    }

    public string GetCategoryName()
    {
        return _categoryName;
    }

    public double GetMonthlyLimit()
    {
        return _monthlyLimit;
    }

    public double GetAmountSpent()
    {
        return _amountSpent;
    }

    public void AddSpending(double amount)
    {
        _amountSpent = _amountSpent + amount;
    }
}
