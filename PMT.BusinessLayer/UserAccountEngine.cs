using PMT.Models;
using PMT.Common;
using PMT.Common.Resources;
using PMT.DataLayer.Repositories;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.BusinessLayer
{
    public class MoneyAccountEngine : IMoneyAccountEngine
    {
        IMoneyAccountRepository MoneyAccountRepository;
        IOperationStatus operationStatus;
        ITransactionRepository transactionRepository;
        public MoneyAccountEngine(IMoneyAccountRepository MoneyAccountRepository,
                                ITransactionRepository transactionRepository,
                                IOperationStatus operationStatus)
        {
            this.MoneyAccountRepository = MoneyAccountRepository;
            this.operationStatus = operationStatus;
            this.transactionRepository = transactionRepository;
        }

        public IOperationStatus AddNewAccountWithInitialBalance(MoneyAccountCreateNewMV MoneyAccountCreateNewMV)
        {

           try
            {
                MoneyAccountRepository.Insert(new Mapping().MoneyAccountCreateNewMV_ToMoneyAccount(MoneyAccountCreateNewMV));
                MoneyAccountRepository.Save();
                var transaction = new Transaction()
                {
                    UserId = MoneyAccountCreateNewMV.UserId,
                    MoneyAccountId = MoneyAccountCreateNewMV.MoneyAccountId,
                    TransactionType = TransactionType.Adjustment,
                    Amount = MoneyAccountCreateNewMV.InitialAmount,
                    TransactionDate = DateTime.UtcNow,
                    Description = ModelText.MoneyAccountInitialAmount
                };
                transactionRepository.Insert(transaction);
                transactionRepository.Save();
            }
            catch(Exception ex)
            {
                operationStatus = OperationStatus.CreateFromException("", ex);
            }

            return operationStatus;
        } 
    }
}
