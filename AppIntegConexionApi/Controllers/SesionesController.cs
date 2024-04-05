using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppIntegConexionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SesionesController : ControllerBase
    {
        private readonly IControlSesionRepository _controlSesionRepository;

        public SesionesController(IControlSesionRepository controlSesionRepository)
        {
            _controlSesionRepository = controlSesionRepository;
        }

        // GET: api/<SesionesController>
        [HttpGet("SesionConsultar/{usuario}")]
        public ControlSesion SesionConsultar(string usuario)
        {
            var sesion = _controlSesionRepository.ConsultarSesion(usuario);
            return sesion;
        }

        // POST api/<SesionesController>
        [HttpPost("SesionCrear")]
        public IActionResult SesionCrear([FromBody] ControlSesion usuario)
        {
            _controlSesionRepository.CrearSesion(usuario);
            return Ok();
        }

        // PUT api/<SesionesController>/5
        [HttpPut("SesionActualizar")]
        public IActionResult SesionActualizar([FromBody] ControlSesion usuario)
        {
            _controlSesionRepository.ActualizarSesion(usuario);
            return Ok();
        }

        // DELETE api/<SesionesController>/5
        [HttpDelete("SesionEliminar/{usuario}")]
        public IActionResult SesionEliminar(string usuario)
        {
            _controlSesionRepository.EliminarSesion(usuario);
            return Ok();
        }
    }
}
