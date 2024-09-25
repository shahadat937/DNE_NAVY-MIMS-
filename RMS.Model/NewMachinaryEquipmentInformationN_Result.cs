
using System.ComponentModel.DataAnnotations;

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;

    public partial class NewMachinaryEquipmentInformationN_Result
    {

        public string SlNo { get; set; }
        public string shipId { get; set; }
        public Nullable<decimal> ParentCode { get; set; }
        public string FirstLevel { get; set; }
        public string FirstHead { get; set; }
        public string SecondLevel { get; set; }
        public string SecondHead { get; set; }
        public string ThirdLevel { get; set; }
        public string ThirdHead { get; set; }
        public Nullable<decimal> ControlCode { get; set; }
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
        public Nullable<int> ControlLevel { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string Level5 { get; set; }
        public string ShipName { get; set; }
        public string Genaralgroup { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Speed { get; set; }
        public Nullable<decimal> Duration { get; set; }
        public Nullable<decimal> EnduranceHr { get; set; }
        public Nullable<short> EnduranceNm { get; set; }
        public Nullable<short> ShaftSpeed { get; set; }
        public Nullable<decimal> Power { get; set; }
        public Nullable<decimal> FuelConsumption { get; set; }
        public string Descriptiom { get; set; }
        public Nullable<decimal> PortSpeed { get; set; }
        public Nullable<decimal> StbdSpeed { get; set; }
        public Nullable<decimal> StableSpeed { get; set; }
    }
}

