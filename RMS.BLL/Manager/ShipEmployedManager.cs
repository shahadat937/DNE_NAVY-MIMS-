using System;
using System.Collections;
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
using RMS.Web.Models.ViewModel;

namespace RMS.BLL.Manager
{
    public class ShipEmployedManager : BaseManager, IShipEmployedManager
    {
        private IShipEmployedRepository _shipEmployedRepository;

        public ShipEmployedManager()
        {
            _shipEmployedRepository = new ShipEmployedRepository(Instance.Context);
        }

        public List<ShipEmployed> GetAll()
        {
            return _shipEmployedRepository.All().Include(x => x.ShipInfo).ToList();
        }

        public ShipEmployed FindOne(int employedIdentity)
        {
            return _shipEmployedRepository.FindOne(x => x.EmployedIdentity == employedIdentity);
        }

        public ResponseModel Save(ShipEmployed model)
        {
            ShipEmployed oldData = _shipEmployedRepository.FindOne(x => x.EmployedIdentity == model.EmployedIdentity);
            if (oldData == null)
            {
                _shipEmployedRepository.Save(model);
                ResponseModel.Message = "Data is saved successfully.";
            }
            else
            {
                oldData.ShipInfoIdentity = model.ShipInfoIdentity;
                oldData.Employment = model.Employment;
                oldData.DaysFrom = model.DaysFrom;
                oldData.DaysTo = model.DaysTo;
                oldData.NoOfDays = model.NoOfDays;
                oldData.CompletionOfLast = model.CompletionOfLast;
                oldData.Date = model.Date;
                oldData.EntryDate = model.EntryDate;
                oldData.LastUpdate = model.LastUpdate;
                oldData.UserID = PortalContext.CurrentUser.UserName;
                _shipEmployedRepository.Edit(oldData);
                ResponseModel.Message = "Data is updated successfully.";
            }
            return ResponseModel;
        }

        public int Delete(long id)
        {
            ShipEmployed oldData = _shipEmployedRepository.FindOne(x => x.EmployedIdentity == id);
            if (oldData != null)
            {
                _shipEmployedRepository.Delete(x => x.EmployedIdentity == id);
                return 1;
            }
            return 0;
        }

    
    }
}
