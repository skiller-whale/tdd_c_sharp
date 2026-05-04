using WordleCheat;

var app = new WordleApp("words.txt");

foreach (var line in app.Run(args))
{
    Console.WriteLine(line);
}
