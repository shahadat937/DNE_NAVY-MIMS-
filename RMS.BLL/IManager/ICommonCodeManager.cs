using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
   public interface ICommonCodeManager
    {
       ResponseModel SaveCommonCode(CommonCode model);
       List<CommonCode> GetValidPhotoId();
       List<CommonCode> GetCommonCodeTeeView(string BankCode,string Type);
       CommonCode FindOne(long code);
       List<CommonCode> GetCommonCode(CommonCode mCommonCode, string BankCode);
       List<CommonCode> GetCommonCodeByType(string Type);
       List<CommonCode> GetNationalities(string branchCode);
       List<CommonCode> GetCommonType(string BankCode);
       List<CommonCode> GetCommonTotalship();

       List<CommonCode> GetPhotoIdes(string branchCode);

       List<CommonCode> GetOccupationList(string branchCode);

       List<CommonCode> GetIssuePlaceList(string branchCode);
       List<CommonCode> GetSecurityQustions(string bankCode);
       List<CommonCode> GetOtherBankPaymentPermissionList(string bankCode);
       List<CommonCode> GetRemittanceHeader();
       List<CommonCode> GetConditionList();

       List<CommonCode> GetCurrencyList();

       List<CommonCode> GetTransactionModeList();

       List<CommonCode> GetTransactionNatureList();

       List<CommonCode> GetPaymentModeList();

       List<CommonCode> GetVoucherTypeList();
       string FindOilName(long? fuelOilId);
       CommonCode GetTypeValue(long command);
       string GetCommonName(long? sqd);
       string GetCourseName(long? RankId);
       string GetRank(long? Course);
    }
}
