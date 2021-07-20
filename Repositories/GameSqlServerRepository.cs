using apiGames.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace apiGames.Repositories
{
    public class GameSqlServerRepository : IGameRepository
    {
        private readonly SqlConnection sqlConnection;

        public GameSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Game>> Obter(int page, int quantity)
        {
            var games = new List<Game>();

            var comando = $"select * from Jogos order by id offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"]
                });
            }

            await sqlConnection.CloseAsync();

            return gamess;
        }

        public async Task<Jogo> GetGame(Guid id)
        {
            Game game = null;

            var comando = $"select * from Jogos where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                game = new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"]
                };
            }

            await sqlConnection.CloseAsync();

            return game;
        }

        public async Task<List<Game>> Obter(string name, string producer)
        {
            var games = new List<Jogo>();

            var comando = $"select * from Jogos where Nome = '{name}' and Produtora = '{producer}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"]
                });
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task Create(Jogo jogo)
        {
            var comando = $"insert Jogos (Id, Name, Producer, Price) values ('{game.Id}', '{game.Name}', '{game.Producer}', {game.Price.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task UpdateGame(Game game)
        {
            var comando = $"update Games set Nome = '{game.Name}', Producer = '{game.Producer}', Preco = {game.Price.ToString().Replace(",", ".")} where Id = '{game.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task DeleteGame(Guid id)
        {
            var comando = $"delete from Jogos where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}