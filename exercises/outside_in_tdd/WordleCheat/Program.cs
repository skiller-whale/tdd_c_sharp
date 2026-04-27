using WordleCheat;

var app = new WordleApp();

foreach (var line in app.Run(args))
{
    Console.WriteLine(line);
}
