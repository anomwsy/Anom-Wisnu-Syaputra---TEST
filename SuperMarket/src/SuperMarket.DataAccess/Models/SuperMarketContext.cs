using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperMarket.DataAccess.Models;

public partial class SuperMarketContext : DbContext
{
    public SuperMarketContext()
    {
    }

    public SuperMarketContext(DbContextOptions<SuperMarketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<WareHouse> WareHouses { get; set; }
    public virtual DbSet<ProductResultModel> ProductResults { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-3UPBPKFI;Initial Catalog=SuperMarket;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductResultModel>().HasNoKey().ToView(null);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07AF706F57");

            entity.ToTable("Product", tb => tb.HasTrigger("ProductInputTrigger"));

            entity.HasIndex(e => e.Id, "Index_Product_Id");

            entity.Property(e => e.DeletedDate).HasColumnType("date");
            entity.Property(e => e.ExpiredDate).HasColumnType("date");
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.WareHouse).WithMany(p => p.Products)
                .HasForeignKey(d => d.WareHouseId)
                .HasConstraintName("FK__Product__WareHou__267ABA7A");
        });

        modelBuilder.Entity<WareHouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WareHous__3214EC07875816C3");

            entity.ToTable("WareHouse");

            entity.HasIndex(e => e.Id, "Index_WareHouse_Id");

            entity.Property(e => e.DeletedDate).HasColumnType("date");
            entity.Property(e => e.Name).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
