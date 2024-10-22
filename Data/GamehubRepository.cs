
using GamehubAPI.Data;
using GamehubAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GamehubAPI.Data
{
    public class GamehubRepository<T> : IGamehubRepository<T> where T : class
    {
        private readonly GamehubDBContext _dbContext;
        private DbSet<T> _dbSet;
        public GamehubRepository(GamehubDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<T> CreateAsync(T dbRecord)
        {
            _dbSet.Add(dbRecord);
            await _dbContext.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(T dbRecord)
        {
            _dbSet.Remove(dbRecord);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        //public async Task<List<T>> GetAllAsync()
        //{
        //    return await _dbSet.ToListAsync();
        //}
        public async Task<List<T>> GetAllAsync(PaginationParams paginationParams)
        {
            var totalItems = await _dbSet.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)paginationParams.PageSize);

            return await _dbSet
           .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
           .Take(paginationParams.PageSize)
           .ToListAsync();

            //return await _dbSet.ToListAsync();
        }
        public async Task<List<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _dbSet.AsNoTracking().Where(filter).ToListAsync();
            else
                return await _dbSet.Where(filter).ToListAsync();

        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _dbSet.Where(filter).FirstOrDefaultAsync();

        }
     
        public async Task<T> UpdateAsync(T dbRecord)
        {

            _dbContext.Update(dbRecord);
            await _dbContext.SaveChangesAsync();

            return dbRecord;
        }
    }
}
