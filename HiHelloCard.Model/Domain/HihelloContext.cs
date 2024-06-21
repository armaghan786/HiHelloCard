using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace HiHelloCard.Model.Domain;

public partial class HihelloContext : DbContext
{
    public HihelloContext()
    {
    }

    public HihelloContext(DbContextOptions<HihelloContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carddesign> Carddesigns { get; set; }

    public virtual DbSet<Cardfield> Cardfields { get; set; }

    public virtual DbSet<Cardfieldcategory> Cardfieldcategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Usercard> Usercards { get; set; }

    public virtual DbSet<Usercardbadge> Usercardbadges { get; set; }

    public virtual DbSet<Usercardfield> Usercardfields { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=hihello;uid=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Carddesign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("carddesign");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<Cardfield>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cardfield");

            entity.HasIndex(e => e.CategoryId, "CategoryId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasMaxLength(250);

            entity.HasOne(d => d.Category).WithMany(p => p.Cardfields)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("cardfield_ibfk_1");
        });

        modelBuilder.Entity<Cardfieldcategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cardfieldcategory");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(250);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.CreatedDateTime).HasMaxLength(6);
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Guid)
                .HasMaxLength(250)
                .HasColumnName("GUID");
            entity.Property(e => e.UpdatedDateTime).HasMaxLength(6);
        });

        modelBuilder.Entity<Usercard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usercard");

            entity.HasIndex(e => e.DesignId, "DesignId");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Accreditations).HasMaxLength(250);
            entity.Property(e => e.AffiliateTitle).HasMaxLength(250);
            entity.Property(e => e.Color).HasMaxLength(250);
            entity.Property(e => e.Company).HasMaxLength(250);
            entity.Property(e => e.CreatedDateTime).HasMaxLength(6);
            entity.Property(e => e.Department).HasMaxLength(250);
            entity.Property(e => e.DesignId).HasColumnType("int(11)");
            entity.Property(e => e.FirstName).HasMaxLength(250);
            entity.Property(e => e.Guid).HasMaxLength(250);
            entity.Property(e => e.Headline).HasMaxLength(250);
            entity.Property(e => e.LastName).HasMaxLength(250);
            entity.Property(e => e.MaidenName).HasMaxLength(250);
            entity.Property(e => e.MiddleName).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.PreferredName).HasMaxLength(250);
            entity.Property(e => e.Prefix).HasMaxLength(250);
            entity.Property(e => e.Pronouns).HasMaxLength(250);
            entity.Property(e => e.Suffix).HasMaxLength(250);
            entity.Property(e => e.UpdatedDateTime).HasMaxLength(6);
            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.Design).WithMany(p => p.Usercards)
                .HasForeignKey(d => d.DesignId)
                .HasConstraintName("usercard_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Usercards)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("usercard_ibfk_1");
        });

        modelBuilder.Entity<Usercardbadge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usercardbadge");

            entity.HasIndex(e => e.CardId, "CardId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.CardId).HasColumnType("int(11)");

            entity.HasOne(d => d.Card).WithMany(p => p.Usercardbadges)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("usercardbadge_ibfk_1");
        });

        modelBuilder.Entity<Usercardfield>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usercardfield");

            entity.HasIndex(e => e.CardId, "CardId");

            entity.HasIndex(e => e.CardFieldId, "usercardfield_ibfk_2");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.CardFieldId).HasColumnType("int(11)");
            entity.Property(e => e.CardId).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Link).HasMaxLength(250);

            entity.HasOne(d => d.CardField).WithMany(p => p.Usercardfields)
                .HasForeignKey(d => d.CardFieldId)
                .HasConstraintName("usercardfield_ibfk_2");

            entity.HasOne(d => d.Card).WithMany(p => p.Usercardfields)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("usercardfield_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
