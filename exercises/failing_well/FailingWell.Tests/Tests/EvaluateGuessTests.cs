using System;
using FluentAssertions;
using FailingWell;
using Xunit;

namespace FailingWell.Tests;

public class EvaluateGuessTests
{
    [Fact]
    public void Correct()
    {
        GuessEvaluator.EvaluateGuess("crane", "crane").Should().Be("ggggg");
    }

    [Fact]
    public void Incorrect()
    {
        GuessEvaluator.EvaluateGuess("bumps", "crane").Should().Be("-----");
    }

    [Fact]
    public void YellowLetters()
    {
        GuessEvaluator.EvaluateGuess("acorn", "crane").Should().Be("yy-yy");
    }

    [Fact]
    public void Mixed()
    {
        GuessEvaluator.EvaluateGuess("grace", "crane").Should().Be("-ggyg");
    }

    [Fact]
    public void DuplicateLetters()
    {
        GuessEvaluator.EvaluateGuess("error", "crane").Should().Be("yg---");
    }
}
