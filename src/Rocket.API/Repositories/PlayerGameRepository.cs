using MySqlConnector;
using Rocket.API.Models;

namespace Rocket.API.Repositories
{
    public class PlayerGameRepository(MySqlConnection connection) : IPlayerGameRepository
    {
        private readonly MySqlConnection _connection = connection;

        public async Task<PlayerGameModel> AddAsync(PlayerGameModel model)
        {
            // open connection and create entry
            await _connection.OpenAsync();
            using var command = _connection.CreateCommand();
            command.CommandText = @"INSERT INTO PlayerGame (PlayerId, GameId, Goals, Assists, Bumps, Demos, BallTouches, OwnGoals, Shots, Score, Saves) 
                VALUES (@PlayerId, @GameId, @Goals, @Assists, @Bumps, @Demos, @BallTouches, @OwnGoals, @Shots, @Score, @Saves);";
            command.Parameters.AddWithValue("@PlayerId", model.PlayerId);
            command.Parameters.AddWithValue("@GameId", model.GameId);
            command.Parameters.AddWithValue("@Goals", model.Goals);
            command.Parameters.AddWithValue("@Assists", model.Assists);
            command.Parameters.AddWithValue("@Bumps", model.Bumps);
            command.Parameters.AddWithValue("@Demos", model.Demos);
            command.Parameters.AddWithValue("@BallTouches", model.BallTouches);
            command.Parameters.AddWithValue("@OwnGoals", model.OwnGoals);
            command.Parameters.AddWithValue("@Shots", model.Shots);
            command.Parameters.AddWithValue("@Score", model.Score);
            command.Parameters.AddWithValue("@Saves", model.Saves);
            await command.ExecuteNonQueryAsync();
            var id = command.LastInsertedId;

            // select created object            
            using var command2 = _connection.CreateCommand();
            command2.CommandText = @"SELECT * FROM PlayerGame WHERE ID = @ID;";
            command2.Parameters.AddWithValue("@ID", command.LastInsertedId);
            var reader = await command2.ExecuteReaderAsync();

            return PlayerGameModel.FromSqlResult(reader);
        }
    }
}