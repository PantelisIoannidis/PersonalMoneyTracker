using PMT.Common.Resources;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class MoneyAccountCreateNewMV : MoneyAccount
    {
        [Display(Name=nameof(ModelText.MoneyAccountInitialAmount),ResourceType =typeof(ModelText))]
        public decimal InitialAmount { get; set; }
    }
}
