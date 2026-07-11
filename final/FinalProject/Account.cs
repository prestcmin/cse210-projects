using System;
using System.Collections.Generic;

public class Account
{
    private List<Transaction> _transactions;

    public Account()
    {
        _transactions = new List<Transaction>();
    }

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

    public List<Transaction> GetTransactions()
    {
        return _transactions;
    }

    public double CalculateBalance()
    {
        double balance = 0;
        foreach (Transaction transaction in _transactions)
        {
            if (transaction is Income)
            {
                balance = balance + transaction.GetAmount();
            }
            else if (transaction is Expense)
            {
                balance = balance - transaction.GetAmount();
            }
        }
        return balance;
    }
}
