namespace apiGames.Repositories
{
    public interface IGameRepository
    {
        Task<List<Game>> GetGame(int page, int quantity);
        Task<Game> GetGame(Guid id);
        Task<List<Game>> GetGame(string name, string producer);
        Task Create(Game game);
        Task UpdateGame(Game game);
        Task DeleteGame(Guid id);
    }
}