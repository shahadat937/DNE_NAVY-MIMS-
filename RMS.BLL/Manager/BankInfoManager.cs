using System;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;

namespace RMS.BLL.Manager
{
    public class BankInfoManager:BaseManager,IBankInfoManager
    {
        private readonly IBankInfoRepository _bankInfoRepository;
        public BankInfoManager()
        {
            _bankInfoRepository = new BankInfoRepository(Instance.Context);
        }

        public BankInfo FindOne(long branchInfoIdentity)
        {
            return _bankInfoRepository.FindOne(x => x.BranchInfoIdentity == branchInfoIdentity);
        }

        public BankInfo FindOneByBranchCode(string branchCode)
        {
            return _bankInfoRepository.FindOne(x => x.Code == branchCode);
        }
    }
}
