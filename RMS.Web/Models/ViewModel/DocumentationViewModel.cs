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
    public class DocumentationViewModel
    {
        public List<Documentation> Documentations { get; set; }
        public Documentation Documentation { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<CommonCode> CommonCodes { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfos { get; set; }

        public DocumentationViewModel()
        {
            Documentation = new Documentation();
            Documentations = new List<Documentation>();
            ShipInfos= new List<ShipInfo>();
            CommonCodes = new List<CommonCode>();
            ControlShipInfos = new List<ControlShipInfo>();
            BrControlShipInfos = new List<vwShipBranchInfo>();
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
        public IEnumerable<SelectListItem> ControlShipSelectListItems
        {
            get { return new SelectList(BrControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }
    }
}