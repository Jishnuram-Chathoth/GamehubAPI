

using GamehubAPI.Data;
using GamehubAPI.Model;

namespace GamehubAPI.Data
{
    public class GameRepository : GamehubRepository<Game>, IGameRepository
    {
        private readonly GamehubDBContext _dbContext;

        public GameRepository(GamehubDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Game>> GetGame()
        {
            
            return null;
        }


    }
}
