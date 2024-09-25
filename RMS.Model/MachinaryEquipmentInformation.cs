
using System.ComponentModel.DataAnnotations;

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;

    public partial class MachinaryEquipmentInformation
    {
        public string ShipId  { get; set; }
        public decimal ParentCode { get; set; }
        public decimal ControlCode { get; set; }
        public string ControlName { get; set; }
        public string GenInfo { get; set; }
        public string Shafting { get; set; }
        public string MainThrusrShift { get; set; }
        public string MiddleShaft { get; set; }
        public string AuxiliaryShaft { get; set; }
        public string PropellerShaft { get; set; }
        public string ControlValue { get; set; }
        public string AdditionalValue { get; set; }
        public string Remarks { get; set; }

        public int ControlLevel { get; set; }
        public int SortOrder { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string Level5 { get; set; }
        public string ShipName { get; set; }
        public string Genaralgroup { get; set; }
        public string Description { get; set; }

        public decimal Speed { get; set; }
        public decimal Duration { get; set; }
        public decimal EnduranceHr { get; set; }
        public int EnduranceNm { get; set; }
        public int ShaftSpeed { get; set; }
        public decimal Power { get; set; }
        public decimal FuelConsumption { get; set; }

        public string Descriptiom { get; set; }
        public decimal PortSpeed { get; set; }
        public decimal StbdSpeed { get; set; }
        public decimal StableSpeed { get; set; }

        public virtual ShipInfo ShipInfo { get; set; }


    }
}

