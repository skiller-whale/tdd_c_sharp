using FluentAssertions;
using System.Collections.Generic;
using Wordle.Core;
using Xunit;

namespace Wordle.Tests.Core.Tests;

[Collection("EvaluateGuessTests")]
public class EvaluateGuessTests
{
    [Fact(Skip = "TODO")]
    public void Identifies_incorrect_letters()
    {
        // characters nowhere in the correct answer should be marked as such
    }

    [Fact(Skip = "TODO")]
    public void Identifies_letters_in_the_right_place()
    {
        // characters that are in the correct answer and in the right place
        // should be marked as such
    }

    [Fact(Skip = "TODO")]
    public void Identifies_letters_in_the_wrong_place()
    {
        // characters that are in the correct answer but not in the right place
        // should be marked as such
    }

    [Fact(Skip = "TODO")]
    public void Does_not_display_duplicates()
    {
        // e.g. if a letter appears twice in the guess, but only once in the
        // correct answer, it should only be marked once
    }
}
