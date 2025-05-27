using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace WebApi29.Context
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        //Las clases que se mapearán en la BD
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        //inv como se hace una semilla en c#

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Insertar en tabla Usuario
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    PkUsuario = 1,
                    Nombre = "Majo",
                    UserName = "Usuario",
                    Password = "123",
                    FkRol = 1 // Aqui debes poner rol correspondiente

                });


            //Insertar en la tabla Roles

            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    PkRol = 1,
                    Nombre = "sa"
                });

        }


    }

}