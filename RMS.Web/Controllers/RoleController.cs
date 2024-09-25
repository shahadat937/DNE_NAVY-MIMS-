using System.Net;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Model;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class RoleController : BaseController
    {
        private IRoleManager _roleManager;
        public RoleController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }
        public ActionResult Index()
        {
            return View(_roleManager.GetRoles());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RoleId,RoleName,LoweredRoleName,Description,IsActive")] Role role)
        {
            //if (ModelState.IsValid)
            {
                ResponseModel = _roleManager.SaveUser(role);
                if (ResponseModel.ResponsStatus > 0)
                {
                    TempData["message"] = ResponseModel.Message;
                }
                else
                {
                    TempData["message"] = ResponseModel.Message;
                }
            }
            return View(role);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = _roleManager.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RoleId,RoleName,LoweredRoleName,Description,IsActive")] Role role)
        {
            if (ModelState.IsValid)
            {
                ResponseModel = _roleManager.Edit(role);
                if (ResponseModel.ResponsStatus > 0)
                {
                    TempData["message"] = ResponseModel.Message;
                }
            }
            return View(role);
        }

    }
}
