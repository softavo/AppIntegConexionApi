using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppIntegConexionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;
        public RolesController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [HttpGet("RolesConsultar")]
        public IEnumerable<Rol> RolesConsultar()
        {
            var roles = _rolesRepository.Consultar();
            return roles;
        }

        [HttpGet("RolesConsultarUsuario/{idUsuario}")]
        public IEnumerable<Rol> RolesConsultarUsuario(int idUsuario)
        {
            var roles = _rolesRepository.ConsultarRolesUsuario(idUsuario);
            return roles;
        }

        [HttpGet("RolesConsultarDisponibles/{idUsuarioRol}/{idUsuario}")]
        public IEnumerable<Rol> RolesConsultarDisponibles(int idUsuarioRol, int idUsuario)
        {
            var roles = _rolesRepository.ConsultarRolesDisponibles(idUsuarioRol,idUsuario);
            return roles;
        }

        [HttpGet("RolesConsultarPorId/{idRol}")]
        public Rol RolesConsultarPorId(int idRol)
        {
            var rol = _rolesRepository.ConsultarPorId(idRol);
            return rol;
        }

        [HttpPost("RolesCrear")]
        public IActionResult RolesCrear([FromBody] Rol rol)
        {
            _rolesRepository.Crear(rol);
            return Ok();
        }

        [HttpPut("RolesActualizar")]
        public IActionResult RolesActualizar([FromBody] Rol rol)
        {
            _rolesRepository.Editar(rol);
            return Ok();
        }

        [HttpDelete("RolesEliminar/{idRol}")]
        public IActionResult RolesEliminar(int idRol)
        {
            _rolesRepository.Eliminar(idRol);
            return Ok();
        }
    }
}
