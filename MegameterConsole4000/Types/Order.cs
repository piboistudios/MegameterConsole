using MegameterConsole4000.Types.Attributes;
using MegameterConsole4000.Types.Interfaces;
using MegameterConsole4000.Types.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegameterConsole.Types
{
    [DbCollection(Type =MegameterConsole4000.Types.Enums.ObjectTypes.Order)]
    public class Order : DbType<Order>
    {
        [MemberDescription(Description = "Reference to the customer who initiated this order")]
        public int CustomerID;

        [MemberDescription(Description = "Reference to the ship-to address of this order")]
        public int ShipToID;

        [MemberDescription(Description = "Reference to the bill-to address of this order")]
        public int BillToID;

        [MemberDescription(Description = "Reference to parent order number")]
        public int ParentID;

        [MemberDescription(Description = "Reference to the originating warehouse")]
        public int WarehouseID;

        [MemberDescription(Description = "Type of order")]
        public int Type;
    }
}
