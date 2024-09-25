using System.Collections.Generic;

namespace RMS.BLL.TreeView
{
    public class TreeViewBuilder
    {
        private readonly ITarget _chartofAccount;

        public TreeViewBuilder(ITarget chartofAccount)
        {
            _chartofAccount = chartofAccount;
        }

        public List<ChartofAccount> GetChartofAccount()
        {
            return _chartofAccount.GetTreeView();
        } 
    }
}
