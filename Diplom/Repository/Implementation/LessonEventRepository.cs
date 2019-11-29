

namespace Diplom.Repository.Implementation
{
    using Models;
    using Interfaces;
    public class LessonEventRepository : CacheBase<LessonEvent>, ILessonEventRepository
    {
        public LessonEventRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
        }
    }
}
