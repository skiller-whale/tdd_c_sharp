using System;
using Moq;
using Wordle.Core;
using Wordle.Services;
using Xunit;

namespace Wordle.Tests.Core.Tests;

[Collection("AnswerServiceTests")]
public class AnswerServiceTests
{
    [Fact]
    public void Returns_A_Valid_Answer()
    {
        var random = new Random();
        var service = new RandomAnswerService(random);
        var answer = service.GetRandomAnswer();

        Assert.Contains(answer, Dictionary.ValidWords);
    }

    [Fact]
    public void Returns_A_Different_Answer_Each_Time()
    {
        // TODO
    }

    [Fact]
    public void Calls_Random_Next_Method_With_Dictionary_Count()
    {
        // TODO
    }

    [Fact]
    public void Returns_The_Same_Answer_When_Random_Next_Returns_The_Same_Value()
    {
        // TODO
    }
}
