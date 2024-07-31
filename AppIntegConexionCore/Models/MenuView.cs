namespace AppIntegConexionCore.Models
{
    public partial class MenuView
    {
        public int IdMenu { get; set; }
        public string Descripcion { get; set; }
        public int IdMenuPadre { get; set; }
        public string Url { get; set; }
        public int Orden { get; set; }
        public bool VentanaNueva { get; set; }
        public string MenuPadre { get; set; }
        public virtual ICollection<MenuView> InverseIdMenuPadreNavigation { get; set; }
    }
}
