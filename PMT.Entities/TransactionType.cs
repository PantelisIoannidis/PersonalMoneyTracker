using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public static class TransactionType
    {
        public const int Income = 0;
        public const int Expense = 1;
        public const int Transfer = 2;
        public const int Adjustment = 3;
    }

    public enum TransactionTypeList
    {
        Income=0,
        Expense=1,
        Transfer=2,
        Adjustment=3
    }
}
