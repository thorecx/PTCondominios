using System;

namespace PTWebApi.ViewModels
{
    public class UpdatePaymentViewModel
    {
        public int IdPago { get; set; }
        public double Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int IdCliente { get; set; }
        public int IdEstado { get; set; }
    }
}
