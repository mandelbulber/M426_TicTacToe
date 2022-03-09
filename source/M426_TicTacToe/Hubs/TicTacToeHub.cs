using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using M426_TicTacToe.Data;

namespace M426_TicTacToe.Hubs
{
    public class TicTacToeHub : Hub
    {
        private ApplicationDbContext _context;
        public TicTacToeHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ClickField(int fieldNumber)
        {
            await Clients.All.SendAsync("UpdateField", fieldNumber, "O");
        }
    }
}
