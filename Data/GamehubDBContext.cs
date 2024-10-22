using GamehubAPI.Config;
using GamehubAPI.Model;
using Microsoft.EntityFrameworkCore;
namespace GamehubAPI.Data

{
    public class GamehubDBContext : DbContext
    {
        public GamehubDBContext(DbContextOptions<GamehubDBContext> options) : base(options)
        {



        }
        public DbSet<Game> Games { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Table 1
            modelBuilder.ApplyConfiguration(new GameConfig());

        }
    }
}
