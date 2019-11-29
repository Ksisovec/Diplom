using Diplom.Models;
using Diplom.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>, ICache<User>
    {
    }
}
