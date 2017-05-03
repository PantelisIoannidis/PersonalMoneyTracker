using PMT.Models;
using PMT.Common;
using PMT.Entities;
using System.Collections.Generic;

namespace PMT.BusinessLayer
{
    public interface IMoneyAccountEngine
    {
        IActionStatus AddNewAccountWithInitialBalance(MoneyAccount moneyAccount);
        List<MoneyAccount> GetMoneyAccountBalance(string userId);
    }
}