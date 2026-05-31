public class Game
{
    private Scripture _scripture;
    private Word _word;
    private int _wordsHidden = 0;
    private int _totalWords = 0;
    private Random _random = new Random();

    public Game()
    {
        _scripture = new Scripture();
    }

    public void Play()
    {
        _scripture.LoadFromFile();
        bool playing = true;

        while (playing)
        {
            string currentScripture = _scripture.GetRandomScripture();
            _scripture.ResetDisplay();
            _word = new Word(_scripture);
            _totalWords = _scripture.GetCurrentScriptureWords().Count;
            _wordsHidden = 0;

            Console.Clear();
            Console.WriteLine(currentScripture);
            Console.Write("Press Enter to start (or type 'quit' to exit): ");
            string startInput = Console.ReadLine();
            if (startInput.ToLower() == "quit")
            {
                playing = false;
                break;
            }

            while (playing && _wordsHidden < _totalWords)
            {
                int wordsToHide = _random.Next(1, 4);
                int wordsHiddenThisRound = 0;

                while (wordsHiddenThisRound < wordsToHide && _wordsHidden < _totalWords)
                {
                    _word.ChooseNewWord();
                    string chosen = _word.GetChosenWord();
                    if (chosen == null) break;

                    bool wasHidden = _scripture.DisplayScripture(_word.GetChosenIndex(), _word.HideCurrentWord());
                    if (wasHidden)
                    {
                        _wordsHidden++;
                        wordsHiddenThisRound++;
                    }
                }

                Console.Clear();
                _scripture.DisplayCurrentState();
                Console.Write("Press Enter for next words (or type 'quit' to exit): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                {
                    playing = false;
                    break;
                }
            }

            if (_wordsHidden >= _totalWords)
            {
                Console.Clear();
                _scripture.DisplayCurrentState();
                Console.WriteLine("Scripture complete! Good job!");
                Console.ReadLine();
                playing = false;
            }
        }

        Console.WriteLine("Thanks for playing!");
    }
}