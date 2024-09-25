using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    public class BooksAndBrController : BaseController
    {
        private IBooksAndBrManager _booksAndBrManager;

        public BooksAndBrController(IBooksAndBrManager booksAndBrManager)
        {
            _booksAndBrManager = booksAndBrManager;
        }
        public ActionResult Index(BooksAndBrViewModel model)
        {
            model.BooksAndBrs = _booksAndBrManager.GetAll().OrderBy(x => x.LastUpdate).ToList();
            foreach (var item in model.BooksAndBrs.ToList())
            {
                if (item.UrlLink != "")
                {
                    item.UrlLink = "/uploads/" + item.UrlLink.Substring(10);
                }
            }
            return View(model);
        }
        public ActionResult SearchByKey(BooksAndBrViewModel model)
        {
            string searchKey = model.SearchKey;
            model.BooksAndBrs = searchKey != null ? _booksAndBrManager.GetAll().Where(x => x.BooksAndBrName.ToLower().Contains(searchKey.ToLower())).ToList() : _booksAndBrManager.GetAll().ToList();
            return View("Index", model);
        }
        public ActionResult Show(long id)
        {
            var BookPath = "";
            var data = _booksAndBrManager.FindUrl(id);
            BookPath = data.UrlLink;
            var path = Server.MapPath(BookPath);
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            string pdf = path;
            string ext = Path.GetExtension(pdf);
           
            if (ext == ".doc" || ext == ".docx")
            {
                return new FileStreamResult(fileStream, "application/vnd.ms-word");
            }
            else if (ext == ".xls" || ext == ".xlsx")
            {
                return new FileStreamResult(fileStream, "application/vnd.ms-excel");
            }
            else
            {
                return new FileStreamResult(fileStream, "application/pdf");
            }

           
           
        }
        public ActionResult Edit(long? id, BooksAndBrViewModel model)
        {
            ModelState.Clear();
            var costCentre = _booksAndBrManager.GetValue(id, model.BooksAndBr) ?? new BooksAndBr();
            model.BooksAndBr = costCentre;
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(BooksAndBrViewModel model)
        {
            if (ModelState.IsValid)
            {
                long maxId = 0;
                string imageUrl = "";
                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    long cMaxId = _booksAndBrManager.MaxId();
                    if (cMaxId > 0)
                        maxId = cMaxId + 1;
                    if (model.BooksAndBr.BooksAndBrIdentity > 0)
                        maxId = model.BooksAndBr.BooksAndBrIdentity;
                    var uploadDir = "~/uploads/Book";
                    string pdf = model.ImageUpload.FileName;
                    string ext = Path.GetExtension(pdf);
                    if (ext == ".pdf" || ext == ".doc" || ext == ".docx" || ext == ".xls" || ext == ".xlsx")
                    {
                        string file = "BooksAndBr_" + maxId + "_" + pdf;
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), file);
                        imageUrl = Path.Combine(uploadDir, file);
                        model.ImageUpload.SaveAs(imagePath);
                    }
                    else
                    {
                        TempData["message"] = "Please Input valid File";
                        return RedirectToAction("Edit", model);
                    }

                }
                model.BooksAndBr.UrlLink = imageUrl;

                ResponseModel saveData = _booksAndBrManager.Saving(model.BooksAndBr);
                if (saveData.Message != string.Empty)
                {
                    TempData["message"] = saveData.Message;
                }
            }
            return RedirectToAction("Edit");
        }
        public ActionResult Delete(long id)
        {
            var deleteIndex = _booksAndBrManager.Delete(id);
            return RedirectToAction("Index");
        }


        public JsonResult SearchBookName(string SearchCharacter)
        {
            List<vwShipNameSearch> val = new List<vwShipNameSearch>();
            var kho = _booksAndBrManager.GetAll().Where(x => x.BooksAndBrName.ToLower().Contains(SearchCharacter.ToLower())).ToList();
            foreach (var item in kho)
            {
                vwShipNameSearch khomeni = new vwShipNameSearch();
                khomeni.ShipName = item.BooksAndBrName;
                val.Add(khomeni);
            }
            return Json(val, JsonRequestBehavior.AllowGet);
        }

	}
}