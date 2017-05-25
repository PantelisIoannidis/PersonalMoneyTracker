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

        public static string FormatNumbers(this decimal val)
        {
            string curCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            var currencyFormat = new System.Globalization.CultureInfo(curCulture).NumberFormat;
            currencyFormat.CurrencyNegativePattern = 1;

            return val.ToString("C2", currencyFormat);
        }

        public static int ParseInt(this string text)
        {
            int tmp;
            int.TryParse(text, out tmp);
            return tmp;
        }


    }
}
