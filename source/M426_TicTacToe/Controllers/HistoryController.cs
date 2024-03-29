﻿using M426_TicTacToe.Data;
using M426_TicTacToe.Enums;
using M426_TicTacToe.Models;
using M426_TicTacToe.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace M426_TicTacToe.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public HistoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Searches the db for all games where the currently logged in user is part of
        /// Converts all found games into GameViewModels and returns the History View with the games as param
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<GameViewModel> games = new List<GameViewModel>();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                foreach (Game game in _dbContext.Games.Where(x => x.Player1 == userId || x.Player2 == userId).ToList())
                {
                    GameViewModel gameViewModel = new()
                    {
                        Id = game.Id,
                        Player1 = _dbContext.Users.FirstOrDefault(x => x.Id == game.Player1).UserName,
                        TimeStamp = game.TimeStamp,
                        GameState = (GameState)game.Winner,
                        Board = JsonConvert.DeserializeObject<FieldState[]>(game.Board)
                    };
                    if (_dbContext.Users.FirstOrDefault(x => x.Id == game.Player2) != null)
                        gameViewModel.Player2 = _dbContext.Users.FirstOrDefault(x => x.Id == game.Player2).UserName;
                    games.Add(gameViewModel);
                }
            }
            return View(games);
        }
    }
}
