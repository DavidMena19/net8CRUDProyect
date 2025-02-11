using net8Proyect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Data.Data.Repository.IRepository
{
    public interface IUsuarioRepository : IRepository<ApplicationUser>
    {
        void BloquearUsuario(string idUsuario);
        void DesbloquearUsurio(string idUsuario);
    }
}
