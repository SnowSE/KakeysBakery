using System;
using System.Collections.Generic;

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

    public virtual DbSet<Addonflavor> Addonflavors { get; set; }

    public virtual DbSet<Addontype> Addontypes { get; set; }

    public virtual DbSet<Basegood> Basegoods { get; set; }

    public virtual DbSet<BasegoodSize> BasegoodSizes { get; set; }

    public virtual DbSet<Basegoodflavor> Basegoodflavors { get; set; }

    public virtual DbSet<Basegoodtype> Basegoodtypes { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerRole> CustomerRoles { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAddonBasegood> ProductAddonBasegoods { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<PurchaseProduct> PurchaseProducts { get; set; }

    public virtual DbSet<Referencephoto> Referencephotos { get; set; }

    public virtual DbSet<Userrole> Userroles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Addon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("addon_pkey");

            entity.ToTable("addon", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Addonflavorid).HasColumnName("addonflavorid");
            entity.Property(e => e.Addontypeid).HasColumnName("addontypeid");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.Suggestedprice)
                .HasColumnType("money")
                .HasColumnName("suggestedprice");

            entity.HasOne(d => d.Addonflavor).WithMany(p => p.Addons)
                .HasForeignKey(d => d.Addonflavorid)
                .HasConstraintName("addon_addonflavor_fk");

            entity.HasOne(d => d.Addontype).WithMany(p => p.Addons)
                .HasForeignKey(d => d.Addontypeid)
                .HasConstraintName("addon_addontype_fk");
        });

        modelBuilder.Entity<Addonflavor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("addonflavor_pkey");

            entity.ToTable("addonflavor", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Flavor)
                .HasMaxLength(50)
                .HasColumnName("flavor");
        });

        modelBuilder.Entity<Addontype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("addontype_pkey");

            entity.ToTable("addontype", "KakeysBakery");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Basetype)
                .HasMaxLength(50)
                .HasColumnName("basetype");
        });

        modelBuilder.Entity<Basegood>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("basegood_pkey");

            entity.ToTable("basegood", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Flavorid).HasColumnName("flavorid");
            entity.Property(e => e.Isavailable).HasColumnName("isavailable");
            entity.Property(e => e.Sizeid).HasColumnName("sizeid");
            entity.Property(e => e.Suggestedprice)
                .HasColumnType("money")
                .HasColumnName("suggestedprice");
            entity.Property(e => e.Typeid).HasColumnName("typeid");

            entity.HasOne(d => d.Flavor).WithMany(p => p.Basegoods)
                .HasForeignKey(d => d.Flavorid)
                .HasConstraintName("flavorid");

            entity.HasOne(d => d.Size).WithMany(p => p.Basegoods)
                .HasForeignKey(d => d.Sizeid)
                .HasConstraintName("basegood_goodsize_fkey");

            entity.HasOne(d => d.Type).WithMany(p => p.Basegoods)
                .HasForeignKey(d => d.Typeid)
                .HasConstraintName("basegoodname");
        });

        modelBuilder.Entity<BasegoodSize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("basegood_size_pkey");

            entity.ToTable("basegood_size", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Size)
                .HasMaxLength(50)
                .HasColumnName("size");
        });

        modelBuilder.Entity<Basegoodflavor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("flavor_pkey");

            entity.ToTable("basegoodflavor", "KakeysBakery");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"KakeysBakery\".flavor_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Flavorname)
                .HasMaxLength(50)
                .HasColumnName("flavorname");
        });

        modelBuilder.Entity<Basegoodtype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("base_pkey");

            entity.ToTable("basegoodtype", "KakeysBakery");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"KakeysBakery\".base_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Basegood)
                .HasMaxLength(50)
                .HasColumnName("basegood");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cart_pkey");

            entity.ToTable("cart", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

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

        modelBuilder.Entity<CustomerRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_role_pkey");

            entity.ToTable("customer_role", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.UserroleId).HasColumnName("userrole_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerRoles)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("customer_role_customer_id_fkey");

            entity.HasOne(d => d.Userrole).WithMany(p => p.CustomerRoles)
                .HasForeignKey(d => d.UserroleId)
                .HasConstraintName("customer_role_userrole_id_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pkey");

            entity.ToTable("product", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Ispublic).HasColumnName("ispublic");
            entity.Property(e => e.Productname)
                .HasMaxLength(50)
                .HasColumnName("productname");
        });

        modelBuilder.Entity<ProductAddonBasegood>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_addon_pkey");

            entity.ToTable("product_addon_basegood", "KakeysBakery");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"KakeysBakery\".product_addon_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Addonid).HasColumnName("addonid");
            entity.Property(e => e.Basegoodid).HasColumnName("basegoodid");
            entity.Property(e => e.Productid).HasColumnName("productid");

            entity.HasOne(d => d.Addon).WithMany(p => p.ProductAddonBasegoods)
                .HasForeignKey(d => d.Addonid)
                .HasConstraintName("product_addon_addonid_fkey");

            entity.HasOne(d => d.Basegood).WithMany(p => p.ProductAddonBasegoods)
                .HasForeignKey(d => d.Basegoodid)
                .HasConstraintName("basegood");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAddonBasegoods)
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

        modelBuilder.Entity<Userrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("userrole_pkey");

            entity.ToTable("userrole", "KakeysBakery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Userrole1)
                .HasMaxLength(20)
                .HasColumnName("userrole");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}