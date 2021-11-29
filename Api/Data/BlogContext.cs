using Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Media> Medias { get; set; }



        public BlogContext(DbContextOptions options)
            : base(options) { }
    }
}