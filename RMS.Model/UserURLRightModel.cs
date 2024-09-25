using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public class UserURLRightModel
    {
        public int RoleUserURLRightMappingId { get; set; }
        public long UserUrlRightsId { get; set; }
        public int RoleId { get; set; }
        public string MenuObjectName { get; set; }
        public string UrlLink { get; set; }
        public bool Visible { get; set; }
    }
}
