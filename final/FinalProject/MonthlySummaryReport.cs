using System;
using System.Collections.Generic;

public class MonthlySummaryReport : Report
{
    public MonthlySummaryReport(Account account)
        : base(account, "Monthly Summary Report")
    {
    }

    public override void Generate()
    {
        PrintHeader();

        List<string> months = new List<string>();

        foreach (Transaction transaction in _account.GetTransactions())
        {
            string month = transaction.GetDate().ToString("yyyy-MM");
            bool found = false;
            foreach (string m in months)
            {
                if (m == month)
                {
                    found = true;
                    break;
                }
            }
            if (found == false)
            {
                months.Add(month);
            }
        }

        if (months.Count == 0)
        {
            Console.WriteLine("  No transactions found.");
            PrintFooter();
            return;
        }

        foreach (string month in months)
        {
            double income   = 0;
            double expenses = 0;

            foreach (Transaction transaction in _account.GetTransactions())
            {
                if (transaction.GetDate().ToString("yyyy-MM") == month)
                {
                    if (transaction is Income)
                    {
                        income = income + transaction.GetAmount();
                    }
                    else if (transaction is Expense)
                    {
                        expenses = expenses + transaction.GetAmount();
                    }
                }
            }

            double net = income - expenses;

            Console.WriteLine();
            Console.WriteLine("  " + month);
            Console.WriteLine(string.Format("    Income:    ${0:0.00}", income));
            Console.WriteLine(string.Format("    Expenses:  ${0:0.00}", expenses));

            if (net >= 0)
            {
                Console.WriteLine(string.Format("    Net:       +${0:0.00}", net));
            }
            else
            {
                Console.WriteLine(string.Format("    Net:       -${0:0.00}", Math.Abs(net)));
            }
        }

        double totalIncome   = GetTotalIncome();
        double totalExpenses = GetTotalExpenses();

        Console.WriteLine();
        Console.WriteLine("  ALL TIME TOTALS");
        Console.WriteLine(string.Format("    Total Income:    ${0:0.00}", totalIncome));
        Console.WriteLine(string.Format("    Total Expenses:  ${0:0.00}", totalExpenses));
        Console.WriteLine(string.Format("    Current Balance: ${0:0.00}", totalIncome - totalExpenses));

        Console.WriteLine();
        PrintFooter();
    }

    private double GetTotalIncome()
    {
        double total = 0;
        foreach (Transaction transaction in _account.GetTransactions())
        {
            if (transaction is Income)
            {
                total = total + transaction.GetAmount();
            }
        }
        return total;
    }

    private double GetTotalExpenses()
    {
        double total = 0;
        foreach (Transaction transaction in _account.GetTransactions())
        {
            if (transaction is Expense)
            {
                total = total + transaction.GetAmount();
            }
        }
        return total;
    }
}