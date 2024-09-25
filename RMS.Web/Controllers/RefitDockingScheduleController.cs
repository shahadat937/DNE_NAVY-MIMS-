using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.Ajax.Utilities;
using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    public class RefitDockingScheduleController : BaseController
    {
        private IRefitDockingScheduleManager _refitDockingSchedule;
        private IShipInfoManager _shioInfoManager;
        private IBranchInfoManager _branchInfoManager;
        private IControlShipInfoManager _controlShipInfoManager;
        private ICommonCodeManager _commonCodeManager;
        private IDamageMachineriesInfoManager _damageMachineriesInfo;

        private IDocumentationManager _documentationManager;
        private readonly IRefitDockingReportExcellManager _refitDockingReportExcellManager;

        public RefitDockingScheduleController(IRefitDockingScheduleManager refitDockingSchedule,
            IShipInfoManager shioInfoManager, IBranchInfoManager branchInfoManager, IControlShipInfoManager controlShipInfoManager, ICommonCodeManager commonCodeManager, IDamageMachineriesInfoManager damageMachineriesInf, IDocumentationManager documentationManager,IRefitDockingReportExcellManager refitDockingReportExcellManager)
        {
            _refitDockingSchedule = refitDockingSchedule;
            _shioInfoManager = shioInfoManager;
            _branchInfoManager = branchInfoManager;
            _controlShipInfoManager = controlShipInfoManager;
            _commonCodeManager = commonCodeManager;
            _damageMachineriesInfo = damageMachineriesInf;
            _documentationManager = documentationManager;
            _refitDockingReportExcellManager = refitDockingReportExcellManager;
        }

        public ActionResult Index(string SearchKey, RefitDockingScheduleViewModel model)
        {
            var ConShip = _controlShipInfoManager.ShipBranchInfo();
            List<RefitDockingSchedule> RDSchedule = new List<RefitDockingSchedule>();
            if (SearchKey != null)
            {
                long RefitDockingIdentity = Convert.ToInt64(SearchKey);
                 RDSchedule = _refitDockingSchedule.GetAll().Where(x=>x.RefitDockingIdentity== RefitDockingIdentity).OrderBy(x => x.LastUpdate).ToList();

            }
            else
            {
                RDSchedule = _refitDockingSchedule.GetAll().OrderBy(x => x.LastUpdate).ToList();

            }
            var query = (from post in RDSchedule join meta in ConShip on post.ControlShipInfoId equals meta.ControlShipInfoId select post).ToList();
            model.RefitDockingSchedules = query;
            return View(model);
        }

        public ActionResult Searchbykey(RefitDockingScheduleViewModel model)
        {
            string searchkey = "";
            if (model.SearchKey != null)
            {
                searchkey = model.SearchKey.ToLower();
                model.RefitDockingSchedules =
                    _refitDockingSchedule.GetAll()
                        .Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(searchkey))
                        .ToList();
            }
            else
            {
                model.RefitDockingSchedules = _refitDockingSchedule.GetAll();
            }

            return View("Index", model);
        }
        public ActionResult RefitDockingScheduleDetails(string id, RefitDockingScheduleViewModel model)
        {

            var costCentre = _refitDockingSchedule.GetRefitDockingScheduleId(id) ?? new RefitDockingSchedule();
            model.RefitDockingSchedule = costCentre;
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(string id, RefitDockingScheduleViewModel model)
        {
            ModelState.Clear();
            model.ShipInfos = _shioInfoManager.GetAll();
            model.Docked = _commonCodeManager.GetCommonCodeByType("Docked");
            model.Status = _commonCodeManager.GetCommonCodeByType("RefitDockingStatus");
            model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();

            var costCentre = _refitDockingSchedule.GetShipId(id) ?? new RefitDockingSchedule();
            model.RefitDockingSchedule = costCentre;
            return View(model);
        }

        [HttpGet]
        public ActionResult RefitDockingScheduleExcelEdit(long? id, RefitDockingScheduleViewModel model)
        {
            ModelState.Clear();
            model.Docked = _commonCodeManager.GetCommonCodeByType("Docked");
            model.FromYearList = _damageMachineriesInfo.returnReportYears().ToList();
            model.ToYearList = _damageMachineriesInfo.returnReportYears().ToList();
            var costCentre= new RefitDockingReportExcell();
            if (id > 0)
            {
                costCentre = _refitDockingReportExcellManager.FindOne(id ?? 0);
            }

            //var costCentre = _refitDockingReportExcellManager.FindOne(id) ?? new RefitDockingReportExcell();
            model.RefitDockingReportExcell = costCentre;
            return View(model);
        }

        public ActionResult RefitDockingScheduleExcelDelete(long id)
        {
            _refitDockingReportExcellManager.Delete(id);
            return RedirectToAction("ScheduleReport");
        }

        [HttpPost]
        public ActionResult RefitDockingScheduleExcelSave(RefitDockingScheduleViewModel model)
        {
            
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png",
                "image/DWG",
                "image/dwg",
                "image/jpg",
                "image/BPG",
                "image/SVG",
                "image/pdf",
                "image/PDF"

            };

            if (ModelState.IsValid)
            {
                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    long maxId = _documentationManager.GetMaxId() + 1;
                    if (model.RefitDockingReportExcell.RefitDockingReportExcellId > 0)
                        maxId = model.RefitDockingReportExcell.RefitDockingReportExcellId;
                    var uploadDir = "~/uploads/RefitDocking";

                    string file = "Documentation_" + maxId + "_" + model.ImageUpload.FileName;
                    string ext = Path.GetExtension(file);
                    if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".DWG" || ext == ".dwg" || ext == ".jpeg" || ext == ".pjpeg" || ext == ".BPG" || ext == ".SVG" || ext == ".PDF" || ext == ".pdf")
                    {
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), file);
                        var imageUrl = Path.Combine(uploadDir, file);
                        model.ImageUpload.SaveAs(imagePath);
                        model.RefitDockingReportExcell.ImageUrl = imageUrl;
                    }
                    else
                    {
                        TempData["message"] = "Input Only PDF File";
                        return RedirectToAction("Index", model);
                    }
                }

                ResponseModel savedata = _refitDockingReportExcellManager.Save(model.RefitDockingReportExcell);
                if (savedata.Message != string.Empty)
                {
                    TempData["message"] = savedata.Message;
                }
            }
            return RedirectToAction("ScheduleReport", model);
        }

        [HttpPost]
        public ActionResult Save(RefitDockingScheduleViewModel model)
        {
            //model.ShipPower.UserId = PortalUser.UserName;
            ResponseModel savedata = _refitDockingSchedule.SaveData(model.RefitDockingSchedule);
            if (savedata.Message != string.Empty)
            {
                TempData["message"] = savedata.Message;
            }
            return RedirectToAction("Edit", model);
        }

        public ActionResult Delete(string id)
        {
            var deleteIndex = _refitDockingSchedule.Delete(id);
            return RedirectToAction("Index");
        }
        public JsonResult PreviousDatefind(long? controlCode)
        {
            var val = _refitDockingSchedule.GetPreviousDate(controlCode);
            vwPreviousDateValue dateValue = new vwPreviousDateValue();
            dateValue.Result = "0";
            if (val != null)
            {
                dateValue.PNextDockingFrom = val.PNextDockingFrom;
                dateValue.PNextDockingTo = val.PNextDockingTo;
                dateValue.PNextRefitFrom = val.PNextRefitFrom;
                dateValue.PNextRefitTo = val.PNextRefitTo;
                dateValue.Result = "1";
            }
            return Json(dateValue, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ScheduleReport(RefitDockingScheduleViewModel model)
        {
            model.Docked = _commonCodeManager.GetCommonCodeByType("Docked");
            //model.FromYearList = _damageMachineriesInfo.returnReportYears().ToList();
            //model.ToYearList = _damageMachineriesInfo.returnReportYears().ToList();
            return View(model);
        }

        public ActionResult ScheduleReportWithYear(string dockName, int DockId)
        {
            RefitDockingScheduleViewModel model = new RefitDockingScheduleViewModel();
            model.RefitDockingReportExcellList = _refitDockingReportExcellManager.GetAllByDockId(DockId);
            foreach (var item in model.RefitDockingReportExcellList)
            {
                if (item.ImageUrl != null)
                {
                    item.ImageUrl = "/uploads/" + item.ImageUrl.Substring(10);
                }
            }

            model.DockName = dockName;
            return View(model);
        }


        public ActionResult Download(long id)
        {

            var localReport = new LocalReport();
            var reportData = new List<vwRefitDockingSchedule>();
            int firstCol = 0,
                SecondCol = 0,
                ThirdCol = 0,
                forthCol = 0,
                fifthCol = 0,
                sixth = 0,
                seventh = 0,
                eighth = 0,
                ninth = 0,
                tenth = 0,
                eleventh = 0,
                twelveth = 0,
                thirteen = 0,
                forteenth = 0,
                fifteenth = 0,
                sixteenth = 0,
                seventeenth = 0,
                eighteenth = 0,
                nineteenth = 0,
                twentyth = 0,
                twentyOneth = 0,
                twentytwoth = 0,
                twentythreeth = 0,
                twentyforeth = 0;

            DateTime cd = DateTime.Now;
            string udt = cd.ToString("dd_MMM_yyyy_H_m_s");

            var viewdata = _refitDockingSchedule.GetAll().Where(x=>x.RefitDockingIdentity==id).ToList();
            foreach (var item in viewdata)
            {
                firstCol = 0;
                SecondCol = 0;
                ThirdCol = 0;
                forthCol = 0;
                fifthCol = 0;
                sixth = 0;
                seventh = 0;
                eighth = 0;
                ninth = 0;
                tenth = 0;
                eleventh = 0;
                twelveth = 0;
                thirteen = 0;
                forteenth = 0;
                fifteenth = 0;
                sixteenth = 0;
                seventeenth = 0;
                eighteenth = 0;
                nineteenth = 0;
                twentyth = 0;
                twentyOneth = 0;
                twentytwoth = 0;
                twentythreeth = 0;
                twentyforeth = 0;


                int dt = 0;
                int rt = 0;


                //DateTime dockingForm = (DateTime)item.PNextDockingFrom;
                DateTime dockingForm = new DateTime();
                if (item.PNextDockingFrom != null)
                {
                dockingForm = (DateTime)item.PNextDockingFrom;
                    
                }
                int df = dockingForm.Month;
                //DateTime dockingto = (DateTime)item.PNextDockingTo;
                DateTime dockingto = new DateTime();
               if(item.PNextDockingTo != null)
               {
                   dockingto = (DateTime)item.PNextDockingTo;
               }
                if ((dockingto.Year - dockingForm.Year) > 0)
                {
                    dt = dockingto.Month + 12;
                }
                else
                {
                    dt = dockingto.Month;
                }
                DateTime refitForm = new DateTime();
                if (item.PNextRefitFrom !=  null)
                {
                 refitForm = (DateTime)item.PNextRefitFrom;
                    
                }
                int rf = refitForm.Month;
                DateTime refitto = new DateTime();
                if (item.PNextRefitTo != null)
                {
                   refitto = (DateTime)item.PNextRefitTo; 
                }
                
                if ((refitto.Year - refitForm.Year) > 0)
                {
                    rt = refitto.Month + 12;
                }
                else
                {
                    rt = refitto.Month;
                }

                int finalRefitTo = (refitto.Year * 12 + refitto.Month) - (refitForm.Year * 12 + refitForm.Month);

                // Refit Value Set

                for (int i = rf; i <= rt; i++)
                {
                    if (i == 1)
                    {
                        firstCol = 2;
                    }
                    else if (i == 2)
                    {
                        SecondCol = 2;
                    }
                    else if (i == 3)
                    {
                        ThirdCol = 2;
                    }
                    else if (i == 4)
                    {
                        forthCol = 2;
                    }

                    else if (i == 5)
                    {
                        fifthCol = 2;
                    }
                    else if (i == 6)
                    {
                        sixth = 2;
                    }
                    else if (i == 7)
                    {
                        seventh = 2;
                    }

                    else if (i == 8)
                    {
                        eighth = 2;
                    }
                    else if (i == 9)
                    {
                        ninth = 2;
                    }
                    else if (i == 10)
                    {
                        tenth = 2;
                    }

                    else if (i == 11)
                    {
                        eleventh = 2;
                    }
                    else if (i == 12)
                    {
                        twelveth = 2;
                    }
                    else if (i == 13)
                    {
                        thirteen = 2;
                    }

                    else if (i == 14)
                    {
                        forteenth = 2;
                    }
                    else if (i == 15)
                    {
                        fifteenth = 2;
                    }
                    else if (i == 16)
                    {
                        sixteenth = 2;
                    }
                    else if (i == 17)
                    {
                        seventeenth = 2;
                    }
                    else if (i == 18)
                    {
                        eighteenth = 2;
                    }
                    else if (i == 19)
                    {
                        nineteenth = 2;
                    }
                    else if (i == 20)
                    {
                        twentyth = 2;
                    }
                    else if (i == 21)
                    {
                        twentyOneth = 2;
                    }
                    else if (i == 22)
                    {
                        twentytwoth = 2;
                    }
                    else if (i == 23)
                    {
                        twentythreeth = 2;
                    }
                    else if (i == 24)
                    {
                        twentyforeth = 2;
                    }

                }
                // Docking Value Set
                for (int i = df; i <= dt; i++)
                {
                    if (i == 1)
                    {
                        firstCol = 1;
                    }
                    else if (i == 2)
                    {
                        SecondCol = 1;
                    }
                    else if (i == 3)
                    {
                        ThirdCol = 1;
                    }
                    else if (i == 4)
                    {
                        forthCol = 1;
                    }

                    else if (i == 5)
                    {
                        fifthCol = 1;
                    }
                    else if (i == 6)
                    {
                        sixth = 1;
                    }
                    else if (i == 7)
                    {
                        seventh = 1;
                    }

                    else if (i == 8)
                    {
                        eighth = 1;
                    }
                    else if (i == 9)
                    {
                        ninth = 1;
                    }
                    else if (i == 10)
                    {
                        tenth = 1;
                    }

                    else if (i == 11)
                    {
                        eleventh = 1;
                    }
                    else if (i == 12)
                    {
                        twelveth = 1;
                    }
                    else if (i == 13)
                    {
                        thirteen = 1;
                    }

                    else if (i == 14)
                    {
                        forteenth = 1;
                    }
                    else if (i == 15)
                    {
                        fifteenth = 1;
                    }
                    else if (i == 16)
                    {
                        sixteenth = 1;
                    }
                    else if (i == 17)
                    {
                        seventeenth = 1;
                    }
                    else if (i == 18)
                    {
                        eighteenth = 1;
                    }
                    else if (i == 19)
                    {
                        nineteenth = 1;
                    }
                    else if (i == 20)
                    {
                        twentyth = 1;
                    }
                    else if (i == 21)
                    {
                        twentyOneth = 1;
                    }
                    else if (i == 22)
                    {
                        twentytwoth = 1;
                    }
                    else if (i == 23)
                    {
                        twentythreeth = 1;
                    }
                    else if (i == 24)
                    {
                        twentyforeth = 1;
                    }
                }

                var oldData = new vwRefitDockingSchedule
                {
                    RefitDockingIdentity = item.RefitDockingIdentity,
                    ControlShipInfoId = item.ControlShipInfoId,
                    ControlName = item.ControlShipInfo.ControlName ?? "",
                    Docked = item.CommonCode.TypeValue ?? "",
                    LastRefitFrom = item.LastRefitFrom,
                    LastRefitTo = item.LastRefitTo,
                    firstCol = firstCol,
                    SecondCol = SecondCol,
                    ThirdCol = ThirdCol,
                    forthCol = forthCol,
                    fifthCol = fifthCol,
                    sixth = sixth,
                    seventh = seventh,
                    eighth = eighth,
                    ninth = ninth,
                    tenth = tenth,
                    eleventh = eleventh,
                    twelveth = twelveth,
                    thirteen = thirteen,
                    forteenth = forteenth,
                    fifteenth = fifteenth,
                    sixteenth = sixteenth,
                    seventeenth = seventeenth,
                    eighteenth = eighteenth,
                    nineteenth = nineteenth,
                    twentyth = twentyth,
                    twentyOneth = twentyOneth,
                    twentytwoth = twentytwoth,
                    twentythreeth = twentythreeth,
                    twentyforeth = twentyforeth,
                    LastDockingFrom = item.LastDockingFrom,
                    LastDockingTo = item.LastDockingTo,
                    PNextRefitFrom = item.PNextRefitFrom,
                    PNextRefitTo = item.PNextRefitTo,
                    PNextDockingFrom = item.PNextDockingFrom,
                    PNextDockingTo = item.PNextDockingTo,
                    BranchCode = item.Docked,
                    UserId = PortalContext.CurrentUser.UserName,
                    LastUpdate = DateTime.Now
                    //fromDate = fromdate,
                    //ToDate = ToDate,
                    //DATEDIFF =,
                };
                reportData.Add(oldData);
                oldData = null;


            }
            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportData;


            localReport.ReportPath = Server.MapPath("~/Reports/BNS/RefitDockungSchudule1.rdlc");
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;

            byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=" + udt + "_Urls." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);
        }
        public ActionResult DownloadAll(RefitDockingScheduleViewModel model)
        {
            int FromYear=model.FromYear;
            int ToYear=model.ToYear;
            if (FromYear > 0 & ToYear > 0)
            {
                var localReport = new LocalReport();
                var reportData = new List<vwRefitDockingSchedule>();
                int firstCol = 0,
                    SecondCol = 0,
                    ThirdCol = 0,
                    forthCol = 0,
                    fifthCol = 0,
                    sixth = 0,
                    seventh = 0,
                    eighth = 0,
                    ninth = 0,
                    tenth = 0,
                    eleventh = 0,
                    twelveth = 0,
                    thirteen = 0,
                    forteenth = 0,
                    fifteenth = 0,
                    sixteenth = 0,
                    seventeenth = 0,
                    eighteenth = 0,
                    nineteenth = 0,
                    twentyth = 0,
                    twentyOneth = 0,
                    twentytwoth = 0,
                    twentythreeth = 0,
                    twentyforeth = 0;

                DateTime cd = DateTime.Now;
                string udt = cd.ToString("dd_MMM_yyyy_H_m_s");

                var viewdata =
                    _refitDockingSchedule.GetAll()
                        .Where(
                            x => (x.PNextDockingFrom == null) ||
                                (x.PNextDockingFrom.Value.Year >= FromYear & x.PNextDockingFrom.Value.Year <= ToYear) ||
                                (x.PNextDockingTo == null) ||
                                (x.PNextDockingTo.Value.Year >= FromYear & x.PNextDockingTo.Value.Year <= ToYear) ||                                                             
                                (x.PNextRefitFrom == null) ||
                                (x.PNextRefitFrom.Value.Year >= FromYear & x.PNextRefitFrom.Value.Year <= ToYear) ||
                                (x.PNextRefitTo == null)||
                                (x.PNextRefitTo.Value.Year >= FromYear & x.PNextRefitTo.Value.Year <= ToYear))
                        .ToList();
                foreach (var item in viewdata)
                {
                    firstCol = 0;
                    SecondCol = 0;
                    ThirdCol = 0;
                    forthCol = 0;
                    fifthCol = 0;
                    sixth = 0;
                    seventh = 0;
                    eighth = 0;
                    ninth = 0;
                    tenth = 0;
                    eleventh = 0;
                    twelveth = 0;
                    thirteen = 0;
                    forteenth = 0;
                    fifteenth = 0;
                    sixteenth = 0;
                    seventeenth = 0;
                    eighteenth = 0;
                    nineteenth = 0;
                    twentyth = 0;
                    twentyOneth = 0;
                    twentytwoth = 0;
                    twentythreeth = 0;
                    twentyforeth = 0;


                    int dt = 0;
                    int rt = 0;
                    int df = 0;

                    if (item.PNextDockingFrom != null)
                    {
                        DateTime dockingForm = (DateTime)item.PNextDockingFrom;
                        df = dockingForm.Month;
                        DateTime dockingto = (DateTime)item.PNextDockingTo;
                        if ((dockingto.Year - dockingForm.Year) > 0)
                        {
                            dt = dockingto.Month + 12;
                        }
                        else
                        {
                            dt = dockingto.Month;
                        }

                    }

                    int rf = 0;
                    int finalRefitTo = 0;
                    if (item.PNextRefitFrom != null && item.PNextRefitTo !=null)
                    {
                        DateTime refitForm = (DateTime)item.PNextRefitFrom;
                        rf = refitForm.Month;
                        DateTime refitto = (DateTime)item.PNextRefitTo;
                        if ((refitto.Year - refitForm.Year) > 0)
                        {
                            rt = refitto.Month + 12;
                        }
                        else
                        {
                            rt = refitto.Month;
                        }
                        finalRefitTo = (refitto.Year * 12 + refitto.Month) - (refitForm.Year * 12 + refitForm.Month);
                    }





                    // Refit Value Set

                    for (int i = rf; i <= rt; i++)
                    {
                        if (i == 1)
                        {
                            firstCol = 2;
                        }
                        else if (i == 2)
                        {
                            SecondCol = 2;
                        }
                        else if (i == 3)
                        {
                            ThirdCol = 2;
                        }
                        else if (i == 4)
                        {
                            forthCol = 2;
                        }

                        else if (i == 5)
                        {
                            fifthCol = 2;
                        }
                        else if (i == 6)
                        {
                            sixth = 2;
                        }
                        else if (i == 7)
                        {
                            seventh = 2;
                        }

                        else if (i == 8)
                        {
                            eighth = 2;
                        }
                        else if (i == 9)
                        {
                            ninth = 2;
                        }
                        else if (i == 10)
                        {
                            tenth = 2;
                        }

                        else if (i == 11)
                        {
                            eleventh = 2;
                        }
                        else if (i == 12)
                        {
                            twelveth = 2;
                        }
                        else if (i == 13)
                        {
                            thirteen = 2;
                        }

                        else if (i == 14)
                        {
                            forteenth = 2;
                        }
                        else if (i == 15)
                        {
                            fifteenth = 2;
                        }
                        else if (i == 16)
                        {
                            sixteenth = 2;
                        }
                        else if (i == 17)
                        {
                            seventeenth = 2;
                        }
                        else if (i == 18)
                        {
                            eighteenth = 2;
                        }
                        else if (i == 19)
                        {
                            nineteenth = 2;
                        }
                        else if (i == 20)
                        {
                            twentyth = 2;
                        }
                        else if (i == 21)
                        {
                            twentyOneth = 2;
                        }
                        else if (i == 22)
                        {
                            twentytwoth = 2;
                        }
                        else if (i == 23)
                        {
                            twentythreeth = 2;
                        }
                        else if (i == 24)
                        {
                            twentyforeth = 2;
                        }

                    }
                    // Docking Value Set
                    for (int i = df; i <= dt; i++)
                    {
                        if (i == 1)
                        {
                            firstCol = 1;
                        }
                        else if (i == 2)
                        {
                            SecondCol = 1;
                        }
                        else if (i == 3)
                        {
                            ThirdCol = 1;
                        }
                        else if (i == 4)
                        {
                            forthCol = 1;
                        }

                        else if (i == 5)
                        {
                            fifthCol = 1;
                        }
                        else if (i == 6)
                        {
                            sixth = 1;
                        }
                        else if (i == 7)
                        {
                            seventh = 1;
                        }

                        else if (i == 8)
                        {
                            eighth = 1;
                        }
                        else if (i == 9)
                        {
                            ninth = 1;
                        }
                        else if (i == 10)
                        {
                            tenth = 1;
                        }

                        else if (i == 11)
                        {
                            eleventh = 1;
                        }
                        else if (i == 12)
                        {
                            twelveth = 1;
                        }
                        else if (i == 13)
                        {
                            thirteen = 1;
                        }

                        else if (i == 14)
                        {
                            forteenth = 1;
                        }
                        else if (i == 15)
                        {
                            fifteenth = 1;
                        }
                        else if (i == 16)
                        {
                            sixteenth = 1;
                        }
                        else if (i == 17)
                        {
                            seventeenth = 1;
                        }
                        else if (i == 18)
                        {
                            eighteenth = 1;
                        }
                        else if (i == 19)
                        {
                            nineteenth = 1;
                        }
                        else if (i == 20)
                        {
                            twentyth = 1;
                        }
                        else if (i == 21)
                        {
                            twentyOneth = 1;
                        }
                        else if (i == 22)
                        {
                            twentytwoth = 1;
                        }
                        else if (i == 23)
                        {
                            twentythreeth = 1;
                        }
                        else if (i == 24)
                        {
                            twentyforeth = 1;
                        }
                    }

                    var oldData = new vwRefitDockingSchedule
                    {
                        RefitDockingIdentity = item.RefitDockingIdentity,
                        ControlShipInfoId = item.ControlShipInfoId,
                        ControlName = item.ControlShipInfo.ControlName ?? "",
                        Docked = item.CommonCode.TypeValue ?? "",
                        LastRefitFrom = item.LastRefitFrom,
                        LastRefitTo = item.LastRefitTo,
                        firstCol = firstCol,
                        SecondCol = SecondCol,
                        ThirdCol = ThirdCol,
                        forthCol = forthCol,
                        fifthCol = fifthCol,
                        sixth = sixth,
                        seventh = seventh,
                        eighth = eighth,
                        ninth = ninth,
                        tenth = tenth,
                        eleventh = eleventh,
                        twelveth = twelveth,
                        thirteen = thirteen,
                        forteenth = forteenth,
                        fifteenth = fifteenth,
                        sixteenth = sixteenth,
                        seventeenth = seventeenth,
                        eighteenth = eighteenth,
                        nineteenth = nineteenth,
                        twentyth = twentyth,
                        twentyOneth = twentyOneth,
                        twentytwoth = twentytwoth,
                        twentythreeth = twentythreeth,
                        twentyforeth = twentyforeth,
                        LastDockingFrom = item.LastDockingFrom,
                        LastDockingTo = item.LastDockingTo,
                        PNextRefitFrom = item.PNextRefitFrom,
                        PNextRefitTo = item.PNextRefitTo,
                        PNextDockingFrom = item.PNextDockingFrom,
                        PNextDockingTo = item.PNextDockingTo,
                        BranchCode = item.Docked,
                        UserId = PortalContext.CurrentUser.UserName,
                        LastUpdate = DateTime.Now
                        //fromDate = fromdate,
                        //ToDate = ToDate,
                        //DATEDIFF =,
                    };
                    reportData.Add(oldData);
                    oldData = null;
                }
                var reportDataSource = new ReportDataSource {Name = "DataSet1"};
                localReport.DataSources.Add(reportDataSource);
                reportDataSource.Value = reportData;


                localReport.ReportPath = Server.MapPath("~/Reports/BNS/RefitDockungSchudule1.rdlc");
                string mimeType;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;

                byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
                    out fileNameExtension, out streams, out warnings);
                Response.AddHeader("content-disposition", "attachment; filename=" + udt + "_Urls." + fileNameExtension);
                return File(renderedBytes, fileNameExtension);
            }
            return RedirectToAction("Index");
        }
       
    }
}