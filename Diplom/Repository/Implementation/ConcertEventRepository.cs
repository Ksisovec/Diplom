

namespace Diplom.Repository.Implementation
{
    using Models;
    using Interfaces;
    using System.Collections.Generic;
    using System.Runtime.Caching;
    using System.Linq;

    public class ConcertEventRepository : CacheBase<ConcertEvent>, IConcertEventRepository
    {
        public ConcertEventRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
        }
        public virtual List<ConcertEvent> GetListFromCache()
        {

            //string key = RegionKey("listOf", typeof(Worker).ToString());
            //IEnumerable<T> list3 = memoryCache.GetValues();
            List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();


            List<ConcertEvent> list = new List<ConcertEvent>();
            foreach (var key in cacheKeys)
            {
                if (key.Contains(typeof(ConcertEvent).ToString()) && !key.Contains("listOf"))
                {
                    list.Add(memoryCache.Get(key) as ConcertEvent);
                }
            }
            //IDictionary<string, object> list = memoryCache.GetValues(cacheKeys);
            //list.ToDictionary(k => k.Key, k => (Worker)k.Value);
            if (list.Count == 0)
            {
                var workers = getAll();
                list.Clear();
                foreach (ConcertEvent wr in workers)
                {
                    string key = RegionKey(wr.ID.ToString(), typeof(ConcertEvent).ToString());
                    memoryCache.Add(key, wr, System.DateTime.Now.AddMinutes(10));
                    list.Add(wr);
                }
            }
            return list;
            //return list.ToDictionary(k => k.Key, k => (Worker)k.Value).Values.ToList();

        }
    }
}
