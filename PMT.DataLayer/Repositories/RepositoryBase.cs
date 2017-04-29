using PMT.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Repositories
{
    abstract public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        internal MainDb context;
        internal DbSet<TEntity> dbSet;

        public RepositoryBase(MainDb context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        public virtual IQueryable<TEntity> GetAllOrderBy(Func<TEntity> orderby)
        {
            return dbSet.OrderBy(o=>orderby);
        }


        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);

            dbSet.Remove(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entity = dbSet.Find(id);
            Delete(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public virtual void Dispose()
        {
            context.Dispose();
        }


    }
}
