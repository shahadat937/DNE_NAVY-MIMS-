//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Module
    {
        public Module()
        {
            this.AccountTypeInfoes = new HashSet<AccountTypeInfo>();
        }
    
        public string ModuleCode { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string TypeofAccount { get; set; }
        public string UserId { get; set; }
        public System.DateTime LastUpdate { get; set; }
    
        public virtual ICollection<AccountTypeInfo> AccountTypeInfoes { get; set; }
    }
}
