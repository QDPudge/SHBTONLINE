using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    namespace System.Linq
    {
        public static class EFExtension
        {
            public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
            {
                return condition ? source.Where(predicate) : source;
            }
            public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, int, bool>> predicate)
            {
                return condition ? source.Where(predicate) : source;
            }
            public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
            {
                return condition ? source.Where(predicate) : source;
            }
            public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
            {
                return condition ? source.Where(predicate) : source;
            }


        }
    }
}
