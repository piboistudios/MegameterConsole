using MegameterConsole4000.Types.Attributes;
using MegameterConsole4000.Types.Interfaces;
using MegameterConsole4000.Types.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegameterConsole.Types
{
    public class PurchaseOrder : DbType<PurchaseOrder>
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
        [MemberDescription(Description = "Reference to the supplier to whom this purchase order was submitted")]
        public int SupplierID;
    }
}
