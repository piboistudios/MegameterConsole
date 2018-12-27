using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegameterConsole4000.LuaLINQ
{
    public static class Enumerable
    {
        public static LuaEnumerable<TSource> ToLuaEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            return new LuaEnumerable<TSource>(source);
        }
        public static IEnumerable<TSource> LWhere<TSource>(this IEnumerable<TSource> source, LuaFunction predicate)
        {
            return source.Where(element => (bool)predicate.Call(element)[0]);
        }
        
        public static string LTest<TSource>(this IEnumerable<TSource> source, LuaFunction predicate)
        {
            return predicate.ToString();
        }
    }
}