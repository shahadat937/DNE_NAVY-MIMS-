using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office2013.Word;
using RMS.Model;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public partial class MachineryInfoViewModel
    {
        public MachineryInfo MachineryInfo { get; set; }
        public List<RunningHourInfo> RunningHourInfos { get; set; }
        public RunningHourInfo RunningHourInfo { get; set; }
        public List<MachineryInfo> MachineryInfos { get; set; }
        public List<MachineryInfo> Machinery { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfos { get; set; }
        public List<CommonCode> MachinariesState { get; set; }
        public List<CommonCode> MachineryCategory { get; set; }
        public List<CommonCode> EquipmentType { get; set; }
        public List<CommonCode> Lifetimetype { get; set; }
        public List<SelectionList> VerifyType { get; set; }
        public MachineryInfoViewModel()
        {
            MachineryInfo = new MachineryInfo();
            MachineryInfos = new List<MachineryInfo>();
            ShipInfos = new List<ShipInfo>();
            ControlShipInfos = new List<ControlShipInfo>();
            BrControlShipInfos = new List<vwShipBranchInfo>();
            MachinariesState = new List<CommonCode>();
            MachineryCategory = new List<CommonCode>();
            EquipmentType = new List<CommonCode>();
            Lifetimetype = new List<CommonCode>();
            RunningHourInfos = new List<RunningHourInfo>();
            RunningHourInfo = new RunningHourInfo();
            Machinery = new List<MachineryInfo>();
            VerifyType = new List<SelectionList>();
        }
        public IEnumerable<SelectListItem> MachineInformationViewModel
        {
            get { return new SelectList(Machinery, "MachineryInfoIdentity", "Machinery"); }
        }
        public IEnumerable<SelectListItem> MachinariesStateViewModel
        {
            get { return new SelectList(MachinariesState, "CommonCodeID", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> MachineryCategorySelectListItems
        {
            get { return new SelectList(MachineryCategory, "CommonCodeID", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> EquipmentTypeSelectListItems
        {
            get { return new SelectList(EquipmentType, "CommonCodeID", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> LifetimeStateViewModel
        {
            get { return new SelectList(Lifetimetype, "CommonCodeID", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> ControlShipSelectListItems
        {
            get { return new SelectList(BrControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }
        public IEnumerable<SelectListItem> ControlShipInfoSelectListItems
        {
            get { return new SelectList(ControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }
        public IEnumerable<SelectListItem> ShipSelectListItems
        {
            get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }

        }
        public IEnumerable<SelectListItem> VerifySelectListCategory
        {
            get { return new SelectList(VerifyType, "Value", "Text"); }
        }
        public string SearchKey { get; set; }
        public long ShipName { get; set; }
        public string ControlName { get; set; }
        public string RunningHourType { get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public int IsSucceed { get; set; }
        public string Message { get; set; }
        public int VerifiedType { get; set; }
        public string EquipmentTypeName { get; set; }
        public string UserName {get; set;}

    }
}