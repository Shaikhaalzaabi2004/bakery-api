using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BakeryAPI.Models;

public partial class BakeryDbContext : DbContext
{
    public BakeryDbContext()
    {
    }

    public BakeryDbContext(DbContextOptions<BakeryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Channel> Channels { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<MemberShipStatus> MemberShipStatuses { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Preference> Preferences { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductsIngredient> ProductsIngredients { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESSSERVER; Initial Catalog=BakeryDB; TrustServerCertificate=True; User Id=sa; Password=1234567");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Channel>(entity =>
        {
            entity.ToTable("Channel");

            entity.Property(e => e.Channel1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Channel");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Age)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("age");
            entity.Property(e => e.Churned)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Gender).WithMany(p => p.Customers)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK_Customers_Gender");

            entity.HasOne(d => d.MemberShipStatus).WithMany(p => p.Customers)
                .HasForeignKey(d => d.MemberShipStatusId)
                .HasConstraintName("FK_Customers_MemberShipStatus");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("Gender");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MemberShipStatus>(entity =>
        {
            entity.ToTable("MemberShipStatus");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.ToTable("PaymentMethod");

            entity.Property(e => e.PaymentMethod1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PaymentMethod");
        });

        modelBuilder.Entity<Preference>(entity =>
        {
            entity.HasOne(d => d.Category).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Preferences_Categories");

            entity.HasOne(d => d.Customer).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Preferences_Customers");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Active)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IntroducedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhotoName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Seasonal)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Categories");
        });

        modelBuilder.Entity<ProductsIngredient>(entity =>
        {
            entity.HasOne(d => d.Ingredient).WithMany(p => p.ProductsIngredients)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("FK_ProductsIngredients_Ingredients");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductsIngredients)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductsIngredients_Products");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Time).HasColumnType("datetime");

            entity.HasOne(d => d.Channel).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ChannelId)
                .HasConstraintName("FK_Transactions_Channel");

            entity.HasOne(d => d.Customer).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Transactions_Customers");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK_Transactions_PaymentMethod");

            entity.HasOne(d => d.Product).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Transactions_Products");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
