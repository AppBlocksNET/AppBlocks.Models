using System;
using System.Collections.Generic;
using System.Data;

namespace AppBlocks.Extensions
{
    public static class IDataReaderExtensions
    {
        public static IEnumerable<T> Select<T>(this IDataReader reader,
                                       Func<IDataReader, T> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }
    }
}