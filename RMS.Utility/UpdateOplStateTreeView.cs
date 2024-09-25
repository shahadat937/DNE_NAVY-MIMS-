using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Utility
{
   public class UpdateOplStateTreeView
    {
       public List<UpdateOplStateTreeView> ChildOPL { get; set; }
       public UpdateOplStateTreeView()
       {
           this.ChildOPL = new List<UpdateOplStateTreeView>();
       }
        //public string MenuId { get; set; }
        //public System.Guid ApplicationId { get; set; }
        //public string UserId { get; set; }
        //public string MenuObjectName { get; set; }
        //public string MenuObjectDescription { get; set; }
        //public string UrlLink { get; set; }
        //public Nullable<bool> AddInfo { get; set; }
        //public Nullable<bool> ChangeInfo { get; set; }
        //public Nullable<bool> DeleteInfo { get; set; }
        //public Nullable<bool> ExecuteProc { get; set; }
        //public Nullable<bool> Visible { get; set; }
        //public string TableName { get; set; }
        //public string SPName { get; set; }
        //public string GroupName { get; set; }
        //public string MenuLevel { get; set; }
        //public string FirstLevel { get; set; }
        //public string SecondLevel { get; set; }
        //public string ThirdLevel { get; set; }
        //public System.DateTime LastUpdate { get; set; }
       public System.DateTime today { get; set; }
       public long ControlShipInfoId { get; set; }
       public Nullable<int> ControlLevel { get; set; }
       public decimal ParentCode { get; set; }
       public string ControlName { get; set; }
       public Nullable<long> statusCode { get; set; }
       public Nullable<long> Organization { get; set; }
       public string ORG { get; set; }
       public string Status { get; set; }
       public string Remarks { get; set; }
       public Nullable<int> TotalShip { get; set; }
       public Nullable<int> NonOpl { get; set; }
       public Nullable<int> Opl { get; set; }
    }
}
