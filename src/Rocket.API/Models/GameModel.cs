using MySqlConnector;

namespace Rocket.API.Models
{
    public class GameModel
    {
        public int ID { get; set; }
        public string GameMode { get; set; }
        public Guid RocketLeagueId { get; set; }
        public DateTime CreatedDatetimeUtc { get; set; }

        public static GameModel FromSqlResult(MySqlDataReader reader)
        {
            var response = new GameModel();
            while (reader.Read())
            {
                response.ID = reader.GetInt32("ID");
                response.GameMode = reader.GetString("GameMode");
                response.RocketLeagueId = reader.GetGuid("RocketLeagueId");
                response.CreatedDatetimeUtc = reader.GetDateTime("CreatedDatetimeUtc");
            }

            return response;
        }
    }
}