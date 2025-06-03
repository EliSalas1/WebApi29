using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Claims;
using System.Text;
using WebApi29.Services.IServices;
using WebApi29.Services.Services;


//3. Creación de endpoint POST /Auth/login que genera un token JWT
//Controller ...
namespace WebApi29.Controllers
{
    [ApiController]
    [Route("[controller]")] //route el controllador jijiij
                            // public class AuthController : Controller
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioService;
        private readonly IConfiguration _configuration;

        public AuthController(IUsuarioServices usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            var user = _usuarioService.ValidarUsuario(login.UserName, login.Password);

            if (user == null)
                return Unauthorized("Credenciales inválidas");

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.FkRol.ToString())
        };

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token) //Genera y devuelve un token JWT
            });
        }
    }
}
