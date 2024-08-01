namespace AppIntegConexionCore.Models
{
    public partial class MenuRolView
    {
        public int IdMenuRol { get; set; }
        public int IdRol { get; set; }
        public int IdMenuPadre { get; set; }
        public int IdMenuHijo { get; set; }
        public string MenuPadre { get; set; }
        public string MenuHijo { get; set; }
        public string Rol { get; set; }
    }
}
