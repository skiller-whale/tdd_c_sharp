namespace WordleCheat;

public class WordleApp
{
    private readonly string _wordFilePath;

    public WordleApp(string wordFilePath)
    {
        _wordFilePath = wordFilePath;
    }

    public IReadOnlyList<string> Run(string[] args)
    {
        return Array.Empty<string>();
    }
}
