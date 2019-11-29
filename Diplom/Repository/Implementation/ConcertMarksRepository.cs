

namespace Diplom.Repository.Implementation
{
    using Models;
    using Interfaces;
    using System.Collections.Generic;
    using System.Runtime.Caching;
    using System.Linq;

    public class ConcertMarksRepository : CacheBase<ConcertMarks>, IConcertMarksRepository
    {
        public ConcertMarksRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
        }

    }
}
