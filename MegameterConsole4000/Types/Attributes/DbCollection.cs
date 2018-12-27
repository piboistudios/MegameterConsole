using MegameterConsole4000.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegameterConsole4000.Types.Attributes
{
    public class DbCollection : Attribute
    {
        public ObjectTypes Type;
    }
}