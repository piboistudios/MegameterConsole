using MegameterConsole4000.Types.Abstract;
using MegameterConsole4000.Types.Attributes;
using MegameterConsole4000.Types.Interfaces;
using MegameterConsole4000.Types.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegameterConsole.Types
{
    public struct ProductCount
    {
        public int ProductID;
        public int Count;
    }
    [DbCollection(Type=MegameterConsole4000.Types.Enums.ObjectTypes.Warehouse)]
    public class Warehouse : DbType<Warehouse>
    {
        public string Name { get; set; }

        public int ID { get; set; }

        public DateTime EnteredDate { get; set; }

        public int EnteredBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }

        public bool Exists(int ID)
        {
            return true;
        }
        [MemberDescription(Description = "Reference to the address of the warehouse")]
        public int AddressID;

        [MemberDescription(Description = "Quantity on hand")]
        public ProductCount[] OnHandCounts;

        [MemberDescription(Description = "Quantity on order")]
        public ProductCount[] OnOrderCounts;

        [MemberDescription(Description = "Quantity on back-order")]
        public ProductCount[] OnBackOrderCounts;
    }
}
