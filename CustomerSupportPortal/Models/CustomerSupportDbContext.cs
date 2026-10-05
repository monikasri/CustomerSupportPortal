using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportPortal.Models;

public partial class CustomerSupportDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public CustomerSupportDbContext()
    {
    }

    public CustomerSupportDbContext(DbContextOptions<CustomerSupportDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<RequestPriority> RequestPriorities { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SupportRequest> SupportRequests { get; set; }

    public virtual DbSet<UsersProfile> UsersProfiles { get; set; }

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
        modelBuilder.Entity<RequestPriority>(entity =>
        {
            entity.ToTable("RequestPriority");

            entity.Property(e => e.RequestPriorityId).ValueGeneratedNever();
            entity.Property(e => e.RequestPriority1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RequestPriority");
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.ToTable("RequestStatus");

            entity.Property(e => e.RequestStatusId).ValueGeneratedNever();
            entity.Property(e => e.RequestStatus1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RequestStatus");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SupportRequest>(entity =>
        {
            entity.ToTable("SupportRequest");

            entity.Property(e => e.SupportRequestId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.RequestDescription).IsUnicode(false);
            entity.Property(e => e.RequestSubject)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.RequestAssignedToNavigation).WithMany()
                .HasForeignKey(d => d.RequestAssignedTo)
                .HasConstraintName("FK_SupportRequest_UsersProfile");

            entity.HasOne(d => d.RequestCreatedByNavigation).WithMany()
                .HasForeignKey(d => d.RequestCreatedBy)
                .HasConstraintName("FK_SupportRequest_UsersProfile1");

            entity.HasOne(d => d.RequestPriority).WithMany(p => p.SupportRequests)
                .HasForeignKey(d => d.RequestPriorityId)
                .HasConstraintName("FK_SupportRequest_RequestPriority");

            entity.HasOne(d => d.RequestStatus).WithMany(p => p.SupportRequests)
                .HasForeignKey(d => d.RequestStatusId)
                .HasConstraintName("FK_SupportRequest_RequestStatus");
        });

        modelBuilder.Entity<UsersProfile>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("UsersProfile");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
