using MegameterConsole4000.Types.Interfaces;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegameterConsole4000.Types.Internal
{
    public abstract class DbType<UnderlyingType> : IDbType where UnderlyingType : DbType<UnderlyingType>
    {
        
        public string Name { get; set; }

        public int ID { get; set; }

        public DateTime EnteredDate { get; set; }

        public int EnteredBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }

        public static bool Exists(int ID)
        {
            return true;
        }
        public bool RunLuaScript(LuaFunction script)
        {
            return (bool)script.Call(this)[0];
        }
        public UnderlyingType GetUnderlyingType()
        {
            return (UnderlyingType)this;
        }


    }
}