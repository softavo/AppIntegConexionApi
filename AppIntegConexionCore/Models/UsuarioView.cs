namespace AppIntegConexionCore.Models
{
    public partial class UsuarioView
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Clave { get; set; }
        public string Cargo { get; set; }
        public int IdConexion { get; set; }
        public bool SysAdmin { get; set; }
        public bool SysEmp { get; set; }
        public int IdVendedor { get; set; }
        public virtual ICollection<UsuarioRolView> UsuariosRoles { get; set; }
    }
}
