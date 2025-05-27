using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi29.Services.IServices;
using WebApi29.Services.Services;


namespace WebApi29.Controllers
{
    [ApiController]
    [Route("[controller]")] //route el controllador jijiij
    public class RolController : ControllerBase
    {
        private readonly IRolServices _rolServices;

        public RolController(IRolServices rolServices)
        {
            _rolServices = rolServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var reponse = await _rolServices.GeAll();

            return Ok(reponse);
        }

        //create
        [HttpPost]
        public async Task<IActionResult> Create(Rol rol)
        {
            var response = await _rolServices.Create(rol);
            return Ok(response);
        }

        //edit
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Rol rol)
        {
            var response = await _rolServices.Update(id, rol);
            return Ok(response);
        }

        //delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _rolServices.Delete(id);
            return Ok(response);
        }
    }
    
}
