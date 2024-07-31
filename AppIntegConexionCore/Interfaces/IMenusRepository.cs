using AppIntegConexionCore.Models;

namespace AppIntegConexionCore.Interfaces
{
    public interface IMenusRepository
    {
        List<Menu> Consultar();
        List<Menu> ConsultarPadres();
        List<Menu> ConsultarHijos(int id);
        Menu ConsultarPorId(int? id);
        List<Menu> ConsultarPorIdUsuario(int? id, bool completo);
        Task Crear(Menu menu);
        Task<bool> Editar(Menu menu);
        Task<bool> Eliminar(int id);
    }
}
