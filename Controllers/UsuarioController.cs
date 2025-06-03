using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi29.Services.IServices;
//4. proteger a UsuarioController y RolController
namespace WebApi29.Controllers
{
    [Authorize] //esto hace que evite que se acceda al endpoint sin un token válido se lo pude a todas
    [ApiController]
    [Route("[controller]")] //route el controllador jijiij
    

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioServices;

        public UsuarioController(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var reponse = await _usuarioServices.GeAll();

            return Ok(reponse);
        }
        //obtener por id
        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult>GetById(int id)
        {
            var response = await _usuarioServices.GetbyId(id);
            return Ok(response);
        }
        //create
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create (UsuarioRequest  request)
        {
            var response = await _usuarioServices.Create(request);
            return Ok(response);
        }

        //edit
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UsuarioRequest request)
        {
            var response = await _usuarioServices.Update(id, request);
            return Ok(response);
        }

        //delete
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _usuarioServices.Delete(id);
            return Ok(response);
        }

    }

}
