using Microsoft.AspNetCore.Mvc;
using Wordle.Core;
using Wordle.Models;
using System.Collections.Generic;
using System.Linq;

namespace Wordle.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index(string guesses, string error)
    {
        var guessesList = string.IsNullOrEmpty(guesses) ? new List<string>() : guesses.Split(',').ToList();
        var status = GameStatus.GetGameStatus(guessesList);

        var model = new WordleViewModel
        {
            Guesses = guessesList,
            Status = status
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult Index(string guesses, [FromForm] object formData)
    {
        var guessesList = string.IsNullOrEmpty(guesses) ? new List<string>() : guesses.Split(',').ToList();
        var latestGuess = Request.Form["latestGuess"].ToString();

        // TODO: validate the guess
        string? error = null;

        // if there's an error, redirect back to GET with error message
        if (error != null)
        {
            return RedirectToAction("Index", new { guesses = string.Join(",", guessesList), error });
        }

        // otherwise, redirect back to GET with updated guesses
        guessesList.Add(latestGuess);
        return RedirectToAction("Index", new { guesses = string.Join(",", guessesList) });
    }
}
