using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class MessageInfoViewModel
    {
        public List<MessageInfo> MessageInfos { get; set; }
        public List<MessageInfoView> MessageInfosV { get; set; }
        public MessageInfo MessageInfo { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<vwShipBankInfo> BrControlShipInfoes { get; set; }
        public List<vwShipBankInfo> FmsgBrControlShipInfoes { get; set; }
        public List<ControlShipInfo> ControlShipInfoes { get; set; }
        public List<CommonCode> CommonCodes { get; set; }
        public List<Role> Roles { get; set; }
        public List<signal> Signals { get; set; }
        public MessageInfoViewModel()
        {
            MessageInfos = new List<MessageInfo>();
            MessageInfosV = new List<MessageInfoView>();
            MessageInfo = new MessageInfo();
            ShipInfos = new List<ShipInfo>();
            BrControlShipInfoes = new List<vwShipBankInfo>();
            FmsgBrControlShipInfoes = new List<vwShipBankInfo>();
            ControlShipInfoes = new List<ControlShipInfo>();
            CommonCodes=new List<CommonCode>();
            Roles = new List<Role>();
            Signals=new List<signal>();
        }
        public IEnumerable<SelectListItem> ShipselectListItems
        {
            get { return new SelectList(BrControlShipInfoes, "ControlShipInfoId", "ControlName"); }
        }
        public IEnumerable<SelectListItem> FmgShipselectListItems
        {
            get { return new SelectList(FmsgBrControlShipInfoes, "ControlShipInfoId", "ControlName"); }
        }
        public IEnumerable<SelectListItem> ShipToselectListItems
        {
            get { return new SelectList(ControlShipInfoes, "ControlShipInfoId", "ControlName"); }
        }
        public IEnumerable<SelectListItem> CommonCodeSelectListType
        {
            get { return new SelectList(CommonCodes, "CommonCodeID", "TypeValue"); }

        }
        public IEnumerable<SelectListItem> RoleSelectListType
        {
            get { return new SelectList(Roles, "RoleId", "RoleName"); }

        }
        public string SearchKey { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}