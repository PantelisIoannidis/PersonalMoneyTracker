using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Save();
        void Delete(TEntity entity);
        void Delete(object id);
        void Dispose();
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllOrderBy(Func<TEntity> orderby);
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
    }

}
