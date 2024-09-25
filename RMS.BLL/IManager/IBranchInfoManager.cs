using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IBranchInfoManager
    {
        List<BranchInfo> GetBankList();
        int TotalOpl(long branchId);
        int TotalNonOpl(long branchId);
        List<BranchInfo> GetDistrictList(string bankCode);
        List<BranchInfo> GetBranchList(string districtCode);
        List<BranchInfo> GetBranchList();
        List<ZoneInfo> ZoneInfoes();

        ZoneInfo FineOneZoneInfo(long zoneInfoIdenity);
        ResponseModel SaveZoneInfo(ZoneInfo zoneInfo);

        ResponseModel DeleteZoneInfo(long zoneInfoIdentity);
        string BranchName(long? BranchIdentity);

        BranchInfo FineOneByBranchInfoIdentity(long? branchInfoIdentiy);
        ResponseModel SaveBankInfo(BranchInfo branchInfo);

        ResponseModel DeleteBank(long branchInfoIdentity);

        ResponseModel SaveDistrictInfo(BranchInfo branchInfo);

        ResponseModel DeleteDistrict(long branchInfoIdentity);

        List<BranchInfo> GetBranchListByBankAndDistrict(string bankCode, string districtCode);

        ResponseModel SaveBranchInfo(BranchInfo branchInfo);

        BranchInfo FineOneByBranchCode(string branchCode);


        List<BranchInfo> GetExchangeHouseDivisionList(string bankCode);

        List<BranchInfo> GetSubBranchInfoes(string branchCode);
        List<ControlShipInfo> GetControlShipInfoes();

        ResponseModel SaveSubBranchInfo(BranchInfo branchInfo);

        ResponseModel DeleteBranchInfo(long branchInfoIdentity);

        ResponseModel DeleteSubBranchInfo(long branchInfoIdentity);


        List<BranchInfo> GetFourthLevelList();

        List<BranchInfo> ExchangeHouseList();

        BankInfo GetBankInfo(long respondingBranch);

        List<BankInfo> GetBankListFromBankInfo();
        List<BankInfo> GetOrgFromBankInfo();

        List<BankInfo> GetDistrictListFromBankInfo(string p);

        List<BranchInfo> GetBranchListByBankCode();
        List<BranchInfo> GetOrganizationList();
       

        BranchInfo FindExchangeByLCAccountNo(string accountLC);

        ZoneInfo FineOneZoneInfoByCode(string zoneCode);

        List<BranchInfo> GetDistrictListByIdentity(string bankCodeIdentity);

        List<BranchInfo> GetBranchListByDistrictIdentity(int districtIdentity);
    }
}
