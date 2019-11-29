using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Repository.Interfaces
{
    using Diplom.Models;
    public interface IDepartamentRepository : IRepository<Departament>, ICache<Departament>
    {
        List<Departament> GetListFromCache();
    }
}
