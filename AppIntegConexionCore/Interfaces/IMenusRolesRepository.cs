using AppIntegConexionCore.Models;

namespace AppIntegConexionCore.Interfaces
{
    public interface IMenusRolesRepository
    {
        List<MenuRol> Consultar();
        MenuRol ConsultarPorId(int? id);
        List<MenuRol> ConsultarMenusRol(int idRol);
        void Crear(MenuRol menusRol);
        void Editar(MenuRol menusRol);
        void Eliminar(int id);
    }
}
