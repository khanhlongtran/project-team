using System;
using System.Collections.Generic;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestPRN231.Models
{
    public partial class PRN2xx_BookContext : DbContext
    {
        public PRN2xx_BookContext()
        {
        }

        public PRN2xx_BookContext(DbContextOptions<PRN2xx_BookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Library> Libraries { get; set; } = null!;
        public virtual DbSet<Publisher> Publishers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                Env.Load(); // Nạp giá trị từ .env
                var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string is not found in .env file.");
                }

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.PublishDate).HasColumnType("date");

                entity.Property(e => e.PublisherId).HasColumnName("PublisherID");

                entity.Property(e => e.TitleBook).HasMaxLength(100);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK__Book__GenreID__4222D4EF");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .HasConstraintName("FK__Book__PublisherI__4316F928");

                entity.HasMany(d => d.Authors)
                    .WithMany(p => p.Books)
                    .UsingEntity<Dictionary<string, object>>(
                        "BookAuthor",
                        l => l.HasOne<Author>().WithMany().HasForeignKey("AuthorId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__BookAutho__Autho__440B1D61"),
                        r => r.HasOne<Book>().WithMany().HasForeignKey("BookId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__BookAutho__BookI__44FF419A"),
                        j =>
                        {
                            j.HasKey("BookId", "AuthorId").HasName("PK__BookAuth__6AED6DE6900A193A");

                            j.ToTable("BookAuthor");

                            j.IndexerProperty<int>("BookId").HasColumnName("BookID");

                            j.IndexerProperty<int>("AuthorId").HasColumnName("AuthorID");
                        });
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.GenreName).HasMaxLength(100);
            });

            modelBuilder.Entity<Library>(entity =>
            {
                entity.ToTable("Library");

                entity.Property(e => e.LibraryId).HasColumnName("LibraryID");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.LibraryName).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.HasMany(d => d.Books)
                    .WithMany(p => p.Libraries)
                    .UsingEntity<Dictionary<string, object>>(
                        "LibraryBook",
                        l => l.HasOne<Book>().WithMany().HasForeignKey("BookId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__LibraryBo__BookI__45F365D3"),
                        r => r.HasOne<Library>().WithMany().HasForeignKey("LibraryId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__LibraryBo__Libra__46E78A0C"),
                        j =>
                        {
                            j.HasKey("LibraryId", "BookId").HasName("PK__LibraryB__C2E84B9D330632F0");

                            j.ToTable("LibraryBook");

                            j.IndexerProperty<int>("LibraryId").HasColumnName("LibraryID");

                            j.IndexerProperty<int>("BookId").HasColumnName("BookID");
                        });
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("Publisher");

                entity.Property(e => e.PublisherId).HasColumnName("PublisherID");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.PublisherName).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
