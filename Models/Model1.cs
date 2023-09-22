using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Productos.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=CadenaConexion")
        {
        }

        public virtual DbSet<CT_Estatus> CT_Estatus { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Existencias> Existencias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CT_Estatus>()
                .HasMany(e => e.Productos)
                .WithRequired(e => e.CT_Estatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.PrecioDetalle)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Producto>()
                .Property(e => e.PrecioMayoreo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.Existencias)
                .WithRequired(e => e.Productos)
                .WillCascadeOnDelete(false);
        }
    }
}
