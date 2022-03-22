using M426_TicTacToe.Enums;
using System;

namespace M426_TicTacToe.Models.ViewModels
{
    public class GameViewModel
    {
        public string Id { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public GameState GameState { get; set; }
        public DateTime TimeStamp { get; set; }
        public FieldState[] Board { get; set; } = new FieldState[9];
    }
}
