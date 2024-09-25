//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;

    public partial class vwShipMovement:ReportHeader
    {
        public long ShipMovementIdentity { get; set; }
       
        public long ShipId { get; set; }
        public string Location { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
        public string CompletionDes { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public string UserId { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public string Ship1 { get; set; }
        public string SHIP2 { get; set; }
        public string SHIP3 { get; set; }
        public string SHIP4 { get; set; }
        public string SHIP5 { get; set; }
        public string ReportName { get; set; }
        public string LetterNo { get; set; }
        public Nullable<int> TimeAwaitingOrderHour { get; set; }
        public Nullable<int> TimeAwaitingOrderMiniute { get; set; }
        public virtual ShipInfo ShipInfo { get; set; }

    }
}
