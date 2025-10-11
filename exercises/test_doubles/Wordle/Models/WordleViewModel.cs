using System.Collections.Generic;
using Wordle.Core;

namespace Wordle.Models;

#nullable enable

public class WordleViewModel
{
    public List<string> Guesses { get; set; } = [];
    public List<List<string>> Evaluations { get; set; } = [];
    public Status Status { get; set; } = Status.Playing;
    public string? Error { get; set; }

    public string GetLetterClass(string evaluation)
    {
        return evaluation switch
        {
            "+" => "green",
            "?" => "yellow",
            "-" => "gray",
            _ => ""
        };
    }
}
