using PMT.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.BusinessLayer
{
    public class CalculationsEngine : BaseEngine, ICalculationsEngine
    {
        ITransactionRepository transactionRepository;
        public CalculationsEngine(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }
    }
}
