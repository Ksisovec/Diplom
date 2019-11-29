
namespace Diplom.Repository.Implementation
{
    using Interfaces;
    using Models;
    public class LessonMarksRepository : CacheBase<LessonMarks>, ILessonMarksRepository
    {
        public LessonMarksRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
        }
    }
}
