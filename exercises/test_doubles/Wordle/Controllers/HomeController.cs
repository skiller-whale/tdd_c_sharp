using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Wordle.Core;
using Wordle.Models;
using Wordle.Services;
using System.Collections.Generic;
using System.Linq;

namespace Wordle.Controllers;

public class HomeController(IDatabase database, IAnswerService answerService) : Controller
{
    private readonly IDatabase _database = database;
    private readonly IAnswerService _answerService = answerService;
    private const string SESSION_ID_KEY = "SessionId";

    [HttpGet]
    public IActionResult Index()
    {
        // create a sessionID cookie if it doesn't already exist
        EnsureSession();

        // display the new game form view
        return View("Index");
    }

    [HttpPost]
    public IActionResult NewGame()
    {
        // create a new game
        // TODO: randomize the answer using _answerService.GetRandomAnswer()
        var correctAnswer = "whale";
        var game = Wordle.Core.Game.CreateNewGame(correctAnswer);
        _database.SaveGame(game);

        // redirect to the page for that game
        return RedirectToAction("Game", new { id = game.Id });
    }

    [HttpGet]
    public IActionResult Game(string id)
    {
        // get the game from the database
        var game = _database.GetGame(id);
        
        // convert the game to view model
        var model = new WordleViewModel
        {
            Guesses = game.Guesses,
            Evaluations = game.Evaluations,
            Status = game.Status,
            Error = game.Error,
        };

        // TODO: include game stats for this session if game is over
        if (game.Status != Status.Playing)
        {
            var sessionId = GetSessionId();
            // ...
        }

        // display the game view
        return View("Game", model);
    }
    
    [HttpPost]
    public IActionResult MakeGuess(string id, string latestGuess)
    {
        // get the game from the database
        var game = _database.GetGame(id);

        // update the game state
        game.MakeGuess(latestGuess.ToLower());
        _database.SaveGame(game);

        // TODO: when the game is over, save the user's game history
        if (game.Status != Status.Playing)
        {
            var sessionId = GetSessionId();
            // ...
        }

        // redirect to the game page
        return RedirectToAction("Game", new { id });
    }

    private void EnsureSession()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString(SESSION_ID_KEY)))
        {
            HttpContext.Session.SetString(SESSION_ID_KEY, System.Guid.NewGuid().ToString());
        }
    }

    private string GetSessionId()
    {
        return HttpContext.Session.GetString(SESSION_ID_KEY)!;
    }
}
