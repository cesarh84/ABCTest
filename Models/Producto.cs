namespace Productos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Productos")]
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            Existencias = new HashSet<Existencias>();
        }

        [Key]
        public int Id_Producto { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public decimal? PrecioDetalle { get; set; }

        public decimal? PrecioMayoreo { get; set; }

        [StringLength(50)]
        public string Posicion { get; set; }

        public int Id_Estatus { get; set; }

        public virtual CT_Estatus CT_Estatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Existencias> Existencias { get; set; }
    }
}
