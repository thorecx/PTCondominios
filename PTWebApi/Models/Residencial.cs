using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PTWebApi.Models
{
    public class Residencial
    {
        [Key]
        public int IdResidencial { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
        [MaxLength(100)]
        public string Direccion { get; set; }
        public double Precio { get; set; }
        [MaxLength(200)]
        public string Descripcion { get; set; }
        [ForeignKey("IdEstado")]
        public int IdEstado { get; set; }
        public Estado Estado { get; set; }

    }
}
