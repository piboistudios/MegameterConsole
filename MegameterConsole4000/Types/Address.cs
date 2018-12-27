using MegameterConsole4000.Types.Attributes;
using MegameterConsole4000.Types.Internal;
using MegameterConsole4000.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegameterConsole4000.Types.Interfaces;

namespace MegameterConsole.Types
{
    [DbCollection(Type=ObjectTypes.Address)]
    public class Address : DbType<Address> , IDbType   {
      
        [MemberDescription(Description = "Reference to another data object")]
        public int ReferenceID;

        [MemberDescription(Description = "Reference to the type of object")]
        public ObjectTypes TypeID;

        [MemberDescription(Description ="Address lines")]
        public string[] Lines;

        [MemberDescription(Description ="City")]
        public string City;

        [MemberDescription(Description ="State/Province")]
        public string StateOrProvince;

        [MemberDescription(Description ="Region")]
        public string Region;
        
        [MemberDescription(Description ="Country")]
        public string Country;

        [MemberDescription(Description ="Zip/Postal Code")]
        public string PostalCode;
    }
}
