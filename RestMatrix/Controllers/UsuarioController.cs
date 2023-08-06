using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccesoDatos;
using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestMatrix.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/Usuario
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                ServiciosUsuario srv = new();
                List<Usuario> lista = srv.ObtenerUsuarios().ToList();
                if (lista.Count > 0)
                    return Ok(lista);
                return NotFound(new { resultado = "No data found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { resultado = ex.Message });
            }
        }

        // GET: api/Usuario/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Usuario
        [HttpPost]
        public IActionResult Post([FromBody] Usuario entidad)
        {
            try
            {
                ServiciosUsuario srv = new();
                bool result = srv.InsertUsuario(entidad);
                if (!result)
                    return BadRequest(new { messagge = "Error al agregar" });
                return new OkObjectResult(new { messagge = "Created" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { resultado = ex.Message });
            }
        }

        // PUT: api/Usuario/5
        [HttpPut]
        public IActionResult Put( [FromBody] Usuario entidad)
        {
            try
            {
                ServiciosUsuario srv = new();
                bool result = srv.UpdateUser(entidad);
                if (!result)
                    return BadRequest(new { messagge = "Error al editar" });
                return new OkObjectResult(new { messagge = "Updated" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { resultado = ex.Message });
            }
        }

        // DELETE: api/Usuario/5
        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            try
            {
                ServiciosUsuario srv = new();
                bool result = srv.DeleteUser(id);
                if (!result)
                    return BadRequest(new { messagge = "Error al eliminar" });
                return new OkObjectResult(new { messagge = "Deleted" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { resultado = ex.Message });
            }
        }
    }
}
