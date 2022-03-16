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
        private ApplicationDbContext _dbContext;
        public LeaderboardController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var leaderboard = new List<LeaderboardViewModel>();
            foreach(var user in _dbContext.Users)
            {
                var gamesAsPlayer1 = _dbContext.Games.Where(g => g.Player1 == user.UserName);
                var gamesAsPlayer2 = _dbContext.Games.Where(g => g.Player2 == user.UserName);
                leaderboard.Add(new LeaderboardViewModel
                {
                    Name = user.UserName,
                    Wins = gamesAsPlayer1.Where(g => (GameState)g.Winner == GameState.player1Won).Count() + gamesAsPlayer2.Where(g => (GameState)g.Winner == GameState.player2Won).Count(),
                    Losses = gamesAsPlayer1.Where(g => (GameState)g.Winner == GameState.player2Won).Count() + gamesAsPlayer2.Where(g => (GameState)g.Winner == GameState.player1Won).Count(),
                    Draws = gamesAsPlayer1.Where(g => (GameState)g.Winner == GameState.draw).Count() + gamesAsPlayer2.Where(g => (GameState)g.Winner == GameState.draw).Count()
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
