using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using AppIntegConexionCore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppIntegConexionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosRolesController : ControllerBase
    {
        private readonly IUsuariosRolesRepository _usuariosRolesRepository;
        public UsuariosRolesController(IUsuariosRolesRepository usuariosRolesRepository)
        {
            _usuariosRolesRepository = usuariosRolesRepository;
        }

        [HttpGet("UsuariosRolesConsultarPorIdUsuario/{idUsuario}")]
        public IEnumerable<UsuarioRolView> UsuariosRolesConsultarPorIdUsuario(int idUsuario)
        {
            var usuariosRoles = _usuariosRolesRepository.ConsultarPorIdUsuario(idUsuario);
            return usuariosRoles;
        }

        [HttpGet("UsuariosRolesConsultarPorId/{idUsuarioRol}")]
        public UsuarioRol MenusRolesConsultarPorId(int idUsuarioRol)
        {
            var usuarioRol = _usuariosRolesRepository.ConsultarPorId(idUsuarioRol);
            return usuarioRol;
        }

        [HttpGet("UsuariosRolesConsultar")]
        public IEnumerable<UsuarioRolView> UsuariosRolesConsultar()
        {
            var usuariosRoles = _usuariosRolesRepository.Consultar();
            return usuariosRoles;
        }

        [HttpGet("UsuariosRolesBuscar/{idUsuarioRol}")]
        public IEnumerable<UsuarioRolView> UsuariosRolesBuscar(int idUsuarioRol)
        {
            var usuariosRoles = _usuariosRolesRepository.Buscar(idUsuarioRol);
            return usuariosRoles;
        }

        [HttpPost("UsuariosRolesCrear")]
        public IActionResult UsuariosRolesCrear([FromBody] UsuarioRol  usuarioRol)
        {
            _usuariosRolesRepository.Crear(usuarioRol);
            return Ok();
        }

        [HttpPut("UsuariosRolesActualizar")]
        public IActionResult UsuariosRolesActualizar([FromBody] UsuarioRol usuarioRol)
        {
            _usuariosRolesRepository.Editar(usuarioRol);
            return Ok();
        }

        [HttpDelete("UsuariosRolesEliminar/{idUsuarioRol}")]
        public IActionResult UsuariosRolesEliminar(int idUsuarioRol)
        {
            _usuariosRolesRepository.Eliminar(idUsuarioRol);
            return Ok();
        }
    }
}
