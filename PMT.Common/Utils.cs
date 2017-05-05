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
    }
}
