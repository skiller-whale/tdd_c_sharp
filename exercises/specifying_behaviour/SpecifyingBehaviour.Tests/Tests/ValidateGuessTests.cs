using FluentAssertions;
using SpecifyingBehaviour;
using Xunit;

namespace SpecifyingBehaviour.Tests;

public class ValidateGuessTests
{
    [Fact]
    public void Returns_Valid_For_A_Five_Letter_Word_In_The_Word_List()
    {
        GuessValidator.ValidateGuess("crane", ["crane", "audio"])
            .Should().BeEquivalentTo(new GuessResult(true));
    }

    [Fact]
    public void Returns_Invalid_For_A_Word_That_Is_Not_Five_Letters()
    {
        GuessValidator.ValidateGuess("cr", ["crane", "audio"])
            .Should().BeEquivalentTo(new GuessResult(false, "Guess must be 5 letters"));
    }

    [Fact]
    public void Returns_Invalid_For_A_Word_Containing_Non_Letter_Characters()
    {
        GuessValidator.ValidateGuess("cr4ne", ["crane", "audio"])
            .Should().BeEquivalentTo(new GuessResult(false, "Guess must only contain letters"));
    }

    [Fact]
    public void Returns_Invalid_For_A_Word_Not_In_The_Word_List()
    {
        GuessValidator.ValidateGuess("audio", ["crane"])
            .Should().BeEquivalentTo(new GuessResult(false, "Not a recognised word"));
    }
}
