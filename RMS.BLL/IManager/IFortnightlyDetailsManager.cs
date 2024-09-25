using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
    public interface IFortnightlyDetailsManager
    {
        List<FortnightlyDetail> GetAll();
        FortnightlyDetail GetFortnightlyDetailById(string id);
        object Delete(string id);
        ResponseModel Saving(FortnightlyDetail fortnightlyDetail);

        List<FortnightlyDetail> GetFortnightlyDetailAll();


        ResponseModel SaveFortnightlyDetail(FortnightlyDetail fortnightlyDetail);

        List<FortnightlyDetail> FindOne(long id);

    }
}
