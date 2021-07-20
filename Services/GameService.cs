using apiGames.Entities;
using apiGames.Exceptions;
using apiGames.InputModel;
using apiGames.Repositories;
using apiGames.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGames.Services
{
    public class GameService : IGameRepository
    {
        private readonly IGameRepository _gameRepository;

        public JogoService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> GetGame(int pagina, int quantidade)
        {
            var games = await _gameRepository.GetGame(page, quantity);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Nome,
                Producer = game.Producer,
                Price = game.Preco
            }).ToList();
        }

        public async Task<GameViewModel> GetGame(Guid id)
        {
            var game = await _gameRepository.GetGame(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> Create(GameInputModel game)
        {
            var entidadeGame = await _gameRepository.GetGame(game.Name, game.Producer);

            if (entidadeGame.Count > 0)
                throw new JogoJaCadastradoException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };

            await _gameRepository.Create(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Nome,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task UpdateGame(Guid id, GameInputModel game)
        {
            var entidadeGame = await _gameRepository.GetGame(id);

            if (entidadeGame == null)
                throw new GameNotCreatedException();

            entidadeGame.Name = game.Nome;
            entidadeGame.Producer = game.Producer;
            entidadeGame.Price = game.Price;

            await _gameRepository.UpdateGame(entidadeGame);
        }

        public async Task UpdateGame(Guid id, double price)
        {
            var entidadeGame = await _gameRepository.GetGame(id);

            if (entidadeGame == null)
                throw new GameNotCreatedException();

            entidadeGame.Price = price;

            await _gameRepository.UpdateGame(entidadeGame);
        }

        public async Task DeleteGame(Guid id)
        {
            var game = await _gameRepository.GetGame(id);

            if (game == null)
                throw new GameNotCreatedException();

            await _gameRepository.Remover(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
        
    }
}