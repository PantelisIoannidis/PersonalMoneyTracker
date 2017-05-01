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
    public class UserAccountEngine : IUserAccountEngine
    {
        IUserAccountRepository userAccountRepository;
        IOperationStatus operationStatus;
        ITransactionRepository transactionRepository;
        public UserAccountEngine(IUserAccountRepository userAccountRepository,
                                ITransactionRepository transactionRepository,
                                IOperationStatus operationStatus)
        {
            this.userAccountRepository = userAccountRepository;
            this.operationStatus = operationStatus;
            this.transactionRepository = transactionRepository;
        }

        public IOperationStatus AddNewAccountWithInitialBalance(UserAccountCreateNewMV userAccountCreateNewMV)
        {

           try
            {
                userAccountRepository.Insert(new Mapping().UserAccountCreateNewMV_ToUserAccount(userAccountCreateNewMV));
                userAccountRepository.Save();
                var transaction = new Transaction()
                {
                    UserId = userAccountCreateNewMV.UserId,
                    UserAccountId = userAccountCreateNewMV.UserAccountId,
                    TransactionType = TransactionType.Adjustment,
                    Amount = userAccountCreateNewMV.InitialAmount,
                    TransactionDate = DateTime.UtcNow,
                    Description = ModelText.UserAccountInitialAmount
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
