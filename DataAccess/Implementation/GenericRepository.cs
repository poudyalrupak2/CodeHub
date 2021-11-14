using DataAccess.Data;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class GenericRepository<T>:IGenericRepository<T> where T:class
    {
        internal CodeDbContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(CodeDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter); //(filter: m=>m.Name==name))
            }
            #region include 
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);//GetAll(includeProperties: "Department")
            }
            #endregion
            if (orderBy != null)        //          GetAll(orderBy: q => q.OrderBy(d => d.Name))
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T obj)
        {
            dbSet.Add(obj);
        }


        public void Update(T obj)
        {
            dbSet.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }

    }
}
