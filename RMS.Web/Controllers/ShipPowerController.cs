using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.BLL.Manager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using Microsoft.AspNet.Identity;

namespace RMS.Web.Controllers
{
    public class ShipPowerController : BaseController
    {
        private readonly IShipPowerManager _shipPowerManager;
        private readonly IShipInfoManager _shipInfoManager;
        private readonly IControlShipInfoManager _controlShipInfoManager;
        private IMachinaryEquipmentInformationManager _machinaryEquipmentInformationManager;
        public ShipPowerController()
        {
            _shipPowerManager = new ShipPowerManager();
            _shipInfoManager = new ShipInfoManager();
            _machinaryEquipmentInformationManager =  new MachinaryEquipmentInformationManager();
            _controlShipInfoManager= new ControlShipInfoManager();
        }

        public ActionResult Index(ShipPowerViewModel model)
        {
            var ConShip = _controlShipInfoManager.ShipBranchInfo();
            var ShipPw = _shipPowerManager.GetAll();
            var query = (from post in ShipPw join meta in ConShip on post.ShipId equals meta.ControlShipInfoId select post).ToList();
            model.ShipPowers = query;
            return View(model);
        }

        public ActionResult Searchbykey(ShipPowerViewModel model)
        {
            string searchkey = "";
            if (model.SearchKey != null)
            {
                searchkey = model.SearchKey.ToLower();
                model.ShipPowers = _shipPowerManager.GetAll().Where(x=>x.ControlShipInfo.ControlName.ToLower().Contains(searchkey)).ToList();
            }
            else
            {
                model.ShipPowers = _shipPowerManager.GetAll();
            }

            return View("Index", model);
        }

        public ActionResult ShipControlIndex(ShipPowerViewModel model)
        {

            return RedirectToAction("Index", "ControlShipInfo");
        }


        [HttpGet]
        public ActionResult Edit(string id, ShipPowerViewModel model)
        {
            ModelState.Clear();
            //model.ShipInfos = _shipInfoManager.GetAll();
            model.ShipInfos = _controlShipInfoManager.ShipBranchInfo();
            var costCentre = _shipPowerManager.GetShipId(id) ?? new ShipPower();
            model.ShipPower = costCentre;
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(ShipPowerViewModel model)
        {
            //model.ShipPower.UserId = PortalUser.UserName;
            ResponseModel savedata = _shipPowerManager.SaveData(model.ShipPower);
            if (savedata.Message != string.Empty)
            {
                TempData["message"] = savedata.Message;
            }
            return RedirectToAction("Edit", model);
        }
        public ActionResult Delete(string id)
        {
            var deleteIndex = _shipPowerManager.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Download(string id)
        {
            var localReport = new LocalReport();
            //long r = Convert.ToInt64(id);
            MachinaryEquipmentInformationViewModel model = new MachinaryEquipmentInformationViewModel();

           // model.MachinaryEquipmentInfos = _machinaryEquipmentInformationManager.GetShipId(id);
            var asdf = _machinaryEquipmentInformationManager.GetShipId(id);
            var cvb = asdf.Select(x => x.ParentCode).ToList();
            var reportDataList = asdf.Select(item => new vwMachinaryEquipmentInformation {
                //OrgName = "Bangladesh Navy",
                //OfficeName = PortalContext.CurrentUser.BankName,
                //Parameter = "Ship's Basic Information",
                //ReportName = "Machinery Parameters - ",
                //ParentCode = item.ParentCode,
                //ShipName = item.ShipName ?? "",
                //ControlCode = item.ControlCode,
                //ControlName = item.ControlName ?? "",
                //GenInfo = item.GenInfo ?? "",
                //Shafting = item.Shafting ?? "",
                //MainThrusrShift = item.MainThrusrShift ?? "",
                //MiddleShaft = item.MiddleShaft ?? "",
                //AuxiliaryShaft = item.AuxiliaryShaft ?? "",
                //PropellerShaft = item.PropellerShaft ?? "",
                //ControlValue = item.ControlValue ?? "",
                //AdditionalValue = item.AdditionalValue ?? "",
                //Remarks = item.Remarks ?? "",
                //ControlLevel = item.ControlLevel,
                //SortOrder = item.SortOrder,
                //Level1 = item.Level1,
                //Level2 = item.Level2,
                //Level3 = item.Level3,
                //Level4 = item.Level4,
                //Level5 = item.Level5,
                //Genaralgroup = item.Genaralgroup ?? "",
                //Description = item.Description ?? "",
                //Speed = item.Speed,
                //Duration = item.Duration,
                //EnduranceHr = item.EnduranceHr,
                //EnduranceNm = item.EnduranceNm,
                //ShaftSpeed = item.ShaftSpeed,
                //Power = item.Power,
                //FuelConsumption = item.FuelConsumption,
                //Descriptiom = item.Descriptiom,
                //PortSpeed = item.PortSpeed,
                //StbdSpeed = item.StbdSpeed,
                //StableSpeed = item.StableSpeed

                 OrgName = "Bangladesh Navy",
                OfficeName = PortalContext.CurrentUser.BankName,
                Parameter = "Ship's Basic Information",
                ReportName = "Machinery Parameters - " + item.ShipName,
                ParentCode = Convert.ToDecimal(item.ParentCode),
                ShipName = item.ShipName ?? "",
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



            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            var GenInfo = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString()=="101");
            reportDataSource = new ReportDataSource { Name = "GenInfo" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = GenInfo;

            var PrincipalDiamension = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == "102");
            reportDataSource = new ReportDataSource { Name = "PrincipalDiamension" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = PrincipalDiamension;

            var Tonnage = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == "103");
            reportDataSource = new ReportDataSource { Name = "Tonnage" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Tonnage;

            var Displacement = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == "104");
            reportDataSource = new ReportDataSource { Name = "Displacement" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Displacement;

            var Draught = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == "105");
            reportDataSource = new ReportDataSource { Name = "Draught" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Draught;

            var CapacityOftanks = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == "106");
            reportDataSource = new ReportDataSource { Name = "CapacityOftanks" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = CapacityOftanks;

            var Storage = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == "107");
            reportDataSource = new ReportDataSource { Name = "Storage" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Storage;

            var stabilityCondition = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == "108");
            reportDataSource = new ReportDataSource { Name = "StabilityCondition" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = stabilityCondition;

            var MomentTocharge = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == "109");
            reportDataSource = new ReportDataSource { Name = "MomentTocharge" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = MomentTocharge;

            var TableOfSpeedPowerAndEndurance = _machinaryEquipmentInformationManager.GetShipId(id).Where(x=>x.Description != null);
            reportDataSource = new ReportDataSource { Name = "TableOfSpeedPowerAndEndurance" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = TableOfSpeedPowerAndEndurance;

            var CopyOfFuelConsumption = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == "113");
            reportDataSource = new ReportDataSource { Name = "CopyOfFuelConsumption" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = CopyOfFuelConsumption;

            var Rudder = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == "114");
            reportDataSource = new ReportDataSource { Name = "Rudder" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Rudder;

            var Propellers = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == "115");
            reportDataSource = new ReportDataSource { Name = "Propellers" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Propellers;

            var Shafting = _machinaryEquipmentInformationManager.GetShipId(id).Where(x => x.ParentCode.ToString() == "116" | x.ParentCode.ToString() == "11603" | x.ParentCode.ToString() == "11604" | x.ParentCode.ToString() == "11605" | x.ParentCode.ToString() == "11606");
            reportDataSource = new ReportDataSource { Name = "Shafting" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = Shafting;



            localReport.ReportPath = Server.MapPath("~/Reports/BNS/MachineryInfo.rdlc");
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
    }
}