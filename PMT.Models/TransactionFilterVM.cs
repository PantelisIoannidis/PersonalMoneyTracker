﻿using PMT.Common.Helpers;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class TransactionFilterVM : TransactionsFilterPreferences
    {
        public Dictionary<int, string> PeriodEnum { get; set; }

        public List<MoneyAccount> MoneyAccountChoiceFilter { get; set; }

        public string PeriodDescription { get; set; }

        public DateTime SelectedDay {get;set;}


        public string AccountFilterName
        { get {
                return MoneyAccountChoiceFilter
                    .FirstOrDefault(x => x.MoneyAccountId == AccountFilterId).Name??"";
            }
        }
        public string PeriodFilterName {
            get {
                return PeriodEnum[PeriodFilterId];
            }
        }
    }
}
