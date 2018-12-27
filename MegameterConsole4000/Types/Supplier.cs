using MegameterConsole4000.Types.Abstract;
using MegameterConsole4000.Types.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegameterConsole.Types
{
    [DbCollection(Type=MegameterConsole4000.Types.Enums.ObjectTypes.Supplier)]
    public class Supplier : Contact<Supplier>
    {
    }
}
