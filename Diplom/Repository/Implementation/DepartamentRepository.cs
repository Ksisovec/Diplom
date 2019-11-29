
namespace Diplom.Repository.Implementation
{
    using Models;
    using Interfaces;
    using System.Collections.Generic;
    using System;
    using System.Runtime.Caching;
    using System.Linq;

    public class DepartamentRepository : CacheBase<Departament>, IDepartamentRepository
    {
        public DepartamentRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
        }

        public virtual List<Departament> GetListFromCache()
        {

            List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
            List<Departament> list = new List<Departament>();
            foreach (var key in cacheKeys)
            {
                if (key.Contains(typeof(Departament).ToString()) && !key.Contains("listOf"))
                {
                    list.Add(memoryCache.Get(key) as Departament);
                }
            }
            if (list.Count < getAll().Count())
            {
                var departaments = getAll();
                list.Clear();
                foreach (Departament dep in departaments)
                {
                    string key = RegionKey(dep.ID.ToString(), typeof(Worker).ToString());
                    memoryCache.Add(key, dep, DateTime.Now.AddMinutes(15));

                    list.Add(dep);
                }
            }
            return list;
        }

    }
}
