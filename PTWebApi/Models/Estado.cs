using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PTWebApi.Models
{
    public class Estado
    {
        [Key]
        public int IdEstado { get; set; }
        [MaxLength(50)]
        public string Desc { get; set; }
        public ICollection<Cliente> Clientes { get; set; }
        public ICollection<Residencial> Residenciales { get; set; }
        public ICollection<Pago> Pagos { get; set; }
        public ICollection<Cuota> Cuotas { get; set; }
    }
}
