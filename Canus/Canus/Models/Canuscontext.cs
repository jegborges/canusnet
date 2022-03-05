namespace Canus.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Canuscontext : DbContext
    {
        public Canuscontext()
            : base("name=Canuscontext")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Estetica> Esteticas { get; set; }
        public virtual DbSet<Estilista> Estilistas { get; set; }
        public virtual DbSet<Servicio> Servicios { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Estilistas)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.AspNetUserId);

            modelBuilder.Entity<Estetica>()
                .HasMany(e => e.Estilistas)
                .WithRequired(e => e.Estetica)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estetica>()
                .HasMany(e => e.Ventas)
                .WithRequired(e => e.Estetica)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estilista>()
                .HasMany(e => e.Ventas)
                .WithRequired(e => e.Estilista)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Servicio>()
                .Property(e => e.Precio)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Servicio>()
                .HasMany(e => e.Ventas)
                .WithRequired(e => e.Servicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Venta>()
                .Property(e => e.Precio)
                .HasPrecision(12, 2);
        }
    }
}
