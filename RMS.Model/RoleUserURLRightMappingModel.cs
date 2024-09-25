using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public class RoleUserURLRightMappingModel
    {
        public int RoleUserURLRightMappingId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<long> UserUrlRightsId { get; set; }
        public bool Visible { get; set; }
    }
}
