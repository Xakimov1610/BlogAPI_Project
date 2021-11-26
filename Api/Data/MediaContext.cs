using Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class MediaContext : DbContext
    {
        DbSet<Post> Posts { get; set; }

        public DbSet<Media> Medias { get; set; }

        public MediaContext(DbContextOptions options)
        : base(options) { }
    }
}