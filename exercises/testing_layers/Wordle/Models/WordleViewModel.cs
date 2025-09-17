using System.Collections.Generic;
using Wordle.Core;

namespace Wordle.Models;

public class WordleViewModel
{
    public List<string> Guesses { get; set; } = new List<string>();
    public GameStatus.Status Status { get; set; }
}
