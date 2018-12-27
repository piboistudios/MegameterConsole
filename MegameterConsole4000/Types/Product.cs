using MegameterConsole4000.Types.Attributes;
using MegameterConsole4000.Types.Interfaces;
using MegameterConsole4000.Types.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegameterConsole.Types
{
    [DbCollection(Type=MegameterConsole4000.Types.Enums.ObjectTypes.Product)]
    public class Product : DbType<Product>
    {

        [MemberDescription(Description = "Unit price")]
        public decimal UnitPrice;

        [MemberDescription(Description = "Weight (lbs)")]
        public decimal Weight;

        [MemberDescription(Description = "Packaging dimensions")]
        public Dimensions Dimensions;

        [MemberDescription(Description = "Is this item discontinued?")]
        public bool Discontinued;

        [MemberDescription(Description = "Reference to the supplier of this product")]
        public int SupplierID;

        [MemberDescription(Description = "Product suppler part number")]
        public string SupplierProductCode;
    }

    public class Dimensions
    {
        public decimal Height;
        public decimal Length;
        public decimal  Width;
    }
}
