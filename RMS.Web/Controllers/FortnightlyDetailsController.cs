using System.Security.Cryptography.X509Certificates;
using Microsoft.Reporting.WinForms;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RMS.Web.Controllers
{
    public class FortnightlyDetailsController : Controller
    {
        //
        // GET: /FortnightlyDetails/
        private readonly IFortnightlyDetailsManager _fortnightlyDetailsManager;
        private readonly IFortnightlyInfoManager _fortnightlyInfoManager;
        public FortnightlyDetailsController(IFortnightlyDetailsManager fortnightlyDetailsManager, IFortnightlyInfoManager fortnightlyInfoManager)
        {
            _fortnightlyDetailsManager = fortnightlyDetailsManager;
            _fortnightlyInfoManager = fortnightlyInfoManager;
        }
        public ActionResult Index(string id, FortnightlyDetailsViewModel model)
        {
            ModelState.Clear();
            //model.FortnightlyInfos = _fortnightlyInfoManager.GetAll();
            model.FortnightlyDetail = _fortnightlyDetailsManager.GetFortnightlyDetailById(id);
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Save(FortnightlyDetailsViewModel model)
        {
            ResponseModel savedata = _fortnightlyDetailsManager.Saving(model.FortnightlyDetail);
            if (savedata.Message != string.Empty)
            {
                TempData["message"] = savedata.Message;
            }
            return RedirectToAction("Index", model);
        }

        public ActionResult SearchByKey(FortnightlyDetailsViewModel model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.ToLower();
                model.FortnightlyDetails =
                    _fortnightlyDetailsManager.GetAll().Where(x => x.FortnightlyInfo.FortnightlyName.Contains(searchKey)).ToList();
            }
            else
            {
                model.FortnightlyDetails = _fortnightlyDetailsManager.GetAll();
            }
            return View("Edit", model);
        }
        [HttpGet]
        public ActionResult Edit(string id, FortnightlyDetailsViewModel model)
        {
            ModelState.Clear();
           // model.FortnightlyInfos = _fortnightlyInfoManager.GetAll();
            model.FortnightlyDetail = _fortnightlyDetailsManager.GetFortnightlyDetailById(id) ?? new FortnightlyDetail();
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            var deletedata = _fortnightlyDetailsManager.Delete(id);
            return RedirectToAction("Edit");
        }

        //public ActionResult Download(DateTime? startDate,DateTime? endDate)
        //{
         
        //    var localReport = new LocalReport();
        //    var model = new FortnightlyInfoViewModel { FortnightlyInfos = _fortnightlyInfoManager.GetAll()};
        //    var model1 = new FortnightlyDetailsViewModel { FortnightlyDetails = _fortnightlyDetailsManager.GetAll() };
        //    var reportDataList = model.FortnightlyInfos.Select(item => new FortnightlyInfo
        //    //var reportDataList = model.FortnightlyInfos.Where(x => x.CreateDate >= startDate && x.CreateDate <= endDate).Select(item => new FortnightlyInfo
        //    {
        //        CommonCodeID = item.CommonCodeID ?? 0,
        //        ShipInfoIdentity = item.ShipInfoIdentity ,
        //        TotalWorkNo = item.TotalWorkNo ?? "নাই",
        //        Progress25Percent = item.Progress25Percent ?? "নাই",
        //        Progress50Percent = item.Progress50Percent ?? "নাই",
        //        Progress75Percent = item.Progress75Percent ?? "নাই",
        //        Progress100Percent = item.Progress100Percent ?? "নাই",
        //        UnfishWork = item.UnfishWork ?? "নাই",
        //        Remarks = item.Remarks ?? ""

        //    }).ToList();

       
        //    var reportDataSource = new ReportDataSource { Name = "DataSet1" };
        //    localReport.DataSources.Add(reportDataSource);
        //    reportDataSource.Value = reportDataList;
         

        //    localReport.ReportPath = Server.MapPath("~/Reports/BNS/navy.rdlc");
        //    string mimeType;
        //    string encoding;
        //    string fileNameExtension;
        //    Warning[] warnings;
        //    string[] streams;

        //    byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
        //        out fileNameExtension, out streams, out warnings);
        //    Response.AddHeader("content-disposition", "attachment; filename=Urls." + fileNameExtension);

        //    return File(renderedBytes, fileNameExtension);

        //}
    }
}