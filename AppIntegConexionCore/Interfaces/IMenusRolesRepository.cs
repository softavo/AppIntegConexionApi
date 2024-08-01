using AppIntegConexionCore.Models;

namespace AppIntegConexionCore.Interfaces
{
    public interface IMenusRolesRepository
    {
        List<MenuRolView> Consultar();
        MenuRol ConsultarPorId(int? id);
        MenuRolView ConsultarViewPorId(int id);
        List<MenuRolView> ConsultarMenusRol(int idRol);
        void Crear(MenuRol menusRol);
        void Editar(MenuRol menusRol);
        void Eliminar(int id);
    }
}
