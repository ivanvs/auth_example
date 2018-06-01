using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T7_P2_1.Models;

namespace T7_P2_1.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<ApplicationUser> UsersRepository { get; }
        IAuthRepository AuthRepository { get; }

        void Save();
    }
}
