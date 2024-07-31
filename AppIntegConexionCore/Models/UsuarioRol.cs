namespace AppIntegConexionCore.Models
{
    public partial class UsuarioRol
    {
        public int IdUsuarioRol { get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string Usuario { get; set; }
        public string Rol { get; set; }
        public string Nombre { get; set; }
        public bool SysAdmin { get; set; }
        public bool SysEmp { get; set; }
    }
}
