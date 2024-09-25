using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;
using Antlr.Runtime.Misc;

namespace RMS.Web.Models.ViewModel
{
    public class ShipsAndCraftsViewModel
    {
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public ControlShipInfo ControlShipInfo { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<CommonCode> CommonCodes { get; set; }

        public ShipsAndCraftsViewModel()
        {
            ControlShipInfo = new ControlShipInfo();
            ControlShipInfos = new List<ControlShipInfo>();
            ShipInfos= new List<ShipInfo>();
            CommonCodes = new List<CommonCode>();
        }
        public string SearchKey { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

        public IEnumerable<SelectListItem> CommonCodeListItems
        {
            get { return new SelectList(CommonCodes, "CommonCodeID", "TypeValue"); }
        }

        public IEnumerable<SelectListItem> ShipInfoSelectListItems
        {
            get { return new SelectList(ShipInfos,"ShipInfoIdentity","ShipName");}
        } 
    }
}