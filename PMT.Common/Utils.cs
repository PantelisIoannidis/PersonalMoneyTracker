using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Common
{
    public static class Utils
    {
        /// <summary>
        /// Eliminates duplicates entries from IEnumerables
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> expression)
        {
            var uniqueValues = new HashSet<TKey>();
            return source.Where(item => uniqueValues.Add(expression(item)));
        }

        /// <summary>
        /// Format decimal accordint to Current Culture with minus sign for negative numbers
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string FormatNumbers(this decimal val)
        {
            string curCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            var currencyFormat = new System.Globalization.CultureInfo(curCulture).NumberFormat;
            currencyFormat.CurrencyNegativePattern = 1;

            return val.ToString("C2", currencyFormat);
        }

        /// <summary>
        /// Tryparse shortcut
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int ParseInt(this string text)
        {
            int tmp;
            int.TryParse(text, out tmp);
            return tmp;
        }

        public static DateTime ToLocalTime(this DateTime dateTime, int offsetInMinutes)
        {
            return dateTime.ToUniversalTime().Add(TimeSpan.FromMinutes(offsetInMinutes));
        }
        /// <summary>
        /// select a date giving an offset from the beginning of the month
        /// </summary>
        /// <param name="date"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static DateTime OffsetInCurrentMonth(this DateTime date, int days)
        {
            if (days == 0) days = 1;
            return new DateTime(date.Year, date.Month, 1).AddDays(days - 1);
        }

        public static TimeSpan FullDateMinusTickSpan()
        {
            DateTime a = new DateTime(2016, 1, 1);
            DateTime b = a.AddDays(1).AddTicks(-1);
            return b.Subtract(a);
        }

        public static DateTime SetTimeTo235959(this DateTime date)
        {
            return date.Date.Add(FullDateMinusTickSpan());
        }

        public static DateTime SetTimeTo000000(this DateTime date)
        {
            return date.Date;
        }

    }
}
