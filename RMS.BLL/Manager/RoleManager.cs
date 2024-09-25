using System.Collections.Generic;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public  class RoleManager:BaseManager, IRoleManager
    {
        private IRoleRepository _roleRepository;
        public RoleManager()
        {
            _roleRepository = new RoleRepository(Instance.Context);
        }

        public List<Role> GetAll()
        {
            List<Role> roles=  _roleRepository.All().ToList();
            return roles;
        }

        public Role FindOne(int roleId)
        {
            return _roleRepository.FindOne(x => x.RoleId == roleId);
            
        }
    }
}
