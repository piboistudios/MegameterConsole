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
    [DbCollection(Type =MegameterConsole4000.Types.Enums.ObjectTypes.Customer)]
    public class Customer : Contact<Customer>
    {
        

        [MemberDescription(Description ="Company Name")]
        public string CompanyName;

        [MemberDescription(Description ="Customer's available credit balance")]
        public decimal Credits;
    }
}
