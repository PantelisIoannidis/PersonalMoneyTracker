using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public static class Literals
    {
        public static class TransactionTypes
        {
            public const int Income = (int)TransactionType.Income;
            public const int Expense = (int)TransactionType.Expense;
            public const int Transfer = (int)TransactionType.Transfer;
            public const int Adjustment = (int)TransactionType.Adjustment;

        }
        public static string TransactionTypeDescription(this int t)
        {
            switch (t)
            {
                case TransactionTypes.Income: return "Income";
                case TransactionTypes.Expense: return "Expense";
                case TransactionTypes.Transfer: return "Transfer";
                case TransactionTypes.Adjustment: return "Adjustment";
                default: return "";
            }
        }

        public static string TransactionTypeDescription(this TransactionType t)
        {
            switch (t)
            {
                case TransactionType.Income: return "Income";
                case TransactionType.Expense: return "Expense";
                case TransactionType.Transfer: return "Transfer";
                case TransactionType.Adjustment: return "Adjustment";
                default: return "";
            }
        }
    }
   


}
