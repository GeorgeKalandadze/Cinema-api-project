using CinemaApiProject.Models;
using System.Data.Entity;

namespace CinemaApiProject.Data
{
    public class DataContext: DbContext
    {
        public DataContext() : base("name=MyConnectionString") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Actors)
                .WithMany(a => a.Movies)
                .Map(ma =>
                {
                    ma.ToTable("MovieActor");
                    ma.MapLeftKey("MovieId");
                    ma.MapRightKey("ActorId");
                });
        }
    }
}