using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Movies_DAL.Models
{
    public partial class MoviesContext : DbContext
    {
        public MoviesContext()
        {
        }

        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<GenreMovie> GenreMovies { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieRole> MovieRoles { get; set; }
        public virtual DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=Movies;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genres");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ImdbName)
                    .HasMaxLength(50)
                    .HasColumnName("imdb_name");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<GenreMovie>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.GenreId })
                    .HasName("PK_genre_movie_raw");

                entity.ToTable("genre_movie");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.GenreMovies)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_genre_movie_genres");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.GenreMovies)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_genre_movie_movies");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movies");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CoverUrl)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("cover_url");

                entity.Property(e => e.ImdbId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("imdb_id");

                entity.Property(e => e.Kind)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("kind");

                entity.Property(e => e.OriginalAirDate)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("original_air_date");

                entity.Property(e => e.Plot)
                    .HasColumnType("text")
                    .HasColumnName("plot");

                entity.Property(e => e.Rating)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("rating");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.Property(e => e.Top250Rank).HasColumnName("top_250_rank");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<MovieRole>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.PersonId, e.Role });

                entity.ToTable("movie_role");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasColumnName("role");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieRoles)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movie_role_movies");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.MovieRoles)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movie_role_persons");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("persons");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Biography)
                    .IsRequired()
                    .HasColumnName("biography");

                entity.Property(e => e.ImdbId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("imdb_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
