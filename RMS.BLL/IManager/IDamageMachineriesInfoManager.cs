using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;

namespace RMS.BLL.IManager
{
    public interface IDamageMachineriesInfoManager
    {
        List<DamageMachineriesInfo> GetAll();
        DamageMachineriesInfo GetShip(string id, DamageMachineriesInfo damageMachineriesInfo);
        List<DamageMachineriesInfo> FindOne(long id);

        List<DamageMachineriesInfo> GetMachineHour();


        List<MonthlyReturn> GetAllMonthlyReturn();
        List<MonthlyReturn> GetSearchMonthlyReturn(int? monthId, int? yearId, string shipId, string monthlyReturnId);
        List<MonthlyReturn> GetMonthlyReturnByReturnStatus();
        List<MonthlyReturn> GetMonthlyReturnByShipId(long shipId);
        string MonthlyReturnActionStatus(string id);
        MonthlyReturn GetMonthlyReturn(string id, MonthlyReturn model);
        object GetMonthlyReturn(string id);

        List<QuaterlyReturn> GetAllQuaterlyReturn();
        QuaterlyReturn GetSearchQuaterlyReturn(int? quaterlyReturnNoId, int? yearId, string shipId, string quaterlyReturnId);
        List<QuaterlyReturn> GetQuaterlyReturnByReturnStatus();
        List<QuaterlyReturn> GetQuaterlyReturnByReturnByShipId(long ShipId);
        string QuaterlyReturnActionStatus(string id);
        QuaterlyReturn GetQuaterlyReturn(string id, QuaterlyReturn model);
        object GetQuaterlyReturn(string id);
        ResponseModel SavingQuaterlyReturn(QuaterlyReturn quaterlyReturn);

        List<YearlyReturn> GetAllYearlyReturn(int returnType);
        List<YearlyReturn> GetAllYearlyReturnByShipId(long shipId);
        YearlyReturn GetYearlyReturn(string id, YearlyReturn model);
        ResponseModel SavingYearlyReturn(YearlyReturn yearlyReturn);
        object GetYearlyReturn(string id);
        List<YearlyReturn> GetYearlyReturnByYearAndShipId(YearlyReturn model);

        List<QuaterlyConsumption> GetQuaterlyConsumptionsByQuaterlyReturnId(long QuaterlyReturnId);
        List<QuaterlyMainEnginesGeneratorsRunningHour> GetQuaterlyMainEnginesGeneratorsRunningHoursByQuaterlyReturnId(long QuaterlyReturnId);




        List<DefectMachinary> GetDefectMachinary(List<MonthlyReturn> monthlyReturns);
        List<MachinaryRunningHour> GetMachinaryRunningHours(List<MonthlyReturn> monthlyReturns);
        List<POLExpenseInfo> GetPOLExpenseInfos(List<MonthlyReturn> monthlyReturns);
        List<ReturnReportNo> returnReportNoByParameter(string returnName);
        List<ReturnReportYear> returnReportYears();


        ResponseModel SavingMonthlyReturn(MonthlyReturn monthlyReturn);
        object Delete(string id);
        ResponseModel Saving(DamageMachineriesInfo damageMachineriesInfo);



        List<DamageMachineriesInfo> GetPol();
        List<DamageMachineriesInfo> GetAllForReport(long id, DateTime dateFrom);
        List<DamageMachineriesInfo> RunningHourReport(long id, DateTime dateFrom);
        List<DamageMachineriesInfo> GetPolData(long id, DateTime dateFrom);
        object UpdateStatus(string id, long status);
        List<DamageMachineriesInfo> UserWiseData(int? verifyType);
        ResponseModel VerifiedStatusUpdate(List<vwDamageMachinaries> vwDamageMachinarieses);


    }
}
