using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTWebApi.Models
{
    public class Cuota
    {
        [Key]
        public int IdCuota { get; set; }
        public double Monto { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public double Mora { get; set; }
        public double MontoTotal { get; set; }
        [ForeignKey("IdPago")]
        public int IdPago { get; set; }
        public Pago Pago { get; set; }
        [ForeignKey("IdEstado")]
        public int IdEstado { get; set; }
        public Estado Estado { get; set; }

    }
}
