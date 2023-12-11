using MySqlConnector;
using Rocket.API.Models;

namespace Rocket.API.Repositories
{
    public class PlayerRepository(MySqlConnection connection) : IPlayerRepository
    {
        private readonly MySqlConnection _connection = connection;

        public async Task<PlayerModel> AddAsync(PlayerModel player)
        {
            // open connection and create entry
            await _connection.OpenAsync();
            using var command = _connection.CreateCommand();
            command.CommandText = @"INSERT INTO Player (Name, RocketLeagueId) VALUES (@Name, @RocketLeagueId);";
            command.Parameters.AddWithValue("@Name", player.Name);
            command.Parameters.AddWithValue("@RocketLeagueId", player.RocketLeagueId);
            await command.ExecuteNonQueryAsync();
            var id = command.LastInsertedId;

            // select created object            
            using var command2 = _connection.CreateCommand();
            command2.CommandText = @"SELECT * FROM Player WHERE ID = @ID;";
            command2.Parameters.AddWithValue("@ID", command.LastInsertedId);
            var reader = await command2.ExecuteReaderAsync();

            return PlayerModel.FromSqlResult(reader);
        }

        public async Task<PlayerModel> GetByRocketIdAsync(double rocketLeagueId)
        {
            // set up query
            await _connection.OpenAsync();
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM Player WHERE RocketLeagueId = @ID;";
            command.Parameters.AddWithValue("@ID", rocketLeagueId);
            var reader = await command.ExecuteReaderAsync();

            return PlayerModel.FromSqlResult(reader);
        }

        public async Task<PlayerModel> GetAsync(int id)
        {
            // set up query
            await _connection.OpenAsync();
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM Player WHERE ID = @ID;";
            command.Parameters.AddWithValue("@ID", id);
            var reader = await command.ExecuteReaderAsync();

            return PlayerModel.FromSqlResult(reader);
        }
    }
}