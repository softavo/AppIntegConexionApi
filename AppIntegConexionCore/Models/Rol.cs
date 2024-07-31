namespace AppIntegConexionCore.Models
{
    public partial class Rol
    {
        public int IdRol { get; set; }
        public string Descripcion { get; set; }
        public bool SysAdmin { get; set; }
        public bool SysEmp { get; set; }
    }
}
