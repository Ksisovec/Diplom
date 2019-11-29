using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Repository
{
    using System.Data.Entity;
    using System.Linq.Expressions;

    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        internal DbSet<T> dbSet;
        internal ApplicationContext context;

        public RepositoryBase(ApplicationContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        
        public virtual bool IsExist(Expression<Func<T, bool>> where = null)
        {
            return dbSet.FirstOrDefault(where) != null ? true : false;
        }

        public virtual T FindByID(object entityId)
        {
            return dbSet.Find(entityId);
        }
        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public virtual IEnumerable<T> getAll()
        {
            return dbSet.ToList();
        }

        public virtual void Modified(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();

        }
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity); //???
            context.Entry(entity).State = EntityState.Modified; //???
            context.SaveChanges();
        }
        public virtual void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            context.SaveChanges();
        }

    }
}
