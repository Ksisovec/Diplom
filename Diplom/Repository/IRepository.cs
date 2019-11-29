using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Diplom.Repository
{
    using System.Linq.Expressions;
    public interface IRepository<T> where T : class
    {
        void Modified(T entity);

        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         string includeProperties = "");
        IEnumerable<T> getAll();

        //IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        bool IsExist(Expression<Func<T, bool>> where = null);
        void Insert(T entity);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        void Delete(object id);
        T FindByID(object entityId);

    }
}
