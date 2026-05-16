public class Entry
{
    public string _newEntry;
    public string currentDate;

    public List<string> Entries = new List<string>();
    public void WriteEntry(out string currentDate, out string _newEntry)
    {
        _newEntry = Console.ReadLine();
        currentDate = DateTime.Now.ToString("MM/dd/yyyy");
    }
}