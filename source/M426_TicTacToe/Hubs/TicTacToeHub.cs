using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace M426_TicTacToe.Hubs
{
    public class TicTacToeHub : Hub
    {
        public async Task ClickField(int fieldNumber)
        {
            char fieldContent = 'X';
            await Clients.All.SendAsync("UpdateField", fieldNumber, fieldContent);
        }
    }
}
