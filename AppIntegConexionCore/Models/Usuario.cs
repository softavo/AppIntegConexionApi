namespace AppIntegConexionCore.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Codigo { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int IdVendedor { get; set; }
        public string Cargo { get; set; }
    }
}
