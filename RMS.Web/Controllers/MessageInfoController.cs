using System;
using System.IO;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Mvc;
using iTextSharp.text.pdf.qrcode;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;

namespace RMS.Web.Controllers
{
     [Authorize]
    public class MessageInfoController : BaseController
    {
        private IMessageInfoManager _messageInfoManager;
        private IRoleManager _roleManager;

         public MessageInfoController( IMessageInfoManager messageInfoManager, IRoleManager roleManager)
        {
            _messageInfoManager = messageInfoManager;
            _roleManager = roleManager;
        }
        public ActionResult Index(string id, MessageInfoViewModel model)
        {
            model.MessageInfos = _messageInfoManager.GetAll();
            return View(model);
        }
        
        
        public ActionResult Edit(string id, MessageInfoViewModel model)
        {
            ModelState.Clear();
            model.MessageInfo = _messageInfoManager.GetById(Convert.ToInt32(id)) ?? new MessageInfo();
            model.Roles = _roleManager.GetAll();
            return View(model);
        }
        public ActionResult Save(string id, MessageInfoViewModel model)
        {
            TempData["message"] = _messageInfoManager.Save(model.MessageInfo);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id)
        {
           _messageInfoManager.Delete(id);
           return RedirectToAction("Index");
        }
	}
}