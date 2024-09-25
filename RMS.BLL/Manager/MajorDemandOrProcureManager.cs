using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using System.Data.Entity.SqlServer;

namespace RMS.BLL.Manager
{
   public class MajorDemandOrProcureManager :BaseManager,IMajorDemandOrProcureManager
    {
         private readonly IMajorDemandOrProcureRepository _majorDemandOrProcureRepository;

         public MajorDemandOrProcureManager()
        {

            _majorDemandOrProcureRepository = new MajorDemandOrProcureRepository(Instance.Context);

        }

       public List<MajorDemandOrProcure> GetAll()
       {
           return _majorDemandOrProcureRepository.All().Include(x => x.ControlShipInfo).ToList();
       }
       public List<MajorDemandOrProcure> GetNotificationAll()
       {
           return _majorDemandOrProcureRepository.All().Include(x => x.ControlShipInfo).Where(x=> (SqlFunctions.DateDiff("DD", x.LastUpdate, DateTime.Now) < 8)).ToList();
       }
       public MajorDemandOrProcure GetMajorDemand(string id, MajorDemandOrProcure majorDemandOrProcure)
       {
           var majordemandid = Convert.ToInt64(id);
           return _majorDemandOrProcureRepository.FindOne(x => x.MajorDemandIdentity == majordemandid);
       }

       public ResponseModel Saving(MajorDemandOrProcure majorDemandOrProcure)
       {
           MajorDemandOrProcure oldData = _majorDemandOrProcureRepository.FindOne(x => x.MajorDemandIdentity == majorDemandOrProcure.MajorDemandIdentity);
           if (oldData != null)
           {
               oldData.ShipIdentity = majorDemandOrProcure.ShipIdentity;
               oldData.MajorDemandName = majorDemandOrProcure.MajorDemandName;
               oldData.MajorDemandModel = majorDemandOrProcure.MajorDemandModel;
               oldData.AuthorityNumber = majorDemandOrProcure.AuthorityNumber;
               oldData.StockedNumber = majorDemandOrProcure.StockedNumber;
               oldData.EthicalApprovalNumber = majorDemandOrProcure.EthicalApprovalNumber;
               oldData.DeficitOrExtraNumber = majorDemandOrProcure.DeficitOrExtraNumber;
               oldData.ProposalBuyNumber = majorDemandOrProcure.ProposalBuyNumber;
               oldData.BeforePrice = majorDemandOrProcure.BeforePrice;
               oldData.TotalPrice = majorDemandOrProcure.TotalPrice;
               oldData.Remarks = majorDemandOrProcure.Remarks;
               oldData.UpdatedBy = PortalContext.CurrentUser.UserName;
               oldData.LastUpdate = DateTime.Now;
               _majorDemandOrProcureRepository.Edit(oldData);
               ResponseModel.Message = "Major Demand information is updated successfully.";
           }
           else
           {

               majorDemandOrProcure.LastUpdate = DateTime.Now;
               majorDemandOrProcure.UserId = PortalContext.CurrentUser.UserName;
               majorDemandOrProcure.EntryDate = DateTime.Now;
               majorDemandOrProcure.UpdatedBy = PortalContext.CurrentUser.UserName;
               majorDemandOrProcure.IsVerified = false;
               _majorDemandOrProcureRepository.Save(majorDemandOrProcure);
               ResponseModel.Message = "Major Demand information is saved successfully.";
           }
           return ResponseModel;
       }

       public object Delete(string id)
       {
           var majordemandid = Convert.ToInt64(id);
           return _majorDemandOrProcureRepository.Delete(x => x.MajorDemandIdentity == majordemandid);
       }

       public List<MajorDemandOrProcure> UserWiseData(int? verifyType)
       {
           var allData = _majorDemandOrProcureRepository.Filter(x => x.ShipIdentity == PortalContext.CurrentUser.ShipId).Include(x => x.ControlShipInfo).ToList();
           return verifyType == 1006
               ? allData.Where(x => x.IsVerified == true).ToList()
               : allData.Where(x => x.IsVerified == false).ToList();
       }

       public ResponseModel VerifiedStatusUpdate(List<MajorDemandOrProcure> majorDemandOrProcures)
       {
           foreach (var item in majorDemandOrProcures)
           {
               var oldData = _majorDemandOrProcureRepository.FindOne(x => x.MajorDemandIdentity == item.MajorDemandIdentity);
               if (oldData != null && oldData.IsVerified != item.IsVerified)
               {
                   oldData.IsVerified = item.IsVerified;
                   oldData.VerifiedBy = PortalContext.CurrentUser.UserName;
                   oldData.VerifiedDate = DateTime.Now.Date;

                   _majorDemandOrProcureRepository.Edit(oldData);
                   ResponseModel.Message = "Status Updated Successfully !";
               }
           }
           return ResponseModel;
       }
    }
}
