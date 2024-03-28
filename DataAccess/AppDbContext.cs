using Azure;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions opt) : base(opt)
        {

        }

        public virtual DbSet<Tarjeta> Tarjeta { get; set; }
        public virtual DbSet<Movimiento> Movimiento { get; set; }
        public virtual DbSet<CuentaBancaria> CuentaBancaria { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tarjeta>(entity =>
            {
                entity.ToTable("Tarjeta");

                entity.HasKey(d => d.IDTarjeta);
                entity.Property(e => e.IDTarjeta).HasColumnName("IDTarjeta");

                entity.Property(e => e.NroTarjeta)
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .IsRequired();

                entity.Property(e => e.Pin)
                        .HasMaxLength(4)
                        .IsUnicode(false)
                        .HasColumnName("Pin");
               
            });


            modelBuilder.Entity<CuentaBancaria>(entity =>
            {

                entity.HasOne(e => e.Tarjeta).WithOne(e => e.CuentaBancaria).HasForeignKey<CuentaBancaria>(e => e.IDTarjeta);

                entity.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                
                entity.HasOne(e => e.CuentaBancaria).WithMany(e => e.Movimientos).HasForeignKey(e => e.IDCuentaBancaria);
                entity.HasOne(e => e.TipoMovimiento).WithOne(e => e.Movimiento).HasForeignKey<Movimiento>(e => e.IDTipoMovimiento);

            });
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasOne(e => e.CuentaBancaria).WithOne(e => e.Usuario).HasForeignKey<Usuario>(e => e.IDCuentaBancaria);


            });

            modelBuilder.Entity<TipoMovimiento>(entity =>
            {
                


            });


        }


    }

}
