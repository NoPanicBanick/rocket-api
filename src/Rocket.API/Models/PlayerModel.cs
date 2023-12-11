using MySqlConnector;

namespace Rocket.API.Models
{
    public class PlayerModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double RocketLeagueId { get; set; }
        public DateTime CreatedDatetimeUtc { get; set; }

        public static PlayerModel FromSqlResult(MySqlDataReader reader)
        {
            var response = new PlayerModel();
            while (reader.Read())
            {
                response.ID = reader.GetInt32("ID");
                response.Name = reader.GetString("Name");
                response.RocketLeagueId = reader.GetInt32("RocketLeagueId");
                response.CreatedDatetimeUtc = reader.GetDateTime("CreatedDatetimeUtc");
            }

            return response;
        }
    }
}
