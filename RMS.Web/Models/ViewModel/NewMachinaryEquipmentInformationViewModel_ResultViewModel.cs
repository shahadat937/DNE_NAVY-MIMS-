using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class NewMachinaryEquipmentInformationViewModel_ResultViewModel
    {
        public NewMachinaryEquipmentInformation_Result MachinaryEquipmentInfo { get; set; }
        public List<NewMachinaryEquipmentInformationN_Result> MachinaryEquipmentInfos { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }

        public NewMachinaryEquipmentInformationViewModel_ResultViewModel()
        {
            MachinaryEquipmentInfo = new NewMachinaryEquipmentInformation_Result();
            MachinaryEquipmentInfos = new List<NewMachinaryEquipmentInformationN_Result>();
            ShipInfos = new List<ShipInfo>();
            ControlShipInfos = new List<ControlShipInfo>();
        }

        public string SearchKey { get; set; }

        public IEnumerable<SelectListItem> ShipinSelectListItems
        {
            get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }
        }

        public IEnumerable<SelectListItem> ControlShipSelectListItems
        {
            get { return new SelectList(ControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }

    }
}
    