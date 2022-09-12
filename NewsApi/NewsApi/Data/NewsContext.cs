using Microsoft.EntityFrameworkCore;
using NewsApi.Models;

namespace NewsApi.Data
{
    public class NewsContext : DbContext
    {
        //Some rows are commented because initially I used the PostgreSQL database
        
        //public const string SCHEMA = "News";

        public NewsContext(DbContextOptions<NewsContext> options)
            : base(options)
        {
        }

        public DbSet<News> News => Set<News>();
        public DbSet<DuplicatedNews> DuplicatedNews => Set<DuplicatedNews>();
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SCHEMA);

            ConfigureEntityTables(modelBuilder);
        }

        private void ConfigureEntityTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>().ToTable(typeof(News).Name, SCHEMA);
            modelBuilder.Entity<DuplicatedNews>().ToTable(typeof(DuplicatedNews).Name, SCHEMA);

        }*/
    }
}
