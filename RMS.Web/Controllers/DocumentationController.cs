using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using iTextSharp.text.pdf.qrcode;
using Microsoft.Reporting.WebForms;

namespace RMS.Web.Controllers
{
     [Authorize]
    public class DocumentationController : Controller
    {
        private IShipInfoManager _shipInfoManager;
        private ICommonCodeManager _commonCodeManager;
        private IDocumentationManager _documentationManager;
         private IControlShipInfoManager _controlShipInfoManager;

        public DocumentationController(IDocumentationManager documentationManager, IShipInfoManager shipInfoManager,
            ICommonCodeManager commonCodeManager,IControlShipInfoManager controlShipInfoManager)
        {
            _documentationManager = documentationManager;
            _commonCodeManager = commonCodeManager;
            _shipInfoManager = shipInfoManager;
            _controlShipInfoManager = controlShipInfoManager;
        }

        public ActionResult Index(DocumentationViewModel model)
        {
            model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);


            model.Documentations = _documentationManager.GetAll().OrderBy(x => x.LastUpdate).ToList();
            foreach (var item in model.Documentations)
            {
                if(item.ImageUrl != null)
                {
                    item.ImageUrl = "/uploads/" + item.ImageUrl.Substring(10);
                }
            }
            return View(model);
        }

        public ActionResult Edit(string id, DocumentationViewModel model)
        {
            ModelState.Clear();
            //model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            model.CommonCodes = _commonCodeManager.GetCommonCodeByType("DocumentationType");
            model.ShipInfos = _shipInfoManager.GetAll();
            model.Documentation = _documentationManager.FindOne(Convert.ToInt32(id))??new Documentation();
            return View(model);
        }

        public ActionResult Save(DocumentationViewModel model)
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
                    if (model.Documentation.DocumentationIdentity > 0)
                        maxId = model.Documentation.DocumentationIdentity;
                    var uploadDir = "~/uploads";
                   
                    string file = "Documentation_" + maxId + "_" + model.ImageUpload.FileName;
                    string ext = Path.GetExtension(file);
                    if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".DWG" || ext == ".dwg" || ext == ".jpeg" || ext == ".pjpeg" || ext == ".BPG" || ext == ".SVG" || ext == ".PDF" || ext == ".pdf")
                    {
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), file);
                        var imageUrl = Path.Combine(uploadDir, file);
                        model.ImageUpload.SaveAs(imagePath);
                        model.Documentation.ImageUrl = imageUrl;
                    }
                    else
                    {
                        TempData["message"] = "Input Only PDF File";
                        return RedirectToAction("Index", model);
                    }
                }

                ResponseModel savedata = _documentationManager.Save(model.Documentation);
                if (savedata.Message != string.Empty)
                {
                    TempData["message"] = savedata.Message;
                }
            }
            return RedirectToAction("Edit",model);
        }

        public ActionResult SearchByKey(DocumentationViewModel model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.Trim().ToLower();
                model.Documentations =
                    _documentationManager.GetAll()
                        .Where(
                            x =>
                                x.ControlShipInfo.ControlName.ToLower().Contains(searchKey) ||
                                x.Name.ToLower().Contains(searchKey) ||
                                x.CommonCode.TypeValue.ToLower().Contains(searchKey))
                        .ToList();
            }
            else
            {
                model.Documentations = _documentationManager.GetAll();
            }
            return View("Index", model);
        }
        public ActionResult Download(int id)
        {
            var path = _documentationManager.FindOne(id).ImageUrl;
            if (!System.IO.File.Exists(Server.MapPath(path))) return null;
            var fileName = path.Split('\\').ElementAt(1);
            return File(Server.MapPath(path), System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public ActionResult Delete(long id)
        {
            _documentationManager.Delete(id);
            return RedirectToAction("Index");
        }
    }
}