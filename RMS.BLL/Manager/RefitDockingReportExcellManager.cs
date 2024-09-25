using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.Manager
{
    public class RefitDockingReportExcellManager : BaseManager, IRefitDockingReportExcellManager
    {
        private IRefitDockingReportExcellRepository _refitDockingReportExcellRepository;

        public RefitDockingReportExcellManager()
        {
            _refitDockingReportExcellRepository = new RefitDockingReportExcellRepository(Instance.Context);
        }
        public void Delete(long id)
        {
            _refitDockingReportExcellRepository.Delete(x => x.RefitDockingReportExcellId == id);
        }

        public RefitDockingReportExcell FindImage(long id)
        {
            throw new NotImplementedException();
        }

        public RefitDockingReportExcell FindOne(long id)
        {
            RefitDockingReportExcell model = new RefitDockingReportExcell();
            model = _refitDockingReportExcellRepository.FindOne(x => x.RefitDockingReportExcellId == id);

            return model;
        }

        public List<RefitDockingReportExcell> GetAll()
        {
            //if (PortalContext.CurrentUser.RoleId == 1)
            //{
            //    return _refitDockingReportExcellRepository.Filter(x => x.DockId == PortalContext.CurrentUser.ShipId).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();
            //}
            //else if (PortalContext.CurrentUser.RoleId == 4)
            //{
            //    return _refitDockingReportExcellRepository.Filter(x => x.ControlShipInfo.Organization == PortalContext.CurrentUser.BranchInfoIdentity).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();

            //}
            //else
            //{

            //}
            //List<RefitDockingReportExcell> model = new List<RefitDockingReportExcell>();
            //model = _refitDockingReportExcellRepository.All().Include(x => x.CommonCode).ToList();

            //return model;
            throw new NotImplementedException();

        }

        public List<RefitDockingReportExcell> GetAllByDockId(long id)
        {
            List<RefitDockingReportExcell> model = new List<RefitDockingReportExcell>();
            model = _refitDockingReportExcellRepository.Filter(x => x.DockId == id).ToList();

            return model;
        }

        public long GetMaxId()
        {
            throw new NotImplementedException();
        }

        public ResponseModel Save(RefitDockingReportExcell refitDockingReportExcell)
        {
            RefitDockingReportExcell oldData = _refitDockingReportExcellRepository.FindOne(x => x.RefitDockingReportExcellId == refitDockingReportExcell.RefitDockingReportExcellId);
            if (oldData != null)
            {
                oldData.DockId = refitDockingReportExcell.DockId;
                oldData.DocumentType = refitDockingReportExcell.DocumentType;
                oldData.Name = refitDockingReportExcell.Name;
                oldData.FromYear = refitDockingReportExcell.FromYear;
                oldData.ToYear = refitDockingReportExcell.ToYear;
                oldData.CreateDate = refitDockingReportExcell.CreateDate;
                oldData.ImageUrl = refitDockingReportExcell.ImageUrl == "" ? oldData.ImageUrl : refitDockingReportExcell.ImageUrl;
                oldData.UserId = PortalContext.CurrentUser.UserName;
                oldData.LastUpdate = DateTime.Now;
                _refitDockingReportExcellRepository.Edit(oldData);
                ResponseModel.Message = "Documentation information is updated successfully.";
            }
            else
            {
                refitDockingReportExcell.UserId = PortalContext.CurrentUser.UserName;
                refitDockingReportExcell.LastUpdate = DateTime.Now.Date;
                _refitDockingReportExcellRepository.Save(refitDockingReportExcell);
                ResponseModel.Message = "Documentation information is saved successfully.";
            }
            return ResponseModel;
        }
    }
}
