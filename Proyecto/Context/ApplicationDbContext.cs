using Microsoft.EntityFrameworkCore;
using Proyecto.Models;

namespace Proyecto.Context
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        { }


        public DbSet<Usuario> Usuarios { get; set; }
        





    }
}
