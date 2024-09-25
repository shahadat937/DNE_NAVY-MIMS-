using System.Collections.Generic;

namespace RMS.BLL.TreeView
{
    public interface ITarget
    {
        List<ChartofAccount> GetTreeView();
    }
}
