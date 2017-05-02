using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class Mapping
    {
        public MoneyAccount MoneyAccountCreateNewMV_ToMoneyAccount(MoneyAccountCreateNewMV source)
        {
            return new MoneyAccount() {
                UserId = source.UserId,
                MoneyAccountId = source.MoneyAccountId,
                Name=source.Name
            };
        }
    }
}
