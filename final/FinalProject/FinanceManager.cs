using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class FinanceManager
{
    private Account _account;
    private List<Budget> _budgets;

    public FinanceManager()
    {
        _account = new Account();
        _budgets = new List<Budget>();
    }

    public Account GetAccount()
    {
        return _account;
    }

    public List<Budget> GetBudgets()
    {
        return _budgets;
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

    public void AddBudget(string categoryName, double limit)
    {
        bool found = false;

        foreach (Budget budget in _budgets)
        {
            if (budget.GetCategoryName() == categoryName)
            {
                found = true;
                break;
            }
        }

        if (found == false)
        {
            _budgets.Add(new Budget(categoryName, limit));
            Console.WriteLine(string.Format("Budget of ${0:0.00}/month set for {1}.", limit, categoryName));
        }
        else
        {
            Console.WriteLine("A budget for that category already exists.");
        }
    }

    public void ShowMonthlySummary()
    {
        MonthlySummaryReport report = new MonthlySummaryReport(_account);
        report.Generate();
    }

    public void ShowBudgetReport(string month)
    {
        BudgetReport report = new BudgetReport(_account, _budgets, month);
        report.Generate();
    }

    public void SaveToFile(string filePath)
    {
        StreamWriter writer = new StreamWriter(filePath);

        foreach (Transaction transaction in _account.GetTransactions())
        {
            if (transaction is Income)
            {
                Income income = (Income)transaction;
                string incomeAmount = income.GetAmount().ToString(CultureInfo.InvariantCulture);
                string incomeDate = income.GetDate().ToString("yyyy-MM-dd");
                string incomeDescription = income.GetDescription();
                string incomeSource = income.GetSource();
                string incomeLine = "INCOME|" + incomeAmount + "|" + incomeDate + "|" + incomeDescription + "|" + incomeSource;
                writer.WriteLine(incomeLine);
            }
            else if (transaction is Expense)
            {
                Expense expense = (Expense)transaction;
                string expenseAmount = expense.GetAmount().ToString(CultureInfo.InvariantCulture);
                string expenseDate = expense.GetDate().ToString("yyyy-MM-dd");
                string expenseDescription = expense.GetDescription();
                string expenseCategory = expense.GetCategory();
                string expenseLine = "EXPENSE|" + expenseAmount + "|" + expenseDate + "|" + expenseDescription + "|" + expenseCategory;
                writer.WriteLine(expenseLine);
            }
        }

        foreach (Budget budget in _budgets)
        {
            string line = "BUDGET|" + budget.GetCategoryName() + "|" +
                budget.GetMonthlyLimit().ToString(CultureInfo.InvariantCulture);
            writer.WriteLine(line);
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
        _budgets = new List<Budget>();

        StreamReader reader = new StreamReader(filePath);
        string line = reader.ReadLine();

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
            else if (parts[0] == "BUDGET")
            {
                string categoryName = parts[1];
                double limit = double.Parse(parts[2], CultureInfo.InvariantCulture);
                _budgets.Add(new Budget(categoryName, limit));
            }

            line = reader.ReadLine();
        }

        reader.Close();
    }
}