using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.TreeView;
using RMS.Model;

namespace RMS.BLL.DashBoardTreeView.UpdateOperationState
{
   public class UpdateChartofAccountAdapter : IUpdateTarget
    {

       private readonly List<UpdateOPlState> _updateOPlStates;

       public UpdateChartofAccountAdapter(List<UpdateOPlState> updateOPlStates)
        {
            _updateOPlStates = updateOPlStates;
        }
       public List<UpdateChartsOfAccount> GetTreeView()
        {
            return GetChartofAccounts();
        }

       private List<UpdateChartsOfAccount> GetChartofAccounts()
        {
            var chartofAccounts = new List<UpdateChartsOfAccount>();
            foreach (UpdateOPlState controlAccount in _updateOPlStates)
            {
                if (controlAccount != null && controlAccount.ParentCode == 0)
                {
                    chartofAccounts.Add(ConvertChartofAcount(controlAccount));
                }
                else
                {
                    foreach (UpdateChartsOfAccount tree in chartofAccounts)
                    {
                        if (controlAccount != null && tree.Id == controlAccount.ParentCode)
                        {
                            tree.Nodes.Add(ConvertChartofAcount(controlAccount));
                        }
                        else
                        {
                            if (tree.Nodes.Any())
                            {
                                foreach (UpdateChartsOfAccount firstNode in tree.Nodes)
                                {
                                    if (controlAccount != null && firstNode.Id == controlAccount.ParentCode)
                                    {
                                        firstNode.Nodes.Add(ConvertChartofAcount(controlAccount));
                                    }
                                    else if (firstNode.Nodes.Any())
                                    {
                                        foreach (UpdateChartsOfAccount secendNode in firstNode.Nodes)
                                        {
                                            if (controlAccount != null && secendNode.Id == controlAccount.Id)
                                            {
                                                secendNode.Nodes.Add(ConvertChartofAcount(controlAccount));
                                            }
                                            if (secendNode.Nodes.Any())
                                            {
                                                foreach (UpdateChartsOfAccount thirdNode in secendNode.Nodes)
                                                {
                                                    if (controlAccount != null && thirdNode.Id == controlAccount.Id)
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
       private UpdateChartsOfAccount ConvertChartofAcount(UpdateOPlState control)
        {
            return control != null
                ? new UpdateChartsOfAccount()
                {
                         
                        ParentCode =control.ParentCode,
                        Id = control.Id,
                        Name = control.Name,
                        LEVEL = control.LEVEL,
               firstLevel =control.firstLevel,
               SecendLevel =control.SecendLevel,
               ThirdLevel =control.ThirdLevel,
               nonOPL =control.nonOPL,
               opl =control.opl,


                    //ControlName = control.ControlName,
                    //ControlValue = control.ControlValue,
                    //AdditionalValue=control.AdditionalValue,
                    //Remarks=control.Remarks,
                    //ControlLevel = control.ControlLevel,
                    //SortOrder = control.SortOrder,
                    Nodes = new List<UpdateChartsOfAccount>(),
                   // IsActive = control.IsActive
                }
                : new UpdateChartsOfAccount();
        }
    }
}
