using Rocket.API.Models;

namespace Rocket.API.Repositories
{
    public interface IPlayerGameRepository
    {
        Task<PlayerGameModel> AddAsync(PlayerGameModel model);
    }
}