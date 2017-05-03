using PMT.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public class MoneyAccount
    {
        public MoneyAccount()
        {
            Transactions = new List<Transaction>();
        }

        public int MoneyAccountId { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }
        
        [Display(Name = nameof(ModelText.MoneyAccountName), ResourceType = typeof(ModelText))]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
