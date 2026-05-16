using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        string userInput;
        int userAction;
        string formatted;
        Entry entry1 = new Entry();
        Prompt prompt1 = new Prompt();
        Journal journal1 = new Journal();

        do
        {
            Console.WriteLine ("Welcome to the Journal! Please select your choice of action:" 
            + Environment.NewLine + "1. Write" 
            + Environment.NewLine + "2. Display" 
            + Environment.NewLine + "3. Load" 
            + Environment.NewLine + "4. Save" 
            + Environment.NewLine + "5. Quit");
            Console.Write("What would you like to do? ");
            userInput = Console.ReadLine();
            userAction = int.Parse(userInput);

            if (userAction == 1)
            {
                prompt1.DisplayPrompt();
                entry1.WriteEntry(out string date, out string entry);
                formatted = $"{date}|{prompt1._entryPrompt}|{entry}";
                entry1.Entries.Add(formatted);
            }
            else if (userAction == 2)
            {
                foreach (string journalEntry in entry1.Entries)
                {
                    Console.WriteLine(journalEntry);
                }
            }
            else if (userAction == 3)
            {
                entry1.Entries = journal1.LoadFromFile();
                foreach (string journalEntry in entry1.Entries)
                {
                    Console.WriteLine(journalEntry);
                }
            }
            else if (userAction == 4)
            {
                if (entry1.Entries.Count == 0)
                {
                    Console.WriteLine("No entries to save. Write an entry first.");
                }
                else
                {
                    journal1.SaveToFile(entry1.Entries);
                }
            }
            else if (userAction == 5)
            {
                Console.WriteLine("Thank you for writing!");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Not a valid input, please try again.");
            }

        } while (userAction != 5);
    }
}