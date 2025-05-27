using Azure;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi29.Context;
using WebApi29.Services.IServices;

namespace WebApi29.Services.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly ApplicationDbContext _context;   //el guion bajo es por la proteccion
        public UsuarioServices(ApplicationDbContext context)
        {
            _context = context;
        }

        //Lista de usuarios
        public async Task<Domain.Entities.Response<List<Usuario>>> GeAll()
        {
            try
            {
                List<Usuario> response = await _context.Usuarios.Include(x => x.Roles).ToListAsync();
                return new Domain.Entities.Response<List<Usuario>>(response, "Lista de usuarios");

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        // Obtener Usuario
        public async Task<Domain.Entities.Response<Usuario>> GetbyId(int id)
        {
            try
            {
                //Usuario usuario = await _context.Usuarios.Where(x => x.PkUsuario == id)
                Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == id); //x= es la DB
                return new Domain.Entities.Response<Usuario>(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error " + ex.Message);
            }
        }

        //Crear usuario
        public async Task<Domain.Entities.Response<Usuario>> Create(UsuarioRequest request) //la cariable del nuevo modelo
        {
            try
            {
                Usuario usuario1 = new Usuario()
                {
                    Nombre = request.Nombre,
                    UserName = request.UserName,
                    Password = request.Password,
                    FkRol = request.FkRol
                };

                _context.Usuarios.Add(usuario1);
                await _context.SaveChangesAsync();

                return new Domain.Entities.Response<Usuario>(usuario1);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        //Editar usuario
        public async Task<Domain.Entities.Response<Usuario>> Update(int id, UsuarioRequest request)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == id);
                if (usuario == null)
                {
                    return new Domain.Entities.Response<Usuario>(null, "Usuario no encontrado");
                }

                usuario.Nombre = request.Nombre;
                usuario.UserName = request.UserName;
                usuario.Password = request.Password;
                usuario.FkRol = request.FkRol;

                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();

                return new Domain.Entities.Response<Usuario>(usuario, "Usuario actualizado");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al actualizar: " + ex.Message);
            }
        }

        //Eliminar usuario
        public async Task<Domain.Entities.Response<string>> Delete(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == id);
                if (usuario == null)
                {
                    return new Domain.Entities.Response<string>(null, "Usuario no encontrado");
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                return new Domain.Entities.Response<string>("Usuario eliminado correctamente");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al eliminar: " + ex.Message); //validaciones necesarias
            }
        }


    }
}
