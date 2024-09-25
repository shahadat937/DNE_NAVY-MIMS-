using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using RMS.BLL.IManager;
using RMS.DAL;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using HasCode;

namespace RMS.BLL.Manager
{
    public class CommonCodeManager : BaseManager, ICommonCodeManager
    {

        private ICommonCodeRepository _commonCodeRepository;

        public CommonCodeManager()
        {

            _commonCodeRepository = new CommonCodeRepository(Context);
        }

        public List<CommonCode> GetValidPhotoId()
        {
            return _commonCodeRepository.Filter(x => x.Type == "OtherIDType" && x.BankCode == PortalContext.CurrentUser.BankCode).ToList();
        }
        public List<CommonCode> GetCommonCodeTeeView(string BankCode,string Type)
        {
            var firstLevel = new List<CommonCode>();
            var secondLevel = new List<CommonCode>();

            var commonCodeTrees = _commonCodeRepository.Filter(x => x.BankCode == BankCode && x.Type == Type);
            var commonCodeTree1 = _commonCodeRepository.Filter(x => x.BankCode == BankCode && x.Type==Type);          
            foreach (CommonCode commonCodeTree in commonCodeTrees)
            {
                firstLevel.Add(commonCodeTree);
                //secondLevel.Add(commonCodeTree);
            }
            //foreach (CommonCode commonCode in firstLevel)
            //{
            //    commonCode.CommonCodeTrees = GetFirstChaild(commonCode.Type, secondLevel).ToList();
            //}
           
            return firstLevel;
        }
        //private IEnumerable<CommonCode> GetFirstChaild(string Type, List<CommonCode> secondLevel)
        //{
        //    secondLevel = secondLevel.Where(x => x.Type == Type).ToList();            
        //    return secondLevel;
        //}

        public List<CommonCode> GetCommonType(string BankCode)
        {
            return _commonCodeRepository.Filter(x=> x.BankCode == BankCode).ToList();
        }

        public List<CommonCode> GetCommonTotalship()
        {
           return  _commonCodeRepository.Filter(x => x.Type == "ShipStatus" && x.Status==true).ToList();
        }
        public CommonCode FindOne(long code)
        {
            return _commonCodeRepository.FindOne(x => x.CommonCodeID == code);
        }
        public List<CommonCode> GetCommonCode(CommonCode mCommonCode, string BankCode)
        {
            return _commonCodeRepository.Filter(x => x.BankCode==BankCode).ToList();
        }
        public List<CommonCode> GetCommonCodeByType(string Type)
        {
            return _commonCodeRepository.Filter(x => x.Type==Type).ToList();
        }
        public ResponseModel SaveCommonCode(CommonCode model)
        {
            var rec = _commonCodeRepository.Filter(x => x.Code == model.Code && x.Type==model.Type && x.BankCode==model.BankCode).ToList();
            if (rec.Count <= 0)
            {
                long cid = _commonCodeRepository.All().Max(x => x.CommonCodeID) +1;
                var CCode = new CommonCode
                {
                    CommonCodeID = cid,
                    BankCode=model.BankCode,
                    Code = model.Code.Trim(),
                    Type=model.Type,
                    TypeValue=model.TypeValue,
                    DisplayCode=model.DisplayCode,
                    Status=model.Status,
                    BTypeValue = model.BTypeValue
                };

                ResponseModel.ResponsStatus = _commonCodeRepository.Save(CCode);
                ResponseModel.Message = "Common Code Saved successfully";
            }
            else
            {
                var recInfo = rec.First();
                //recInfo.BankCode = PortalContext.CurrentUser.BankCode;
                //recInfo.Code = model.Code;
                recInfo.Type = model.Type;
                recInfo.TypeValue = model.TypeValue;
                recInfo.DisplayCode = model.DisplayCode;
                recInfo.Status = model.Status;
                recInfo.BTypeValue = model.BTypeValue;
                ResponseModel.ResponsStatus = _commonCodeRepository.Edit(recInfo);
                ResponseModel.Message = "Common Code Updated successfully";

            }
            return ResponseModel;

        }
        public List<CommonCode> GetNationalities(string branchCode)
        {
            return _commonCodeRepository.Filter(x => x.BankCode == branchCode && x.Type == "Nationality").ToList();
        }

        public List<CommonCode> GetPhotoIdes(string branchCode)
        {
            return _commonCodeRepository.Filter(x => x.BankCode == branchCode && x.Type == "PhotoIdType").ToList();
        }

        public List<CommonCode> GetOccupationList(string branchCode)
        {
            return _commonCodeRepository.Filter(x => x.BankCode == branchCode && x.Type == "Occupation").ToList();
        }

        public List<CommonCode> GetIssuePlaceList(string branchCode)
        {
            return _commonCodeRepository.Filter(x => x.BankCode == branchCode).ToList();
        }

        public List<CommonCode> GetSecurityQustions(string bankCode)
        {
            return
                _commonCodeRepository.Filter(x => x.BankCode == bankCode && x.Type == CommonCodeType.SecurityQuestion.ToString()).ToList();
        }

        public List<CommonCode> GetOtherBankPaymentPermissionList(string bankCode)
        {
            return _commonCodeRepository.Filter(x => x.BankCode == bankCode && x.Type == "OtherBankPayment").ToList();
        }
        public List<CommonCode> GetRemittanceHeader()
        {
            List<CommonCode> remittanceInfoHead = _commonCodeRepository.Filter(x => x.Type == "Inquery" && x.Status == true).ToList();

            return remittanceInfoHead;
        }
        public List<CommonCode> GetConditionList()
        {
            List<CommonCode> condition = _commonCodeRepository.Filter(x => x.Type == "EqualorBetween" && x.Status == true).ToList();

            return condition;
        }

        public List<CommonCode> GetCurrencyList()
        {
            return _commonCodeRepository.Filter(x => x.Type == "Currency" && x.Status==true).ToList();
        }

        public List<CommonCode> GetTransactionModeList()
        {
            return _commonCodeRepository.Filter(x => x.Type == "Transaction Mode").ToList();
        }

        public List<CommonCode> GetTransactionNatureList()
        {
            return _commonCodeRepository.Filter(x => x.Type == "TransactionNature").ToList();
        }

        public List<CommonCode> GetPaymentModeList()
        {
            return
                _commonCodeRepository.Filter(
                    x => x.Type == "PaymentMode" && x.BankCode == PortalContext.CurrentUser.BankCode).ToList();
        }

        public List<CommonCode> GetVoucherTypeList()
        {
            return
                _commonCodeRepository.Filter(
                    x => x.Type == "VoucherType" && x.BankCode == PortalContext.CurrentUser.BankCode && x.Status).OrderBy(x=>x.TypeValue).ToList();
        }

        public string FindOilName(long? fuelOilId)
        {
            return _commonCodeRepository.FindOne(x => x.CommonCodeID == fuelOilId).BTypeValue;
        }

        public CommonCode GetTypeValue(long command)
        {
            return _commonCodeRepository.FindOne(x => x.CommonCodeID == command);
        }
        public string GetCommonName(long? sqd)
        {
            return _commonCodeRepository.FindOne(x => x.CommonCodeID == sqd).TypeValue;
        }

        public string GetCourseName(long? RankId)
        {
            return _commonCodeRepository.FindOne(x => x.CommonCodeID == RankId).TypeValue;
        }
        public string GetRank(long? Course)
        {
            return _commonCodeRepository.FindOne(x => x.CommonCodeID == Course).TypeValue;
        }
    }
}