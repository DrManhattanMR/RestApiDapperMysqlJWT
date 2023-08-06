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
    
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        // GET: api/Rol
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                ServiciosCatalogo srv = new();
                List<Rol> lista = srv.ObtenerRoles().ToList();
                if (lista.Count > 0)
                    return Ok(lista);
                return NotFound(new { resultado = "No data found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { resultado = ex.Message });
            }
        }

        // GET: api/Rol/5
        // [HttpGet("{id}", Name = "Get")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // POST: api/Rol
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Rol entidad)
        {
            try
            {
                ServiciosCatalogo srv = new();
                bool result = srv.InserRol(entidad);
                if (!result)
                    return BadRequest(new { messagge = "Error al agregar" });
                return new OkObjectResult(new { messagge = "Created" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { resultado = ex.Message });
            }
        }

        // PUT: api/Rol/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Rol/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
