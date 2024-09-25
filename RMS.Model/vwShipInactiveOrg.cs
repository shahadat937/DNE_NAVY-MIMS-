

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;


    public partial class vwShipInactiveOrg : ReportHeader
    {
        public long SInactiveOrgIdentity { get; set; }
        public long ShipInfoIdentity { get; set; }
        public long OrgId { get; set; }
        public string TotalShip { get; set; }
        public string Operational { get; set; }
        public string NonOperational { get; set; }
        public string sDescription { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> DockingDateFrom { get; set; }
        public Nullable<System.DateTime> DockingDateTo { get; set; }
        public Nullable<System.DateTime> RefitDateFrom { get; set; }
        public Nullable<System.DateTime> RefitDateTo { get; set; }
        public string UserID { get; set; }
       
        public DateTime todate { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public string ReportName { get; set; }
        public string TypeValue { get; set; }
        public virtual CommonCode CommonCode { get; set; }
        public virtual ShipInfo ShipInfo { get; set; }
    }
}