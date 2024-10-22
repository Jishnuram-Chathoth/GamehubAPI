using GamehubAPI.Data;
using GamehubAPI.Model;

namespace GamehubAPI.Data
{
    public interface IGameRepository : IGamehubRepository<Game>
    {
        Task<List<Game>> GetGame();

    }
}
