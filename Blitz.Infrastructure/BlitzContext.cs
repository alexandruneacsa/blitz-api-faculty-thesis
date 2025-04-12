using Blitz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Blitz.Infrastructure
{
    public partial class BlitzContext : DbContext
    {
        private readonly IConfiguration _config;

        public BlitzContext(IConfiguration configuration)
        {
            _config = configuration;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }
        public virtual DbSet<Code> Codes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:Blitz"])
                .LogTo(mesaj => Debug.WriteLine(mesaj));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.Property(e => e.Extension).HasColumnType("nvarchar(250)");

                entity.Property(e => e.ContentType).HasColumnType("nvarchar(250)");

                entity.Property(e => e.Name).HasColumnType("nvarchar(250)");

                entity.Property<byte[]>("DocumentContent").HasColumnName("DocumentContent")
                    .HasColumnType("Varbinary(max)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id").HasColumnType("nvarchar(1000)");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("Email").HasColumnType("nvarchar(255)");

                entity.Property(e => e.IsAdmin).HasColumnName("IsAdmin").HasColumnType("bit");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(3000)
                    .HasColumnName("PasswordHash");

                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .HasColumnName("Username").HasColumnType("nvarchar(255)");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .HasColumnName("PhoneNumber").HasColumnType("nvarchar(255)");
            });

            modelBuilder.Entity<User>()
                .HasKey(o => o.Id);
        }
    }
}