//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS.Model.Test
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShipAndDocking
    {
        public long ShipAndDockingId { get; set; }
        public Nullable<long> ControlShipInfoId { get; set; }
        public Nullable<long> DockingId { get; set; }
    
        public virtual ZoneInfo ZoneInfo { get; set; }
        public virtual ControlShipInfo ControlShipInfo { get; set; }
    }
}
