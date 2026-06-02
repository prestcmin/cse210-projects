public class Word
{
    private string _chosen;
    private int _chosenIndex = -1;
    private Scripture _scripture;
    private List<string> _words;
    private List<int> _remainingIndices;
    private Random _random = new Random();

    public Word(Scripture scripture)
    {
        _scripture = scripture;
        _words = _scripture.GetCurrentScriptureWords();
        _remainingIndices = Enumerable.Range(0, _words.Count).ToList();
        for (int i = _remainingIndices.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (_remainingIndices[i], _remainingIndices[j]) = (_remainingIndices[j], _remainingIndices[i]);
        }
    }

    public void ChooseNewWord()
    {
        if (_remainingIndices.Count > 0)
        {
            _chosenIndex = _remainingIndices[0];
            _chosen = _words[_chosenIndex];
            _remainingIndices.RemoveAt(0);
        }
        else
        {
            _chosen = null;
            _chosenIndex = -1;
        }
    }

    public string GetChosenWord()
    {
        return _chosen;
    }
    public int GetChosenIndex()
    {
        return _chosenIndex;
    }

    public string HideCurrentWord()
    {
        return HideWord(_chosen);
    }

    private string HideWord(string word)
    {
        string hidden = "";
        foreach (char c in word)
        {
            if (char.IsLetter(c))
            {
                hidden += "_";
            }
            else
            {
                hidden += c;
            }
        }
        return hidden;
    }
}