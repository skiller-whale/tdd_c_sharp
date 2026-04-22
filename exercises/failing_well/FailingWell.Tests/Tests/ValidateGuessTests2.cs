using System;
using FluentAssertions;
using FailingWell;
using Xunit;

namespace FailingWell.Tests;

public class ValidateGuessTests2
{
    [Fact]
    public void Returns_Valid_For_A_Recognised_Five_Letter_Word()
    {
        var result = GuessValidator.ValidateGuess("crane", ["crane", "rates"]);
        result.Valid.Should().BeTrue();
    }

    [Fact]
    public void Returns_Invalid_With_A_Reason_For_A_Guess_That_Is_Too_Short()
    {
        var result = GuessValidator.ValidateGuess("cran", ["crane", "rates"]);
        result.Valid.Should().BeFalse();
        result.Reason.Should().Be("Guess must be 5 letters");
    }

    [Fact]
    public void Returns_Invalid_With_A_Reason_For_Non_Letter_Characters()
    {
        var result = GuessValidator.ValidateGuess("cr4ne", ["crane", "rates"]);
        result.Valid.Should().BeFalse();
        result.Reason.Should().Be("Guess must only contain letters");
    }

    [Fact]
    public void Returns_Invalid_With_A_Reason_For_A_Guess_Not_In_The_Word_List()
    {
        var result = GuessValidator.ValidateGuess("bumps", ["crane", "rates"]);
        result.Valid.Should().BeFalse();
        result.Reason.Should().Be("Not a recognised word");
    }
}
