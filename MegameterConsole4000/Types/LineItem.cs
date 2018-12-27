using MegameterConsole4000.Types.Attributes;
using MegameterConsole4000.Types.Interfaces;
using MegameterConsole4000.Types.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegameterConsole.Types
{
    [DbCollection(Type=MegameterConsole4000.Types.Enums.ObjectTypes.LineItem)]
    public class LineItem : DbType<LineItem>
    {
      
        [MemberDescription(Description="Reference to the originating order")]
        public int OrderID;

        [MemberDescription(Description ="Reference the product ordered on this line")]
        public int ProductID;

        [MemberDescription(Description ="Line item number")]
        public int LineNo;

        [MemberDescription(Description ="Quantity ordered")]
        public int QtyOrdered;

        [MemberDescription(Description = "Quantity shipped")]
        public int QtyShipped;

        [MemberDescription(Description = "Quantity backordered")]
        public int QtyBackordered;
    }
}
