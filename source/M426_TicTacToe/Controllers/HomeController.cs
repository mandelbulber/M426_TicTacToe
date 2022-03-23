using M426_TicTacToe.Data;
using M426_TicTacToe.Enums;
using M426_TicTacToe.Models;
using M426_TicTacToe.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace M426_TicTacToe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) 
            {
                List<Game> dbGames = _context.Games.ToList();
                List<GameViewModel> games = new List<GameViewModel>();

                foreach (var game in dbGames)
                {
                    GameViewModel gvm = new GameViewModel();
                    gvm.Id = game.Id;
                    gvm.Player1 = _context.Users.FirstOrDefault(x => x.Id == game.Player1).UserName;
                    if (_context.Users.FirstOrDefault(x => x.Id == game.Player2) != null) 
                    {
                        gvm.Player2 = _context.Users.FirstOrDefault(x => x.Id == game.Player2).UserName;
                    }
                    gvm.TimeStamp = game.TimeStamp;
                    gvm.GameState = (GameState)game.Winner;
                    gvm.Board = JsonConvert.DeserializeObject<FieldState[]>(game.Board);
                    games.Add(gvm);
                }
                return View(games);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
