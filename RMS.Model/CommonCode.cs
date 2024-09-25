//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CommonCode
    {
        public CommonCode()
        {
            this.AsAndAsInfoes = new HashSet<AsAndAsInfo>();
            this.ControlShipInfoes = new HashSet<ControlShipInfo>();
            this.ControlShipInfoes1 = new HashSet<ControlShipInfo>();
            this.ControlShipInfoes2 = new HashSet<ControlShipInfo>();
            this.ControlShipInfoes3 = new HashSet<ControlShipInfo>();
            this.DamageMachineriesInfoes = new HashSet<DamageMachineriesInfo>();
            this.DeckInfoes = new HashSet<DeckInfo>();
            this.DefectMachinaries = new HashSet<DefectMachinary>();
            this.Documentations = new HashSet<Documentation>();
            this.FortNightlies = new HashSet<FortNightly>();
            this.FortNightlies1 = new HashSet<FortNightly>();
            this.FortnightlyInfoes = new HashSet<FortnightlyInfo>();
            this.ICE = new HashSet<ICE>();
            this.LubOilConsumptions = new HashSet<LubOilConsumption>();
            this.MachineryInfoes = new HashSet<MachineryInfo>();
            this.MachineryInfoes1 = new HashSet<MachineryInfo>();
            this.MachineryRunningInfoes = new HashSet<MachineryRunningInfo>();
            this.MachineryRunningInfoes1 = new HashSet<MachineryRunningInfo>();
            this.MessageInfoes = new HashSet<MessageInfo>();
            this.MonthlyReturns = new HashSet<MonthlyReturn>();
            this.POLExpenseInfoes = new HashSet<POLExpenseInfo>();
            this.ProcurementSchedules = new HashSet<ProcurementSchedule>();
            this.QuaterlyReturns = new HashSet<QuaterlyReturn>();
            this.QuaterlyReturns1 = new HashSet<QuaterlyReturn>();
            this.QuaterlyReturns2 = new HashSet<QuaterlyReturn>();
            this.QuaterlyReturns3 = new HashSet<QuaterlyReturn>();
            this.QuaterlyReturns4 = new HashSet<QuaterlyReturn>();
            this.QuaterlyReturns5 = new HashSet<QuaterlyReturn>();
            this.RefitDockingSchedules = new HashSet<RefitDockingSchedule>();
            this.ShipDetails = new HashSet<ShipDetail>();
            this.ShipInactives = new HashSet<ShipInactive>();
            this.ShipInactiveOrgs = new HashSet<ShipInactiveOrg>();
            this.ShipInfoes = new HashSet<ShipInfo>();
            this.ShipInfoes1 = new HashSet<ShipInfo>();
            this.ShipInfoes2 = new HashSet<ShipInfo>();
            this.TrainingInfoes = new HashSet<TrainingInfo>();
            this.TrainingInfoes1 = new HashSet<TrainingInfo>();
            this.TrainingInfoes2 = new HashSet<TrainingInfo>();
            this.UserProfiles = new HashSet<UserProfile>();
            this.UserProfiles1 = new HashSet<UserProfile>();
            this.YearlyReturns = new HashSet<YearlyReturn>();
        }

        public long CommonCodeID { get; set; }
        public string BankCode { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string TypeValue { get; set; }
        public string BTypeValue { get; set; }
        public string AdditonalValue { get; set; }
        public string DisplayCode { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }


        public virtual ICollection<AsAndAsInfo> AsAndAsInfoes { get; set; }

        public virtual ICollection<ControlShipInfo> ControlShipInfoes { get; set; }

        public virtual ICollection<ControlShipInfo> ControlShipInfoes1 { get; set; }

        public virtual ICollection<ControlShipInfo> ControlShipInfoes2 { get; set; }

        public virtual ICollection<ControlShipInfo> ControlShipInfoes3 { get; set; }

        public virtual ICollection<DamageMachineriesInfo> DamageMachineriesInfoes { get; set; }

        public virtual ICollection<DeckInfo> DeckInfoes { get; set; }

        public virtual ICollection<DefectMachinary> DefectMachinaries { get; set; }

        public virtual ICollection<Documentation> Documentations { get; set; }

        public virtual ICollection<FortNightly> FortNightlies { get; set; }

        public virtual ICollection<FortNightly> FortNightlies1 { get; set; }

        public virtual ICollection<FortnightlyInfo> FortnightlyInfoes { get; set; }

        public virtual ICollection<ICE> ICE { get; set; }

        public virtual ICollection<LubOilConsumption> LubOilConsumptions { get; set; }

        public virtual ICollection<MachineryInfo> MachineryInfoes { get; set; }

        public virtual ICollection<MachineryInfo> MachineryInfoes1 { get; set; }

        public virtual ICollection<MachineryRunningInfo> MachineryRunningInfoes { get; set; }

        public virtual ICollection<MachineryRunningInfo> MachineryRunningInfoes1 { get; set; }

        public virtual ICollection<MessageInfo> MessageInfoes { get; set; }

        public virtual ICollection<MonthlyReturn> MonthlyReturns { get; set; }

        public virtual ICollection<POLExpenseInfo> POLExpenseInfoes { get; set; }

        public virtual ICollection<ProcurementSchedule> ProcurementSchedules { get; set; }

        public virtual ICollection<QuaterlyReturn> QuaterlyReturns { get; set; }

        public virtual ICollection<QuaterlyReturn> QuaterlyReturns1 { get; set; }

        public virtual ICollection<QuaterlyReturn> QuaterlyReturns2 { get; set; }

        public virtual ICollection<QuaterlyReturn> QuaterlyReturns3 { get; set; }

        public virtual ICollection<QuaterlyReturn> QuaterlyReturns4 { get; set; }

        public virtual ICollection<QuaterlyReturn> QuaterlyReturns5 { get; set; }

        public virtual ICollection<RefitDockingSchedule> RefitDockingSchedules { get; set; }

        public virtual ICollection<ShipDetail> ShipDetails { get; set; }

        public virtual ICollection<ShipInactive> ShipInactives { get; set; }

        public virtual ICollection<ShipInactiveOrg> ShipInactiveOrgs { get; set; }

        public virtual ICollection<ShipInfo> ShipInfoes { get; set; }

        public virtual ICollection<ShipInfo> ShipInfoes1 { get; set; }

        public virtual ICollection<ShipInfo> ShipInfoes2 { get; set; }

        public virtual ICollection<TrainingInfo> TrainingInfoes { get; set; }

        public virtual ICollection<TrainingInfo> TrainingInfoes1 { get; set; }

        public virtual ICollection<TrainingInfo> TrainingInfoes2 { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }

        public virtual ICollection<UserProfile> UserProfiles1 { get; set; }

        public virtual ICollection<YearlyReturn> YearlyReturns { get; set; }

    }
}
