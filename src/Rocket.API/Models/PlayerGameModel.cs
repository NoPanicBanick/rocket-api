using MySqlConnector;

namespace Rocket.API.Models
{
    public class PlayerGameModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Bumps { get; set; }
        public int Demos { get; set; }
        public int BallTouches { get; set; }
        public int OwnGoals { get; set; }
        public int Shots { get; set; }
        public int Score { get; set; }
        public int Saves { get; set; }
        public DateTime CreatedDatetimeUtc { get; set; }

        public static PlayerGameModel FromSqlResult(MySqlDataReader reader)
        {
            var response = new PlayerGameModel();
            while (reader.Read())
            {
                response.Id = reader.GetInt32("ID");
                response.PlayerId = reader.GetInt32("PlayerId");
                response.GameId = reader.GetInt32("GameId");
                response.Goals = reader.GetInt32("Goals");
                response.Assists = reader.GetInt32("Assists");
                response.Bumps = reader.GetInt32("Bumps");
                response.Demos = reader.GetInt32("Demos");
                response.BallTouches = reader.GetInt32("BallTouches");
                response.OwnGoals = reader.GetInt32("OwnGoals");
                response.Shots = reader.GetInt32("Shots");
                response.Score = reader.GetInt32("Score");
                response.Saves = reader.GetInt32("Saves");
            }

            return response;
        }
    }
}