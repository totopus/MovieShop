using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MovieShopDbContext: DbContext
    {
        //get the connection string into constructor
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //specify Fluent API rules for your Entities

            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PurchaseNumber).HasColumnType("uniqueidentifier");
            builder.Property(p => p.TotalPrice).HasColumnType("decimal(18,2)").HasDefaultValue(9.9m);
            builder.Property(p => p.PurchaseDateTime).HasDefaultValueSql("getdate()");
            builder.HasOne(m => m.Movie).WithMany(p => p.Purchases).HasForeignKey(m => m.MovieId);
            builder.HasOne(u => u.User).WithMany(p => p.Purchases).HasForeignKey(u => u.UserId);
        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(ur=>new {ur.UserId ,ur.RoleId });
            builder.HasOne(u => u.User).WithMany(r => r.Roles).HasForeignKey(u => u.UserId);
            builder.HasOne(r => r.Role).WithMany(u => u.Users).HasForeignKey(r => r.RoleId);
        }

        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorite");
            builder.HasKey(f => f.Id);
            builder.HasOne(u => u.User).WithMany(f => f.Favorites).HasForeignKey(u=>u.UserId);
            builder.HasOne(m => m.Movie).WithMany(f => f.Favorites).HasForeignKey(m => m.MovieId);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(r => new { r.MovieId, r.UserId });
            builder.HasOne(m=>m.Movie).WithMany(r=>r.Reviews).HasForeignKey(m=>m.MovieId);
            builder.HasOne(u => u.User).WithMany(r => r.Reviews).HasForeignKey(u => u.UserId);
            builder.Property(r=>r.Rating).HasColumnType("decimal(3, 2)").HasDefaultValue(9.9m);
            
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(mc => mc.Character);
            builder.Property(mc => mc.Character).HasMaxLength(450);
            builder.HasOne(m => m.Movie).WithMany(m => m.Casts).HasForeignKey(m => m.MovieId);
            builder.HasOne(c => c.Cast).WithMany(c => c.Movies).HasForeignKey(c => c.CastId);
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            builder.HasOne(m => m.Movie).WithMany(m => m.Genres).HasForeignKey(m => m.MovieId);
            builder.HasOne(g => g.Genre).WithMany(g => g.Movies).HasForeignKey(g => g.GenreId);
        }
        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);
        }

            private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).HasMaxLength(128);
            builder.Property(u => u.LastName).HasMaxLength(128);
            builder.Property(u => u.DateOfBirth).HasDefaultValueSql("getdate()");
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.HashedPassword).HasMaxLength(1024);
            builder.Property(u => u.Salt).HasMaxLength(1024);
            builder.Property(u => u.PhoneNumber).HasMaxLength(16);
            builder.Property(u => u.TwoFactorEnabled).HasColumnType("bit");
            builder.Property(u => u.LockoutEndDate).HasDefaultValueSql("getdate()");
            builder.Property(u => u.LastLoginDateTime).HasDefaultValueSql("getdate()");
            builder.Property(u => u.IsLocked).HasColumnType("bit");
            builder.Property(u => u.AccessFailedCount);
        }
        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).HasMaxLength(20);
        }
        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.ToTable("Cast");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(128);
            builder.Property(c => c.Gender);
            builder.Property(c => c.TmdbUrl);
            builder.Property(c => c.ProfilePath).HasMaxLength(2084);
        }
        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            //specity all the constraints and rules for Movie Entity
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            
            //tell EF to ignore rating property and not create the column
            builder.Ignore(m => m.Rating);
        }
        //make sure entity classes are represented as DbSets
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

    }
}
