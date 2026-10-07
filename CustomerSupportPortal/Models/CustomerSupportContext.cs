using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportPortal.Models;

public partial class CustomerSupportContext : DbContext
{
    private readonly IConfiguration _configuration;
    public CustomerSupportContext()
    {
    }

    public CustomerSupportContext(DbContextOptions<CustomerSupportContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestComment> RequestComments { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Priority>(entity =>
        {
            entity.ToTable("Priority");

            entity.Property(e => e.PriorityId).ValueGeneratedNever();
            entity.Property(e => e.PriorityType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.ToTable("Request");

            entity.Property(e => e.RequestId).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            #region ForReference
            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.RequestAssignedToNavigations)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK_Request_UserId");

            entity.HasOne(d => d.Customer).WithMany(p => p.RequestCustomers)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Request_CustomerUserId");
            #endregion
            //entity.HasOne(d => d.AssignedToNavigation).WithMany()
            //    .HasForeignKey(d => d.AssignedTo)
            //    .HasConstraintName("FK_Request_UserId");

            //entity.HasOne(d => d.Customer).WithMany()
            //    .HasForeignKey(d => d.CustomerId)
            //    .HasConstraintName("FK_Request_CustomerUserId");

            entity.HasOne(d => d.Priority).WithMany(p => p.Requests)
                .HasForeignKey(d => d.PriorityId)
                .HasConstraintName("FK_Request_Priority");

            entity.HasOne(d => d.Status).WithMany(p => p.Requests)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_Status");
        });

        modelBuilder.Entity<RequestComment>(entity =>
        {
            entity.HasKey(e => e.CommentId);

            entity.ToTable("RequestComment");

            entity.Property(e => e.CommentId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestComments)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK_RequestComment_Request");

            entity.HasOne(d => d.User).WithMany(p => p.RequestComments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RequestComment_UserProfile");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.StatusId).ValueGeneratedNever();
            entity.Property(e => e.StatusType).HasMaxLength(100);
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("UserProfile");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProfile_UserRoles");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
