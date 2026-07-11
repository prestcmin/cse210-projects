using System;
using System.Collections.Generic;
using System.Globalization;

public class Menu
{
    private FinanceManager _financeManager;
    private bool _running;

    public Menu()
    {
        _financeManager = new FinanceManager();
        _running = true;
    }

    public void Run()
    {
        Console.WriteLine("Welcome to your Personal Finance Tracker!");
        Console.WriteLine("(This is a work in progress. Budgets and reports are not hooked up yet.)");

        while (_running == true)
        {
            DisplayMenuOptions();
            string choice = GetValidString("Enter your choice: ");

            if (choice == "1")
            {
                AddIncomeFlow();
            }
            else if (choice == "2")
            {
                AddExpenseFlow();
            }
            else if (choice == "3")
            {
                ViewTransactionsFlow();
            }
            else if (choice == "4")
            {
                SaveFlow();
            }
            else if (choice == "5")
            {
                LoadFlow();
            }
            else if (choice == "0")
            {
                _running = false;
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine("That is not a valid option. Please try again.");
            }
        }
    }

    private void DisplayMenuOptions()
    {
        Console.WriteLine();
        Console.WriteLine("---------------------------------------");
        Console.WriteLine("1. Add Income");
        Console.WriteLine("2. Add Expense");
        Console.WriteLine("3. View All Transactions and Balance");
        Console.WriteLine("4. Save Data to File");
        Console.WriteLine("5. Load Data from File");
        Console.WriteLine("0. Exit");
        Console.WriteLine("---------------------------------------");
    }

    private void AddIncomeFlow()
    {
        double amount = GetValidDouble("Enter the amount of the paycheck: ");
        DateTime date = GetValidDate("Enter the date (MM/dd/yyyy): ");
        string source = GetValidString("Enter the income source (e.g. Job, Freelance): ");
        string description = GetValidString("Enter a short description: ");

        _financeManager.AddIncome(amount, date, description, source);
        Console.WriteLine("Income recorded successfully.");
    }

    private void AddExpenseFlow()
    {
        double amount = GetValidDouble("Enter the amount spent: ");
        DateTime date = GetValidDate("Enter the date (MM/dd/yyyy): ");
        string category = GetValidString("Enter the category for this expense: ");
        string description = GetValidString("Enter a short description: ");

        _financeManager.AddExpense(amount, date, description, category);
        Console.WriteLine("Expense recorded successfully.");
    }

    private void ViewTransactionsFlow()
    {
        List<Transaction> transactions = _financeManager.GetAccount().GetTransactions();

        if (transactions.Count == 0)
        {
            Console.WriteLine("No transactions have been recorded yet.");
            return;
        }

        Console.WriteLine();
        foreach (Transaction transaction in transactions)
        {
            Console.WriteLine(transaction.RecordTransaction());
        }

        double balance = _financeManager.GetAccount().CalculateBalance();
        Console.WriteLine(string.Format("Current Balance: ${0,8:0.00}", balance));
    }

    private void SaveFlow()
    {
        string filePath = GetValidString("Enter the file name to save to (e.g. budget.txt): ");
        _financeManager.SaveToFile(filePath);
        Console.WriteLine("Data saved successfully.");
    }

    private void LoadFlow()
    {
        string filePath = GetValidString("Enter the file name to load from (e.g. budget.txt): ");
        _financeManager.LoadFromFile(filePath);
        Console.WriteLine("Data loaded successfully.");
    }

    private double GetValidDouble(string prompt)
    {
        double result;
        bool isValid = false;
        result = 0;

        while (isValid == false)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";

            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out result) && result >= 0)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine("Please enter a valid, non-negative number.");
            }
        }

        return result;
    }

    private DateTime GetValidDate(string prompt)
    {
        DateTime result;
        bool isValid = false;
        result = DateTime.Now;

        while (isValid == false)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";

            if (DateTime.TryParseExact(input, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine("Please enter a valid date in the format MM/dd/yyyy.");
            }
        }

        return result;
    }

    private string GetValidString(string prompt)
    {
        string result;
        bool isValid = false;
        result = "";

        while (isValid == false)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";

            if (input.Trim().Length > 0)
            {
                result = input.Trim();
                isValid = true;
            }
            else
            {
                Console.WriteLine("This cannot be blank. Please try again.");
            }
        }

        return result;
    }
}
