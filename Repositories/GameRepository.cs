namespace apiGames.Repositories
{
    public class GameRepository
    {
        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Game{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Spider-Man", Produtora = "Sony", Preco = 150} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Game{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "Watch Dogs", Produtora = "Ubisoft", Preco = 120} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Game{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "Cyberpunk 2077", Produtora = "CD Projekt", Preco = 70} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Game{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Horizon Zero Dawn", Produtora = "Guerrilla", Preco = 100} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Game{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "Red Dead Redemption 2", Produtora = "Rockstar", Preco = 250} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Game{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome = "Grand Theft Auto V", Produtora = "Rockstar", Preco = 190} }
        };

        public Task<List<Game>> Obter(int page, int quantity)
        {
            return Task.FromResult(games.Values.Skip((page - 1) * quantity).Take(quantity).ToList());
        }

        public Task<Game> Obter(Guid id)
        {
            if (!games.ContainsKey(id))
                return Task.FromResult<Game>(null);

            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> Obter(string name, string producer)
        {
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.Producer.Equals(producer)).ToList());
        }

        public Task<List<Jogo>> ObterSemLambda(string name, string producer)
        {
            var retorno = new List<Game>();

            foreach (var game in games.Values)
            {
                if (game.Name.Equals(name) && jogo.Producer.Equals(producer))
                    retorno.Add(game);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task UpdateGame(Jogo jogo)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }

        public Task DeleteGame(Guid id)
        {
            games.DeleteGame(id);
            return Task.CompletedTask;
        }

    }
}