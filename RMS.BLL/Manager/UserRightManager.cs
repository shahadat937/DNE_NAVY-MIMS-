using System.Collections.Generic;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class UserRightManager :BaseManager, IUserRightManager
    {
        private readonly IUserRightRepository _userRightRepository;
        private readonly IUserRepository _userRepository;
        public UserRightManager()
        {
            _userRightRepository = new UserRightRepository(Context);
            _userRepository = new UserRepository(Context); 
        }


        public List<UserRightTreeView> GetUserRightTreeView(string userId)
        {
            string roleName = _userRepository.GetUserRoleNameByUserName(userId);
            var userRights = _userRightRepository.Filter(x =>  x.Visible == true &&( x.UserId == roleName ||roleName==null)).OrderBy(x => x.MenuLevel).ToList();
         

            var menuList=new List<UserRightTreeView>();
            foreach (var userRight in userRights)
            {
                if (userRight.MenuLevel=="1")
                {

                    var menu = new UserRightTreeView()
                    {
                        UserId = userRight.UserId,
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
                            UserId = menu1.UserId,
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
    }
}
