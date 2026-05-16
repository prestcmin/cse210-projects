public class Journal
{
    public string filePath;

    public void SaveToFile(List<string> Entries)
    {
        Console.Write("What would you like to name the file?"
        + Environment.NewLine + "> ");
        filePath = Console.ReadLine();
        if (!filePath.EndsWith(".txt"))
        {
            filePath += ".txt";
        }
        filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), filePath);
        string content = string.Join(Environment.NewLine, Entries);
        File.WriteAllText(filePath, content);
        Console.WriteLine($"Saved to {filePath}");
    }
    
    public List<string> LoadFromFile()
    {
        Console.Write("What is the name of the file you would like to load from?"
        + Environment.NewLine + "> ");
        filePath = Console.ReadLine();
        if (!filePath.EndsWith(".txt"))
        {
            filePath += ".txt";
        }
        filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), filePath);
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return new List<string>();
        }
        List<string>loadedEntries = new List<string>(File.ReadAllLines(filePath));
        Console.WriteLine("Journal loaded successfully.");
        return loadedEntries;
    }
}