using Antlr.Runtime.Misc;
using RMS.BLL.DashBoardTreeView.ProcurementByDne;
using RMS.BLL.DashBoardTreeView.RefitDocking;
using RMS.BLL.DashBoardTreeView.ShipInactive;
using RMS.BLL.DashBoardTreeView.UpdateOperationState;
using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Models.ViewModel
{
    public class UpdateOplStateViewModel: UpdateOPlState
    {
        public List<UpdateOPlState> UpdateOPlStatesTreeViews { get; set; }
        public List<vwNotification> vwNotifications { get; set; }
        public List<MachineryInfo> machineryInfos { get; set; }
        public List<ControlShipInfo> controlShipInfos { get; set; }
        public List<vwRefitDokingNotification> vwRefitDokingNotifications { get; set; }
        public List<UpdateOPlState> UpdateOPlState { get; set; }
        public List<UpdateChartsOfAccount> UpdateChartsOfAccounts { get; set; }
        public List<ProcurementChartsOfAccount> ProcurementChartsOfAccounts { get; set; }
        public List<ShipInactiveChartsOfAccount> ShipInactiveChartsOfAccounts { get; set; }
        public List<RefitDockingChartsOfAccount> ApprovedRefitDockingSchedules { get; set; } 
        public List<RefitDockingSchedule> refitDockingSchedules { get; set; }
        public List<ShipInfo> shipInfos { get; set; }
        public List<AsAndAsInfo> asAndAsInfos { get; set; }
        public List<CommonCode> AccountCategory { get; set; }
        public List<CommonCode> AccountStatus { get; set; }
        public string SearchKey { get; set; }
        public int NumberOfNotification { get; set; }
        public List<BankInfo> OrgBranch { get; set; }
        public List<BranchInfo> branchInfos { get; set; }
        public List<MessageInfoView> MessageInfos { get; set; }
        public List<ShipInactiveView> shipInactives { get; set; }
        public List<MonthlyReturn> MonthlyReturns { get; set; }
        public List<QuaterlyReturn> quaterlyReturns { get; set; }
        public List<Documentation> Documentations { get; set; }
        public List<CommonCode> EquipmentTypes { get; set; }
        public UpdateOplStateViewModel()
        {
            ApprovedRefitDockingSchedules = new List<RefitDockingChartsOfAccount>();
            UpdateOPlState = new List<UpdateOPlState>();
            machineryInfos = new List<MachineryInfo>();
            controlShipInfos = new List<ControlShipInfo>();
            vwNotifications = new List<vwNotification>();
            vwRefitDokingNotifications = new List<vwRefitDokingNotification>();
            refitDockingSchedules = new List<RefitDockingSchedule>();
            shipInfos = new List<ShipInfo>();
            asAndAsInfos = new List<AsAndAsInfo>();
            AccountCategory = new List<CommonCode>();
            AccountStatus = new List<CommonCode>();
            UpdateOPlStatesTreeViews = new List<UpdateOPlState>();
            UpdateChartsOfAccounts =new List<UpdateChartsOfAccount>();
            ProcurementChartsOfAccounts =new List<ProcurementChartsOfAccount>();
            ShipInactiveChartsOfAccounts =new List<ShipInactiveChartsOfAccount>();
            OrgBranch = new List<BankInfo>();
            branchInfos = new List<BranchInfo>();
            MessageInfos = new List<MessageInfoView>();
            shipInactives = new List<ShipInactiveView>();
            MonthlyReturns = new List<MonthlyReturn>();
            Documentations = new List<Documentation>();
            EquipmentTypes = new List<CommonCode>();
        }
        public IEnumerable<SelectListItem> CategoryListItem
        {
            get { return new SelectList(AccountCategory, "DisplayCode", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> AccountStatusListItem
        {
            get { return new SelectList(AccountStatus, "DisplayCode", "TypeValue"); }
        }
        public string DashboardFor { get; set; }
        public string UserFullName { get; set; }
        public string BranchName { get; set; }
        public string DistrictName { get; set; }    
        public long ShipId { get; set; }
        public int monthlyReturnCount { get; set; }
        public int quaterlyReturnCount { get; set; }
        public int TotalShip { get; set; }
        public int TotalNonOplShip { get; set; }
        public int TotalBoat { get; set; }
        public int TotalPontoon { get; set; }
        public int TotalEstablishment { get; set; }
        public string Bulletin { get; set; }

    }
}