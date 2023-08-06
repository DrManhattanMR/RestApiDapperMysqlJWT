using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AccesoDatos;
using Microsoft.AspNetCore.Authorization;

namespace RestMatrix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // GET: api/Login
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        // [HttpGet("{id}", Name = "Get")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // POST: api/Login
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Usuario entidad)
        {
            try
            {
                ServiciosUsuario srv = new();
                Usuario usuario = srv.Login(entidad);
                if (!string.IsNullOrEmpty(usuario.nombre) && usuario.id != 0)
                {
                    // Generate JWT token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz");
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, usuario.nombre+usuario.apellidos)
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    usuario.token=tokenHandler.WriteToken(token);
                    return Ok(usuario);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { resultado = ex.Message });
            }
        }

        // PUT: api/Login/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Login/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
