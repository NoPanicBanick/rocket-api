using MySqlConnector;
using Rocket.API.Models;

namespace Rocket.API.Repositories
{
    public class GameRepository(MySqlConnection connection) : IGameRepository
    {
        private readonly MySqlConnection _connection = connection;

        public async Task<GameModel> AddAsync(GameModel model)
        {
            // open connection and create entry
            await _connection.OpenAsync();
            using var command = _connection.CreateCommand();
            command.CommandText = @"INSERT INTO Game (GameMode, RocketLeagueId) VALUES (@GameMode, @RocketLeagueId);";
            command.Parameters.AddWithValue("@GameMode", model.GameMode);
            command.Parameters.AddWithValue("@RocketLeagueId", model.RocketLeagueId);
            await command.ExecuteNonQueryAsync();
            var id = command.LastInsertedId;

            // select created object            
            using var command2 = _connection.CreateCommand();
            command2.CommandText = @"SELECT * FROM Game WHERE ID = @ID;";
            command2.Parameters.AddWithValue("@ID", command.LastInsertedId);
            var reader = await command2.ExecuteReaderAsync();

            return GameModel.FromSqlResult(reader);
        }

        public async Task<GameModel> GetByRocketIdAsync(Guid rocketLeagueId)
        {
            // set up query
            await _connection.OpenAsync();
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM Game WHERE RocketLeagueId = @ID;";
            command.Parameters.AddWithValue("@ID", rocketLeagueId);
            var reader = await command.ExecuteReaderAsync();

            return GameModel.FromSqlResult(reader);
        }

        public async Task<GameModel> GetAsync(int id)
        {
            // set up query
            await _connection.OpenAsync();
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM Game WHERE ID = @ID;";
            command.Parameters.AddWithValue("@ID", id);
            var reader = await command.ExecuteReaderAsync();

            return GameModel.FromSqlResult(reader);
        }
    }
}