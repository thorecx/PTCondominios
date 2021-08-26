using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTWebApi.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [MaxLength(50)]
        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        [MaxLength(50)]
        public string Apellido { get; set; }
        [MaxLength(13)]
        public string Cedula { get; set; }
        [MaxLength(100)]
        public string Direccion { get; set; }
        [MaxLength(12)]
        public string Telefono { get; set; }

        [ForeignKey("IdEstado")]
        public Estado Estado { get; set; }

        public ICollection<Pago> Pagos { get; set; }
    }
}
