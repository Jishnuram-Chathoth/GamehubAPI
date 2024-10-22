using GamehubAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamehubAPI.Config
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.Title).IsRequired();
            builder.Property(n => n.Genre).IsRequired().HasMaxLength(50);
            builder.Property(n => n.Description).IsRequired().HasMaxLength(500);
            builder.Property(n => n.Price).IsRequired();
            builder.Property(n => n.ReleaseDate).IsRequired();
            builder.Property(n => n.StockQty).IsRequired();
        }
    }
}
