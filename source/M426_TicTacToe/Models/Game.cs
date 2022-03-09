using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using M426_TicTacToe.Enums;
using Newtonsoft.Json;

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


        /// <summary>
        /// Uses Board to determine, if player 1 is active or not.
        /// </summary>
        /// <returns>True, if player 1 is active, otherwhise false.</returns>
        public bool IsPlayer1()
        {
            var fieldStates = JsonConvert.DeserializeObject<FieldState[]>(Board);
            int counter = 0;
            for (int i = 0; i < fieldStates.Length; i++)
            {
                if (fieldStates[i] != FieldState.none)
                    counter++;
            }
            if (counter % 2 == 0)
                return true;
            return false;
        }
    }
}
