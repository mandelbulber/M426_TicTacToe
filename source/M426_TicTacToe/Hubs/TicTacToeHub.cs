using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using M426_TicTacToe.Data;
using M426_TicTacToe.Models;
using M426_TicTacToe.Enums;
using Newtonsoft.Json;

namespace M426_TicTacToe.Hubs
{
    public class TicTacToeHub : Hub
    {
        private ApplicationDbContext _dbContext;
        public TicTacToeHub(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ClickField(string gameId, int fieldNumber)
        {
            string userId = Context.UserIdentifier;
            Game game = _dbContext.Games.ToList().FirstOrDefault(x => x.Id == gameId);
            if (game == null)
                return;
            FieldState[] fieldStates = JsonConvert.DeserializeObject<FieldState[]>(game.Board);
            bool isPlayer1 = game.IsPlayer1();

            // If game not found or no authorisation
            if (game.Player1 != userId && game.Player2 != userId)
                return;
            if ((GameState)game.Winner != GameState.pending)
                return;
            if (isPlayer1)
            {
                if (userId != game.Player1)
                    return;
            }
            else
            {
                if (userId != game.Player2)
                    return;
            }
            if (fieldStates[fieldNumber] != FieldState.none)
                return;
            if (isPlayer1)
                fieldStates[fieldNumber] = FieldState.player1;
            else
                fieldStates[fieldNumber] = FieldState.player2;

            game.Board = JsonConvert.SerializeObject(fieldStates);
            game.TimeStamp = DateTime.Now;
            _dbContext.Update(game);
            _dbContext.SaveChanges();

            await Clients.Users(game.Player1, game.Player2).SendAsync("UpdateField", fieldNumber, isPlayer1 ? "X" : "O");
        }
    }
}
