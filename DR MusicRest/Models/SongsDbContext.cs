using Microsoft.EntityFrameworkCore;
namespace DR_MusicRest.Models
{
    public class SongsDbContext : DbContext
    {
        public SongsDbContext(DbContextOptions<SongsDbContext> options) : base(options)
        {
        }
        public DbSet<Song> Songs { get; set; }

    }
}
