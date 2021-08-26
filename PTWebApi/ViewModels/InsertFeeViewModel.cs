using System;

namespace PTWebApi.ViewModels
{
    public class InsertFeeViewModel
    {
        public double Monto { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public double Mora { get; set; }
        public double MontoTotal { get; set; }
        public int IdPago { get; set; }
        public int IdEstado { get; set; }
    }
}
