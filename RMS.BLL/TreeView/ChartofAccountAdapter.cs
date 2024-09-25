using System.Collections.Generic;
using System.Linq;
using RMS.Model;

namespace RMS.BLL.TreeView
{
    public class ChartofAccountAdapter:ITarget
    {
        private readonly List<AccountInfo> _controlAccounts;
        public ChartofAccountAdapter(List<AccountInfo> controlAccounts)
        {
            _controlAccounts = controlAccounts;
        }
        public List<ChartofAccount> GetTreeView()
        {
          return  GetChartofAccounts();
        }

        private List<ChartofAccount> GetChartofAccounts()
        {
            var chartofAccounts=new List<ChartofAccount>();
            foreach (AccountInfo controlAccount in _controlAccounts)
            {
                if (controlAccount!=null&& controlAccount.ParentCode == 0)
                {
                    chartofAccounts.Add(ConvertChartofAcount(controlAccount));
                }
                else
                {
                    foreach (ChartofAccount tree in chartofAccounts)
                    {
                        if (controlAccount != null && tree.ControlCode == controlAccount.ParentCode)
                        {
                            tree.Nodes.Add(ConvertChartofAcount(controlAccount));
                        }
                        else
                        {
                            if (tree.Nodes.Any())
                            {
                                foreach (ChartofAccount firstNode in tree.Nodes)
                                {
                                    if (controlAccount != null && firstNode.ControlCode == controlAccount.ParentCode)
                                    {
                                        firstNode.Nodes.Add(ConvertChartofAcount(controlAccount));
                                    }
                                    else if (firstNode.Nodes.Any())
                                    {
                                        foreach (ChartofAccount secendNode in firstNode.Nodes)
                                        {
                                            if (controlAccount != null && secendNode.ControlCode == controlAccount.ParentCode)
                                            {
                                                secendNode.Nodes.Add(ConvertChartofAcount(controlAccount));
                                            }
                                            if (secendNode.Nodes.Any())
                                            {
                                                foreach (ChartofAccount thirdNode in secendNode.Nodes)
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

        private ChartofAccount ConvertChartofAcount(AccountInfo control)
        {
            return control != null
                ? new ChartofAccount()
                {
                    AccountInfoId = control.AccountInfoId,
                    ParentCode = control.ParentCode,
                    ControlCode = control.ControlCode,
                    ControlName = control.ControlName,
                    ControlLevel = control.ControlLevel,
                    SortOrder = control.SortOrder,
                    Nodes = new List<ChartofAccount>(),
                    IsActive = control.IsActive
                }
                : new ChartofAccount();
        }
    }
}
