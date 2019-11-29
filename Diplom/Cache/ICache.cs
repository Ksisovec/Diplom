using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    public interface ICache<T> where T : class
    {
        string RegionKey(string key, string regionName);


        IEnumerable<T> GetAllFromCache();
         bool AddToCache(T value, int id);
        T GetByIdFromCahce(int id);

        void ClearCache();

        void DeleteFromCache(string key);
         void UpdateCache(T value, int id);

         void DeleteFromCache(int id);
    }
}
