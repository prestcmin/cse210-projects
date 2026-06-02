using System.Text.RegularExpressions;

public class Scripture
{
    private string _currentScripture;
    private string _displayScripture;
    private string _filePath;
    private List<string> _loadedScriptures;
    private HashSet<int> _hiddenWords = new HashSet<int>();
    private List<(int start, int length)> _wordPositions = new List<(int start, int length)>();
    private List<string> _cachedWords;
    private Random _random = new Random();

    private List<string> _builtInScriptures = new List<string>
    {
        "D&C 10:5 Pray always, that you may come off conqueror; yea, that you may conquer Satan, and that you may escape the hands of the servants of Satan that do uphold his work.",
        "2 Nephi 28:7-9 Yea, and there shall be many which shall say: Eat, drink, and be merry, for tomorrow we die; and it shall be well with us. And there shall also be many which shall say: Eat, drink, and be merry; nevertheless, fear God—he will justify in committing a little sin; yea, lie a little, take the advantage of one because of his words, dig a pit for thy neighbor; there is no harm in this; and do all these things, for tomorrow we die; and if it so be that we are guilty, God will beat us with a few stripes, and at last we shall be saved in the kingdom of God. Yea, and there shall be many which shall teach after this manner, false and vain and foolish doctrines, and shall be puffed up in their hearts, and shall seek deep to hide their counsels from the Lord; and their works shall be in the dark.",
        "1 Nephi 3:7 And it came to pass that I, Nephi, said unto my father: I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them.",
        "2 Nephi 25:23 For we labor diligently to write, to persuade our children, and also our brethren, to believe in Christ, and to be reconciled to God; for we know that it is by grace that we are saved, after all we can do."
    };

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
        Console.WriteLine("How would you like to load scriptures?");
        Console.WriteLine("1. Load from a file");
        Console.WriteLine("2. Use built-in scripture list");
        Console.Write("> ");
        string choice = Console.ReadLine();

        if (choice == "2")
        {
            return LoadBuiltIn();
        }
        else
        {
            return LoadFromUserFile();
        }
    }

    private List<string> LoadBuiltIn()
    {
        _loadedScriptures = _builtInScriptures;
        Console.WriteLine($"Loaded {_loadedScriptures.Count} built-in scriptures.");
        return _loadedScriptures;
    }

    private List<string> LoadFromUserFile()
    {
        Console.Write("What is the name of the file you would like to load from?"
        + Environment.NewLine + "> ");
        _filePath = Console.ReadLine();
        if (!_filePath.EndsWith(".txt"))
        {
            _filePath += ".txt";
        }
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _filePath);
        if (!File.Exists(_filePath))
        {
            Console.WriteLine("File not found.");
            return new List<string>();
        }
        List<string> scriptures = new List<string>(File.ReadAllLines(_filePath));
        _loadedScriptures = scriptures;
        return scriptures;
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

        _wordPositions = new List<(int start, int length)>();
        _cachedWords = new List<string>();
        foreach (Match m in matches)
        {
            _wordPositions.Add((m.Index + offset, m.Length));
            _cachedWords.Add(m.Value);
        }
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