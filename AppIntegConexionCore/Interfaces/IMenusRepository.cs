using AppIntegConexionCore.Models;

namespace AppIntegConexionCore.Interfaces
{
    public interface IMenusRepository
    {
        List<MenuView> Consultar();
        List<Menu> ConsultarPadres();
        List<Menu> ConsultarHijos(int id);
        Menu ConsultarPorId(int? id);
        List<MenuView> ConsultarPorIdUsuario(int? id, bool completo);
        void Crear(Menu menu);
        void Editar(Menu menu);
        void Eliminar(int id);
    }
}
