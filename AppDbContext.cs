using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MODELS;
using MODELS.Entities;

namespace DAL;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    } public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }
    // ✅ Add GymPackage1 table
    public DbSet<GymPackage1> GymPackage1 { get; set; }

   
    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<EmployeeData> EmployeeData { get; set; }
    public virtual DbSet<DesignationData> DesignationData { get; set; }

    public virtual DbSet<PatientData> PatientData { get; set; }

    public virtual DbSet<GymTimings1> GymTimings1 { get; set; }


    public virtual DbSet<Gyms1> Gyms1 { get; set; }

    public virtual DbSet<LabReportDetail> LabReportDetails { get; set; }

    public virtual DbSet<StudentData> StudentData { get; set; }

    public virtual DbSet<Registration> Registration { get; set; }

    public virtual DbSet<GymCloseDates> GymCloseDates { get; set; }
    public virtual DbSet<BusBookingData> BusBookingData { get; set; }

    public virtual DbSet<GymCloseReason> GymCloseReason { get; set; }

    //public virtual DbSet<GymImageMapping> GymImageMapping { get; set; }

    public DbSet<GymImageMapping> GymImageMapping { get; set; }


    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);
    //    // 🧩 Gym–ImageMapping relation
    //    modelBuilder.Entity<GymImageMapping>()
    //            .HasOne(g => g.Gym)
    //            .WithMany(g => g.GymImageMappings)
    //            .HasForeignKey(g => g.GymId)
    //            .OnDelete(DeleteBehavior.Cascade);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GymTimings1>()
            .HasOne(gt => gt.Gym)
            .WithMany(g => g.GymTimings)
            .HasForeignKey(gt => gt.GymId)
            .OnDelete(DeleteBehavior.SetNull); // match the SQL choice

        base.OnModelCreating(modelBuilder);
    }

}


