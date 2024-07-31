using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppIntegConexionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenusController : ControllerBase
    {
        private readonly IMenusRepository _menusRepository;

        public MenusController(IMenusRepository menusRepository)
        {
            _menusRepository = menusRepository;
        }

        [HttpGet("MenusUsuario/{idUsuario}/{completo}")]
        public IEnumerable<MenuView> MenusUsuario(int idUsuario, bool completo)
        {
            var menus = _menusRepository.ConsultarPorIdUsuario(idUsuario, completo);
            return menus;
        }

        [HttpGet("MenusConsultar")]
        public IEnumerable<MenuView> MenusConsultar()
        {
            var menus = _menusRepository.Consultar();
            return menus;
        }

        [HttpGet("MenusConsultarPadres")]
        public IEnumerable<Menu> MenusConsultarPadres()
        {
            var menus = _menusRepository.ConsultarPadres();
            return menus;
        }

        [HttpGet("MenusConsultarHijos/{idPadre}")]
        public IEnumerable<Menu> MenusConsultarHijos(int idPadre)
        {
            var menus = _menusRepository.ConsultarHijos(idPadre);
            return menus;
        }

        [HttpGet("MenusConsultarPorId/{idMenu}")]
        public Menu MenusConsultarPorId(int idMenu)
        {
            var menu = _menusRepository.ConsultarPorId(idMenu);
            return menu;
        }

        [HttpPost("MenusCrear")]
        public IActionResult MenusCrear([FromBody] Menu menu)
        {
            _menusRepository.Crear(menu);
            return Ok();
        }

        [HttpPut("MenusActualizar")]
        public IActionResult MenusActualizar([FromBody] Menu menu)
        {
            _menusRepository.Editar(menu);
            return Ok();
        }

        [HttpDelete("MenusEliminar/{idMenu}")]
        public IActionResult MenusEliminar(int idMenu)
        {
            _menusRepository.Eliminar(idMenu);
            return Ok();
        }
    }
}
