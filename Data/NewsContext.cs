using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proyecto_Web.Models;

namespace Proyecto_Web.Data;

public partial class NewsContext : DbContext
{
    public NewsContext()
    {
    }

    public NewsContext(DbContextOptions<NewsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<YohaNews> News { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=News;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(15);
            entity.Property(e => e.Description).HasColumnType("ntext");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.Capital)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Continent)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Currency)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Language)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.PictureFlag)
                .HasColumnType("image")
                .HasColumnName("Picture_Flag");
            entity.Property(e => e.PicturePath)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Population).HasMaxLength(200);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleD).HasName("PK_Rols");

            entity.Property(e => e.RoleD).ValueGeneratedNever();
            entity.Property(e => e.RoleName)
                .HasMaxLength(15)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_Roles");
        });

        modelBuilder.Entity<YohaNews>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK_News");

            entity.ToTable("yohaNews");

            entity.Property(e => e.NewsId).HasColumnName("NewsID");
            entity.Property(e => e.Author)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsFixedLength();
            entity.Property(e => e.Picture).HasColumnType("image");
            entity.Property(e => e.PicturePath)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PublicationDate).HasColumnName("Publication_Date");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.YohaNews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_yohaNews_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
