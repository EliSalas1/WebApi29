using Azure;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi29.Context;
using WebApi29.Services.IServices;

namespace WebApi29.Services.Services
{
    public class RolServices : IRolServices
    {
        private readonly ApplicationDbContext _context;

        public RolServices(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista de Roles
        public async Task<Domain.Entities.Response<List<Rol>>> GeAll()
        {
            try
            {
                var response = await _context.Roles.ToListAsync();
                return new Domain.Entities.Response<List<Rol>>(response, "Lista de Roles");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error: " + ex.Message);
            }
        }

        // Obtener Rol por ID
        public async Task<Domain.Entities.Response<Rol>> GetById(int id)
        {
            try
            {
                var rol = await _context.Roles.FirstOrDefaultAsync(r => r.PkRol == id);
                return new Domain.Entities.Response<Rol>(rol, "Rol encontrado");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar rol: " + ex.Message);
            }
        }

        // Crear Rol
        public async Task<Domain.Entities.Response<Rol>> Create(Rol request)
        {
            try
            {
                Rol nuevoRol = new Rol
                {
                    Nombre = request.Nombre
                };

                _context.Roles.Add(nuevoRol);
                await _context.SaveChangesAsync();

                return new Domain.Entities.Response<Rol>(nuevoRol, "Rol creado");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear rol: " + ex.Message);
            }
        }

        // Actualizar Rol
        public async Task<Domain.Entities.Response<Rol>> Update(int id, Rol request)
        {
            try
            {
                var rol = await _context.Roles.FirstOrDefaultAsync(r => r.PkRol == id);
                if (rol == null)
                {
                    return new Domain.Entities.Response<Rol>(null, "Rol no encontrado");
                }

                rol.Nombre = request.Nombre;
                _context.Roles.Update(rol);
                await _context.SaveChangesAsync();

                return new Domain.Entities.Response<Rol>(rol, "Rol actualizado");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar rol: " + ex.Message);
            }
        }

        // Eliminar Rol
        public async Task<Domain.Entities.Response<string>> Delete(int id)
        {
            try
            {
                var rol = await _context.Roles.FirstOrDefaultAsync(r => r.PkRol == id);
                if (rol == null)
                {
                    return new Domain.Entities.Response<string>(null, "Rol no encontrado");
                }

                _context.Roles.Remove(rol);
                await _context.SaveChangesAsync();

                return new Domain.Entities.Response<string>("Rol eliminado correctamente");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar rol: " + ex.Message);
            }
        }
    }

}
