using apiGames.InputModel;
using apiGames.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGames.Services
{
    public interface IGameService
    {
        Task<List<GameViewModel>> GetGame(int page, int quantity);
        Task<GameViewModel> GetGame(Guid id);
        Task<GameViewModel> CreateGame(GameInputModel game);
        Task UpdateGame(Guid id, GameInputModel game);
        Task UpdateGame(Guid id, double price);
        Task DeleteGame(Guid id); 
    }
}