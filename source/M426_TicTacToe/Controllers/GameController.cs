using M426_TicTacToe.Data;
using M426_TicTacToe.Enums;
using M426_TicTacToe.Models;
using M426_TicTacToe.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;

namespace M426_TicTacToe.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public GameController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("play/{id}")]
        public IActionResult Game(string id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Game game = _dbContext.Games.FirstOrDefault(x => x.Id == id);
            if (game == null)
                return RedirectToAction("Index");

            // Join existing game

            if (game.Player2 == null && userId != game.Player1)
            {
                game.Player2 = userId;
                _dbContext.Games.Update(game);
                _dbContext.SaveChanges();
            }

            if (userId != game.Player1 && userId != game.Player2) // Unauthorized?
                return RedirectToAction("Index");

            GameViewModel gameViewModel = new()
            {
                Id = game.Id,
                Player1 = _dbContext.Users.FirstOrDefault(u => u.Id == game.Player1)?.UserName,
                Player2 = _dbContext.Users.FirstOrDefault(u => u.Id == game.Player2)?.UserName,
                TimeStamp = game.TimeStamp,
                GameState = (GameState)game.Winner,
                Board = JsonConvert.DeserializeObject<FieldState[]>(game.Board)
            };

            return View(gameViewModel);
        }

        public IActionResult NewGame()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            FieldState[] fieldStates = new FieldState[9];
            for (int i = 0; i < 9; i++)
                fieldStates[i] = FieldState.none;

            Game newGame = new()
            {
                Player1 = userId,
                Board = JsonConvert.SerializeObject(fieldStates),
                TimeStamp = DateTime.Now
            };

            _dbContext.Games.Add(newGame);
            _dbContext.SaveChanges();

            return RedirectToAction("Game", new { id = newGame.Id });
        }
    }
}
