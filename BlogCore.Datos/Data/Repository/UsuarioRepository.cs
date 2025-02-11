using net8Proyect.Data.Data.Repository.IRepository;
using net8Proyect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Data.Data.Repository
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext contex) : base(contex)
        {
            _context = contex;
        }
        public void BloquearUsuario(string idUsuario)
        {
            var usuarioDesdeBd = _context.ApplicationUser.FirstOrDefault(i => i.Id == idUsuario);
            //LockoutEnd es un campo dentro de la tabla de IdentityUser para bloquear usuarios
            usuarioDesdeBd.LockoutEnd = DateTime.Now.AddDays(1000);

            _context.SaveChanges();
        }

        public void DesbloquearUsurio(string idUsuario)
        {
            var usuarioDesdeBd = _context.ApplicationUser.FirstOrDefault(i => i.Id == idUsuario);
            usuarioDesdeBd.LockoutEnd = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
