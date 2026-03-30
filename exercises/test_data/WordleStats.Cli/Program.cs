using WordleStats.Cli;

var commandArgs = Environment.GetCommandLineArgs().Skip(1).ToArray();

if (commandArgs.Length == 0)
{
    // Interactive mode
    Commands.RunInteractive();
}
else
{
    // Command-line mode
    var command = commandArgs[0];

    switch (command)
    {
        case "list":
            Commands.ListGames();
            break;
        case "stats":
            if (commandArgs.Length > 1)
            {
                Commands.ShowPlayerStats(commandArgs[1]);
            }
            else
            {
                Console.WriteLine("\nError: Please specify a player name\n");
                Console.WriteLine($"Available players: {string.Join(", ", Commands.GetPlayerNames())}\n");
            }
            break;
        case "report":
            if (commandArgs.Length > 1)
            {
                Commands.ShowPlayerReport(commandArgs[1]);
            }
            else
            {
                Console.WriteLine("\nError: Please specify a player name\n");
                Console.WriteLine($"Available players: {string.Join(", ", Commands.GetPlayerNames())}\n");
            }
            break;
        case "help":
            Commands.ShowHelp();
            break;
        default:
            Console.WriteLine($"\nUnknown command: {command}\n");
            Commands.ShowHelp();
            break;
    }
}
