using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AutoShopAppLibrary.Data;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Workorder> Workorders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("pg_catalog", "azure")
            .HasPostgresExtension("pg_catalog", "pgaadauth");

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("car_pkey");

            entity.ToTable("car");

            entity.HasIndex(e => e.LicensePlate, "license_plate_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustId).HasColumnName("cust_id");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(8)
                .HasColumnName("license_plate");
            entity.Property(e => e.Make)
                .HasMaxLength(16)
                .HasColumnName("make");
            entity.Property(e => e.Model)
                .HasMaxLength(32)
                .HasColumnName("model");
            entity.Property(e => e.Year)
                .HasMaxLength(4)
                .HasColumnName("year");

            entity.HasOne(d => d.Cust).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CustId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("car_cust_id_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.HasIndex(e => e.Email, "email_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(64)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Workorder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workorder_pkey");

            entity.ToTable("workorder");

            entity.HasIndex(e => new { e.CustId, e.CarId }, "customer_car_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CarId).HasColumnName("car_id");
            entity.Property(e => e.Comments)
                .HasMaxLength(512)
                .HasColumnName("comments");
            entity.Property(e => e.Concerns)
                .HasMaxLength(1024)
                .HasColumnName("concerns");
            entity.Property(e => e.CustId).HasColumnName("cust_id");
            entity.Property(e => e.Datesubmitted)
                .HasMaxLength(20)
                .HasColumnName("datesubmitted");
            entity.Property(e => e.Odometer).HasColumnName("odometer");

            entity.HasOne(d => d.Car).WithMany(p => p.Workorders)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("workorder_car_id_fkey");

            entity.HasOne(d => d.Cust).WithMany(p => p.Workorders)
                .HasForeignKey(d => d.CustId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("workorder_cust_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}