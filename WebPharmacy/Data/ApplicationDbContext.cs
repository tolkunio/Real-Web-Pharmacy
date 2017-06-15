using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebPharmacy.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebPharmacy.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Firm>(entity =>
            {
                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Formulation>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Incoming>(entity =>
            {
                entity.Property(e => e.IncomedAt).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal");

                entity.HasOne(d => d.Medicament)
                    .WithMany(p => p.Incoming)
                    .HasForeignKey(d => d.MedicamentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Incoming_Medicament");
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Price).HasColumnType("decimal");

                entity.HasOne(d => d.Firm)
                    .WithMany(p => p.Medicament)
                    .HasForeignKey(d => d.FirmId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Medicament_ToFirm");

                entity.HasOne(d => d.Formulation)
                    .WithMany(p => p.Medicament)
                    .HasForeignKey(d => d.FormulationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Medicament_Formulation");

                entity.HasOne(d => d.MedicamentType)
                    .WithMany(p => p.Medicament)
                    .HasForeignKey(d => d.MedicamentTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Medicament_ToMedicamentType");
            });

            modelBuilder.Entity<MedicamentType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Outcoming>(entity =>
            {
                entity.Property(e => e.OutcomedAt).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal");

                entity.HasOne(d => d.Medicament)
                    .WithMany(p => p.Outcoming)
                    .HasForeignKey(d => d.MedicamentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Outcoming_Medicament");
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasOne(d => d.Medicament)
                    .WithMany(p => p.Storage)
                    .HasForeignKey(d => d.MedicamentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Storage_Medicament");
            });
        }
        public virtual DbSet<Firm> Firm { get; set; }
        public virtual DbSet<Formulation> Formulation { get; set; }
        public virtual DbSet<Incoming> Incoming { get; set; }
        public virtual DbSet<Medicament> Medicament { get; set; }
        public virtual DbSet<MedicamentType> MedicamentType { get; set; }
        public virtual DbSet<Outcoming> Outcoming { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
      // public DbSet<Cart> ShoppingCart { get; set; }
    }
}
