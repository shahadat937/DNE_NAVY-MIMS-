using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public partial class ProcurementScheduleViewModel
    {
        public List<ProcurementSchedule> ProcurementSchedules { get; set; }
        public ProcurementSchedule ProcurementSchedule { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<CommonCode> Common { get; set; }
        public string SearchKey { get; set; }
        public ProcurementScheduleViewModel()
        {
            ProcurementSchedules = new List<ProcurementSchedule>();
            ProcurementSchedule = new ProcurementSchedule();
            ShipInfos = new List<ShipInfo>();
            Common=new List<CommonCode>();
        }
        public IEnumerable<SelectListItem> ShipMovementSelectListItems
        {
            get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }

        }

        public IEnumerable<SelectListItem> CommonSelectListItems
        {
            get { return new SelectList(Common, "CommonCodeID", "TypeValue"); }

        }
        public bool ReTender { get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public long DefaultCheckboxCheck { get; set; }
    }
}