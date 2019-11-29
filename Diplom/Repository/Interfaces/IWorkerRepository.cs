using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Repository.Interfaces
{
    using Diplom.Models;
    public interface IWorkerRepository : IRepository<Worker>, ICache<Worker>
    {

        IEnumerable<Worker> GetAllFromCache(int depeID = 0);
        List<Worker> GetListFromCache(int depId = 0);
    }
}
