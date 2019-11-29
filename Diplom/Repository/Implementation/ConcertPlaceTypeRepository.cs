using Diplom.Models;
using Diplom.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Repository.Implementation
{
    public class ConcertPlaceTypeRepository : CacheBase<ConcertPlaceType>, IConcertPlaceTypeRepository
    {
        public ConcertPlaceTypeRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
        }
    }
}
