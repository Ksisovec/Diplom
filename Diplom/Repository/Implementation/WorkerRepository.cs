

namespace Diplom.Repository.Implementation
{
    using Models;
    using Interfaces;
    using System.Collections.Generic;
    using System;
    using System.Runtime.Caching;
    using System.Linq;

    public class WorkerRepository : CacheBase<Worker>, IWorkerRepository //RepositoryBase<Worker>,
    {
        public WorkerRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
        }


        public  virtual IEnumerable<Worker> GetAllFromCache(int depeID = 0)
        {

            string key = RegionKey("listOf", typeof(Worker).ToString());
            IEnumerable<Worker> list = memoryCache.Get(key) as IEnumerable<Worker>;
            if (list == null)
            {
                list = getAll();
                if (depeID != 0)
                    list = list.Where(p => p.DepartamentID == depeID);
                memoryCache.Add(key, list, DateTime.Now.AddMinutes(20));
            }
            return list;
        }
        public virtual List<Worker> GetListFromCache(int depeID = 0) 
        {

            //string key = RegionKey("listOf", typeof(Worker).ToString());
            //IEnumerable<T> list3 = memoryCache.GetValues();
            List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();


            List<Worker> list = new List<Worker>();
            foreach (var key in cacheKeys)
            {
                if (key.Contains(typeof(Worker).ToString()) && !key.Contains("listOf"))
                {
                    list.Add(memoryCache.Get(key) as Worker);
                }
            }
            //IDictionary<string, object> list = memoryCache.GetValues(cacheKeys);
            //list.ToDictionary(k => k.Key, k => (Worker)k.Value);
            if (list.Count == 0)
            {
                IEnumerable<Worker> workers = getAll();
                if (depeID != 0)
                   workers = workers.Where(p => p.DepartamentID == depeID);
               
                list.Clear();
                foreach (Worker wr in workers)
                {
                    string key = RegionKey(wr.ID.ToString(), typeof(Worker).ToString());
                    memoryCache.Add(key, wr, DateTime.Now.AddMinutes(10));
                    list.Add(wr);
                }
            }
            return list;
            //return list.ToDictionary(k => k.Key, k => (Worker)k.Value).Values.ToList();

        }

    }
}
