using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IG.ENMS.Starlink.Models.DB;

public partial class IgcmsContext : DbContext
{
    private readonly string _connectionString;

    public IgcmsContext()
    {
    }

    public IgcmsContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IgcmsContext(DbContextOptions<IgcmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Config> Configs { get; set; }

    public virtual DbSet<TbCustomer> TbCustomers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Config>(entity =>
        {
            entity.ToTable("Config");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConfigName).HasMaxLength(50);
        });

        modelBuilder.Entity<TbCustomer>(entity =>
        {
            entity.ToTable("tbCustomer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("createdBy");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("datetime")
                .HasColumnName("dateDeleted");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("deletedBy");
            entity.Property(e => e.ExternalId)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("externalID");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.SegAid)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("segAId");
            entity.Property(e => e.ServiceNowId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("serviceNowId");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updatedBy");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Username, "IX_Users").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Internal)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
