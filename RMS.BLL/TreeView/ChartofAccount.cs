using System.Collections.Generic;
using RMS.Model;

namespace RMS.BLL.TreeView
{
   public class ChartofAccount:AccountInfo
    {
       public List<ChartofAccount> Nodes { get; set; }
       public ChartofAccount()
       {
           Nodes = new List<ChartofAccount>();
       }
    }
}
