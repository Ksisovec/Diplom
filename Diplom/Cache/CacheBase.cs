using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    
    using Repository;
    using System.Runtime.Caching;
    public class CacheBase<T> : RepositoryBase<T>, ICache<T> where T : class
    {
        //internal DbSet<T> dbSet;
        //internal ApplicationContext context;
        internal MemoryCache memoryCache;
        public CacheBase(ApplicationContext context) : base(context)
        {
            memoryCache = MemoryCache.Default;
        }
        public string RegionKey(string key, string regionName)
        {
            return string.IsNullOrEmpty(regionName) ? key : string.Format("{0}{1}{2}", key, "::", regionName);
        }


        public virtual IEnumerable<T> GetAllFromCache()
        {

            string key = RegionKey("listOf", typeof(T).ToString());
            IEnumerable<T> list = memoryCache.Get(key) as IEnumerable<T>;
            if (list == null)
            {
                list = getAll();
                memoryCache.Add(key, list, DateTime.Now.AddMinutes(20));
            }
            return list;
        }
        public virtual bool AddToCache(T value, int id)
        {
            var cacheKey = RegionKey(id.ToString(), typeof(T).ToString());
            return memoryCache.Add(cacheKey, value, DateTime.Now.AddMinutes(10));
        }
        public virtual T GetByIdFromCahce(int id)
        {
            var cacheKey = RegionKey(id.ToString(), typeof(T).ToString());
            var value = memoryCache.Get(cacheKey) as T;
            if (value == null)
            {
                value = FindByID(id);

                memoryCache.Add(cacheKey, value, DateTime.Now.AddMinutes(5));
            }
            return value;
        }
        public virtual void DeleteFromCache(int id)
        {
            var cacheKey = RegionKey(id.ToString(), typeof(T).ToString());
            if (memoryCache.Contains(cacheKey))
            {
                memoryCache.Remove(cacheKey);
            }
        }
        public virtual void DeleteFromCache(string key)
        {
                memoryCache.Remove(key);
        }

        public virtual void ClearCache()
        {
            List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
            
            foreach (string key in cacheKeys)
            {
                if (key.Contains(typeof(T).ToString()))
                {
                    memoryCache.Remove(key);
                }
            }
        }

        //public virtual bool IsExist(int id)
        //{
        //    return memoryCache.Get(id) != null ? true : false;
        //}

        public void UpdateCache(T value, int id)
        {
            var cacheKey = RegionKey(id.ToString(), typeof(T).ToString());
            memoryCache.Set(cacheKey, value, DateTime.Now.AddMinutes(10));
        }



    }
}
