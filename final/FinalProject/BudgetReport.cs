using System;
using System.Collections.Generic;

public class BudgetReport : Report
{
    private List<Budget> _budgets;
    private string _month;

    public BudgetReport(Account account, List<Budget> budgets, string month)
        : base(account, "Budget Report - " + month)
    {
        _budgets = budgets;
        _month   = month;
    }

    public override void Generate()
    {
        PrintHeader();

        if (_budgets.Count == 0)
        {
            Console.WriteLine("  No budgets have been set.");
            PrintFooter();
            return;
        }

        Console.WriteLine(string.Format("  {0,-18}  {1,10}  {2,10}  {3,12}  {4}", "Category", "Budgeted", "Spent", "Remaining", "Status"));
        Console.WriteLine(new string('-', 60));

        foreach (Budget budget in _budgets)
        {
            double spent = 0;

            foreach (Transaction transaction in _account.GetTransactions())
            {
                if (transaction is Expense)
                {
                    Expense expense = (Expense)transaction;
                    if (expense.GetCategory() == budget.GetCategoryName() &&
                        expense.GetDate().ToString("yyyy-MM") == _month)
                    {
                        spent = spent + expense.GetAmount();
                    }
                }
            }

            double remaining = budget.GetMonthlyLimit() - spent;

            string status;
            if (spent > budget.GetMonthlyLimit())
            {
                status = "OVER BUDGET";
            }
            else if (remaining < budget.GetMonthlyLimit() * 0.1)
            {
                status = "WARNING";
            }
            else
            {
                status = "OK";
            }

            string remainingDisplay;
            if (remaining < 0)
            {
                remainingDisplay = string.Format("-${0:0.00}", Math.Abs(remaining));
            }
            else
            {
                remainingDisplay = string.Format("${0:0.00}", remaining);
            }

            Console.WriteLine(string.Format("  {0,-18}  ${1,9:0.00}  ${2,9:0.00}  {3,12}  {4}",
                budget.GetCategoryName(),
                budget.GetMonthlyLimit(),
                spent,
                remainingDisplay,
                status));
        }

        Console.WriteLine();
        PrintFooter();
    }
}