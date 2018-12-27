using MegameterConsole4000.Types.Attributes;
using MegameterConsole4000.Types.Interfaces;
using MegameterConsole4000.Types.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegameterConsole.Types
{
    [DbCollection(Type=MegameterConsole4000.Types.Enums.ObjectTypes.Invoice)]
    public class Invoice : DbType<Invoice> { 

        [MemberDescription(Description="A reference to the originating order")]
        public int OrderID;
    }
}
