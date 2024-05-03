using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using AppIntegConexionCore.Repository;
using Microsoft.AspNetCore.Mvc;


namespace AppIntegConexionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConexionesController : ControllerBase
    {
        private readonly IConexionesRepository _conexionesRepository;

        public ConexionesController(IConexionesRepository conexionesRepository)
        {
            _conexionesRepository = conexionesRepository;
        }
        //http://localhost:5092/swagger/index.html // Swagger
        // GET: api/<ConexionesController>
        [HttpGet("ConexionesUsuario/{usuario}")]
        public IEnumerable<Conexion> ConexionesUsuario(string usuario)
        {
            var empresas =  _conexionesRepository.ConsultarConexionPorUsuario(usuario);
            return empresas;
        }

        // GET: api/<ConexionesController>
        [HttpGet("ConexionUsuarioEmpresa/{usuario}/{conexion}")]
        public Conexion ConexionUsuarioEmpresa(string usuario, int conexion)
        {
            var conexionEmpresa = _conexionesRepository.ConsultarConexionPorUsuarioEmpresa(usuario, conexion);
            return conexionEmpresa;
        }

        // POST api/<ConexionController>
        [HttpPost("EmpresaCrear")]
        public IActionResult EmpresaCrear([FromBody] Conexion empresa)
        {
            _conexionesRepository.CrearEmpresa(empresa);
            return Ok();
        }
    }
}
