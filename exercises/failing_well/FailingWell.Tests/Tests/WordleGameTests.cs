using System;
using FluentAssertions;
using FailingWell;
using Xunit;

namespace FailingWell.Tests;

public class WordleGameTests
{
    public class Constructor
    {
        [Fact]
        public void Initializes_The_Game_State()
        {
            string[] wordList = ["whale", "water", "fishy", "skill"];
            var game = new WordleGame("whale", wordList);
            game.CorrectAnswer.Should().Be("whale");
            game.WordList.Should().BeSameAs(wordList);
            game.Guesses.Should().BeEmpty();
            game.Evaluations.Should().BeEmpty();
        }

        [Fact]
        public void Throws_An_Error_If_The_Answer_Is_Not_In_The_Word_List()
        {
            string[] wordList = ["whale", "water", "fishy", "skill"];
            Action act = () => new WordleGame("crane", wordList);
            act.Should().Throw<ArgumentException>();
        }
    }

    public class SubmitGuess
    {
        [Fact]
        public void Correctly_Evaluates_The_Guess()
        {
            string[] wordList = ["whale", "water", "fishy", "skill"];
            var game = new WordleGame("whale", wordList);
            game.SubmitGuess("water");
            game.Evaluations[0].Should().Be(GuessEvaluator.EvaluateGuess("water", "whale"));
        }

        [Fact]
        public void Does_Not_Accept_Further_Guesses_After_The_Game_Is_Over()
        {
            string[] wordList = ["whale", "water", "fishy", "skill"];
            var game = new WordleGame("whale", wordList);
            game.SubmitGuess("whale");
            game.SubmitGuess("water");
            game.Guesses.Should().HaveCount(1);
        }

        [Fact]
        public void Throws_An_Error_For_Invalid_Guesses()
        {
            string[] wordList = ["whale", "water", "fishy", "skill"];
            var game = new WordleGame("whale", wordList);
            Action act = () => game.SubmitGuess("crane");
            act.Should().Throw<ArgumentException>();
        }
    }
}
