using AppIntegConexionCore.Models;

namespace AppIntegConexionCore.Interfaces
{
    public interface IMenusRolesRepository
    {
        List<MenuRol> Consultar();
        MenuRol ConsultarPorId(int? id);
        List<MenuRol> ConsultarMenusRol(int idRol);
        Task Crear(MenuRol menusRol);
        Task<bool> Editar(MenuRol menusRol);
        Task<bool> Eliminar(int id);
    }
}
