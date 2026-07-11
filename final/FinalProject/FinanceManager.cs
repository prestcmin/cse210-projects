using System;
using System.Globalization;
using System.IO;

public class FinanceManager
{
    private Account _account;

    public FinanceManager()
    {
        _account = new Account();
    }

    public Account GetAccount()
    {
        return _account;
    }

    public void AddIncome(double amount, DateTime date, string description, string source)
    {
        Income income = new Income(amount, date, description, source);
        _account.AddTransaction(income);
    }

    public void AddExpense(double amount, DateTime date, string description, string categoryName)
    {
        Expense expense = new Expense(amount, date, description, categoryName);
        _account.AddTransaction(expense);
    }

    public void SaveToFile(string filePath)
    {
        StreamWriter writer = new StreamWriter(filePath);

        foreach (Transaction transaction in _account.GetTransactions())
        {
            if (transaction is Income income)
            {
                string line = "INCOME|" + income.GetAmount().ToString(CultureInfo.InvariantCulture) + "|" +
                    income.GetDate().ToString("yyyy-MM-dd") + "|" +
                    income.GetDescription() + "|" +
                    income.GetSource();
                writer.WriteLine(line);
            }
            else if (transaction is Expense expense)
            {
                string line = "EXPENSE|" + expense.GetAmount().ToString(CultureInfo.InvariantCulture) + "|" +
                    expense.GetDate().ToString("yyyy-MM-dd") + "|" +
                    expense.GetDescription() + "|" +
                    expense.GetCategory();
                writer.WriteLine(line);
            }
        }

        writer.Close();
    }

    public void LoadFromFile(string filePath)
    {
        if (File.Exists(filePath) == false)
        {
            Console.WriteLine("That file does not exist.");
            return;
        }

        _account = new Account();

        StreamReader reader = new StreamReader(filePath);
        string? line = reader.ReadLine();

        while (line != null)
        {
            string[] parts = line.Split('|');

            if (parts[0] == "INCOME")
            {
                double amount = double.Parse(parts[1], CultureInfo.InvariantCulture);
                DateTime date = DateTime.ParseExact(parts[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                string description = parts[3];
                string source = parts[4];
                Income income = new Income(amount, date, description, source);
                _account.AddTransaction(income);
            }
            else if (parts[0] == "EXPENSE")
            {
                double amount = double.Parse(parts[1], CultureInfo.InvariantCulture);
                DateTime date = DateTime.ParseExact(parts[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                string description = parts[3];
                string category = parts[4];
                Expense expense = new Expense(amount, date, description, category);
                _account.AddTransaction(expense);
            }

            line = reader.ReadLine();
        }

        reader.Close();
    }
}
