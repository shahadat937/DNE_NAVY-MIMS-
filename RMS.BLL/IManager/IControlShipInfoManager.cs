using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RMS.BLL.TreeView;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IControlShipInfoManager
    {
        List<ShipChartofAccount> GetChartofAccount();

        ControlShipInfo GetControlAccountById(long? controlId);
        ControlShipInfo GetControlShipInfoById(long? controlId);
        List<vwPontoon> GetvwPontoons(long? controlId);
        List<ControlShipInfo> GetAll();
        List<vwShipBranchInfo> ShipBranchInfo();
        List<vwShipBankInfo> ShipBankInfo();
        string ShipBankName(long? ConId);
        string ShipBranchName(long? ShipId);

        List<vwShipBankInfo> FMsgShipBankInfo();
        List<vwShipBankInfo> TMsgShipBankInfo();
        ControlShipInfo FindOne(string id);
        long GetMaxControlCode(decimal newParentCode);

        //int EditControlAccount(ControlShipInfo model);

        long SaveControlAccoun(ControlShipInfo model);
        List<ControlShipInfo> AllByControlLevel(int controlLevel);
        IEnumerable GetBranchList(string branchCode);
        List<ControlShipInfo> GetShipAndShipCategory();
        //new
        IEnumerable GetDistrictList1(string bankCode);

        int Delete(long controlShipInfoIdentity);
        int EditControlAccount(ControlShipInfo model, string[] controlnm, string[] controlvle);
        int SaveControlAccount(ControlShipInfo model, string[] controlnm, string[] controlvle);
        List<ControlShipInfo> GetAllShipList();
        //new
        List<ControlShipInfo> GetBankList();
        List<ControlShipInfo> GetControlShipInfoByOrganizationId(long organizationId);
        List<ControlShipInfo> Totalship();
        //new
        List<ControlShipInfo> GetBranchListByBankCode(string bankCode);
        //object GetDistrictList(string bankCode);
        ResponseModel ShipStatusUpdate(long controlShipInfoIdentity, long? shipStatus);
        string GetShipName(long shipId);
    }
}
