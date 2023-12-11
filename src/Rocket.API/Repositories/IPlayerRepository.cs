using Rocket.API.Models;

namespace Rocket.API
{
    public interface IPlayerRepository
    {
        Task<PlayerModel> AddAsync(PlayerModel player);
        Task<PlayerModel> GetByRocketIdAsync(double id);
        Task<PlayerModel> GetAsync(int id);
    }
}