using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Excel.Log;
using iTextSharp.text.pdf.qrcode;
using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.BLL.Manager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class ControlShipInfoController : BaseController
    {
        private IControlShipInfoManager _controlShipInfoManager;
        private IMachineryInfoManager _machineryInfoManager;
        private IShipCraftManager _shipInfoManager;
        private IMachinaryEquipmentInformationManager _machinaryEquipmentInformationManager;
        private IShipCraftManager _shipCraftManager;
        private ICommonCodeManager _commonCodeManager;
        private IShipcommOrgnaizationManager _shipcommOrgnaizationManager;
        private INewMachinaryEquipmentInformation_ResultManager _newMachinaryEquipmentInformationResultManager;
        private IDocumentationManager _documentationManager;
        private IBranchInfoManager _branchInfoManager;



        public ControlShipInfoController(IControlShipInfoManager controlShipInfoManager,
            IMachineryInfoManager machineryInfoManager, IShipCraftManager shipInfoManager,
            IMachinaryEquipmentInformationManager machinaryEquipmentInformationManager,
            IShipCraftManager shipCraftManager, ICommonCodeManager commonCodeManager,
            IShipcommOrgnaizationManager shipcommOrgnaizationManager,
            INewMachinaryEquipmentInformation_ResultManager newMachinaryEquipmentInformationResultManager, IDocumentationManager documentationManager, IBranchInfoManager branchInfoManager)
        {
            _controlShipInfoManager = controlShipInfoManager;
            _machineryInfoManager = machineryInfoManager;
            _shipInfoManager = shipInfoManager;
            _machinaryEquipmentInformationManager = machinaryEquipmentInformationManager;
            _shipCraftManager = shipCraftManager;
            _commonCodeManager = commonCodeManager;
            _shipcommOrgnaizationManager = shipcommOrgnaizationManager;
            _newMachinaryEquipmentInformationResultManager = newMachinaryEquipmentInformationResultManager;
            _documentationManager = documentationManager;
            _branchInfoManager = branchInfoManager;
        }

        public ActionResult Index(ControlAccountViewModel model)
        {
            model.ChartofAccounts = _controlShipInfoManager.GetChartofAccount();
            return View(model);
        }
        [HttpPost]
        public ActionResult ShipNameList(string Prefix)
        {
            List<ControlShipInfo> shipList = _controlShipInfoManager.GetAllShipList();
            var ShipList = (from N in shipList
                            where N.ControlName.Contains(Prefix.ToUpper())
                            select new { N.ControlName , N.ControlShipInfoId});
            return Json(ShipList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShipInfoDetails(string SearchKey, ControlAccountViewModel model)
        {
            if (PortalContext.CurrentUser.LoweredRoleName == "S" || PortalContext.CurrentUser.LoweredRoleName == "V")
            {
                model.ControlShipInfo = _controlShipInfoManager.GetControlShipInfoById(PortalContext.CurrentUser.ShipId);
            }
            else
            {
                model.ControlShipInfo = _controlShipInfoManager.GetControlShipInfoById(Convert.ToInt64(model.SearchKey));
            }

            model.OrgBranch = _branchInfoManager.GetOrgFromBankInfo();
            return View(model);
        }
        public ActionResult ShipCraftIndex(ControlShipInfo model)
        {

            return RedirectToAction("Index", "ShipCraft");
        }

        public ActionResult StateofShipIndex(Int64? id, ControlAccountViewModel model)
        {
            model.SqdCommonCodes = _commonCodeManager.GetCommonCodeByType("SQD");
            model.OrgBranch = _branchInfoManager.GetOrgFromBankInfo();
            model.ShipstatusCommonCodes = _commonCodeManager.GetCommonCodeByType("ShipStatus");
            var controlAccount = _controlShipInfoManager.GetControlAccountById(id);
            model.ParentCode = controlAccount.ParentCode;
            model.ShipTypes = _commonCodeManager.GetCommonCodeByType("Ship Type");
            model.ClassTypes = _commonCodeManager.GetCommonCodeByType("Class Type");
            if (controlAccount.ControlCode > 0)
                model.ControlCode = controlAccount.ControlCode;
            else
            {
                decimal controlCode = _controlShipInfoManager.GetMaxControlCode(model.ControlShipInfoId);
                if (controlCode > 1)
                {
                    model.ControlCode = controlCode;
                }
                else
                {
                    model.ControlCode = Convert.ToInt64(model.ParentCode + "" + controlCode);
                }
            }

            //model.ControlLevel = controlAccount.ControlLevel + 1;
            model.ControlLevel = controlAccount.ControlLevel;
            //model.SortOrder = controlAccount.SortOrder + 1;
            model.SortOrder = controlAccount.SortOrder;
            model.ControlName = controlAccount.ControlName;
            model.ControlValue = controlAccount.ControlValue;
            model.AdditionalValue = controlAccount.AdditionalValue;
            model.Remarks = controlAccount.Remarks;
            model.IsActive = true;
            model.ControlShipInfoId = controlAccount.ControlShipInfoId;
            model.SQD = controlAccount.SQD;
            model.Status = controlAccount.Status;
            model.Organization = controlAccount.Organization;
            model.ShipType = controlAccount.ShipType;
            model.Authority = controlAccount.Authority;
            model.Sqdn = controlAccount.Sqdn;
            model.ContactNo = controlAccount.ContactNo;
            model.CommotionDate = controlAccount.CommotionDate;
            model.NickName = controlAccount.NickName;
            model.ClassId = controlAccount.ClassId;
            model.ShipsPackFile = controlAccount.ShipsPackFile;
            model.DisplacementFullLoad = controlAccount.DisplacementFullLoad;
            model.DisplacementLightWeight = controlAccount.DisplacementLightWeight;
            model.Manufacturer = controlAccount.Manufacturer;
            model.LengthMTR = controlAccount.LengthMTR;
            model.DateOfCommission = controlAccount.DateOfCommission;
            model.Breadth = controlAccount.Breadth;
            model.HeightOfMast = controlAccount.HeightOfMast;
            model.DraftAft = controlAccount.DraftAft;
            model.Depth = controlAccount.Depth;
            model.Depth1 = controlAccount.Depth1;
            model.DraftFwd = controlAccount.DraftFwd;
            model.MaximumSpeed = controlAccount.MaximumSpeed;
            model.FreshWaterCapacity = controlAccount.FreshWaterCapacity;
            model.MaximumContinuousSpeed = controlAccount.MaximumContinuousSpeed;
            model.FualCapacity = controlAccount.FualCapacity;
            model.CruisingSpeed = controlAccount.CruisingSpeed;
            model.LuboilCapcity = controlAccount.LuboilCapcity;
            model.EconomicSpeed = controlAccount.EconomicSpeed;
            model.Address = controlAccount.Address;

            ModelState.Clear();
            return View(model);
            //ModelState.Clear();
            //model.ControlShipInfos = _controlShipInfoManager.GetAll();
            //var costCentre = _controlShipInfoManager.FindOne(id) ?? new ControlShipInfo();
            //model.ControlShipInfo = costCentre;
            //return View(model);
        }

        public ActionResult StateOfShipIndexSave(ControlShipInfo model)
        {
            
              long saveIndex = 0;

            saveIndex = _controlShipInfoManager.SaveControlAccoun(model);
            if (saveIndex > 0)
            {
                //ModelState.Clear();
                TempData["Msg"] = "Data has been saved";
            }

            ControlAccountViewModel viewModel = new ControlAccountViewModel();
            return RedirectToAction("ShipInfoDetails", new { SearchKey = saveIndex });
        }

        //public ActionResult StateOfShipIndexEdit(ControlShipInfo model)
        //{
        //    var saveIndex = 0;

        //    saveIndex = _controlShipInfoManager.EditStateOfShipIndex(model);
        //    if (saveIndex > 0)
        //    {
        //        ModelState.Clear();
        //        TempData["Msg"] = "Data Updated Successfully";
        //    }

        //    ControlAccountViewModel viewModel = new ControlAccountViewModel();
        //    return View("StateofShipIndex", viewModel);
        //}
        public ActionResult IndexDisplay(ControlAccountViewModel model)
        {
            model.ControlShipInfos = new List<ControlShipInfo>(_controlShipInfoManager.AllByControlLevel(0).OrderByDescending(x => x.LastUpdateDate ));

            model.vwPontoons= new List<vwPontoon>(_controlShipInfoManager.GetvwPontoons(0).OrderByDescending(x => x.LastUpdateDate));

            foreach (var item in model.ControlShipInfos.ToList())
            {
                if (item.CommonCode3 == null)
                {
                    item.CommonCode3 = new CommonCode();
                }
            }
            
            model.OrgBranch = _branchInfoManager.GetOrgFromBankInfo();
            foreach (var item in model.OrgBranch)
            {
                item.Opl = _branchInfoManager.TotalOpl(item.BranchInfoIdentity);
                item.NonOpl = _branchInfoManager.TotalNonOpl(item.BranchInfoIdentity);
            }
            
            return View(model);
        }
        public ActionResult IndexOrgBranchDisplay(string OrgBranchId, ControlAccountViewModel model)
        {
            int organizationId = Convert.ToInt32(OrgBranchId);
            model.ControlShipInfos = new List<ControlShipInfo>(_controlShipInfoManager.AllByControlLevel(0).Where(x=>x.Organization==organizationId).OrderByDescending(x => x.LastUpdateDate));
            model.vwPontoons = new List<vwPontoon>(_controlShipInfoManager.GetvwPontoons(0).OrderByDescending(x => x.LastUpdateDate));
            foreach (var item in model.ControlShipInfos.ToList())
            {
                if (item.CommonCode3 == null)
                {
                    item.CommonCode3 = new CommonCode();
                }
            }

            model.OrgBranch = _branchInfoManager.GetOrgFromBankInfo().Where(x => x.BranchInfoIdentity == organizationId).ToList();
            model.OrgBranchName = model.OrgBranch.Select(x=>x.BranchName).SingleOrDefault();
            return View(model);
        }
        public ActionResult Create(string controlShipInfoId, string type, ControlAccountViewModel model)
        {
            ModelState.Clear();
            model.ControlShipInfoId = Convert.ToInt64(controlShipInfoId);
            if (type == "Create")
            {
                if (model.ControlShipInfoId > 0)
                {
                    var controlAccount = _controlShipInfoManager.GetControlAccountById(model.ControlShipInfoId);
                    model.ParentCode = controlAccount.ControlCode;
                    decimal controlCode = _controlShipInfoManager.GetMaxControlCode(model.ControlShipInfoId);
                    if (controlCode > 1)
                    {
                        model.ControlCode = controlCode;
                    }
                    else
                    {
                        model.ControlCode = Convert.ToUInt64(model.ParentCode + "00" + controlCode);
                    }
                    model.ControlLevel = controlAccount.ControlLevel + 1;

                    model.SortOrder = controlAccount.SortOrder + 1;
                    model.controlnm = null;
                    model.ControlValue = "";
                    model.AdditionalValue = "";
                    model.Remarks = "";
                    model.IsActive = true;
                    model.ControlShipInfoId = 0;
                }
            }
            else
            {
                string[] connam = new string[3];
                string[] conval = new string[3];
                var controlAccount = _controlShipInfoManager.GetControlAccountById(model.ControlShipInfoId);
                model.ParentCode = controlAccount.ParentCode;
                model.ControlCode = controlAccount.ControlCode;
                model.ControlLevel = controlAccount.ControlLevel;
                model.SortOrder = controlAccount.SortOrder;
                connam[0] = controlAccount.ControlName;
                conval[0] = controlAccount.ControlValue;
                model.controlnm = connam;
                model.controlvle = conval;
                model.AdditionalValue = controlAccount.AdditionalValue;
                model.Remarks = controlAccount.Remarks;
                model.IsActive = true;
            }

            return View(model);
        }

        public ActionResult SaveControlAccount(string[] controlnm, string[] controlvle, ControlShipInfo model)
        {
            var obj = new ControlAccountViewModel();

            long firstIdentity = model.ControlShipInfoId;

            var saveIndex = 0;
            saveIndex = model.ControlShipInfoId > 0
                ? _controlShipInfoManager.EditControlAccount(model, controlnm, controlvle)
                : _controlShipInfoManager.SaveControlAccount(model, controlnm, controlvle);
            if (firstIdentity > 0)
            {
                TempData["Msg"] = "Data has been updated";
                obj.ControlShipInfoId = model.ControlShipInfoId;
                obj.ParentCode = model.ParentCode;
                obj.ControlCode = model.ControlCode;
                obj.ControlLevel = model.ControlLevel;
                obj.SortOrder = model.ControlLevel;
                obj.ControlName = model.ControlName;
                obj.IsActive = true;
                
            }
            else
            {
                TempData["Msg"] = "Data has been saved";
                obj.ParentCode = model.ParentCode;
                obj.ControlCode = model.ControlCode + 1;
                obj.ControlLevel = model.ControlLevel;
                obj.SortOrder = model.ControlLevel;
                obj.ControlName = model.ControlName;
                obj.IsActive = true;
                

            }
            return RedirectToAction("Create", new { ParentCode = model.ParentCode, Type = "Create" });
        }
        public ActionResult DownloadA(int id)
        {
            var Doc = _documentationManager.FindOne(id);
            if (Doc != null)
            {
                var path = _documentationManager.FindOne(id).ImageUrl;
                if (!System.IO.File.Exists(Server.MapPath(path))) return null;
                var fileName = path.Split('\\').ElementAt(1);
                return File(Server.MapPath(path), System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult DownloadImage(int id)
        {
            var Doc = _documentationManager.FindImage(id);
            if (Doc != null)
            {
                var path = _documentationManager.FindImage(id).ImageUrl;
                if (!System.IO.File.Exists(Server.MapPath(path))) return null;
                var fileName = path.Split('\\').ElementAt(1);
                return File(Server.MapPath(path), System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult CreateWithoutLoad(ControlAccountViewModel model)
        {
            ModelState.Clear();
            return View("Create", model);
        }

        //public ActionResult StateOfShip(string id, ControlAccountViewModel model)
        //{
        //    ModelState.Clear();
        //    model.ControlShipInfos = _controlShipInfoManager.GetAll();
        //    var costCentre = _controlShipInfoManager.FindOne(id) ?? new ControlShipInfo();
        //    model.ControlShipInfo = costCentre;
        //    return View(model);
        //}

        public ActionResult Delete(ControlAccountViewModel model)
        {
            var controlAccount = _controlShipInfoManager.GetControlAccountById(model.ControlShipInfoId);
            model.ControlCode = controlAccount.ControlCode;
            model.ParentCode = controlAccount.ParentCode;
            model.ControlLevel = controlAccount.ControlLevel;
            model.SortOrder = controlAccount.SortOrder;
            model.ControlName = controlAccount.ControlName;
            model.ControlValue = controlAccount.ControlValue;
            model.AdditionalValue = controlAccount.AdditionalValue;
            model.Remarks = controlAccount.Remarks;
            model.IsActive = true;
            return View(model);
        }

        public ActionResult DeleteX(ControlAccountViewModel model)
        {
            int isDeleted = _controlShipInfoManager.Delete(model.ControlShipInfoId);
            if (isDeleted == 1)
            {
                TempData["Msg"] = "Data has been deleted.";
            }
            else
            {
                TempData["Msg"] = "Data is not deleted.";
            }
            return RedirectToAction("Delete");
        }

        public ActionResult Refresh()
        {

            return RedirectToAction("Index");
        }

        public ActionResult ShipPowerEdit(ControlShipInfo model)
        {

            return RedirectToAction("Edit", "ShipPower");
        }

        public ActionResult ShipspeedTrialEdit(ControlShipInfo model)
        {

            return RedirectToAction("Edit", "ShipSpeedTrial");
        }
        public ActionResult SearchByKey(ControlAccountViewModel model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.Trim().ToLower();
                model.OrgBranch = _branchInfoManager.GetOrgFromBankInfo();
                model.ControlShipInfos =
                   _controlShipInfoManager.AllByControlLevel(0).Where(
                            x =>
                                x.ControlName.ToLower().Contains(searchKey))
                        .ToList();
                foreach (var item in model.ControlShipInfos.ToList())
                {
                    if (item.CommonCode3 == null)
                    {
                        item.CommonCode3 = new CommonCode();
                    }
                }
            }
            else
            {
                model.ControlShipInfos = _controlShipInfoManager.GetAll();
            }
            return View("IndexDisplay", model);
        }

        public FileResult ExportTo(string reportType, string searchKey, string id)
        {
            var localReport = new LocalReport();
            var model = new ShipInfoViewModel();

            if (searchKey != null)
            {
                searchKey = searchKey.Trim().ToLower();
                model.ShipInfoes = _shipCraftManager.GetAll()
                    .Where(
                        x =>
                            x.ShipId.ToLower().Contains(searchKey) || x.ShipName.ToLower().Contains(searchKey) ||
                            x.CommonCode.TypeValue.ToLower().Contains(searchKey) ||
                            x.ShipId.ToLower().Contains(searchKey))
                    .ToList();

            }
            else
            {
                model.ControlShipInfos = _controlShipInfoManager.GetAll();
                model.ShipInfoes = _shipInfoManager.GetAll();
            }
            var reportDataList = model.ShipInfoes.Select(item => new vwShipInfo()
            {
                OrgName = PortalContext.CurrentUser.BankName,
                OfficeName = PortalContext.CurrentUser.BranchName,
                Parameter = "STATE OF SHIPS AND CRAFTS",
                ReportName = "Refit Docking Interval And Duration Of BN Ships/Craft",
                ShipId = item.ShipId,
                ShipName = item.ShipName,
                Organazation = _branchInfoManager.FineOneByBranchInfoIdentity(item.ControlShipInfo.Organization ?? 0).BranchName.ToString(),
                ControlName = item.ShipName,
               //CallSign =item.ControlValue,
                SQD = item.CommonCode.TypeValue,
                RefitInterval = item.RefitInterval ?? 0,
                RefitIntervalType = item.CommonCode2.TypeValue,
                RefitDuration = item.RefitDuration ?? 0,
                RefitDurationType = item.CommonCode1.TypeValue,
                DockingDuration = item.DockingDuration ?? 0,
                DockingDurationType = item.CommonCode.TypeValue,
                LastRefitDate = item.LastRefitDate,
                LastDockingDate = item.LastDockingDate,
                NextRefitDate = item.NextRefitDate,
                NextDockingDate = item.NextDockingDate,
                Length = item.Length,
                Breadth = item.Breadth,
                DraughtAFT = item.DraughtAFT,
                DraughtFWD = item.DraughtFWD,
                Displacement = item.Displacement,
                PropellerQty = item.PropellerQty,
                RudderQty = item.RudderQty,
                SpeedMax = item.SpeedMax,
                SpeedCont = item.SpeedCont,
                SpeedEcon = item.SpeedEcon,
                SpeedMin = item.SpeedMin,
                TankCapacityFW = item.TankCapacityFW,
                TankCapacityFuel = item.TankCapacityFuel,
                TankCapacityLuboil = item.TankCapacityLuboil,
                Remarks = item.Remarks
            }).ToList();
            if (id == "Basic")
            {
                localReport.ReportPath = Server.MapPath("~/Reports/BNS/ShipBasicInfo.rdlc");
            }
            else if (id == "RefitDockingDuration")
            {
                localReport.ReportPath = Server.MapPath("~/Reports/BNS/RefitDockinIntervalAndDuration.rdlc");
            }

            var reportDataSource = new ReportDataSource {Name = "Report"};
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;

            byte[] renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding,
                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "inline; filename=Urls." + fileNameExtension);

            return File(renderedBytes, fileNameExtension);

        }

        public FileResult ExportTo1(string reportType, string searchKey, string id)
        {
            var localReport = new LocalReport();
            var model = new ControlAccountViewModel();

            if (searchKey != null)
            {
                searchKey = searchKey.Trim().ToLower();
                model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0).OrderBy(x =>x.SQD)
                    .Where(
                        x =>
                            x.ControlName.ToLower().Contains(searchKey))
                    .ToList();

            }
            else
            {
                model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
                model.ControlShipInfos = new List<ControlShipInfo>(_controlShipInfoManager.AllByControlLevel(0).OrderBy(x => x.SQD));

            }
            var reportDataList = model.ControlShipInfos.Select(item => new vwControlShipInfo()
            {
                
                ControlShipInfoId = item.ControlShipInfoId,
                ControlCode = item.ControlCode,
                SqdId = item.SQD,
                SQD = _commonCodeManager.GetCommonName(item.SQD),
                ControlValue = item.ControlValue,
                ControlName = item.ControlName ?? "",
                ParentCode = item.ParentCode,
                Organization = item.Organization,
                Organazation = _branchInfoManager.BranchName(item.Organization),
                Status = item.Status,
                ControlLevel = item.ControlLevel,
                Remarks = item.Remarks
            }).ToList();
            if (id == "Basic")
            {
                localReport.ReportPath = Server.MapPath("~/Reports/BNS/ShipBasicInfo.rdlc");
            }
           

            var reportDataSource = new ReportDataSource { Name = "Report" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;

            byte[] renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding,
                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "inline; filename=Urls." + fileNameExtension);

            return File(renderedBytes, fileNameExtension);

        }
       public FileResult OplReport(string reportType, string searchKey)
        {
            var localReport = new LocalReport();
            var model = new ShipcommOrgnaizationViewModel();

            if (searchKey != null)
            {
                searchKey = searchKey.Trim().ToLower();
                model.ShipcommOrgnaizations = _shipcommOrgnaizationManager.GetAll()
                    .Where(
                        x =>
                            x.ORG.ToLower().Contains(searchKey) || x.ControlName.ToLower().Contains(searchKey))
                    .ToList();

            }
            else
            {
                model.ShipcommOrgnaizations = _shipcommOrgnaizationManager.GetAll();
            }

            var reportDataList = model.ShipcommOrgnaizations.Select(item => new vwShipcommOrgnaization()
            {
                
                ParentCode = Convert.ToDecimal(item.ParentCode),
                today = item.today,
                ControlShipInfoId = item.ControlShipInfoId,
                ControlLevel = Convert.ToInt32(item.ControlLevel),
                ControlName = item.ControlName,
                statusCode = item.statusCode,
                Organization = Convert.ToInt32(item.Organization),
                ORG = item.ORG,
                Status = item.Status,
                Remarks = item.Remarks,
                TotalShip = item.TotalShip,
                NonOpl = item.NonOpl,
                Opl = item.Opl
               

            }).ToList();


            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            localReport.ReportPath = Server.MapPath("~/Reports/BNS/Org.rdlc");
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;

            byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=Urls." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);

        }

        public ActionResult DownloadReport(string controlShipInfoId)
        {
            string id = controlShipInfoId;
            var localReport = new LocalReport();
            var model = new NewMachinaryEquipmentInformationViewModel_ResultViewModel();
            model.MachinaryEquipmentInfos = _newMachinaryEquipmentInformationResultManager.GetShipId(id);

            var reportDataList = model.MachinaryEquipmentInfos.Select(item => new vwNewMachinaryEquipmentInformation_Result()
            {
               SlNo = item.SlNo,
               shipId = item.shipId,
               ParentCode= item.ParentCode,
               FirstLevel=item.FirstLevel,
               FirstHead=item.FirstHead,
               SecondLevel=item.SecondLevel,
               SecondHead  =item.SecondHead,
               ThirdLevel=item.ThirdLevel,
               ThirdHead = item.ThirdHead,
               ControlCode=item.ControlCode,
               ControlName=item.ControlName,
               GenInfo=item.GenInfo,
               Shafting=item.Shafting,
               MainThrusrShift = item.MainThrusrShift,
               MiddleShaft = item.MiddleShaft,
               AuxiliaryShaft = item.AuxiliaryShaft,
               PropellerShaft = item.PropellerShaft,
               ControlValue = item.ControlValue,
               AdditionalValue = item.AdditionalValue,
               Remarks = item.Remarks,
               ControlLevel = item.ControlLevel,
               SortOrder = item.SortOrder,
               Level1 = item.Level1,
               Level2 = item.Level2,
               Level3=item.Level3,
               Level4 = item.Level4,
               Level5 = item.Level5,
               ShipName = item.ShipName,
               Genaralgroup = item.Genaralgroup,
               Description = item.Description,
               Speed = item.Speed,
               Duration = item.Duration,
               EnduranceHr = item.EnduranceHr,
               EnduranceNm = item.EnduranceNm,
               ShaftSpeed = item.ShaftSpeed,
               Power = item.Power,
               FuelConsumption = item.FuelConsumption,
               Descriptiom = item.Descriptiom,
               PortSpeed = item.PortSpeed,
               StbdSpeed = item.StbdSpeed,
               StableSpeed = item.StableSpeed
            }).ToList();

            var reportDataSource = new ReportDataSource { Name = "DataSet1" };////////////1///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;
            localReport.ReportPath = Server.MapPath("~/Reports/BNS/NewMachineryInfo.rdlc");
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;

            byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "Inline; filename=Urls." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);
       
        }

        public ActionResult Download(string controlShipInfoId)
        {
            string id = controlShipInfoId;
            var localReport = new LocalReport();
            var asdf = _machinaryEquipmentInformationManager.GetShipId(id);

            var reportDataList = asdf.Select(item => new vwMachinaryEquipmentInformation
            {
                OrgName = "Bangladesh Navy",
                OfficeName = PortalContext.CurrentUser.BankName,
                Parameter = "Ship's Basic Information",
                ReportName = "Machinery Parameters - " + item.ShipName,
                ParentCode = Convert.ToDecimal(item.ParentCode),
                ShipName = item.ShipName ?? item.ControlName,
                ControlCode = Convert.ToDecimal(item.ControlCode),
                ControlName = item.ControlName ?? "",
                GenInfo = item.GenInfo ?? "",
                Shafting = item.Shafting ?? "",
                MainThrusrShift = item.MainThrusrShift ?? "",
                MiddleShaft = item.MiddleShaft ?? "",
                AuxiliaryShaft = item.AuxiliaryShaft ?? "",
                PropellerShaft = item.PropellerShaft ?? "",
                ControlValue = item.ControlValue ?? "",
                AdditionalValue = item.AdditionalValue ?? "",
                Remarks = item.Remarks ?? "",
                ControlLevel = Convert.ToInt32(item.ControlLevel),
                SortOrder = Convert.ToInt32(item.SortOrder),
                Level1 = item.Level1,
                Level2 = item.Level2,
                Level3 = item.Level3,
                Level4 = item.Level4,
                Level5 = item.Level5,
                Genaralgroup = item.Genaralgroup ?? "",
                Description = item.Description ?? "",
                Speed = Convert.ToDecimal(item.Speed),
                Duration = Convert.ToDecimal(item.Duration),
                EnduranceHr = Convert.ToDecimal(item.EnduranceHr),
                EnduranceNm = Convert.ToInt32(item.EnduranceNm),
                ShaftSpeed = Convert.ToInt32(item.ShaftSpeed),
                Power = Convert.ToDecimal(item.Power),
                FuelConsumption = Convert.ToDecimal(item.FuelConsumption),
                Descriptiom = item.Descriptiom,
                PortSpeed = Convert.ToDecimal(item.PortSpeed),
                StbdSpeed = Convert.ToDecimal(item.StbdSpeed),
                StableSpeed = Convert.ToDecimal(item.StableSpeed)
            }).ToList();



            var reportDataSource = new ReportDataSource { Name = "DataSet1" };////////////1///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            var GenInfo = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "01");
            reportDataSource = new ReportDataSource { Name = "GenInfo" };////////////2///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = GenInfo;

            var PrincipalDiamension = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "02");
            reportDataSource = new ReportDataSource { Name = "PrincipalDiamension" };////////////3///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = PrincipalDiamension;

            var Tonnage = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "03");
            reportDataSource = new ReportDataSource { Name = "Tonnage" };////////////4///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Tonnage;

            var Displacement = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "04");
            reportDataSource = new ReportDataSource { Name = "Displacement" };////////////5///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Displacement;

            var Draught = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "05");
            reportDataSource = new ReportDataSource { Name = "Draught" };////////////6///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Draught;

            var CapacityOftanks = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "06");
            reportDataSource = new ReportDataSource { Name = "CapacityOftanks" };////////////7///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = CapacityOftanks;

            var Storage = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "07");
            reportDataSource = new ReportDataSource { Name = "Storage" };////////////8///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Storage;

            var stabilityCondition = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "08");
            reportDataSource = new ReportDataSource { Name = "StabilityCondition" };////////////9///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = stabilityCondition;

            var MomentTocharge = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "09");
            reportDataSource = new ReportDataSource { Name = "MomentTocharge" };////////////10///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = MomentTocharge;

            var TableOfSpeedPowerAndEndurance = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.Description != null);
            reportDataSource = new ReportDataSource { Name = "TableOfSpeedPowerAndEndurance" };////////////11///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = TableOfSpeedPowerAndEndurance;

            var CopyOfFuelConsumption = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "13");
            reportDataSource = new ReportDataSource { Name = "CopyOfFuelConsumption" };////////////12///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = CopyOfFuelConsumption;

            var Rudder = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "14");
            reportDataSource = new ReportDataSource { Name = "Rudder" };////////////13///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Rudder;

            var Propellers = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "15");
            reportDataSource = new ReportDataSource { Name = "Propellers" };////////////14///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Propellers;

            var Shafting = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "16" | x.ParentCode.ToString() == id + "1603" | x.ParentCode.ToString() == id + "1604" | x.ParentCode.ToString() == id + "1605" | x.ParentCode.ToString() == id + "1606");
            reportDataSource = new ReportDataSource { Name = "Shafting" };////////////15///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Shafting;



            var SternTube = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "18");
            reportDataSource = new ReportDataSource { Name = "SternTube" };////////////16///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = SternTube;

            var PlummerBlock = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "19");
            reportDataSource = new ReportDataSource { Name = "PlummerBlock" };////////////17///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = PlummerBlock;

            var BulkheadGland = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "20");
            reportDataSource = new ReportDataSource { Name = "BulkheadGland" };////////////18///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = BulkheadGland;

            var SideMountedBearing = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "21");
            reportDataSource = new ReportDataSource { Name = "SideMountedBearing" };////////////19///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = SideMountedBearing;

            var Cuplings = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "22");
            reportDataSource = new ReportDataSource { Name = "Cuplings" };////////////20///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Cuplings;

            var GearBoxes = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "23");
            reportDataSource = new ReportDataSource { Name = "GearBoxes" };////////////21///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = GearBoxes;


            var MainEngine = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "24" | x.ParentCode.ToString() == id + "2416" | x.ParentCode.ToString() == id + "2417" | x.ParentCode.ToString() == id + "2418" | x.ParentCode.ToString() == id + "2419" | x.ParentCode.ToString() == id + "2420" | x.ParentCode.ToString() == id + "2421" | x.ParentCode.ToString() == id + "2422" | x.ParentCode.ToString() == id + "2423" | x.ParentCode.ToString() == id + "2424" | x.ParentCode.ToString() == id + "2425" | x.ParentCode.ToString() == id + "2426" | x.ParentCode.ToString() == id + "2427" | x.ParentCode.ToString() == id + "2428" | x.ParentCode.ToString() == id + "2429" && x.ControlCode.ToString().StartsWith(id + "24"));
            reportDataSource = new ReportDataSource { Name = "MainEngine" };///////////22///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = MainEngine;

            var Steeringsystem = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "25" | x.ControlCode.ToString().StartsWith(id + "2502") | x.ControlCode.ToString().StartsWith(id + "2505") | x.ControlCode.ToString().StartsWith(id + "2506"));
            reportDataSource = new ReportDataSource { Name = "Steeringsystem" };////////////23///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Steeringsystem;

            var Stabilizers = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "26");
            reportDataSource = new ReportDataSource { Name = "Stabilizers" };////////////24///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Stabilizers;

            var ControllablePitchProperller = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "27");
            reportDataSource = new ReportDataSource { Name = "ControllablePitchProperller" };////////////25///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = ControllablePitchProperller;

            var MainGeneratorSet = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "28" | x.ControlCode.ToString().StartsWith(id + "2801"));
            reportDataSource = new ReportDataSource { Name = "MainGeneratorSet" };////////////26///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = MainGeneratorSet;

            var EngineParticulars = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "28" | x.ControlCode.ToString().StartsWith(id + "2802"));
            reportDataSource = new ReportDataSource { Name = "EngineParticulars" };////////////27///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = EngineParticulars;

            var houbourGen = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "29" && x.ControlCode.ToString().StartsWith(id + "2901") | x.ControlCode.ToString().StartsWith(id + "2902"));
            reportDataSource = new ReportDataSource { Name = "houbourGen" };////////////28///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = houbourGen;

            var GeneralParticulars = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ControlCode.ToString().StartsWith(id + "2903"));
            reportDataSource = new ReportDataSource { Name = "GeneralParticulars" };////////////29///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = GeneralParticulars;

            var EnginParticulars = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ControlCode.ToString().StartsWith(id + "2904"));
            reportDataSource = new ReportDataSource { Name = "EnginParticulars" };////////////30///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = EnginParticulars;

            var MineweepingGensets = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "30" && x.ControlCode.ToString().StartsWith(id + "3001") | x.ControlCode.ToString().StartsWith(id + "3002"));
            reportDataSource = new ReportDataSource { Name = "MineweepingGensets" };////////////31///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = MineweepingGensets;

            var MineweepingGenaralperticulars = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "30" | x.ControlCode.ToString().StartsWith(id + "3003"));
            reportDataSource = new ReportDataSource { Name = "MineweepingGenaralperticulars" };////////////32///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = MineweepingGenaralperticulars;

            var MainCompressor = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "31" | x.ControlCode.ToString().StartsWith(id + "3111") | x.ControlCode.ToString().StartsWith(id + "3112"));
            reportDataSource = new ReportDataSource { Name = "MainCompressor" };////////////33///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = MainCompressor;

            var EmergencyAirCompressor = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "32");
            reportDataSource = new ReportDataSource { Name = "EmergencyAirCompressor" };////////////34///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = EmergencyAirCompressor;

            var Airbottales = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "33");
            reportDataSource = new ReportDataSource { Name = "Airbottales" };////////////35///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Airbottales;

            var Deepfreeze = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "35");
            reportDataSource = new ReportDataSource { Name = "Deepfreeze" };////////////36///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Deepfreeze;

            var AirConditioningPlant = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "36");
            reportDataSource = new ReportDataSource { Name = "AirConditioningPlant" };////////////37///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = AirConditioningPlant;

            var AirConditioningUnit = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "37");
            reportDataSource = new ReportDataSource { Name = "AirConditioningUnit" };////////////38///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = AirConditioningUnit;


            var FreshWaterGenerator = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "38");
            reportDataSource = new ReportDataSource { Name = "FreshWaterGenerator" };////////////39///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = FreshWaterGenerator;

            var FreshWaterPump = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "39");
            reportDataSource = new ReportDataSource { Name = "FreshWaterPump" };////////////40///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = FreshWaterPump;

            var FireBilgePump = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "40");
            reportDataSource = new ReportDataSource { Name = "FireBilgePump" };////////////41///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = FireBilgePump;

            var PreWettingPump = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "41");
            reportDataSource = new ReportDataSource { Name = "PreWettingPump" };////////////42///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = PreWettingPump;

            var GeneralServicePump = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "42");
            reportDataSource = new ReportDataSource { Name = "GeneralServicePump" };////////////43///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = GeneralServicePump;

            var MainGearboxLubeOilPump = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "43");
            reportDataSource = new ReportDataSource { Name = "MainGearboxLubeOilPump" };////////////44///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = MainGearboxLubeOilPump;

            var GearboxLubeOilCoolingPump = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "44");
            reportDataSource = new ReportDataSource { Name = "GearboxLubeOilCoolingPump" };////////////45///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = GearboxLubeOilCoolingPump;

            var LubeOilTansferPump = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "45");
            reportDataSource = new ReportDataSource { Name = "LubeOilTansferPump" };////////////46///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = LubeOilTansferPump;

            var FuleOilTansferPump = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "46");
            reportDataSource = new ReportDataSource { Name = "FuleOilTansferPump" };////////////47///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = FuleOilTansferPump;

            var LubOilPrimingPump = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "47");
            reportDataSource = new ReportDataSource { Name = "LubOilPrimingPump" };////////////48///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = LubOilPrimingPump;

            var HotFreshWaterCircularPump = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "48");
            reportDataSource = new ReportDataSource { Name = "HotFreshWaterCircularPump" };////////////49///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = HotFreshWaterCircularPump;

            var SbmersiblePump = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "49");
            reportDataSource = new ReportDataSource { Name = "SbmersiblePump" };////////////50///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = SbmersiblePump;

            var PortablediseselfirefightingPump = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "50");
            reportDataSource = new ReportDataSource { Name = "PortablediseselfirefightingPump" };////////////51///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = PortablediseselfirefightingPump;

            var LubOilCentrifuge = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "51");
            reportDataSource = new ReportDataSource { Name = "LubOilCentrifuge" };////////////52///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = LubOilCentrifuge;

            var LatheMachine = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "52");
            reportDataSource = new ReportDataSource { Name = "LatheMachine" };////////////53///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = LatheMachine;

            var DrillMachine = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "53");
            reportDataSource = new ReportDataSource { Name = "DrillMachine" };////////////54///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = DrillMachine;

            var GrindingMachine = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "54");
            reportDataSource = new ReportDataSource { Name = "GrindingMachine" };////////////55///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = GrindingMachine;

            var Anchors = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "55");
            reportDataSource = new ReportDataSource { Name = "Anchors" };////////////56///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Anchors;

            var Cable = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "56");
            reportDataSource = new ReportDataSource { Name = "Cable" };////////////57///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Cable;

            var MotorBoat = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "57" | x.ControlCode.ToString().StartsWith(id + "5711"));
            reportDataSource = new ReportDataSource { Name = "MotorBoat" };////////////58///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = MotorBoat;

            var InflatableBoat = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "58");
            reportDataSource = new ReportDataSource { Name = "InflatableBoat" };////////////59///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = InflatableBoat;

            var System1211 = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "59");
            reportDataSource = new ReportDataSource { Name = "System1211" };////////////60///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = System1211;

            var FireWarning = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "60");
            reportDataSource = new ReportDataSource { Name = "FireWarning" };////////////61///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = FireWarning;

            var DomessticReffrigerator = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "61");
            reportDataSource = new ReportDataSource { Name = "DomessticReffrigerator" };////////////62///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = DomessticReffrigerator;

            var PrimeMoverEngine = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "62");
            reportDataSource = new ReportDataSource { Name = "PrimeMoverEngine" };////////////63///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = PrimeMoverEngine;

            var OilyWaterSeparator = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "63");
            reportDataSource = new ReportDataSource { Name = "OilyWaterSeparator" };////////////64///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = OilyWaterSeparator;

            var Davit = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "64");
            reportDataSource = new ReportDataSource { Name = "Davit" };////////////65///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Davit;

            var DivingAirCompressor = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "65");
            reportDataSource = new ReportDataSource { Name = "DivingAirCompressor" };////////////66///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = DivingAirCompressor;

            var OilDispersantUnit = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "66");
            reportDataSource = new ReportDataSource { Name = "OilDispersantUnit" };////////////67///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = OilDispersantUnit;

            var StructureInformation = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "67");
            reportDataSource = new ReportDataSource { Name = "StructureInformation" };////////////68///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = StructureInformation;

            var ABracket = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == id + "17");
            reportDataSource = new ReportDataSource { Name = "ABracket" };////////////69///////
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = ABracket;

            localReport.ReportPath = Server.MapPath("~/Reports/BNS/MachineryInfo.rdlc");
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;

            byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "Inline; filename=Urls." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);

        }


    }
}