using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class TransactionVM : Transaction
    {
        private static System.Globalization.NumberFormatInfo currencyFormat;
        public TransactionVM()
        {
            //replace parenthesis with negative sign
            string curCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            currencyFormat = new System.Globalization.CultureInfo(curCulture).NumberFormat;
            currencyFormat.CurrencyNegativePattern = 1;
        }
        public string MoneyAccountName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryIcon { get; set; }
        public string CategoryColor { get; set; }
        public string SubCategoryColor { get; set; }

        public string FormattedDate 
        {
            get { 
                return TransactionDate.ToString("D");
            }
        }

        public string FormattedAmount
        {
            get
            {
                return Amount.ToString("C2", currencyFormat);
            }
        }
    }
}
