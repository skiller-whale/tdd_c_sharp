using System;
using FluentAssertions;
using FailingWell;
using Xunit;

namespace FailingWell.Tests;

public class ValidateGuessTests1
{
    [Fact]
    public void ValidatesAndInvalidatesGuessesCorrectly()
    {
        string[] words = ["crane", "rates", "towns"];
        GuessValidator.ValidateGuess("crane", words).Valid.Should().BeTrue();
        GuessValidator.ValidateGuess("cran", words).Valid.Should().BeFalse();
        GuessValidator.ValidateGuess("cr4ne", words).Valid.Should().BeFalse();
        GuessValidator.ValidateGuess("bumps", words).Valid.Should().BeFalse();
    }

    [Fact]
    public void ReturnsAReasonForInvalidGuesses()
    {
        string[] words = ["crane"];
        GuessValidator.ValidateGuess("cran", words).Reason.Should().NotBeNull();
        GuessValidator.ValidateGuess("cr4ne", words).Reason.Should().NotBeNull();
        GuessValidator.ValidateGuess("bumps", words).Reason.Should().NotBeNull();
    }
}
