using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace hospital
{
    public partial class dental_hospitalContext : DbContext
    {
        public dental_hospitalContext()
        {
        }

        public dental_hospitalContext(DbContextOptions<dental_hospitalContext> options)
            : base(options)
        {
        }
        public virtual DbSet<DentalHo> DentalHos { get; set; }
        public virtual DbSet<Diagnosis> Diagnoses { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=dental_hospital;Username=postgres;Password=51543001");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DentalHo>(entity =>
            {
                entity.HasKey(e => new { e.Visit, e.Doctor, e.Patient })
                    .HasName("dentalHos_pkey");

                entity.ToTable("dental_hos");

                entity.Property(e => e.Visit).HasColumnName("visit");

                entity.Property(e => e.Doctor).HasColumnName("doctor");

                entity.Property(e => e.Patient).HasColumnName("patient");

                entity.HasOne(d => d.DoctorNavigation)
                    .WithMany(p => p.DentalHos)
                    .HasForeignKey(d => d.Doctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("doctor_key");

                entity.HasOne(d => d.PatientNavigation)
                    .WithMany(p => p.DentalHos)
                    .HasForeignKey(d => d.Patient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("patient_key");

                entity.HasOne(d => d.VisitNavigation)
                    .WithMany(p => p.DentalHos)
                    .HasForeignKey(d => d.Visit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("visit_key");
            });

            modelBuilder.Entity<Diagnosis>(entity =>
            {
                entity.ToTable("diagnoses");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.Diagnosis1)
                    .HasColumnType("character varying")
                    .HasColumnName("diagnosis");

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("doctors");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasColumnType("character varying")
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("patients");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnType("character varying")
                    .HasColumnName("address");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasColumnType("character varying")
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("plan");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Diagnosis).HasColumnName("diagnosis");

                entity.Property(e => e.Treatment)
                    .HasColumnType("character varying")
                    .HasColumnName("treatment");

                entity.HasOne(d => d.DiagnosisNavigation)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.Diagnosis)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("diagnosis_key");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.ToTable("visits");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Plan).HasColumnName("plan");

                entity.Property(e => e.Stamp).HasColumnName("stamp");

                entity.HasOne(d => d.PlanNavigation)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.Plan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("plan_key");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
