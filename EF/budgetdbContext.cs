using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace csvToDb.EF
{
    public partial class budgetdbContext : DbContext
    {
        public budgetdbContext()
        {
        }

        public budgetdbContext(DbContextOptions<budgetdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Transactions> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                AppSettings appSettings = new AppSettings("appsettings.json");
                optionsBuilder.UseSqlServer(appSettings.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.ToTable("transactions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasColumnName("account")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.MainCategory)
                    .IsRequired()
                    .HasColumnName("main_category")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.SubCategory)
                    .HasColumnName("sub_category")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(4000)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
