namespace Canus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Estetica
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Estetica()
        {
            Ventas = new HashSet<Venta>();
            Estilistas = new HashSet<Estilista>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Nombre Estetica")]
        public string Nombre { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Venta> Ventas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Estilista> Estilistas { get; set; }
    }
}
