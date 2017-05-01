using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT.Contracts.Repositories;

namespace PMT.DataLayer.Repositories
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        
    }
}
