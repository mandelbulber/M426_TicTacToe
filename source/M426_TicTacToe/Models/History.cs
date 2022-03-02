using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M426_TicTacToe.Models
{
    public class History
    {
        public int Id { get; set; }
        public virtual string Player1 { get; set; }
        public virtual string Player2 { get; set; }
        public string Game { get; set; }

    }
}
