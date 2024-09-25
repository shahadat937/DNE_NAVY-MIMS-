using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Web.Models.ViewModel;
using System.Web.Mvc;
using RMS.Utility;
using System.Linq;

namespace RMS.Web.Controllers
{
    public class TrainingInfoController : BaseController
    {
        private readonly ITrainingInfoManager _trainingInfoManager;
        private ICommonCodeManager _commonCodeManager;
        //
        // GET: /TrainingInfo/
        public TrainingInfoController(ITrainingInfoManager trainingInfoManager, ICommonCodeManager commonCodeManager)
        {
            _trainingInfoManager = trainingInfoManager;
            _commonCodeManager = commonCodeManager;
        }
        public ActionResult Index(TrainingInfoViewModel model)
        {
            //model.Ranks = _commonCodeManager.GetCommonCodeByType("RANK");
            //model.NameOfCourses = _commonCodeManager.GetCommonCodeByType("NameofCourse");
            model.TrainingInfos = new List<TrainingInfo>(_trainingInfoManager.GetAll().OrderBy(x =>x.RankId));
            return View(model);
        }
        public ActionResult Edit(string id, TrainingInfoViewModel model)
        {
            ModelState.Clear();
            model.Ranks = _commonCodeManager.GetCommonCodeByType("RANK");
            model.CountryName = _commonCodeManager.GetCommonCodeByType("Country").OrderBy(x =>x.TypeValue).ToList();
            model.TrainingCategory = _commonCodeManager.GetCommonCodeByType("TrainingCategory");
            model.NameOfCourses = _commonCodeManager.GetCommonCodeByType("NameofCourse");
            model.TrainingInfos = _trainingInfoManager.GetAll();
            var costCentre = _trainingInfoManager.GetTrainingInfoById(id) ?? new TrainingInfo();
            model.TrainingInfo = costCentre;
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(TrainingInfoViewModel model)
        {
            //model.DamageMachineriesInfo.UserId = User.Identity.GetUserName();
            ResponseModel saveData = _trainingInfoManager.Saving(model.TrainingInfo);
            if (saveData.Message != string.Empty)
            {
                TempData["message"] = saveData.Message;
            }
            return RedirectToAction("Edit", model);
        }
        public ActionResult SearchByKey(TrainingInfoViewModel model)
        {
            string searchKey = model.SearchKey;
            model.TrainingInfos = searchKey != null ? _trainingInfoManager.GetAll().Where(x => x.TrainerName.ToLower().Contains(searchKey.ToLower()) || x.CommonCode1.TypeValue.Contains(searchKey)).ToList() : _trainingInfoManager.GetAll().ToList();
            return View("Index",model);
        }


        //public ActionResult Search(TrainingInfoViewModel model)
        //{

        //    string serachKey = "";
        //    long search = model.S;
        //    long R = 0;

        //    if (model.SearchKey != null)
        //    {
        //        serachKey = model.SearchKey.Trim().ToLower();

        //        model.TrainingInfos = _trainingInfoManager.GetAll().Where(x =>  x.ONOorPNO.ToLower().Contains(serachKey.ToLower()) || x.CommonCode.TypeValue.ToLower().Contains(serachKey.ToLower()) || x.TrainerName.ToLower().Contains(serachKey.ToLower()) || x.TrainingType.ToLower().Contains(serachKey.ToLower())).ToList();
        //    }
        //    if (model.S != null)
        //    {

        //        model.TrainingInfos = search != null
        //           ? _trainingInfoManager.GetAll()
        //               .Where(x => x.CommonCode.CommonCodeID == search || x.CommonCode1.CommonCodeID == R || (x.CommonCode.CommonCodeID == search && x.CommonCode1.CommonCodeID == R)).ToList() : _trainingInfoManager.GetAll();
        //    }
        //    if (model.SearchKey != null && model.S != null)
        //    {
        //        serachKey = model.SearchKey.Trim().ToLower();

        //        model.TrainingInfos = _trainingInfoManager.GetAll().Where(x =>  x.ONOorPNO.ToLower().Contains(serachKey.ToLower())  || x.TrainerName.ToLower().Contains(serachKey.ToLower()) || x.TrainingType.ToLower().Contains(serachKey.ToLower()) && x.CommonCode.CommonCodeID == search || x.CommonCode1.CommonCodeID == R || (x.CommonCode.CommonCodeID == search && x.CommonCode1.CommonCodeID == R)).ToList();
        //    }

        //    model.Ranks = _commonCodeManager.GetCommonCodeByType("RANK");
        //    model.NameOfCourses = _commonCodeManager.GetCommonCodeByType("NameofCourse");
        //    return View("Index", model);
        //}


        public ActionResult Search(TrainingInfoViewModel model)
        {

            string serachKey = "";


            if (model.SearchKey != null)
            {
                serachKey = model.SearchKey.Trim().ToLower();

                model.TrainingInfos = _trainingInfoManager.GetAll().Where(x =>x.CommonCode.TypeValue.ToLower().Contains(serachKey.ToLower())|| x.CommonCode1.TypeValue.ToLower().Contains(serachKey.ToLower()) || x.ONOorPNO.ToLower().Contains(serachKey.ToLower()) || x.CommonCode.TypeValue.ToLower().Contains(serachKey.ToLower()) || x.TrainerName.ToLower().Contains(serachKey.ToLower()) || x.TrainingType.ToLower().Contains(serachKey.ToLower())).ToList();
            }

            //model.CommonCodes = _commonCodeManager.GetCommonCodeByType("HallStatus");
            return View("Index", model);
        }
        public ActionResult Delete(string id)
        {
            var deleteIndex = _trainingInfoManager.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Download(long id)
        {
            var localReport = new LocalReport();
            //var model = new TrainingInfoViewModel { TrainingInfos = new List<TrainingInfo> { _trainingInfoManager.GetCourseById(id) } };
            var model = new TrainingInfoViewModel { TrainingInfos = _trainingInfoManager.GetAll().Where(x =>x.NameofCourse== id).ToList() };

            var reportDataList = model.TrainingInfos.Select(item => new vwTrainingInfo()
            {

                TrainingInfoId = item.TrainingInfoId,
                ONOorPNO = item.ONOorPNO,
                TrainerName = item.TrainerName ?? "",
                Ranks = _commonCodeManager.GetCommonName(item.RankId),
                TrainingType = item.TrainingType ?? "",
                NameOfCourse = _commonCodeManager.GetCourseName(item.NameofCourse),
                TrainingLocation = _commonCodeManager.GetCommonName(item.TrainingLocation),
                TrainingFrom = item.TrainingFrom,

                TrainingTo = item.TrainingTo,
                Remarks = item.Remarks ?? "",
                

            }).ToList();


            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;


            localReport.ReportPath = Server.MapPath("~/Reports/BNS/TrainingReport.rdlc");
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