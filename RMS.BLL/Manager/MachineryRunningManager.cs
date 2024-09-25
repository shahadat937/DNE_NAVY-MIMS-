using System;
using System.Collections;
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
    public class MachineryRunningManager : BaseManager, IMachineryRunningManager
    {
        private readonly IMachineryRunningRepository _machineryRunningRepository;

        public MachineryRunningManager()
        {

            _machineryRunningRepository = new MachineryRunningRepository(Instance.Context);

        }

        public List<MachineryRunningInfo> GetAll()
        {
            return _machineryRunningRepository.All().Include(x => x.ControlShipInfo).Include(x => x.CommonCode).Include(x=>x.CommonCode1).ToList();
        }

        public MachineryRunningInfo GetShip(string id, MachineryRunningInfo machineryRunningInfo)
        {
            var shipid = Convert.ToInt64(id);
            var costCentre = _machineryRunningRepository.FindOne(x => x.MachineryRunningIdentity == shipid);
            return costCentre;
        }

        public List<MachineryRunningInfo> FindOne(long id)
        {
            return _machineryRunningRepository.Filter(x => x.ShipId == id).ToList();
        }

        public object Delete(string id)
        {

            var machineid = Convert.ToInt64(id);
            return _machineryRunningRepository.Delete(x => x.MachineryRunningIdentity == machineid);

        }

        public ResponseModel Saving(MachineryRunningInfo model)
        {

            MachineryRunningInfo oldData = _machineryRunningRepository.FindOne(x => x.MachineryRunningIdentity == model.MachineryRunningIdentity);
            if (oldData != null)
            {
                oldData.ShipId = model.ShipId;
                oldData.HourRunTime = model.HourRunTime;
                oldData.Hour = model.Hour;
                oldData.Minute = model.Minute;
                oldData.Type = model.Type;
                oldData.CompletionDate = model.CompletionDate;
                oldData.UpdatedBy = PortalContext.CurrentUser.UserName;
                oldData.LastUpdate = DateTime.Now;
                _machineryRunningRepository.Edit(oldData);
                ResponseModel.Message = "Machinery information is updated successfully.";
            }
            else
            {
                model.LastUpdate = DateTime.Now;
                model.UserId = PortalContext.CurrentUser.UserName;
                model.EntryDate = DateTime.Now;
                model.UpdatedBy = PortalContext.CurrentUser.UserName;
                model.IsVerified = false;
                _machineryRunningRepository.Save(model);
                ResponseModel.Message = "Machinery information is saved successfully.";
            }
            return ResponseModel;
        }

        public MachineryRunningInfo GetShipId(string searchKey)
        {
            return _machineryRunningRepository.Filter(x => x.ControlShipInfo.ControlName.Contains(searchKey)).FirstOrDefault();
        }

        public List<MachineryRunningInfo> GetReportData(DateTime fromdate, DateTime toDate, long shipid)
        {
            return _machineryRunningRepository.Filter(
   x => x.ShipId == shipid && x.LastUpdate >= fromdate && x.LastUpdate <= toDate).Include(x => x.ControlShipInfo).Include(x=>x.CommonCode).Include(x=>x.CommonCode1).ToList();
        }
    }
}
