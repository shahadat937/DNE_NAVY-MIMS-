using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    public class ShipInactiveOrgController : BaseController
    {
        private IShipInactiveOrgManager _shipInactiveOrgManager;
        private IShipInfoManager _shipInfoManager;
        private readonly ICommonCodeManager _commonCodeManager;




        public ShipInactiveOrgController(IShipInactiveOrgManager shipInactiveOrgManager, IShipInfoManager shipInfoManager, ICommonCodeManager commonCodeManager)
        {
            _shipInactiveOrgManager = shipInactiveOrgManager;
            _shipInfoManager = shipInfoManager;
            _commonCodeManager = commonCodeManager;
        }
        public ActionResult Index(ShipInactiveOrgViewModel model)
        {
            //model.ShipMovements = _shipMovementManager.GetAll();
            model.ShipInactiveOrgs = _shipInactiveOrgManager.GetAll();
            return View(model);
        }

        public ActionResult Save(ShipInactiveOrgViewModel model)
        {
            ResponseModel savedata = _shipInactiveOrgManager.Save(model.ShipInactiveOrg);
          if (savedata.Message != string.Empty)
          {
              TempData["message"] = savedata.Message;
          }
          return RedirectToAction("Edit", model);
        }

        public ActionResult Edit(string id, ShipInactiveOrgViewModel model)
        {
            ModelState.Clear();
            model.ShipInactiveOrgs = _shipInactiveOrgManager.GetAll();
            model.CommonCodes = _commonCodeManager.GetCommonCodeByType("organization");
            model.ShipInfos = _shipInfoManager.GetAll();
            model.ShipInactiveOrg = _shipInactiveOrgManager.GetShipInactiveOrgById(Convert.ToInt64(id)) ?? new ShipInactiveOrg();
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            var deletedata = _shipInactiveOrgManager.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Search(ShipInactiveOrgViewModel model)
        {
         
            string searchkey = model.SearchKey;
            model.ShipInactiveOrgs = searchkey != null
                ? _shipInactiveOrgManager.GetAll()
                    .Where(x => x.ShipInfo.ShipName.ToLower().Contains(searchkey.ToLower()))
                    .ToList()
                : _shipInactiveOrgManager.GetAll();
            return View( model);
        }


        public ActionResult Download(long id)
        {
            var localReport = new LocalReport();
            var model = new ShipInactiveOrgViewModel { ShipInactiveOrgs  = _shipInactiveOrgManager.GetAll()};

            var reportDataList = model.ShipInactiveOrgs.Select(item => new vwShipInactiveOrg()
            {
                OrgName = "Bangladesh Navy",
                OfficeName = PortalContext.CurrentUser.BankName,
                Parameter = "Ship Inactive org",
                ReportName = "Ship Inactive org", //+ item.ShipName,
                SInactiveOrgIdentity = item.SInactiveOrgIdentity,
               ShipInfoIdentity = item.ShipInfoIdentity,
                TypeValue = item.CommonCode.TypeValue,
               TotalShip = item.TotalShip,
               todate =DateTime.Now.Date,
               Operational = item.Operational,
               NonOperational= item.NonOperational,
               sDescription = item.sDescription,
               Remark = item.Remark,
               RefitDateFrom = item.RefitDateFrom,
               RefitDateTo = item.RefitDateTo,
               DockingDateFrom = item.DockingDateFrom,
               DockingDateTo= item.DockingDateTo,
               UserID = PortalContext.CurrentUser.UserName,
               LastUpdate = DateTime.Now
                

            }).ToList();


            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            //var FuelConsumption = _shipInactiveOrgManager.FindOne(id);
            //var reportDataSource1 = new ReportDataSource { Name = "DataSet1" };
            //localReport.DataSources.Add(reportDataSource1);
            //reportDataSource1.Value = FuelConsumption;



            localReport.ReportPath = Server.MapPath("~/Reports/BNS/InactiveOrg.rdlc");
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