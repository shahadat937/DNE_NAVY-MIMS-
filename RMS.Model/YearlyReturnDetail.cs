using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public partial class YearlyReturnDetail
    {
        public long YearlyReturnDetailsId { get; set; }
        public Nullable<long> YearlyReturnId { get; set; }
        public string CompartmentAndLocation { get; set; }
        public Nullable<System.DateTime> ExmAndAirPreTestDate { get; set; }
        public string Plates { get; set; }
        public string Frames { get; set; }
        public string Rivets { get; set; }
        public string Cements { get; set; }
        public string PaintsDescription { get; set; }
        public string PaintsState { get; set; }
        public string SuctionAndDischargeLineWhetherClear { get; set; }
        public string SuctionAndDischargeLineState { get; set; }       
        public string SuctionAndDischargeNonReturnValvesWhetherState { get; set; }
        public string SuctionAndDischargeNonReturnValvesState { get; set; }
        public string WatertightDoorWhetherTested { get; set; }
        public string WatertightDoorState { get; set; }
        public string DefectsDescription { get; set; }
        public string DefectsActionTaken { get; set; }

        public virtual YearlyReturn YearlyReturn { get; set; }
    }
}
