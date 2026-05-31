using System.Text.RegularExpressions;

public class Scripture
{
    private string _currentScripture;
    private string _displayScripture;
    private string filePath;
    private List<string> _loadedScriptures;
    private HashSet<int> _hiddenWords = new HashSet<int>();
    private List<(int start, int length)> _wordPositions = new List<(int start, int length)>();
    private List<string> _cachedWords;
    private Random _random = new Random();

    public void ResetDisplay()
    {
        _displayScripture = _currentScripture;
        _hiddenWords.Clear();
        _wordPositions.Clear();
        _cachedWords = null;
    }

    public bool DisplayScripture(int wordIndex, string hiddenVersion)
    {
        if (_hiddenWords.Contains(wordIndex))
            return false;

        _hiddenWords.Add(wordIndex);
        var (start, length) = _wordPositions[wordIndex];
        _displayScripture = _displayScripture.Substring(0, start)
                          + hiddenVersion
                          + _displayScripture.Substring(start + length);
        return true;
    }

    public void DisplayCurrentState()
    {
        Console.WriteLine(_displayScripture);
    }

    public List<string> LoadFromFile()
    {
        Console.Clear();
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
        List<string> Scriptures = new List<string>(File.ReadAllLines(filePath));
        _loadedScriptures = Scriptures;
        return Scriptures;
    }

    public string GetRandomScripture()
    {
        if (_loadedScriptures == null || _loadedScriptures.Count == 0)
            return "No scriptures loaded.";

        int randomIndex = _random.Next(_loadedScriptures.Count);
        _currentScripture = _loadedScriptures[randomIndex];
        return _currentScripture;
    }

    public List<string> GetCurrentScriptureWords()
    {
        if (_cachedWords != null)
            return _cachedWords;

        if (string.IsNullOrEmpty(_currentScripture))
            return new List<string>();

        int offset = 0;
        int colonIndex = _currentScripture.IndexOf(":");
        if (colonIndex != -1)
        {
            int spaceAfterColon = _currentScripture.IndexOf(" ", colonIndex);
            if (spaceAfterColon != -1)
                offset = spaceAfterColon + 1;
        }

        string textOnly = ExtractTextOnly(_currentScripture);
        var matches = Regex.Matches(textOnly, @"[a-zA-Z]+");

        _wordPositions = matches.Select(m => (m.Index + offset, m.Length)).ToList();
        _cachedWords = matches.Select(m => m.Value).ToList();
        return _cachedWords;
    }

    private string ExtractTextOnly(string scripture)
    {
        int colonIndex = scripture.IndexOf(":");
        if (colonIndex != -1)
        {
            int spaceAfterColon = scripture.IndexOf(" ", colonIndex);
            if (spaceAfterColon != -1)
            {
                return scripture.Substring(spaceAfterColon + 1);
            }
        }
        return scripture;
    }
}