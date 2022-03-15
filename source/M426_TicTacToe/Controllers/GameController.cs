﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using M426_TicTacToe.Hubs;
using M426_TicTacToe.Models;
using M426_TicTacToe.Models.ViewModels;
using M426_TicTacToe.Data;
using M426_TicTacToe.Enums;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Security.Claims;
using Newtonsoft.Json;

namespace M426_TicTacToe.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private ApplicationDbContext _dbContext;
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
                Player1 = game.Player1,
                Player2 = game.Player2,
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