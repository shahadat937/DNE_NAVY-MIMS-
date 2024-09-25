using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.DashBoardTreeView.ShipInactive
{
  public  class ShipInactiveChartofAccountAdapter :IShipInactiveTarget
    {
          private readonly List<ShipInactiveDescription> _updateOPlStates;

          public ShipInactiveChartofAccountAdapter(List<ShipInactiveDescription> updateOPlStates)
        {
            _updateOPlStates = updateOPlStates;
        }
       public List<ShipInactiveChartsOfAccount> GetTreeView()
        {
            return GetChartofAccounts();
        }

       private List<ShipInactiveChartsOfAccount> GetChartofAccounts()
        {
            var chartofAccounts = new List<ShipInactiveChartsOfAccount>();
            foreach (ShipInactiveDescription controlAccount in _updateOPlStates)
            {
                if (controlAccount != null && controlAccount.ParentCode == 0)
                {
                    chartofAccounts.Add(ConvertChartofAcount(controlAccount));
                }
                else
                {
                    foreach (ShipInactiveChartsOfAccount tree in chartofAccounts)
                    {
                        if (controlAccount != null && tree.id == controlAccount.ParentCode)
                        {
                            tree.Nodes.Add(ConvertChartofAcount(controlAccount));
                        }
                        else
                        {
                            if (tree.Nodes.Any())
                            {
                                foreach (ShipInactiveChartsOfAccount firstNode in tree.Nodes)
                                {
                                    if (controlAccount != null && firstNode.id == controlAccount.ParentCode)
                                    {
                                        firstNode.Nodes.Add(ConvertChartofAcount(controlAccount));
                                    }
                                    else if (firstNode.Nodes.Any())
                                    {
                                        foreach (ShipInactiveChartsOfAccount secendNode in firstNode.Nodes)
                                        {
                                            if (controlAccount != null && secendNode.id == controlAccount.id)
                                            {
                                                secendNode.Nodes.Add(ConvertChartofAcount(controlAccount));
                                            }
                                            if (secendNode.Nodes.Any())
                                            {
                                                foreach (ShipInactiveChartsOfAccount thirdNode in secendNode.Nodes)
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
       private ShipInactiveChartsOfAccount ConvertChartofAcount(ShipInactiveDescription control)
        {
            return control != null
                ? new ShipInactiveChartsOfAccount()
                {
                         
                        ParentCode =control.ParentCode,
                        id = control.id,
                        Name = control.Name,
                        LEVEL = control.LEVEL,
               firstLevel =control.firstLevel,
               SecendLevel =control.SecendLevel,
               ThirdLevel =control.ThirdLevel,
            


                    //ControlName = control.ControlName,
                    //ControlValue = control.ControlValue,
                    //AdditionalValue=control.AdditionalValue,
                    //Remarks=control.Remarks,
                    //ControlLevel = control.ControlLevel,
                    //SortOrder = control.SortOrder,
                        Nodes = new List<ShipInactiveChartsOfAccount>(),
                   // IsActive = control.IsActive
                }
                : new ShipInactiveChartsOfAccount();
        }
    }
}
