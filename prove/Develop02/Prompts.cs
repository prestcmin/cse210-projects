public class Prompt
{
    public string _entryPrompt;
    public static List<string> Prompts = new List<string>{
        "What is one good thing that happened to you today?",
        "What are you looking forward to for tomorrow?",
        "How was school today?",
        "Did you eat anything good today?",
        "What was the most fun thing you did today?"
    };


    public string DisplayPrompt()
    {
        Random randomGenerator = new Random();
        int promptNum = randomGenerator.Next(0,Prompts.Count);
        _entryPrompt = Prompts[promptNum];
        Console.WriteLine(_entryPrompt);
        return _entryPrompt;
    }
}