using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Save();
        void Delete(TEntity entity);
        void Delete(object id);
        void Dispose();
        IQueryable<TEntity> GetAll();
        TEntity GetById(object id);
        MainDb GetDB();
        void Insert(TEntity entity);
        void Update(TEntity entity);
    }

}
