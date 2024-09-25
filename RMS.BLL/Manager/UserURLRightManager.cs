using System;
using System.Collections.Generic;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class UserURLRightManager : BaseManager, IUserURLRightManager
    {
        private IUserURLRightRepository _userURLRightRepository;
        private IRoleUserURLRightMappingRepository _roleUserURLRightMappingRepository;
        
        RM_AGBEntities db = new RM_AGBEntities();
        public UserURLRightManager()
        {
            _userURLRightRepository = new UserURLRightRepository(Instance.Context);
            _roleUserURLRightMappingRepository = new RoleUserURLRightMappingRepository(Instance.Context);
        }
        public List<UserRightTreeView> GetUserRightTreeView(string userRole)
        {
            //string roleName = _userRepository.GetUserRoleNameByUserName(userId);
            string rquery = "select RoleId from Role where RoleName='" + userRole + "'";
            int roleId = db.Database.SqlQuery<int>(rquery).SingleOrDefault();
            string uurquery= "select uur.UserUrlRightsId, uur.MenuId, uur.ApplicationId, uur.UserId, uur.MenuObjectName, uur.MenuObjectDescription, uur.UrlLink, uur.AddInfo, uur.ChangeInfo, uur.DeleteInfo, uur.ExecuteProc, uur.Visible, uur.TableName, uur.SPName, uur.GroupName, uur.MenuLevel, uur.FirstLevel, uur.SecondLevel, uur.ThirdLevel, uur.LastUpdate from UserURLRights uur left join RoleUserURLRightMapping ruurm on ruurm.UserUrlRightsId = uur.UserUrlRightsId where ruurm.RoleId =" + roleId+" and ruurm.Visible = 1";
            var userRights = db.Database.SqlQuery<UserURLRight>(uurquery).ToList(); 
            var menuList = new List<UserRightTreeView>();
            foreach (var userRight in userRights)
            {
                if (userRight.MenuLevel == "1")
                {

                    var menu = new UserRightTreeView()
                    {
                        //UserId = userRight.UserId,
                        MenuLevel = userRight.MenuLevel,
                        FirstLevel = userRight.FirstLevel,
                        MenuId = userRight.MenuId,
                        MenuObjectName = userRight.MenuObjectName,
                        UrlLink = userRight.UrlLink

                    };
                    menuList.Add(menu);

                }

            }

            foreach (var userRightTreeView in menuList)
            {
                foreach (var menu1 in userRights)
                {
                    if (userRightTreeView.MenuId == menu1.FirstLevel && menu1.MenuLevel == "2")
                    {
                        var submenu = new UserRightTreeView()
                        {
                            //UserId = menu1.UserId,
                            MenuLevel = menu1.MenuLevel,
                            FirstLevel = menu1.FirstLevel,
                            MenuId = menu1.MenuId,
                            MenuObjectName = menu1.MenuObjectName,
                            UrlLink = menu1.UrlLink
                        };
                        userRightTreeView.ChildUserRight.Add(submenu);
                    }

                }

            }

            return menuList;
        }

        public List<UserURLRightModel> GetUserRighsByRole(string userRole)
        {
            string query = "select RoleId from Role where RoleName='" + userRole + "'";
            int roleId = db.Database.SqlQuery<int>(query).SingleOrDefault();


            string urlRightQuery = "select IsNull((select RoleUserURLRightMappingId from RoleUserURLRightMapping where UserUrlRightsId=uur.UserUrlRightsId and RoleId="+ roleId + "), 0) as RoleUserURLRightMappingId, uur.UserUrlRightsId, "+roleId+" as RoleId, uur.MenuObjectName, uur.UrlLink, IsNull((select Visible from RoleUserURLRightMapping where UserUrlRightsId=uur.UserUrlRightsId and RoleId="+ roleId + "), 0)as Visible from UserURLRights as uur where uur.UserId = 'DNE User'";
            List<UserURLRightModel> userURLRights = db.Database.SqlQuery<UserURLRightModel>(urlRightQuery).ToList();


            return userURLRights;
        }

        public ResponseModel Save(List<UserURLRightModel> model)
        {

            if (model == null)
            {
                ResponseModel.Message="Batch Information is missing";
            }
            int count = 0;
            foreach (var item in model)
            {
                RoleUserURLRightMapping roleUserURLRightMapping = _roleUserURLRightMappingRepository.FindOne(x => x.RoleId == item.RoleId && x.UserUrlRightsId == item.UserUrlRightsId);
                if (roleUserURLRightMapping == null)
                {
                    RoleUserURLRightMapping rum = new RoleUserURLRightMapping()
                    {
                        UserUrlRightsId = item.UserUrlRightsId,
                        RoleId = item.RoleId,
                        Visible = item.Visible
                    };

                    count += _roleUserURLRightMappingRepository.Save(rum);

                }
                else
                {
                    roleUserURLRightMapping.Visible = item.Visible;
                    count += _roleUserURLRightMappingRepository.Edit(roleUserURLRightMapping);

                }
            }
            if (count > 0)
            {
                ResponseModel.Message = "Data has been updated";
                ResponseModel.ResponsStatus = 1;
            }
            else
            {
                ResponseModel.Message = "Data is not updated.";
            }
            return ResponseModel;
        }
    }
}
