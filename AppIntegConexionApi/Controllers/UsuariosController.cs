using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppIntegConexionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly IUsuariosRepository _usuariosRepository;
        public UsuariosController(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        // GET: api/<UsuariosController>
        [HttpPost("UsuarioClave")]
        public Usuario UsuarioClave([FromBody] Usuario usuario)
        {
            var usuarioEmpresa = _usuariosRepository.ConsultarUsuarioPorUsuarioClave(usuario);
            return usuarioEmpresa;
        }

        // POST api/<UsuariosController>
        [HttpPost("UsuarioCrear")]
        public IActionResult UsuarioCrear([FromBody] Usuario usuario)
        {
            _usuariosRepository.CrearUsuario(usuario);
            return Ok();
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("UsuarioActualizar")]
        public IActionResult UsuarioActualizar([FromBody] Usuario usuario)
        {
            _usuariosRepository.ActualizarUsuario(usuario);
            return Ok();
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("UsuarioEliminarEmpresa/{usuario}/{conexion}")]
        public IActionResult UsuarioEliminarEmpresa(string usuario, int conexion)
        {
            _usuariosRepository.EliminarUsuarioEmpresa(usuario, conexion);
            return Ok();
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("UsuarioEliminar/{usuario}")]
        public IActionResult UsuarioEliminar(string usuario)
        {
            _usuariosRepository.EliminarUsuario(usuario);
            return Ok();
        }
    }
}
