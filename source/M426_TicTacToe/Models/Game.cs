﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace M426_TicTacToe.Models
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public virtual string Player1 { get; set; }
        public virtual string Player2 { get; set; }
        public int Winner { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Board { get; set; }


    }
}
