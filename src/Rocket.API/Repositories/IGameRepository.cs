using Rocket.API.Models;

namespace Rocket.API.Repositories
{
    public interface IGameRepository
    {
        Task<GameModel> AddAsync(GameModel model);
        Task<GameModel> GetByRocketIdAsync(Guid rocketLeagueId);
        Task<GameModel> GetAsync(int id);
    }
}