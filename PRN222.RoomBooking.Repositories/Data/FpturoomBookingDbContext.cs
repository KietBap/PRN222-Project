using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PRN222.RoomBooking.Repositories.Data;

public partial class FpturoomBookingDbContext : DbContext
{
    public FpturoomBookingDbContext()
    {
    }

    public FpturoomBookingDbContext(DbContextOptions<FpturoomBookingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Campus> Campuses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomSlot> RoomSlots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-V6AACL1\\SQLEXPRESS;uid=sa;pwd=12345;database=FPTURoomBookingDB;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AED6034E9F4");

            entity.Property(e => e.BookingStatus).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Purpose).HasMaxLength(500);
            entity.Property(e => e.UserCode).HasMaxLength(50);

            entity.HasOne(d => d.UserCodeNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__UserCo__33D4B598");

            entity.HasMany(d => d.RoomSlots).WithMany(p => p.Bookings)
                .UsingEntity<Dictionary<string, object>>(
                    "BookingRoomSlot",
                    r => r.HasOne<RoomSlot>().WithMany()
                        .HasForeignKey("RoomSlotId")
                        .HasConstraintName("FK__BookingRo__RoomS__37A5467C"),
                    l => l.HasOne<Booking>().WithMany()
                        .HasForeignKey("BookingId")
                        .HasConstraintName("FK__BookingRo__Booki__36B12243"),
                    j =>
                    {
                        j.HasKey("BookingId", "RoomSlotId").HasName("PK__BookingR__0DC8C8CF786614D9");
                        j.ToTable("BookingRoomSlots");
                    });
        });

        modelBuilder.Entity<Campus>(entity =>
        {
            entity.HasKey(e => e.CampusId).HasName("PK__Campus__FD598DD6A128D4FD");

            entity.ToTable("Campus");

            entity.Property(e => e.CampusName).HasMaxLength(255);
            entity.Property(e => e.Location).HasMaxLength(500);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BEDC8E53C50");

            entity.Property(e => e.DepartmentName).HasMaxLength(255);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Rooms__32863939D1100A9D");

            entity.Property(e => e.RoomName).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Campus).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.CampusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rooms__CampusId__2D27B809");
        });

        modelBuilder.Entity<RoomSlot>(entity =>
        {
            entity.HasKey(e => e.RoomSlotId).HasName("PK__RoomSlot__E5DD22263D191E73");

            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Room).WithMany(p => p.RoomSlots)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoomSlots__RoomI__300424B4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserCode).HasName("PK__Users__1DF52D0D17BF20C0");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534AAD0CE36").IsUnique();

            entity.Property(e => e.UserCode).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);

            entity.HasOne(d => d.Campus).WithMany(p => p.Users)
                .HasForeignKey(d => d.CampusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__CampusId__29572725");

            entity.HasOne(d => d.Department).WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Departmen__2A4B4B5E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
