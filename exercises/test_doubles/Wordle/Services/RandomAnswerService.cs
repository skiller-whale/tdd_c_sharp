using System;
using System.Linq;
using Wordle.Core;

namespace Wordle.Services
{
    public interface IAnswerService
    {
        string GetRandomAnswer();
    }

    public class RandomAnswerService(Random random) : IAnswerService
    {
        private readonly Random _random = random;
        private int _lastIndex = -1;

        public string GetRandomAnswer()
        {
            // TODO: implement this function
            return "whale";
        }
    }
}
