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


        [HttpGet("UsuarioConsultarConexion/{idUsuario}/{idConexion}")]
        public IEnumerable<Usuario> UsuarioConsultarConexion(int idUsuario, int idConexion)
        {
            var usuarios = _usuariosRepository.Consultar(idUsuario, idConexion);
            return usuarios;
        }

        // GET: api/<UsuariosController>
        [HttpPost("UsuarioConsultarClave")]
        public Usuario UsuarioConsultarClave([FromBody] Usuario usuario)
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

        //// DELETE api/<UsuariosController>/5
        //[HttpDelete("UsuarioEliminarEmpresa/{usuario}/{conexion}")]
        //public IActionResult UsuarioEliminarEmpresa(string usuario, int conexion)
        //{
        //    _usuariosRepository.EliminarUsuarioEmpresa(usuario, conexion);
        //    return Ok();
        //}

        // DELETE api/<UsuariosController>/5
        [HttpDelete("UsuarioEliminar/{usuario}")]
        public IActionResult UsuarioEliminar(string usuario)
        {
            _usuariosRepository.EliminarUsuario(usuario);
            return Ok();
        }

        [HttpGet("UsuarioConsultarIdUsuario/{idUsuario}")]
        public IEnumerable<Usuario> UsuarioConsultarIdUsuario(int idUsuario)
        {
            var usuarios = _usuariosRepository.ConsultarIdUsuario(idUsuario);
            return usuarios;
        }

        [HttpGet("UsuarioBuscar")]
        public IEnumerable<Usuario> UsuarioBuscar()
        {
            var usuarios = _usuariosRepository.Buscar();
            return usuarios;
        }

        [HttpGet("UsuarioConsultarId/{id}")]
        public Usuario UsuarioConsultarId(int Id)
        {
            var usuario = _usuariosRepository.ConsultarPorId(Id);
            return usuario;
        }

        [HttpGet("UsuarioConsultarCodigo/{codigo}")]
        public Usuario UsuarioConsultarCodigo(string codigo)
        {
            var usuario = _usuariosRepository.ConsultarPorCodigo(codigo);
            return usuario;
        }

        [HttpGet("UsuarioConsultarAprobacion/{codigo}/{clave}")]
        public Usuario UsuarioConsultarAprobacion(string codigo, string clave)
        {
            var usuario = _usuariosRepository.ConsultarAprobacion(codigo, clave);
            return usuario;
        }
    }
}
