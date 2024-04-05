namespace AppIntegConexionCore.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Codigo { get; set; }
        public string Clave { get; set; }
        public int IdConexion { get; set; }
    }
}
