using System;
using System.Linq;
using Wordle.Core;

namespace Wordle.Services
{
    public interface IAnswerService
    {
        string GetRandomAnswer();
    }

    public class RandomAnswerService : IAnswerService
    {
        private readonly Random _random;
        private int _lastIndex = -1;

        public RandomAnswerService(Random random)
        {
            _random = random;
        }

        public string GetRandomAnswer()
        {
            // TODO: implement this function
            return "whale";
        }
    }
}
