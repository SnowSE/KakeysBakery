using System;
using System.Collections.Generic;
using KakeysBakeryClassLib.Data;
using Microsoft.EntityFrameworkCore;
namespace KakeysBakery.Data;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Addon> Addons { get; set; }

    public virtual DbSet<Basegood> Basegoods { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAddon> ProductAddons { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<PurchaseProduct> PurchaseProducts { get; set; }

    public virtual DbSet<Referencephoto> Referencephotos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Addon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("addon_pkey");

            entity.ToTable("addon", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Addontypename)
                .HasMaxLength(60)
                .HasColumnName("addontypename");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.Flavor)
                .HasMaxLength(70)
                .HasColumnName("flavor");
            entity.Property(e => e.Suggestedprice)
                .HasColumnType("money")
                .HasColumnName("suggestedprice");
        });

        modelBuilder.Entity<Basegood>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("basegood_pkey");

            entity.ToTable("basegood", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Basegoodname)
                .HasMaxLength(50)
                .HasColumnName("basegoodname");
            entity.Property(e => e.Flavor)
                .HasMaxLength(50)
                .HasColumnName("flavor");
            entity.Property(e => e.Suggestedprice)
                .HasColumnType("money")
                .HasColumnName("suggestedprice");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cart_pkey");

            entity.ToTable("cart", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Productid).HasColumnName("productid");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("cart_customerid_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("cart_productid_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_pkey");

            entity.ToTable("customer", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Forename)
                .HasMaxLength(50)
                .HasColumnName("forename");
            entity.Property(e => e.Issubscribed).HasColumnName("issubscribed");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.Preferredcontact)
                .HasMaxLength(30)
                .HasColumnName("preferredcontact");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pkey");

            entity.ToTable("product", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Basegoodid).HasColumnName("basegoodid");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Ispublic).HasColumnName("ispublic");
            entity.Property(e => e.Productname)
                .HasMaxLength(50)
                .HasColumnName("productname");

            entity.HasOne(d => d.Basegood).WithMany(p => p.Products)
                .HasForeignKey(d => d.Basegoodid)
                .HasConstraintName("product_basegoodid_fkey");
        });

        modelBuilder.Entity<ProductAddon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_addon_pkey");

            entity.ToTable("product_addon", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Addonid).HasColumnName("addonid");
            entity.Property(e => e.Productid).HasColumnName("productid");

            entity.HasOne(d => d.Addon).WithMany(p => p.ProductAddons)
                .HasForeignKey(d => d.Addonid)
                .HasConstraintName("product_addon_addonid_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAddons)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("product_addon_productid_fkey");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("purchase_pkey");

            entity.ToTable("purchase", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Actualprice)
                .HasColumnType("money")
                .HasColumnName("actualprice");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Fulfillmentdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fulfillmentdate");
            entity.Property(e => e.Isfulfilled).HasColumnName("isfulfilled");
            entity.Property(e => e.Orderdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("orderdate");
            entity.Property(e => e.Specifications)
                .HasMaxLength(300)
                .HasColumnName("specifications");

            entity.HasOne(d => d.Customer).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("purchase_customerid_fkey");
        });

        modelBuilder.Entity<PurchaseProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("purchase_product_pkey");

            entity.ToTable("purchase_product", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Purchaseid).HasColumnName("purchaseid");

            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseProducts)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("purchase_product_productid_fkey");

            entity.HasOne(d => d.Purchase).WithMany(p => p.PurchaseProducts)
                .HasForeignKey(d => d.Purchaseid)
                .HasConstraintName("purchase_product_purchaseid_fkey");
        });

        modelBuilder.Entity<Referencephoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("referencephoto_pkey");

            entity.ToTable("referencephoto", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.Purchaseid).HasColumnName("purchaseid");

            entity.HasOne(d => d.Purchase).WithMany(p => p.Referencephotos)
                .HasForeignKey(d => d.Purchaseid)
                .HasConstraintName("referencephoto_purchaseid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
