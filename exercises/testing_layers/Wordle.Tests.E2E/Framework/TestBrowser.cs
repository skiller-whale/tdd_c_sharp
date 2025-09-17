using Microsoft.Playwright;

namespace Wordle.Tests.E2E.Framework;

public class TestBrowser
{
    private readonly IPage _page;

    public TestBrowser(IPage page)
    {
        _page = page;
    }

    public Task Visit(string url) => _page.GotoAsync(url);

    public async Task EnterGuess(string guess)
    {
        var input = _page.GetByLabel("guess");
        await input.FillAsync(guess);
        await input.PressAsync("Enter");
    }

    public async Task<string> GetStatus()
    {
        var statusElement = await _page.QuerySelectorAsync(".status");
        if (statusElement == null) {
            throw new Exception("Status element not found in page");
        }
        return (await statusElement.InnerTextAsync()).Trim();
    }

    public async Task<string> GetError()
    {
        var errorElement = await _page.QuerySelectorAsync(".error");
        if (errorElement == null) {
            throw new Exception("Error element not found in page");
        }
        return (await errorElement.InnerTextAsync()).Trim();
    }

    public async Task<string> GetCorrectAnswer()
    {
        var correctAnswerElement = await _page.QuerySelectorAsync(".correct-answer");
        if (correctAnswerElement == null) {
            throw new Exception("Correct answer element not found in page");
        }
        return (await correctAnswerElement.InnerTextAsync()).Trim();
    }

    public async Task<string> GetGuess(int index)
    {
        var guessElement = await _page.QuerySelectorAsync($".guess:nth-child({index + 1})");
        if (guessElement == null) {
            throw new Exception($"Guess element {index} not found in page");
        }
        var innerText = (await guessElement.InnerTextAsync())
            .Replace("\n", "")
            .Trim();
        return innerText;
    }

    public async Task<string> GetGuessCharClass(int guessIndex, int charIndex)
    {
        var guessElement = await _page.QuerySelectorAsync($".guess:nth-child({guessIndex + 1})");
        if (guessElement == null) {
            throw new Exception($"Guess element {guessIndex} not found in page");
        }
        var squareElement = await guessElement.QuerySelectorAsync(
            $"span:nth-child({charIndex + 1})"
        );
        if (squareElement == null) {
            throw new Exception($"Character element {charIndex} in guess {guessIndex} not found in page");
        }
        return await squareElement.GetAttributeAsync("class") ?? "";
    }
}
