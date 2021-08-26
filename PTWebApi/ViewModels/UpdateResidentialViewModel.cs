namespace PTWebApi.ViewModels
{
    public class UpdateResidentialViewModel
    {
        public int IdResidencial { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; }
        public int IdEstado { get; set; }
    }
}
