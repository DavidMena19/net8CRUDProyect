using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace net8Proyect.Data
{
    //ahora el dbContext no hereda de DbContext ahora es de IdentityDbContext
    //Identity nos ayudara con lo que viene siendo la clasificacion de roles en el programa y con la seguridad los log in etc
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //aqui van todos los modelos
    }
}
