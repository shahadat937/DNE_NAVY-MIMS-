using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class MachinaryEquipmentInformationViewModel
    {
        public MachinaryEquipmentInformation_Result MachinaryEquipmentInfo { get; set; }
        public List<MachinaryEquipmentInformation_Result> MachinaryEquipmentInfos { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; } 

        public MachinaryEquipmentInformationViewModel()
        {
            MachinaryEquipmentInfo = new MachinaryEquipmentInformation_Result();
            MachinaryEquipmentInfos = new List<MachinaryEquipmentInformation_Result>();
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
    