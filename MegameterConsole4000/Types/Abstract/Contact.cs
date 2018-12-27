using MegameterConsole4000.Types.Attributes;
using MegameterConsole4000.Types.Interfaces;
using MegameterConsole4000.Types.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegameterConsole4000.Types.Abstract
{
    
    public abstract class Contact<UnderlyingType> : DbType<UnderlyingType> where UnderlyingType : DbType<UnderlyingType>
    {
        

        [MemberDescription(Description ="A table of the party's contact information")]
        public Dictionary<string, object> Info;

        
    }
}
