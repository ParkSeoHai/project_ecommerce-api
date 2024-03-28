using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using project_ecommerce_api.Models;

namespace project_ecommerce_api.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressShop> AddressShops { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public virtual DbSet<FlashSale> FlashSales { get; set; }

    public virtual DbSet<FlashSaleItem> FlashSaleItems { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductShop> ProductShops { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressShop>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.District).HasMaxLength(255);
            entity.Property(e => e.NameShop).HasMaxLength(255);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.UrlMap).HasMaxLength(255);
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_Carts_CustomerId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts).HasForeignKey(d => d.CustomerId);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => new { e.CartId, e.ProductId });

            entity.HasIndex(e => e.ProductId, "IX_CartItems_ProductId");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems).HasForeignKey(d => d.CartId);

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Categories_Name").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_Colors_ProductId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Image)
                .HasMaxLength(500)
                .HasDefaultValue("");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Product).WithMany(p => p.Colors).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_Customers_Email").IsUnique();

            entity.HasIndex(e => e.EmailConfirmed, "IX_Customers_EmailConfirmed").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.EmailConfirmed).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(1000);
            entity.Property(e => e.Picture).HasMaxLength(255);
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_CustomerAddresses_CustomerId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerAddresses).HasForeignKey(d => d.CustomerId);
        });

        modelBuilder.Entity<FlashSale>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<FlashSaleItem>(entity =>
        {
            entity.HasKey(e => new { e.FlashSaleId, e.ProductId });

            entity.HasIndex(e => e.ProductId, "IX_FlashSaleItems_ProductId");

            entity.HasOne(d => d.FlashSale).WithMany(p => p.FlashSaleItems).HasForeignKey(d => d.FlashSaleId);

            entity.HasOne(d => d.Product).WithMany(p => p.FlashSaleItems).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_Images_ProductId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Src).HasMaxLength(255);

            entity.HasOne(d => d.Product).WithMany(p => p.Images).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasIndex(e => e.Title, "IX_News_Title").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasIndex(e => e.ColorId, "IX_Options_ColorId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Value).HasMaxLength(100);

            entity.HasOne(d => d.Color).WithMany(p => p.Options).HasForeignKey(d => d.ColorId);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerId");

            entity.HasIndex(e => e.StatusId, "IX_Orders_StatusId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Note).HasMaxLength(1000);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Status).WithMany(p => p.Orders).HasForeignKey(d => d.StatusId);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.HasIndex(e => e.ProductId, "IX_OrderItems_ProductId");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.BrandId, "IX_Products_BrandId");

            entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

            entity.HasIndex(e => e.Name, "IX_Products_Name").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy).HasMaxLength(500);
            entity.Property(e => e.DefaultImage)
                .HasMaxLength(500)
                .HasDefaultValue("");
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.UpdatedBy).HasMaxLength(500);

            entity.HasOne(d => d.Brand).WithMany(p => p.Products).HasForeignKey(d => d.BrandId);

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasForeignKey(d => d.CategoryId);
        });

        modelBuilder.Entity<ProductShop>(entity =>
        {
            entity.HasKey(e => new { e.OptionId, e.AddressShopId });

            entity.HasIndex(e => e.AddressShopId, "IX_ProductShops_AddressShopId");

            entity.HasOne(d => d.AddressShop).WithMany(p => p.ProductShops).HasForeignKey(d => d.AddressShopId);

            entity.HasOne(d => d.Option).WithMany(p => p.ProductShops).HasForeignKey(d => d.OptionId);
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_Properties_ProductId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Value).HasMaxLength(255);

            entity.HasOne(d => d.Product).WithMany(p => p.Properties).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
