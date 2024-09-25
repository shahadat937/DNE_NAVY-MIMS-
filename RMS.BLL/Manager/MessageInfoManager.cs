using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.BLL.Manager
{
    public class MessageInfoManager : BaseManager, IMessageInfoManager
    {
        private IMessageInfoRepository _messageInfoRepository;

        public MessageInfoManager()
        {
            _messageInfoRepository = new MessageInfoRepository(Instance.Context);
        }

        public List<MessageInfo> GetAll()
        {
            return _messageInfoRepository.All().Include(x => x.Role).ToList();
        }
        public async Task<string> GetCurrentMessageByRoleId(int roleId)
        {
            return await _messageInfoRepository.Filter(x => x.MessageFor==roleId &&  x.IsActive == true).FirstOrDefaultAsync() !=null? await _messageInfoRepository.Filter(x => x.MessageFor == roleId && x.IsActive == true).Select(x => x.Message).FirstOrDefaultAsync(): null ;
        }
        public List<MessageInfo> GetNotAll()
        {
            return _messageInfoRepository.All().Include(x => x.Role).Where(x => SqlFunctions.DateDiff("DD", x.LastUpdate, DateTime.Now) < 8).ToList();
        }
        public MessageInfo GetById(long id)
        {
            return _messageInfoRepository.FindOne(x => x.MessageInfoIdentity == id);
        }

        //public void Save(MessageInfo model)
        //{

        //}

        public MessageInfo FindOne(int id)
        {
            return _messageInfoRepository.FindOne(x => x.MessageInfoIdentity == id);
        }


        public long GetMaxId()
        {
            long returnValue = 0;
            var list = _messageInfoRepository.All();
            if (list.Any())
            {
                returnValue = _messageInfoRepository.All().Max(x => x.MessageInfoIdentity);
            }
            return returnValue;
        }

        public void Delete(long id)
        {
            _messageInfoRepository.Delete(x => x.MessageInfoIdentity == id);
        }

        public ResponseModel Save(MessageInfo messageInfo)
        {
            if (messageInfo.IsActive == true)
            {
                MessageInfo currentMessage = _messageInfoRepository.Filter(x => x.MessageFor == messageInfo.MessageFor && x.IsActive == true).FirstOrDefault();
                currentMessage.IsActive = false;
                _messageInfoRepository.Edit(currentMessage);
            }

            MessageInfo oldData =
                _messageInfoRepository.FindOne(x => x.MessageInfoIdentity == messageInfo.MessageInfoIdentity);
            if (oldData != null)
            {
                oldData.MessageInfoIdentity = messageInfo.MessageInfoIdentity;
                oldData.MessageFromDate = messageInfo.MessageFromDate;
                oldData.MessageToDate = messageInfo.MessageToDate;
                oldData.Message = messageInfo.Message;
                oldData.MessageFor = messageInfo.MessageFor;
                oldData.IsActive = messageInfo.IsActive;
                oldData.Remark = messageInfo.Remark;
                oldData.Message = messageInfo.Message;
                oldData.LastUpdate = DateTime.Now;
                oldData.UserId = PortalContext.CurrentUser.UserName;
                _messageInfoRepository.Edit(oldData);
                ResponseModel.Message = "Message information is updated successfully.";
            }
            else
            {
                var newData = new MessageInfo()
                {
                    MessageInfoIdentity = messageInfo.MessageInfoIdentity,
                    MessageFromDate = messageInfo.MessageFromDate,
                    MessageToDate = messageInfo.MessageToDate,
                    Message = messageInfo.Message,
                    MessageFor = messageInfo.MessageFor,
                    IsActive = messageInfo.IsActive,
                    Remark = messageInfo.Remark,
                    CreateDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    UserId = PortalContext.CurrentUser.UserName
                };
                _messageInfoRepository.Save(newData);
                ResponseModel.Message = "message information is saved successfully.";
            }
            return ResponseModel;
        }






    }
}
