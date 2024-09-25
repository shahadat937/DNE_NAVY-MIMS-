using System;
using System.Linq;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class UserURLRightController : Controller
    {
        private IUserURLRightManager _userURLRightManager;
        private IRoleManager _roleManager;

        public UserURLRightController(IUserURLRightManager userURLRightManager, IRoleManager roleManager)
        {
            _userURLRightManager = userURLRightManager;
            _roleManager = roleManager;
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        [HttpGet]
        public ActionResult Index(string roleName, UserURLRightViewModel model)
        {
            ModelState.Clear();
            model.Roles = PortalContext.CurrentUser.LoweredRoleName == "B" ? _roleManager.GetAll().OrderBy(x => x.Sequence).ToList() : null;
            if (roleName != null)
            {
                model.UserURLRightModels = _userURLRightManager.GetUserRighsByRole(roleName);
                model.UserUrlRight.UserId = roleName;
            }
            return View(model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        [HttpPost]
        public ActionResult UpdateUserRights(UserURLRightViewModel model)
        {
            {
                ResponseModel response = _userURLRightManager.Save(model.UserURLRightModels);
                model.Message = response.Message;
                model.IsSucceed = response.ResponsStatus;
                model.Roles = _roleManager.GetAll();
                model.UserUrlRightModel.RoleId = model.UserURLRightModels.FirstOrDefault().RoleId;
                return View("Index", model);
            }
        }
    }
}