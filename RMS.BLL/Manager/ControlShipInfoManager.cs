using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using RMS.BLL.IManager;
using RMS.BLL.TreeView;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using System.Collections;
using RMS.DAL;

namespace RMS.BLL.Manager
{
    public class ControlShipInfoManager : BaseManager, IControlShipInfoManager
    {
        private IControlShipInfoRepository _controlShipInfoRepository;
        private IVwShipBranchInfoRepository _vwShipBranchInfoRepository;
        private IvwPontoonRepository _vwPontoonRepository;

        RM_AGBEntities db = new RM_AGBEntities();

        public ControlShipInfoManager()
        {
            _controlShipInfoRepository = new ControlShipInfoRepository(Instance.Context);
            _vwPontoonRepository = new vwPontoonRepository(Instance.Context);
        }

        public List<ShipChartofAccount> GetChartofAccount()
        {
            var ShipInfo = new List<vwShipBranchInfo>();
            if (PortalContext.CurrentUser.LoweredRoleName == "S" || PortalContext.CurrentUser.LoweredRoleName == "V")
            {
                ShipInfo = db.vwShipBranchInfoes.Where(x => x.ControlShipInfoId == PortalContext.CurrentUser.ShipId).ToList();
            }
            else
            {
                ShipInfo = db.vwShipBranchInfoes.Where(x => x.DistrictCodeIdentity == PortalContext.CurrentUser.BranchInfoIdentity || x.BranchInfoIdentity == PortalContext.CurrentUser.BranchInfoIdentity).ToList();
            }
            var controlAccounts = _controlShipInfoRepository.Filter(x => x.IsActive).OrderBy(x => x.ControlLevel).ThenBy(x => x.ControlCode).ToList();
            var query = (from post in controlAccounts join meta in ShipInfo on post.Level1 equals meta.ControlCode.ToString() select post).ToList();
            //var ControlShipInfo1 = controlAccounts.Join(ShipInfo, ca => ca.Level1, si => si.ControlCode.ToString(), (ca, si) => new { CA = ca, SI = si }).Select(x => x.CA).ToList();
            IShipTarget chTarget = new ShipChartofAccountAdapter(query);
            var client = new ShipTreeViewBuilder(chTarget);
            return client.GetChartofAccount();
        }

        public ControlShipInfo GetControlAccountById(long? controlId)
        {
            return _controlShipInfoRepository.FindOne(x => x.ControlShipInfoId == controlId) ?? new ControlShipInfo();
        }
        public ControlShipInfo GetControlShipInfoById(long? controlId)
        {
            return _controlShipInfoRepository.Filter(x => x.ControlShipInfoId == controlId).Include(x=>x.CommonCode).Include(x => x.CommonCode1).Include(x => x.CommonCode2).Include(x => x.CommonCode3).FirstOrDefault() ?? new ControlShipInfo();
        }
        public List<ControlShipInfo> GetBankList()
        {
            var branchInfos = _controlShipInfoRepository.Filter(x => x.ParentCode == 0);
            return branchInfos.ToList();
        }

        public IEnumerable GetDistrictList1(string bankCode)
        {
            var districInfo = _controlShipInfoRepository.Filter(x => x.Level1 == bankCode && x.ControlLevel == 2).Select(x => new
            {
                BranchCode = x.ControlCode,
                BranchName = x.ControlName
            }
           );
            return districInfo.ToList();
        }

        public List<ControlShipInfo> GetBranchListByBankCode(string bankCode)
        {
            return _controlShipInfoRepository.Filter(x => x.Level2 == bankCode && x.ControlLevel == 3 ).ToList();
        }

        public ResponseModel ShipStatusUpdate(long controlShipInfoIdentity, long? shipStatus)
        {
            var oldData = _controlShipInfoRepository.FindOne(x => x.ControlShipInfoId == controlShipInfoIdentity);
            if (oldData != null)
            {
                oldData.Status = shipStatus;
                _controlShipInfoRepository.Edit(oldData);
                ResponseModel.Message = "Ship Status information is updated successfully.";

            }
            return ResponseModel;
        }

        public string GetShipName(long shipId)
        {
            return _controlShipInfoRepository.FindOne(x => x.ControlShipInfoId == shipId).ControlName;
        }

        public IEnumerable GetBranchList(string branchCode)
        {
            return _controlShipInfoRepository.Filter(x => x.Level2 == branchCode && x.ControlLevel == 3 ).Select(x => new
            {
                BranchCode = x.ControlCode,
                BranchName = x.ControlName
            }).ToList();
        }
        public long GetMaxControlCode(decimal parentCode)
        {
            return _controlShipInfoRepository.GetMaxControlCode(parentCode);
        }

        public List<ControlShipInfo> GetAll()
        {
           
            return _controlShipInfoRepository.All().Include(x=>x.BranchInfo).ToList();
        }

        public List<vwShipBranchInfo> ShipBranchInfo()
        {
            var ShipInfo = new List<vwShipBranchInfo>();
            if (PortalContext.CurrentUser.LoweredRoleName == "S" || PortalContext.CurrentUser.LoweredRoleName == "V")
            {
                ShipInfo = db.vwShipBranchInfoes.Where(x => x.ControlShipInfoId == PortalContext.CurrentUser.ShipId).ToList();
            }
            else if (PortalContext.CurrentUser.RoleId == 4)
            {
                ShipInfo = db.vwShipBranchInfoes.Where(x => x.DistrictCodeIdentity == PortalContext.CurrentUser.BranchInfoIdentity || x.BranchInfoIdentity == PortalContext.CurrentUser.BranchInfoIdentity).ToList();

            }
            else
            {
                ShipInfo = db.vwShipBranchInfoes.Where(x => x.DistrictCodeIdentity == PortalContext.CurrentUser.BranchInfoIdentity || x.BranchInfoIdentity == PortalContext.CurrentUser.BranchInfoIdentity).ToList();
            }
            return ShipInfo;
        }

        public List<vwShipBankInfo> ShipBankInfo()
        {
            var ShipInfo = new List<vwShipBankInfo>();
            if (PortalContext.CurrentUser.LoweredRoleName == "S" || PortalContext.CurrentUser.LoweredRoleName == "V")
            {
                ShipInfo = db.vwShipBankInfoes.Where(x => x.ControlShipInfoId == PortalContext.CurrentUser.ShipId).ToList();
            }
            else
            {
                ShipInfo = db.vwShipBankInfoes.Where(x => x.DistrictCodeIdentity == PortalContext.CurrentUser.BranchInfoIdentity || x.BranchInfoIdentity == PortalContext.CurrentUser.BranchInfoIdentity).ToList();
            }
            return ShipInfo;
        }
        public string ShipBankName(long? ConId)
        {
            return db.vwShipBankInfoes.FirstOrDefault(x=>x.ControlShipInfoId==ConId).ControlName.ToString();
        }

        public string ShipBranchName(long? ShipId)
        {
            return db.ControlShipInfoes.Where(x => x.ControlShipInfoId == ShipId).Include(x=>x.BranchInfo).FirstOrDefault().BranchInfo.BranchName.ToString();
        }
        public List<vwShipBankInfo> FMsgShipBankInfo()
        {
            var ShipInfo = new List<vwShipBankInfo>();
            if (PortalContext.CurrentUser.LoweredRoleName == "S" || PortalContext.CurrentUser.LoweredRoleName == "V")
            {
                ShipInfo = db.vwShipBankInfoes.Where(x => x.ControlShipInfoId == PortalContext.CurrentUser.ShipId).ToList();
            }
            else
            {
                ShipInfo = db.vwShipBankInfoes.Where(x => x.ControlShipInfoId == PortalContext.CurrentUser.BranchInfoIdentity).ToList();
            } 
            return ShipInfo;
        }
        public List<vwShipBankInfo> TMsgShipBankInfo()
        {
            var ShipInfo = new List<vwShipBankInfo>();
            if (PortalContext.CurrentUser.LoweredRoleName == "S" || PortalContext.CurrentUser.LoweredRoleName == "V")
            {
                ShipInfo = db.vwShipBankInfoes.Where(x => x.ControlShipInfoId != PortalContext.CurrentUser.ShipId).ToList();
            }
            else
            {
                ShipInfo = db.vwShipBankInfoes.Where(x => x.ControlShipInfoId != PortalContext.CurrentUser.BranchInfoIdentity).ToList();
            }
            return ShipInfo;
        }
        public ControlShipInfo FindOne(string id)
        {
            long control = Convert.ToInt32(id);
            return _controlShipInfoRepository.FindOne(x => x.ControlShipInfoId == control);
        }

        public long SaveControlAccoun(ControlShipInfo model)
        {
            int isSaved = 0;
            long ShipId = 0;
            ControlShipInfo exist = _controlShipInfoRepository.FindOne(x => x.ControlShipInfoId == model.ControlShipInfoId);
            if (exist == null)
            {
                if (model.ParentCode == 0)
                {
                    model.ControlLevel = 1;
                    model.SortOrder = 1;
                    model.LastUpdateDate = DateTime.Now;
                    model.LastUpdateId = PortalContext.CurrentUser.UserName;
                    model.CreateUserDate = DateTime.Now;
                    model.CreateUserId = PortalContext.CurrentUser.UserName;
                    isSaved = _controlShipInfoRepository.Save(model);
                    ShipId = model.ControlShipInfoId;
                    if (isSaved > 0)
                    {
                        AccountLevel(model);
                    }
                }
                else
                {
                    isSaved = _controlShipInfoRepository.Save(model);
                    ShipId = model.ControlShipInfoId;
                    if (isSaved > 0)
                    {
                        AccountLevel(model);
                    }
                }
            }
            else
            {
                exist.ParentCode = model.ParentCode;
                exist.ControlCode = model.ControlCode;
                exist.ControlName = model.ControlName;
                exist.ControlValue = model.ControlValue;
                exist.AdditionalValue = model.AdditionalValue;
                exist.SQD = model.SQD;
                exist.Organization = model.Organization;
                exist.ShipType = model.ShipType;
                exist.Sqdn = model.Sqdn;
                exist.Authority = model.Authority;
                exist.ContactNo = model.ContactNo;
                exist.CommotionDate = model.CommotionDate;
                exist.NickName = model.NickName;
                exist.ClassId = model.ClassId;
                exist.ShipsPackFile = model.ShipsPackFile;
                exist.DisplacementFullLoad = model.DisplacementFullLoad;
                exist.DisplacementLightWeight = model.DisplacementLightWeight;
                exist.Manufacturer = model.Manufacturer;
                exist.LengthMTR = model.LengthMTR;
                exist.DateOfCommission = model.DateOfCommission;
                exist.Breadth = model.Breadth;
                exist.HeightOfMast = model.HeightOfMast;
                exist.DraftAft = model.DraftAft;
                exist.Depth = model.Depth;
                exist.Depth1 = model.Depth1;
                exist.DraftFwd = model.DraftFwd;
                exist.MaximumSpeed = model.MaximumSpeed;
                exist.FreshWaterCapacity = model.FreshWaterCapacity;
                exist.MaximumContinuousSpeed = model.MaximumContinuousSpeed;
                exist.FualCapacity = model.FualCapacity;
                exist.CruisingSpeed = model.CruisingSpeed;
                exist.LuboilCapcity = model.LuboilCapcity;
                exist.EconomicSpeed = model.EconomicSpeed;
                exist.Address = model.Address;


                exist.Status = model.Status;
                exist.Remarks = model.Remarks;
                exist.ControlLevel = model.ControlLevel;
                exist.SortOrder = model.SortOrder;
                exist.IsActive = model.IsActive;
                exist.Level1 = exist.Level1;
                exist.Level2 = exist.Level2;
                exist.Level3 = exist.Level3;
                exist.Level4 = exist.Level4;
                exist.Level5 = exist.Level5;
                exist.LastUpdateDate = DateTime.Now;
                exist.LastUpdateId = PortalContext.CurrentUser.UserName;
                isSaved = _controlShipInfoRepository.Edit(exist);
                ShipId = exist.ControlShipInfoId;
            }
            
            return ShipId;
        }
        public List<ControlShipInfo> AllByControlLevel(int controlLevel)
        {
            if (PortalContext.CurrentUser.BranchInfoIdentity == 3)
            {
                return _controlShipInfoRepository.Filter(x => x.ParentCode == controlLevel && x.Organization==3).Include(x => x.CommonCode).Include(x => x.CommonCode1).Include(x => x.CommonCode2).Include(x => x.CommonCode3).ToList();
            }
            else
            {
                return _controlShipInfoRepository.Filter(x => x.ParentCode == controlLevel).Include(x => x.CommonCode).Include(x => x.CommonCode1).Include(x => x.CommonCode2).Include(x => x.CommonCode3).ToList();

            }

        }

        //public List<ControlShipInfo> AllByControlLevel(int controlLevel)
        //{
        //    return _controlShipInfoRepository.Filter(x => x.ParentCode == controlLevel).ToList();
        //}

        public List<vwPontoon> GetvwPontoons(long? controlId)
        {
            return _vwPontoonRepository.Filter(x => x.ParentCode == controlId).ToList();

        }

        public List<ControlShipInfo> GetShipAndShipCategory()
        {
           return  _controlShipInfoRepository.Filter(x => x.SortOrder == 0).ToList();
        }

        public List<ControlShipInfo> Totalship()
        {
            //var com =new CommonCode();

            //return _controlShipInfoRepository.Filter(x => x.Organization ==com.CommonCodeID).ToList();
          
        var a=_controlShipInfoRepository.All().Where(x => x.ControlLevel == 1).ToList();
            return a;

        }

        public int Delete(long controlShipInfoIdentity)
        {
            var oldData = _controlShipInfoRepository.FindOne(x => x.ControlShipInfoId == controlShipInfoIdentity);
            var childData = _controlShipInfoRepository.Filter(x => x.ParentCode == oldData.ControlCode);
            if (!childData.Any())
            {
                _controlShipInfoRepository.Delete(x => x.ControlShipInfoId == controlShipInfoIdentity);
                return 1;
            }
            return 0;
        }

        public int EditControlAccount(ControlShipInfo model, string[] controlnm, string[] controlvle)
        {
       
            ControlShipInfo oldData = _controlShipInfoRepository.FindOne(x => x.ControlShipInfoId == model.ControlShipInfoId);
            oldData.ControlName = controlnm[0];
            oldData.ControlValue = controlvle[0];
            oldData.AdditionalValue = model.AdditionalValue;
            oldData.Remarks = model.Remarks;
            return _controlShipInfoRepository.Edit(oldData);
       
        }

        public int SaveControlAccount(ControlShipInfo model, string[] controlnm, string[] controlvle)
        {
            
            int isSaved = 0;
          
            ControlShipInfo exist = _controlShipInfoRepository.FindOne(x => x.ControlCode == model.ControlCode);
            if (exist == null)
            {
                if (model.ParentCode == 0)
                {
                    model.ControlLevel = 1;
                    model.SortOrder = 1;
                    model.ControlName = controlnm[0];
                    model.ControlValue = controlvle[0];
                    isSaved = _controlShipInfoRepository.Save(model);
                    if (isSaved > 0)
                    {
                        AccountLevel(model);
                    }
                }
                else
                {

                  
                    for (int i =0;i< controlnm.Length;i++)
                    {
                            ControlShipInfo control =new ControlShipInfo();
                            control.ControlCode = model.ControlCode;
                            control.ControlLevel = model.ControlLevel;
                            control.IsActive = model.IsActive;
                            control.AdditionalValue = model.AdditionalValue;
                            control.Remarks = model.Remarks;
                            control.ControlName = controlnm[i];
                            control.ControlValue = controlvle[i];
                            control.SortOrder = model.SortOrder;
                            control.ParentCode = model.ParentCode;
                            if (i != 0)
                            {
                                model.ControlCode = model.ControlCode + i;
                                control.ControlCode = model.ControlCode;
                            }

                        
                            isSaved = _controlShipInfoRepository.Save(control);
                            if (isSaved > 0)
                            {
                            AccountLevel(model);
                            }
                    }
                  
                }
            }
            return isSaved;
       
        }

        public List<ControlShipInfo> GetAllShipList()
        {
            if (PortalContext.CurrentUser.LoweredRoleName == "S" || PortalContext.CurrentUser.LoweredRoleName == "V")
            {
                return _controlShipInfoRepository.All().Where(x => x.ParentCode == 0 && x.ControlShipInfoId== PortalContext.CurrentUser.ShipId).ToList();
            }
            else if(PortalContext.CurrentUser.BranchInfoIdentity == 3)
            {
                return _controlShipInfoRepository.All().Where(x => x.ParentCode == 0 && x.Organization==3).ToList();

            }
            {
                return _controlShipInfoRepository.All().Where(x => x.ParentCode == 0).ToList();
            }

        }


        private void AccountLevel(ControlShipInfo model)
        {

            ControlShipInfo oldData = _controlShipInfoRepository.FindOne(x => x.ControlCode == model.ControlCode);

            if (oldData != null)
            {

                if (oldData.ControlLevel == 1)
                {
                    model.Level1 = model.ControlCode.ToString(CultureInfo.InvariantCulture);
                }
                else if (oldData.ControlLevel == 2)
                {
                    oldData.Level1 = model.ParentCode.ToString(CultureInfo.InvariantCulture);
                    oldData.Level2 = model.ControlCode.ToString(CultureInfo.InvariantCulture);
                }
                else if (oldData.ControlLevel == 3)
                {
                    oldData.Level2 = model.ParentCode.ToString(CultureInfo.InvariantCulture);
                    oldData.Level3 = model.ControlCode.ToString(CultureInfo.InvariantCulture);
                    var l1 = _controlShipInfoRepository.FindOne(x => x.ControlCode == model.ParentCode);
                    oldData.Level1 = l1.ParentCode.ToString(CultureInfo.InvariantCulture);
                }
                else if (oldData.ControlLevel == 4)
                {
                    oldData.Level3 = model.ParentCode.ToString(CultureInfo.InvariantCulture);
                    oldData.Level4 = model.ControlCode.ToString(CultureInfo.InvariantCulture);
                    var l2 = _controlShipInfoRepository.FindOne(x => x.ControlCode == model.ParentCode);
                    oldData.Level2 = l2.ParentCode.ToString(CultureInfo.InvariantCulture);
                    var l1 = _controlShipInfoRepository.FindOne(x => x.ControlCode == l2.ParentCode);
                    oldData.Level1 = l1.ParentCode.ToString(CultureInfo.InvariantCulture);
                }
                else if (oldData.ControlLevel == 5)
                {
                    oldData.Level5 = model.ControlCode.ToString(CultureInfo.InvariantCulture);
                    oldData.Level4 = model.ParentCode.ToString(CultureInfo.InvariantCulture);
                    var l3 = _controlShipInfoRepository.FindOne(x => x.ControlCode == model.ParentCode);
                    oldData.Level3 = l3.ParentCode.ToString(CultureInfo.InvariantCulture);
                    var l2 = _controlShipInfoRepository.FindOne(x => x.ControlCode == l3.ParentCode);
                    oldData.Level2 = l2.ParentCode.ToString(CultureInfo.InvariantCulture);
                    var l1 = _controlShipInfoRepository.FindOne(x => x.ControlCode == l2.ParentCode);
                    oldData.Level1 = l1.ParentCode.ToString(CultureInfo.InvariantCulture);
                }
                _controlShipInfoRepository.Edit(oldData);
                ControlShipInfo con =new ControlShipInfo();
            }
        }

        public List<ControlShipInfo> GetControlShipInfoByOrganizationId(long organizationId)
        {
            return _controlShipInfoRepository.Filter(x => x.Organization == organizationId).ToList();
        }
    }
}
