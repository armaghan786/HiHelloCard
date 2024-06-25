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

    public virtual DbSet<Aspnetrole> Aspnetroles { get; set; }

    public virtual DbSet<Aspnetroleclaim> Aspnetroleclaims { get; set; }

    public virtual DbSet<Aspnetuser> Aspnetusers { get; set; }

    public virtual DbSet<Aspnetuserclaim> Aspnetuserclaims { get; set; }

    public virtual DbSet<Aspnetuserlogin> Aspnetuserlogins { get; set; }

    public virtual DbSet<Aspnetusertoken> Aspnetusertokens { get; set; }

    public virtual DbSet<Carddesign> Carddesigns { get; set; }

    public virtual DbSet<Cardfield> Cardfields { get; set; }

    public virtual DbSet<Cardfieldcategory> Cardfieldcategories { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Usercard> Usercards { get; set; }

    public virtual DbSet<Usercardbadge> Usercardbadges { get; set; }

    public virtual DbSet<Usercardfield> Usercardfields { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=hihello;uid=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Aspnetrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroles");

            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<Aspnetroleclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroleclaims");

            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.HasOne(d => d.Role).WithMany(p => p.Aspnetroleclaims)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
        });

        modelBuilder.Entity<Aspnetuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetusers");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.AccessFailedCount).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LockoutEnd).HasMaxLength(6);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Aspnetuserrole",
                    r => r.HasOne<Aspnetrole>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId"),
                    l => l.HasOne<Aspnetuser>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("aspnetuserroles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<Aspnetuserclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetuserclaims");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserclaims)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetuserlogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("aspnetuserlogins");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserlogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetusertoken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("aspnetusertokens");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetusertokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Carddesign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("carddesigns");

            entity.Property(e => e.Id).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Cardfield>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cardfields");

            entity.HasIndex(e => e.CategoryId, "IX_Cardfields_CategoryId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CategoryId).HasColumnType("int(11)");

            entity.HasOne(d => d.Category).WithMany(p => p.Cardfields)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Cardfields_Cardfieldcategories_CategoryId");
        });

        modelBuilder.Entity<Cardfieldcategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cardfieldcategories");

            entity.Property(e => e.Id).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CreatedDateTime).HasMaxLength(6);
            entity.Property(e => e.UpdatedDateTime).HasMaxLength(6);
        });

        modelBuilder.Entity<Usercard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usercards");

            entity.HasIndex(e => e.DesignId, "IX_Usercards_DesignId");

            entity.HasIndex(e => e.UserId, "IX_Usercards_UserId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CreatedDateTime).HasMaxLength(6);
            entity.Property(e => e.DesignId).HasColumnType("int(11)");
            entity.Property(e => e.UpdatedDateTime).HasMaxLength(6);
            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.Design).WithMany(p => p.Usercards)
                .HasForeignKey(d => d.DesignId)
                .HasConstraintName("FK_Usercards_Carddesigns_DesignId");

            entity.HasOne(d => d.User).WithMany(p => p.Usercards)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Usercards_Users_UserId");
        });

        modelBuilder.Entity<Usercardbadge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usercardbadges");

            entity.HasIndex(e => e.CardId, "IX_Usercardbadges_CardId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CardId).HasColumnType("int(11)");

            entity.HasOne(d => d.Card).WithMany(p => p.Usercardbadges)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("FK_Usercardbadges_Usercards_CardId");
        });

        modelBuilder.Entity<Usercardfield>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usercardfields");

            entity.HasIndex(e => e.CardFieldId, "IX_Usercardfields_CardFieldId");

            entity.HasIndex(e => e.CardId, "IX_Usercardfields_CardId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CardFieldId).HasColumnType("int(11)");
            entity.Property(e => e.CardId).HasColumnType("int(11)");

            entity.HasOne(d => d.CardField).WithMany(p => p.Usercardfields)
                .HasForeignKey(d => d.CardFieldId)
                .HasConstraintName("FK_Usercardfields_Cardfields_CardFieldId");

            entity.HasOne(d => d.Card).WithMany(p => p.Usercardfields)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("FK_Usercardfields_Usercards_CardId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
