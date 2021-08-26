namespace PTWebApi.ViewModels
{
    public class UpdateCustomerViewModel
    {
        public int IdCliente { get; set; }
        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
