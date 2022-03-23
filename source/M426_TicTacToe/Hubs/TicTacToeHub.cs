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
        //All possible Wins
        static private int[,] Winners = new int[,]
        {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 0, 4, 8 },
            { 2, 4, 6 }
        };
        private ApplicationDbContext _dbContext;
        public TicTacToeHub(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task IsUserPlayer2(string gameId)
        {
            var userId = Context.UserIdentifier;
            var game = _dbContext.Games.ToList().FirstOrDefault(x => x.Id == gameId);
            if (game?.Player2 != null)
            {
                var player2 = _dbContext.Users.First(u => u.Id == game.Player2).UserName;
                await Clients.Users(game.Player1).SendAsync("UpdatePlayer2", player2);
            }

        }

        public async Task ClickField(string gameId, int fieldNumber)
        {
            // Setup variables
            string userId = Context.UserIdentifier;
            Game game = _dbContext.Games.ToList().FirstOrDefault(x => x.Id == gameId);
            if (game == null) return;
            FieldState[] fieldStates = JsonConvert.DeserializeObject<FieldState[]>(game.Board);
            bool isPlayer1 = game.IsPlayer1();

            if (!IsInputValid(userId, game, isPlayer1, fieldNumber, fieldStates))
                return;

            fieldStates[fieldNumber] = isPlayer1 ? FieldState.player1 : FieldState.player2;
            game.Board = JsonConvert.SerializeObject(fieldStates);
            game.TimeStamp = DateTime.Now;

            //Check if game is over
            for (var i = 0; i < 8; i++)
            {
                int a = Winners[i, 0], b = Winners[i, 1], c = Winners[i, 2];
                FieldState field1 = fieldStates[a], field2 = fieldStates[b], field3 = fieldStates[c];
                if (field1 == field2 && field2 == field3)
                {
                    switch (field1)
                    {
                        case FieldState.none:
                            break;
                        case FieldState.player1:
                            //Player1 won
                            game.Winner = (int)GameState.player1Won;
                            break;
                        case FieldState.player2:
                            //Player2 won
                            game.Winner = (int)GameState.player2Won;
                            break;
                    }
                }
            }
            if (!fieldStates.Contains(FieldState.none) && game.Winner == (int)FieldState.none)
            {
                //Draw
                game.Winner = (int)GameState.draw;
            }

            _dbContext.Update(game);
            _dbContext.SaveChanges();

            await Clients.Users(game.Player1, game.Player2).SendAsync("UpdateField", fieldNumber, isPlayer1 ? "X" : "O");

            if (!((GameState)game.Winner == GameState.pending))
            {
                var winnerId = (GameState)game.Winner == GameState.player1Won ? game.Player1 : game.Player2;
                var winner = _dbContext.Users.First(u => u.Id == winnerId).UserName;
                await Clients.Users(game.Player1, game.Player2).SendAsync("EndGame", game.Winner, winner);
            }
        }


        /// <summary>
        /// Checks, if given circumstances are valid for claiming the field.
        /// It's checking for authorization, game state, field state and if it's the users turn.
        /// </summary>
        /// <param name="userId">Id of user, calling the method.</param>
        /// <param name="game">The game in the given context.</param>
        /// <param name="isPlayer1">Is it player 1's turn?</param>
        /// <param name="fieldNumber">Number of field wich user wants to claim.</param>
        /// <param name="fieldStates">States of the fields (deserialized)</param>
        /// <returns></returns>
        private bool IsInputValid(string userId, Game game, bool isPlayer1, int fieldNumber, FieldState[] fieldStates)
        {
            if ((game.Player1 == userId || game.Player2 == userId) && // Authorized?
                (GameState)game.Winner == GameState.pending &&      // Game pending?
                fieldStates[fieldNumber] == FieldState.none &&      // Field empty?
                (isPlayer1 == (userId == game.Player1)))            // User's turn?
                return true;
            return false;
        }
    }
}
