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
    [DbCollection(Type=MegameterConsole4000.Types.Enums.ObjectTypes.Employee)]
    public class Employee : Contact<Employee>
    {
        [MemberDescription(Description ="Employee's login username")]
        public string Username;

        [MemberDescription(Description = "Employee's login password")]
        public string Password;
    }
}
