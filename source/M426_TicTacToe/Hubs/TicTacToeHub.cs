using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace M426_TicTacToe.Hubs
{
    public class TicTacToeHub : Hub
    {
        private static List<Game> _games = new();
        public async Task ClickField(int fieldNumber)
        {
            string userId = Context.UserIdentifier;
            Game foundGame = _games.Find(x => (x.User1Id == userId || x.User2Id == userId) && !x.IsFinished);
            bool senderIsUser1 = foundGame.User1Id == userId;
            if (foundGame.IsUser1 && !senderIsUser1 || !foundGame.IsUser1 && senderIsUser1)
                return;

            if (foundGame.Fields[fieldNumber] != '\0')
                return;

            char fieldValue;
            if (senderIsUser1)
                fieldValue = 'X';
            else
                fieldValue = 'O';

            foundGame.Fields[fieldNumber] = fieldValue;
            foundGame.IsUser1 = !foundGame.IsUser1;
            await Clients.Users(foundGame.User1Id, foundGame.User2Id).SendAsync("UpdateField", fieldNumber, fieldValue);
        }

        public override Task OnConnectedAsync()
        {
            string userId = Context.UserIdentifier;
            Game foundGame = _games.Find(x => (x.User1Id == userId || x.User2Id == userId) && !x.IsFinished);
            if (foundGame == null)
            {
                if (_games.Count == 0 || _games.Last().User2Id != String.Empty)
                    _games.Add(new() { User1Id = userId });
                else
                    _games.Last().User2Id = userId;
            }

            return base.OnConnectedAsync();
        }
    }

    public class Game
    {
        public int Id { get; set; }
        public string User1Id { get; set; }
        public string User2Id { get; set; }
        public bool IsUser1 { get; set; } = true;
        public bool IsFinished { get; set; }
        public char[] Fields { get; set; } = new char[9];

        public Game()
        {
            Random random = new Random();
            Id = random.Next(1000000, 9999999);
        }
    }
}
