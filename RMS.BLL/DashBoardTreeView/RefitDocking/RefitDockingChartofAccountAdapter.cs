using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.DashBoardTreeView.RefitDocking
{
   public class RefitDockingChartofAccountAdapter :IRefitDockingTarget
    {
               private readonly List<ApprovedRefitDockingSchedule> _updateOPlStates;

               public RefitDockingChartofAccountAdapter(List<ApprovedRefitDockingSchedule> updateOPlStates)
        {
            _updateOPlStates = updateOPlStates;
        }
       public List<RefitDockingChartsOfAccount> GetTreeView()
        {
            return GetChartofAccounts();
        }

       private List<RefitDockingChartsOfAccount> GetChartofAccounts()
        {
            var chartofAccounts = new List<RefitDockingChartsOfAccount>();
            foreach (ApprovedRefitDockingSchedule controlAccount in _updateOPlStates)
            {
                if (controlAccount != null && controlAccount.ParentCode == 0)
                {
                    chartofAccounts.Add(ConvertChartofAcount(controlAccount));
                }
                else
                {
                    foreach (RefitDockingChartsOfAccount tree in chartofAccounts)
                    {
                        if (controlAccount != null && tree.id == controlAccount.ParentCode)
                        {
                            tree.Nodes.Add(ConvertChartofAcount(controlAccount));
                        }
                        else
                        {
                            if (tree.Nodes.Any())
                            {
                                foreach (RefitDockingChartsOfAccount firstNode in tree.Nodes)
                                {
                                    if (controlAccount != null && firstNode.id == controlAccount.ParentCode)
                                    {
                                        firstNode.Nodes.Add(ConvertChartofAcount(controlAccount));
                                    }
                                    else if (firstNode.Nodes.Any())
                                    {
                                        foreach (RefitDockingChartsOfAccount secendNode in firstNode.Nodes)
                                        {
                                            if (controlAccount != null && secendNode.id == controlAccount.id)
                                            {
                                                secendNode.Nodes.Add(ConvertChartofAcount(controlAccount));
                                            }
                                            if (secendNode.Nodes.Any())
                                            {
                                                foreach (RefitDockingChartsOfAccount thirdNode in secendNode.Nodes)
                                                {
                                                    if (controlAccount != null && thirdNode.id == controlAccount.id)
                                                    {
                                                        thirdNode.Nodes.Add(ConvertChartofAcount(controlAccount));
                                                    }

                                                }
                                            }

                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            return chartofAccounts;
        }
       private RefitDockingChartsOfAccount ConvertChartofAcount(ApprovedRefitDockingSchedule control)
        {
            return control != null
                ? new RefitDockingChartsOfAccount()
                {
                         
                        ParentCode =control.ParentCode,
                        id = control.id,
                        Name = control.Name,
                        LEVEL = control.LEVEL,
               firstLevel =control.firstLevel,
               SecendLevel =control.SecendLevel,
               ThirdLevel =control.ThirdLevel,
            LastRefitForm =control.LastRefitForm,
            LastRefitTo =control.LastRefitTo,
            LastDockingForm =control.LastDockingForm,
               LastDockingTo =control.LastDockingTo,
                 
                    //ControlName = control.ControlName,
                    //ControlValue = control.ControlValue,
                    //AdditionalValue=control.AdditionalValue,
                    //Remarks=control.Remarks,
                    //ControlLevel = control.ControlLevel,
                    //SortOrder = control.SortOrder,
                        Nodes = new List<RefitDockingChartsOfAccount>(),
                   // IsActive = control.IsActive
                }
                : new RefitDockingChartsOfAccount();
        }
    }
}
