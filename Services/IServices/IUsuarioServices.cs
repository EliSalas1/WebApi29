using Domain.DTO;
using Domain.Entities;

namespace WebApi29.Services.IServices
{
    public interface IUsuarioServices
    {
        public Task<Response<List<Usuario>>> GeAll();
        public Task<Response<Usuario>> GetbyId(int id);

        public Task<Response<Usuario>> Create(UsuarioRequest usuario);//nuevo modelo Usuario

        public Task<Response<Usuario>> Update(int id, UsuarioRequest request);

        public Task<Response<string>> Delete(int id);

        public Usuario? ValidarUsuario(string userName, string password);//Validar usuario(?)

    }
}
