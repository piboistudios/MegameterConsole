using NLua;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegameterConsole4000.LuaLINQ
{
    public class LuaEnumerable<T>
    {

        public IEnumerable<T> value { get; set; }
        public LuaEnumerable(IEnumerable<T> value)
        {
            this.value = value;
        }
        public static Func<T, bool> As_NETPredicate(LuaFunction predicate)
        {
            return delegate (T input)
            {
                return (bool)predicate.Call(input)[0];
            };
        }
        public IEnumerable<T> Where(LuaFunction luaPredicate)
        {

            return value.Where(As_NETPredicate(luaPredicate));
        }
        public object[] Join(object[] inner, LuaFunction outerKeySelector, LuaFunction innerKeySelector, LuaFunction resultSelector)
        {
            Func<T, object> netOuterKeySelector = delegate (T o)
            {
                return (object)outerKeySelector.Call(o)[0];
            };
            Func<object, object> netInnerKeySelector = delegate (object i)
            {
                return (object)innerKeySelector.Call(i)[0];
            };
            Func<T, object, object> netResultSelector = delegate (T o, object i)
            {
                return (object)resultSelector.Call(o, i)[0];
            };
            return value.Join(inner, netOuterKeySelector, netInnerKeySelector, netResultSelector).ToArray();
        }

    }
}