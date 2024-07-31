namespace AppIntegConexionCore.Models
{
    public partial class Rol
    {
        public int IdRol { get; set; }
        public string Descripcion { get; set; }
        public bool SysAdmin { get; set; }
        public bool SysEmp { get; set; }
        //public virtual ICollection<Usuario> Usuarios { get; set; }
        //public virtual ICollection<MenuRol> MenusRoles { get; set; }
        //public virtual ICollection<UsuarioRol> UsuariosRoles { get; set; }
    }
}
