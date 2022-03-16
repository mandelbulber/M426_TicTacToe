using M426_TicTacToe.Models.ViewModels;
using M426_TicTacToe.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M426_TicTacToe.Enums;

namespace M426_TicTacToe.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public LeaderboardController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var leaderboard = new List<LeaderboardViewModel>();
            foreach(var user in _dbContext.Users.ToList())
            {
                var gamesAsPlayer1 = _dbContext.Games.Where(g => g.Player1 == user.UserName).ToList();
                var gamesAsPlayer2 = _dbContext.Games.Where(g => g.Player2 == user.UserName).ToList();
                leaderboard.Add(new LeaderboardViewModel
                {
                    Name = user.UserName,
                    Wins = gamesAsPlayer1.Where(g => (GameState)g.Winner == GameState.player1Won).ToList().Count() + gamesAsPlayer2.Where(g => (GameState)g.Winner == GameState.player2Won).ToList().Count(),
                    Losses = gamesAsPlayer1.Where(g => (GameState)g.Winner == GameState.player2Won).ToList().Count() + gamesAsPlayer2.Where(g => (GameState)g.Winner == GameState.player1Won).ToList().Count(),
                    Draws = gamesAsPlayer1.Where(g => (GameState)g.Winner == GameState.draw).ToList().Count() + gamesAsPlayer2.Where(g => (GameState)g.Winner == GameState.draw).ToList().Count()
                });
            }

            leaderboard = leaderboard.OrderBy(l => l.Wins).ToList();
            var rank = 1;
            foreach(var user in leaderboard)
            {
                user.Rank = rank++;
            }
            return View(leaderboard);
        }
    }
}
