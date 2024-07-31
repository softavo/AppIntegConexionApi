using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppIntegConexionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenusRolesController : ControllerBase
    {
        private readonly IMenusRolesRepository _menusRolesRepository;
        public MenusRolesController(IMenusRolesRepository menusRolesRepository)
        {
            _menusRolesRepository = menusRolesRepository;
        }

        [HttpGet("MenusRolesConsultar")]
        public IEnumerable<MenuRol> MenusRolesConsultar()
        {
            var menuRoles = _menusRolesRepository.Consultar();
            return menuRoles;
        }

        [HttpGet("MenusRolesConsultarPorId/{idMenuRol}")]
        public MenuRol MenusRolesConsultarPorId(int idMenuRol)
        {
            var menuRol = _menusRolesRepository.ConsultarPorId(idMenuRol);
            return menuRol;
        }

        [HttpGet("MenusRolesConsultarRol/{idRol}")]
        public IEnumerable<MenuRol> MenusRolesConsultarRol(int idRol)
        {
            var menuRoles = _menusRolesRepository.ConsultarMenusRol(idRol);
            return menuRoles;
        }

        [HttpPost("MenusRolesCrear")]
        public IActionResult MenusRolesCrear([FromBody] MenuRol menusRol)
        {
            _menusRolesRepository.Crear(menusRol);
            return Ok();
        }

        [HttpPut("MenusRolesActualizar")]
        public IActionResult MenusRolesActualizar([FromBody] MenuRol menusRol)
        {
            _menusRolesRepository.Editar(menusRol);
            return Ok();
        }

        [HttpDelete("MenusRolesEliminar/{idMenuRol}")]
        public IActionResult MenusRolesEliminar(int idMenuRol)
        {
            _menusRolesRepository.Eliminar(idMenuRol);
            return Ok();
        }
    }
}
