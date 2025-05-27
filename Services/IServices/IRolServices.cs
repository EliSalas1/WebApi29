using Domain.Entities;

namespace WebApi29.Services.IServices
{
    public interface IRolServices
    {
        public Task<Response<List<Rol>>> GeAll();
        public Task<Response<Rol>> GetById(int id);
        public Task<Response<Rol>> Create(Rol rol);
        public Task<Response<Rol>> Update(int id, Rol rol);
        public Task<Response<string>> Delete(int id);

    }
}
