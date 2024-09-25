using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
   public class ProcurementScheduleManager:BaseManager,IProcurementScheduleManager
    {
        private IProcurementScheduleRepository _procurementRepository;

        public ProcurementScheduleManager()
        {
            _procurementRepository=new ProcurementScheduleRepository(Instance.Context);
        }
        public List<ProcurementSchedule> GetAll()
        {
            return _procurementRepository.All().Include(x => x.CommonCode).ToList();
        }


        public ProcurementSchedule GetProcurmentmentById(long ProcurementID)
       {
           return _procurementRepository.FindOne(x => x.ProcurementId == ProcurementID);
       }
        public List<ProcurementSchedule> FindOne(long id)
       {
           return _procurementRepository.Filter(x => x.ProcurementId == id).ToList();
       }

        public List<ProcurementSchedule> FindOne1(DateTime DateFrom, DateTime DateTo)
        {
            return _procurementRepository.Filter(x => x.LastUpdate <DateFrom && x.LastUpdate >DateTo).ToList();
        }

       public object Delete(string id)
       {
           var shipid = Convert.ToInt64(id);
           return _procurementRepository.Delete(x => x.ProcurementId == shipid);
       }

       public ResponseModel Save(ProcurementSchedule procurement)
       {

           ProcurementSchedule oldData = _procurementRepository.FindOne(x => x.ProcurementId == procurement.ProcurementId);
           if (oldData != null)
           {

               oldData.ProcurementId = procurement.ProcurementId;
               oldData.ProcurementType = procurement.ProcurementType;
               oldData.Description = procurement.Description;
               oldData.Qty = procurement.Qty;
               oldData.EstTotalPrice = procurement.EstTotalPrice;
               oldData.Currency = procurement.Currency;
               oldData.AIPReceived = procurement.AIPReceived;
               oldData.SpecSentToDTS = procurement.SpecSentToDTS;

               oldData.SpecSentToNSSD = procurement.SpecSentToNSSD;
               oldData.SpecSentToDGDP = procurement.SpecSentToDGDP;
               oldData.TenderOpened = procurement.TenderOpened;
               oldData.QuatationRec = procurement.QuatationRec;
               oldData.AccepSentToDTS = procurement.AccepSentToDTS;
               oldData.AccepSentToNSSD = procurement.AccepSentToNSSD;
               oldData.AccepSentToDGDP = procurement.AccepSentToDGDP;
               oldData.AccepSentToAFD = procurement.AccepSentToAFD;

               oldData.ApprovedByAFD = procurement.ApprovedByAFD;
               oldData.ConttractSigned = procurement.ConttractSigned;
               oldData.Taka = procurement.Taka;
               oldData.DTSReTender = procurement.DTSReTender;
               oldData.DGDPReTender = procurement.DGDPReTender;
               oldData.NSSDReTender = procurement.NSSDReTender;
               oldData.TenderOpenReTender = procurement.TenderOpenReTender;
               oldData.QuotRecReTender = procurement.QuotRecReTender;

               oldData.Remark = procurement.Remark;

               oldData.UpdatedBy = PortalContext.CurrentUser.UserName;
               oldData.LastUpdate = DateTime.Now;
               _procurementRepository.Edit(oldData);
               ResponseModel.Message = "Procurement Schedule is updated successfully.";
           }
           else
           {
               if (procurement.ProcurementType.ToString() =="THROUGH DGDP")
               {
                 
               }

              
                   procurement.LastUpdate = DateTime.Now;
                   procurement.UserId = PortalContext.CurrentUser.UserName;
                   procurement.EntryDate = DateTime.Now;
                   procurement.UpdatedBy = PortalContext.CurrentUser.UserName;
                   procurement.IsVerified = false;
                   _procurementRepository.Save(procurement);
                   ResponseModel.Message = "Procurement Schedule is saved successfully.";

           }
           return ResponseModel;
    }
    }
}
