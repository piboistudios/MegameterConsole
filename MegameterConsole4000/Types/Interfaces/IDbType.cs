using MegameterConsole4000.Types.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegameterConsole4000.Types.Interfaces
{
    public interface IDbType
    {
        
        [MemberDescription(Description = "DB Identifier")]
         int ID { get; set; }

        [MemberDescription(Description = "Date record was created")]
         DateTime EnteredDate { get; set; }

        [MemberDescription(Description = "ID of the author of this record")]
         int EnteredBy { get; set; }

        [MemberDescription(Description = "ID of the last modifier of this record")]
         int ModifiedBy { get; set; }

        [MemberDescription(Description = "Date record was last modified")]
         DateTime ModifiedDate { get; set; }

         
        
        //public static GetByID()
        //{

        //}
    }
}
