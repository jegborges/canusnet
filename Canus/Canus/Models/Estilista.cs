namespace Canus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Estilista
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Estilista()
        {
            Ventas = new HashSet<Venta>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Nombre Estilista")]
        public string Nombre { get; set; }

        public int EsteticaId { get; set; }

        [ForeignKey("AspNetUser")]
        [StringLength(128)]
        public string AspNetUserId { get; set; }

        public virtual Estetica Estetica { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Venta> Ventas { get; set; }
    }
}
