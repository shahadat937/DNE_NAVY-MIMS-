using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.DashBoardTreeView.ProcurementByDne
{
   public class ProcurementChartofAccountAdapter : IProcurementTarget
    {
        private readonly List<ProcurementByDNE> _updateOPlStates;

        public ProcurementChartofAccountAdapter(List<ProcurementByDNE> updateOPlStates)
        {
            _updateOPlStates = updateOPlStates;
        }
       public List<ProcurementChartsOfAccount> GetTreeView()
        {
            return GetChartofAccounts();
        }

       private List<ProcurementChartsOfAccount> GetChartofAccounts()
        {
            var chartofAccounts = new List<ProcurementChartsOfAccount>();
            foreach (ProcurementByDNE controlAccount in _updateOPlStates)
            {
                if (controlAccount != null && controlAccount.ParentCode == 0)
                {
                    chartofAccounts.Add(ConvertChartofAcount(controlAccount));
                }
                else
                {
                    foreach (ProcurementChartsOfAccount tree in chartofAccounts)
                    {
                        if (controlAccount != null && tree.id == controlAccount.ParentCode)
                        {
                            tree.Nodes.Add(ConvertChartofAcount(controlAccount));
                        }
                        else
                        {
                            if (tree.Nodes.Any())
                            {
                                foreach (ProcurementChartsOfAccount firstNode in tree.Nodes)
                                {
                                    if (controlAccount != null && firstNode.id == controlAccount.ParentCode)
                                    {
                                        firstNode.Nodes.Add(ConvertChartofAcount(controlAccount));
                                    }
                                    else if (firstNode.Nodes.Any())
                                    {
                                        foreach (ProcurementChartsOfAccount secendNode in firstNode.Nodes)
                                        {
                                            if (controlAccount != null && secendNode.id == controlAccount.id)
                                            {
                                                secendNode.Nodes.Add(ConvertChartofAcount(controlAccount));
                                            }
                                            if (secendNode.Nodes.Any())
                                            {
                                                foreach (ProcurementChartsOfAccount thirdNode in secendNode.Nodes)
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
       private ProcurementChartsOfAccount ConvertChartofAcount(ProcurementByDNE control)
        {
            return control != null
                ? new ProcurementChartsOfAccount()
                {
                         
                        ParentCode =control.ParentCode,
                        id = control.id,
                        Name = control.Name,
                        LEVEL = control.LEVEL,
               firstLevel =control.firstLevel,
               SecendLevel =control.SecendLevel,
               ThirdLevel =control.ThirdLevel,
            Quantity =control.Quantity,
            EstTotal =control.EstTotal,
            Currency =control.Currency,

                 
                    //ControlName = control.ControlName,
                    //ControlValue = control.ControlValue,
                    //AdditionalValue=control.AdditionalValue,
                    //Remarks=control.Remarks,
                    //ControlLevel = control.ControlLevel,
                    //SortOrder = control.SortOrder,
                        Nodes = new List<ProcurementChartsOfAccount>(),
                   // IsActive = control.IsActive
                }
                : new ProcurementChartsOfAccount();
        }
    }
}
