using FluentAssertions;
using SpecifyingBehaviour;
using Xunit;

namespace SpecifyingBehaviour.Tests;

public class GetGameStatusTests
{
    [Fact]
    public void Returns_Won_When_The_Last_Guess_Matches_The_Target()
    {
        GameStatus.GetGameStatus(["crane"], "crane").Should().Be("won");
    }

    [Fact]
    public void Returns_Lost_When_Six_Wrong_Guesses_Have_Been_Made()
    {
        GameStatus.GetGameStatus(["audio", "ghost", "plumb", "fizzy", "words", "crane"], "blank")
            .Should().Be("lost");
    }

    [Fact]
    public void Returns_In_Progress_When_Fewer_Than_Six_Guesses_Have_Been_Made()
    {
        GameStatus.GetGameStatus(["audio"], "ghost").Should().Be("in_progress");
    }
}
