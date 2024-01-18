using Microsoft.EntityFrameworkCore;
using rovic_rating_app.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace rovic_rating_app.Data
{
    public class DataContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)  { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<MovieTag> MovieTags { get; set; }
        public DbSet<AlbumTag> AlbumTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.HasOne(u => u.User)
                    .WithMany(t => t.Tags)
                    .HasForeignKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title)
                    .HasMaxLength(200)
                    .IsRequired();
                entity.Property(x => x.Description)
                    .HasMaxLength(400)
                    .IsRequired();
                entity.Property(x => x.ProductionYear)
                    .IsRequired();
                entity.Property(x => x.Poster)
                    .IsRequired();
                entity.Property(x => x.Rate)
                    .IsRequired();

                entity.HasOne(u => u.User)
                    .WithMany(m => m.Movies)
                    .HasForeignKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title)
                    .HasMaxLength(200)
                    .IsRequired();
                entity.Property(x => x.Artist)
                    .HasMaxLength(50)
                    .IsRequired();
                entity.Property(x => x.ProductionYear)
                    .IsRequired();
                entity.Property(x => x.Cover)
                    .IsRequired();
                entity.Property(x => x.Rate)
                    .IsRequired();

                entity.HasOne(u => u.User)
                    .WithMany(a => a.Albums)
                    .HasForeignKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MovieTag>(entity =>
            {
                entity.HasKey(mt => new { mt.MovieId, mt.TagId });

                entity.HasOne(mt => mt.Movie)
                    .WithMany(m => m.MovieTags)
                    .HasForeignKey(mt => mt.MovieId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(mt => mt.Tag)
                    .WithMany(m => m.MovieTags)
                    .HasForeignKey(mt => mt.TagId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AlbumTag>(entity =>
            {
                entity.HasKey(at => new { at.AlbumId, at.TagId });

                entity.HasOne(at => at.Album)
                    .WithMany(a => a.AlbumTags)
                    .HasForeignKey(at => at.AlbumId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(at => at.Tag)
                    .WithMany(a => a.AlbumTags)
                    .HasForeignKey(at => at.TagId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
