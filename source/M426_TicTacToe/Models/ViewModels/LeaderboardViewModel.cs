using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M426_TicTacToe.Models.ViewModels
{
    public class LeaderboardViewModel
    {
        public int Rank { get; set; }
        public string Name { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
    }
}
