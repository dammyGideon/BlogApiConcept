using Microsoft.EntityFrameworkCore;

namespace Waji.Api.Data.Models
{
    public class WajeInterViewDbContext : DbContext
    {
        public WajeInterViewDbContext(DbContextOptions<WajeInterViewDbContext> options) : base(options) { }


        public DbSet<Author> Authors { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(b => b.Blogs)
                .WithOne(b => b.Author)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Blog>()
                .HasMany(b=>b.Posts)
                .WithOne(p=>p.Blog)
                .HasForeignKey(b=>b.BlogId) 
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
