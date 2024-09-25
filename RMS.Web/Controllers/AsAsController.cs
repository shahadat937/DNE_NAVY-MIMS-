using System;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.BLL.Manager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;

namespace RMS.Web.Controllers
{
    public class AsAsController : BaseController
    {
        private IAsAsInfoManager _asAsInfoManager;
        private IShipInfoManager _shipInfoManager;
        private IControlShipInfoManager _controlShipInfoManager;
        private ICommonCodeManager _commonCodeManager;

        public AsAsController(IAsAsInfoManager asAsInfoManager, IShipInfoManager shipInfoManager, IControlShipInfoManager controlShipInfoManager, ICommonCodeManager commonCodeManager)
        {
            _asAsInfoManager = asAsInfoManager;
            _shipInfoManager = shipInfoManager;
            _controlShipInfoManager = controlShipInfoManager;
            _commonCodeManager = commonCodeManager;
        }
        public ActionResult Index(string ShipId, As_AsViewModel model)
        {
            if(model.SearchKey >0 )
            {
                //model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
                var AsAs = _asAsInfoManager.GetAll().Where(x=>x.AsAndAsIdentity== model.SearchKey);
                model.AsAsInfos = AsAs.ToList();
            }
            else
            {
                //model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
                var AsAs = new List<AsAndAsInfo>(_asAsInfoManager.GetAll());

                var AsAsfirstsByCompareInGroups = from asAs in AsAs
                                                  group asAs by asAs.ControlShipInfoId into grp
                                                  select grp.OrderByDescending(a => a.AsAndAsIdentity).First();

                //var query = (from post in AsAsfirstsByCompareInGroups join meta in model.BrControlShipInfos on post.ControlShipInfoId equals meta.ControlShipInfoId select post).ToList();
                model.AsAsInfos = AsAsfirstsByCompareInGroups.ToList();
            }

            return View(model);
        }

        public ActionResult ShipAsAs(As_AsViewModel model)
        {
            if (PortalContext.CurrentUser.LoweredRoleName == "S" || PortalContext.CurrentUser.LoweredRoleName == "V")
            {
                model.AsAsInfos = _asAsInfoManager.GetAll().Where(x => x.ControlShipInfoId == PortalContext.CurrentUser.ShipId).ToList();

            }
            else
            {
                model.AsAsInfos = _asAsInfoManager.GetAll().Where(x => x.ControlShipInfoId == model.SearchKey).ToList();
            }

            return View(model);
        }

        public ActionResult Searchbykey(As_AsViewModel model)
        {
            //string searchkey = "";
            //if (model.SearchKey != null)
            //{
            //    searchkey = model.SearchKey.ToLower();
            //    model.AsAsInfos = _asAsInfoManager.GetAll().Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(searchkey)).ToList();
            //}
            //else
            //{
            //    model.AsAsInfos = _asAsInfoManager.GetAll();
            //}

            //return View("Index", model);
            long searchkey = model.SearchKey;
            model.AsAsInfos = searchkey != null
                ? _asAsInfoManager.GetAll()
                    .Where(x => x.ControlShipInfoId == searchkey).ToList() : _asAsInfoManager.GetAll();
            //model.OrgCommonCodes = _commonCodeManager.GetCommonCodeByType("organization");
            model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);

            return View("Index", model);
        }

        public ActionResult Edit( string id,As_AsViewModel model)
        {
            model.ShipInfos = _shipInfoManager.GetAll();
            //model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.TempDisCodes = _commonCodeManager.GetCommonCodeByType("TempDisCode").Select(x => new SelectionList
            {
                Text = x.TypeValue,
                Value = x.CommonCodeID
            }).ToList();
            model.ClassList = _commonCodeManager.GetCommonCodeByType("AsASClass").Select(x => new SelectionList
            {
                Text = x.TypeValue,
                Value = x.CommonCodeID
            }).ToList();
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            model.AsAsInfo = _asAsInfoManager.GetAsAsInfoById(Convert.ToInt64(id)) ?? new AsAndAsInfo();
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            var deletedata = _asAsInfoManager.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Save(As_AsViewModel model)
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
                    string md = DateTime.Now.ToString("dd_MMM_yyy_hs_ms_ss");
                    var uploadDir = "~/uploads/AsNAs";

                    string file = "AsNAs_" + md + "_" + model.ImageUpload.FileName;
                    string ext = Path.GetExtension(file);
                    if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".DWG" || ext == ".dwg" || ext == ".jpeg" || ext == ".pjpeg" || ext == ".BPG" || ext == ".SVG" || ext == ".PDF" || ext == ".pdf" || ext == ".doc" || ext == ".docx")
                    {
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), file);
                        var imageUrl = Path.Combine(uploadDir, file);
                        model.ImageUpload.SaveAs(imagePath);
                        model.AsAsInfo.DockingPlace = imageUrl;
                    }
                    else
                    {
                        TempData["message"] = "Input Only Image";
                        return RedirectToAction("Edit", model);
                    }
                }

                ResponseModel savedata = _asAsInfoManager.Save(model.AsAsInfo);
                //if (savedata.Message != string.Empty)
                //{
                //    TempData["message"] = savedata.Message;
                //}
            }
            
            return RedirectToAction("ShipAsAs");
        }
        public ActionResult FileDownload(int id)
        {
            var path = _asAsInfoManager.FindFile(id).DockingPlace;
            if (!System.IO.File.Exists(Server.MapPath(path))) return null;
            var fileName = path.Split('\\').ElementAt(1);
            return File(Server.MapPath(path), System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public ActionResult Search(As_AsViewModel model)
        {
            long searchkey = model.SearchKey;
            model.AsAsInfos = searchkey != null
                ? _asAsInfoManager.GetAll()
                    .Where(x => x.ControlShipInfoId == searchkey).ToList() : _asAsInfoManager.GetAll();
            //model.OrgCommonCodes = _commonCodeManager.GetCommonCodeByType("organization");
            model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);

            return View("Index", model);
        }


        public ActionResult Download(long id)
        {
            var localReport = new LocalReport();
            var model = new As_AsViewModel { AsAsInfos = _asAsInfoManager.GetAsAsId(id) };

            var reportDataList = model.AsAsInfos.Select(item => new vwAsAndAsInfo
            {
              
                ShipName = item.ControlShipInfo.ControlName,
                ShipId = item.ControlShipInfoId,
                TempDisCodeId = item.TempDisCodeId ?? 0,
                AsAndAsClassification = item.AsAndAsClassification ?? "",
                DescriptionOfProp = item.DescriptionOfProp ?? "",
                FightingEfficiency = item.FightingEfficiency ?? "",
                SeaGoingEfficiency = item.SeaGoingEfficiency ?? "",
                AsAndAsDate = item.AsAndAsDate,
                EffectOnHabitability = item.EffectOnHabitability ?? "",
                EffectOnStabilityTrim = item.EffectOnStabilityTrim ?? "",
                AuthorityRemark = item.AuthorityRemark ?? "",
                UserId = item.UserId,
                LastUpdate = item.LastUpdate

            }).ToList();


            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;


            localReport.ReportPath = Server.MapPath("~/Reports/BNS/AsAndAs.rdlc");
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
        public ActionResult VerifyAsAndAsData(As_AsViewModel model)
        {
            model.VerifyType = _commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            return View(model);
        }

        public ActionResult VerifyAsANdAsSearch(int? VerifyType, DateTime? DateFrom, DateTime? DateTo, As_AsViewModel model)
        {
            model.AsAsInfos = _asAsInfoManager.UserWiseData(VerifyType);
            if (model.AsAsInfos.Any())
            {
                model.AsAsInfos = DateFrom != null && DateTo != null ? model.AsAsInfos.Where(x => x.EntryDate >= DateFrom && x.EntryDate <= DateTo).ToList() : model.AsAsInfos;
            }
            model.VerifyType = _commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            return View("VerifyAsAndAsData", model);
        }

        public ActionResult StatusUpdate(As_AsViewModel model)
        {
            ResponseModel verifiedStatusUpdate = _asAsInfoManager.VerifiedStatusUpdate(model.AsAsInfos);
            if (verifiedStatusUpdate.Message != string.Empty)
            {
                model.Message = verifiedStatusUpdate.Message;
                model.IsSucceed = 1;
            }
            else
            {
                model.Message = "Status Not Updated";
            }
            model.VerifyType = _commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            return View("VerifyAsAndAsData", model);
        }
	}
}