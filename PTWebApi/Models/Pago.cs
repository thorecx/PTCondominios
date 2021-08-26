using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PTWebApi.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }
        public double Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        [ForeignKey("IdCliente")]
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        [ForeignKey("IdEstado")]
        public int IdEstado { get; set; }
        public Estado Estado { get; set; }
    }
}
