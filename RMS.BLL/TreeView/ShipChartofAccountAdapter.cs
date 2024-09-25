using System.Collections.Generic;
using System.Linq;
using RMS.Model;

namespace RMS.BLL.TreeView
{
    public class ShipChartofAccountAdapter:IShipTarget
    {
        private readonly List<ControlShipInfo> _controlShipInfos;

        public ShipChartofAccountAdapter(List<ControlShipInfo> controlShipInfos)
        {
            _controlShipInfos = controlShipInfos;
        }
        public List<ShipChartofAccount> GetTreeView()
        {
            return GetChartofAccounts();
        }

        private List<ShipChartofAccount> GetChartofAccounts()
        {
            var chartofAccounts = new List<ShipChartofAccount>();
            foreach (ControlShipInfo controlAccount in _controlShipInfos)
            {
                if (controlAccount != null && controlAccount.ParentCode == 0)
                {
                    chartofAccounts.Add(ConvertChartofAcount(controlAccount));
                }
                else
                {
                    foreach (ShipChartofAccount tree in chartofAccounts)
                    {
                        if (controlAccount != null && tree.ControlCode == controlAccount.ParentCode)
                        {
                            tree.Nodes.Add(ConvertChartofAcount(controlAccount));
                        }
                        else
                        {
                            if (tree.Nodes.Any())
                            {
                                foreach (ShipChartofAccount firstNode in tree.Nodes)
                                {
                                    if (controlAccount != null && firstNode.ControlCode == controlAccount.ParentCode)
                                    {
                                        firstNode.Nodes.Add(ConvertChartofAcount(controlAccount));
                                    }
                                    else if (firstNode.Nodes.Any())
                                    {
                                        foreach (ShipChartofAccount secendNode in firstNode.Nodes)
                                        {
                                            if (controlAccount != null && secendNode.ControlCode == controlAccount.ParentCode)
                                            {
                                                secendNode.Nodes.Add(ConvertChartofAcount(controlAccount));
                                            }
                                            if (secendNode.Nodes.Any())
                                            {
                                                foreach (ShipChartofAccount thirdNode in secendNode.Nodes)
                                                {
                                                    if (controlAccount != null && thirdNode.ControlCode == controlAccount.ParentCode)
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
        private ShipChartofAccount ConvertChartofAcount(ControlShipInfo control)
        {
            return control != null
                ? new ShipChartofAccount()
                {
                    ControlShipInfoId = control.ControlShipInfoId,
                    ParentCode = control.ParentCode,
                    ControlCode = control.ControlCode,
                    ControlName = control.ControlName,
                    ControlValue = control.ControlValue,
                    AdditionalValue=control.AdditionalValue,
                    Remarks=control.Remarks,
                    ControlLevel = control.ControlLevel,
                    SortOrder = control.SortOrder,
                    Nodes = new List<ShipChartofAccount>(),
                    IsActive = control.IsActive
                }
                : new ShipChartofAccount();
        }
    }
}
