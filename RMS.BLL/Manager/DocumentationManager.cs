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
    public class DocumentationManager : BaseManager, IDocumentationManager
    {
        private IDocumentationRepository _documentationRepository;

        public DocumentationManager()
        {
            _documentationRepository = new DocumentationRepository(Instance.Context);
        }

        public List<Documentation> GetAll()
        {
            if (PortalContext.CurrentUser.RoleId ==1 )
            {
                return _documentationRepository.Filter(x=>x.ShipId== PortalContext.CurrentUser.ShipId).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();
            }
            else if (PortalContext.CurrentUser.RoleId == 4)
            {
                return _documentationRepository.Filter(x => x.ControlShipInfo.Organization == PortalContext.CurrentUser.BranchInfoIdentity).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();

            }
            else
            {
                return _documentationRepository.All().Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();

            }
        }

        public long GetMaxId()
        {
            long returnValue = 0;
            var list = _documentationRepository.All();
            if (list.Any())
            {
                returnValue = _documentationRepository.All().Max(x => x.DocumentationIdentity);
            }
            return returnValue;
        }

        //public void Save(Documentation model)
        //{
        //    Documentation oldData =
        //        _documentationRepository.FindOne(x => x.DocumentationIdentity == model.DocumentationIdentity);
        //    if (oldData != null)
        //    {
        //        oldData.ShipId = model.ShipId;
        //        oldData.DocumentType = model.DocumentType;
        //        oldData.Name = model.Name;
        //        oldData.CreateDate = model.CreateDate;
        //        oldData.ImageUrl = model.ImageUrl;
        //        oldData.UserId = PortalContext.CurrentUser.UserName;
        //        oldData.LastUpdate = DateTime.Now;
        //        _documentationRepository.Edit(oldData);
        //    }
        //    else
        //    {
        //        var obj = new Documentation()
        //        {
        //            ShipId = model.ShipId,
        //            DocumentType = model.DocumentType,
        //            Name = model.Name,
        //            CreateDate = model.CreateDate,
        //            ImageUrl = model.ImageUrl,
        //            UserId = PortalContext.CurrentUser.UserName,
        //            LastUpdate = DateTime.Now,
        //        };
        //        _documentationRepository.Save(obj);
        //    }
        //}

        public Documentation FindOne(long id)
        {
            return _documentationRepository.FindOne(x => x.DocumentationIdentity == id);
        }
        public Documentation FindImage(long id)
        {
            var sd= _documentationRepository.All().Where(x=>x.ShipId==id).OrderByDescending(x=>x.DocumentationIdentity);
            if (sd.Count() > 0)
            {
                return sd.First();
            }
            else
            {
                return null;
            }
            
        }
        public ResponseModel Save(Documentation documentation)
        {
            Documentation oldData =
                _documentationRepository.FindOne(x => x.DocumentationIdentity == documentation.DocumentationIdentity);
            if (oldData != null)
            {
                oldData.ShipId = documentation.ShipId;
                oldData.DocumentType = documentation.DocumentType;
                oldData.Name = documentation.Name;
                oldData.CreateDate = documentation.CreateDate;
                oldData.ImageUrl = documentation.ImageUrl == "" ? oldData.ImageUrl : documentation.ImageUrl;
                oldData.UserId = PortalContext.CurrentUser.UserName;
                oldData.LastUpdate = DateTime.Now;
                _documentationRepository.Edit(oldData);
                ResponseModel.Message = "Documentation information is updated successfully.";
            }
            else
            {
                documentation.UserId = PortalContext.CurrentUser.UserName;
                documentation.LastUpdate = DateTime.Now.Date;
                _documentationRepository.Save(documentation);
                ResponseModel.Message = "Documentation information is saved successfully.";
            }
            return ResponseModel;
        }

        public void Delete(long id)
        {
            _documentationRepository.Delete(x => x.DocumentationIdentity == id);
        }
    }
}
