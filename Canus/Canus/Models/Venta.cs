namespace Canus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Venta
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        public int EsteticaId { get; set; }

        public int EstilistaId { get; set; }

        public int ServicioId { get; set; }

        public decimal Precio { get; set; }

        public virtual Estetica Estetica { get; set; }

        public virtual Estilista Estilista { get; set; }

        public virtual Servicio Servicio { get; set; }
    }
}
