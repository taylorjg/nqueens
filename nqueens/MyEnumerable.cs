using System;
using System.Collections.Generic;

namespace nqueens
{
    internal static class MyEnumerable
    {
        public static void ForEachWithIndex<T>(this IEnumerable<T> source, Action<T, int> f)
        {
            var index = 0;
            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    f(e.Current, index++);
                }
            }
        }
    }
}
