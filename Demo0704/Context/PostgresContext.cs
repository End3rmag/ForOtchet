using System;
using System.Collections.Generic;
using Demo0704.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo0704.Context;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Izmerenie> Izmerenies { get; set; }

    public virtual DbSet<Maker> Makers { get; set; }

    public virtual DbSet<Postavshik> Postavshiks { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProduct> UserProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost:5432;Database=postgres;Username=postgres;password=123123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pk");

            entity.ToTable("category", "demo0704");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Izmerenie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("izmerenie_pk");

            entity.ToTable("izmerenie", "demo0704");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Maker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("maker_pk");

            entity.ToTable("maker", "demo0704");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Postavshik>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("postavshik_pk");

            entity.ToTable("postavshik", "demo0704");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pk");

            entity.ToTable("product", "demo0704");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Articul)
                .HasColumnType("character varying")
                .HasColumnName("articul");
            entity.Property(e => e.DiscountNow).HasColumnName("discount_now");
            entity.Property(e => e.Discription)
                .HasColumnType("character varying")
                .HasColumnName("discription");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.IdIzmerenie).HasColumnName("id_izmerenie");
            entity.Property(e => e.IdMaker).HasColumnName("id_maker");
            entity.Property(e => e.IdPostavshik).HasColumnName("id_postavshik");
            entity.Property(e => e.InSklad).HasColumnName("in_sklad");
            entity.Property(e => e.MaxDiscount).HasColumnName("max_discount");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Picture)
                .HasColumnType("character varying")
                .HasColumnName("picture");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_category_fk");

            entity.HasOne(d => d.IdIzmerenieNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdIzmerenie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_izmerenie_fk");

            entity.HasOne(d => d.IdMakerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdMaker)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_maker_fk");

            entity.HasOne(d => d.IdPostavshikNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdPostavshik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_postavshik_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pk");

            entity.ToTable("role", "demo0704");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.ToTable("users", "demo0704");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.NameLastname)
                .HasColumnType("character varying")
                .HasColumnName("name_lastname");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_fk");
        });

        modelBuilder.Entity<UserProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_product_pk");

            entity.ToTable("user_product", "demo0704");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.UserProducts)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_product_product_fk");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserProducts)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_product_users_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
