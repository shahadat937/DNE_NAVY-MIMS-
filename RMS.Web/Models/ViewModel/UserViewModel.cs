using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class UserViewModel:AppCommonInfo
    {
        [Required]
        public string UserIdForReset { get; set; }
        public BankInfo BankInfo { get; set; }
        public List<UserBankInfo> UserBankInfos { get; set; } 
        public User User { get; set; }
        public List<User> Users { get; set; }
        public List<BranchInfo> BankList { get; set; }
        public List<BranchInfo> DistrictList { get; set; }
        public List<BranchInfo> BranchList { get; set; }
        public List<Role> RoleList { get; set; }
        public List<CommonCode> SecurityQustionList { get; set; }
        public List<CommonCode> Sex { get; set; }
        public List<CommonCode> Designation { get; set; }
        public List<User> AspnetUserseList { get; set; }
        public string BankCode { get; set; }
        [Required]
        public string DistrictCode { get; set; }
        [Required]
        public string BranchCode { get; set; }
        public string AgentCode { get; set; }
        public List<BranchInfo> AgentList { get; set; } 
        public string OldPassword { get; set; }
        public UserProfile UserProfile { get; set; }
        public List<ControlShipInfo> controlShipInfos { get; set; }
        
        public UserViewModel()
        {
            UserProfile = new UserProfile();
            AgentList = new List<BranchInfo>();
            UserBankInfos = new List<UserBankInfo>();
            User = new User();
            Users=new List<User>();
            BankList=new List<BranchInfo>();
            DistrictList=new List<BranchInfo>();
            BranchList=new List<BranchInfo>();
            RoleList=new List<Role>();
            SecurityQustionList = new List<CommonCode>();
            Sex = new List<CommonCode>();
            Designation = new List<CommonCode>();
            AspnetUserseList=new List<User>();
            controlShipInfos = new List<ControlShipInfo>();
        }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PhotographUrl { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase SignatureUrl { get; set; }
        public IEnumerable<SelectListItem> RoleSelectdItemList
        {
            get { return new SelectList(RoleList, "RoleId", "RoleName"); }
        }
        public IEnumerable<SelectListItem> BankSelectdItemList
        {
            get { return new SelectList(BankList, "BranchCode", "BranchName"); }
        }
        public IEnumerable<SelectListItem> DistricSelectdItemList
        {
            get { return new SelectList(DistrictList, "BranchCode", "BranchName"); }
        }
        public IEnumerable<SelectListItem> BranchInfosSelectdItemList
        {
            get { return new SelectList(BranchList, "BranchInfoIdentity", "BranchName"); }
        }
        public IEnumerable<SelectListItem> AgentSelectListItems
        {
            get { return new SelectList(AgentList, "BranchCode", "BranchName"); }
        }
        public IEnumerable<SelectListItem> SexItemList
        {
            get { return new SelectList(Sex, "CommonCodeID", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> DesignationItemList
        {
            get { return new SelectList(Designation, "CommonCodeID", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> SecurityQustionSelectdItemList
        {
            get { return new SelectList(SecurityQustionList, "Code", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> ControlShipInfoSelectdItemList
        {
            get { return new SelectList(controlShipInfos, "ControlShipInfoId", "ControlName"); }
        }
    }
}