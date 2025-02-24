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
    
    public partial class User
    {
        public User()
        {
            this.UserProfiles = new HashSet<UserProfile>();
        }

        public long UserId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public long BranchInfoIdentity { get; set; }
        public long ControlShipInfoId { get; set; }
        public Nullable<bool> IsActiveDirectory { get; set; }
        public string UserFullName { get; set; }
        public string Createdby { get; set; }
        public bool ApprovedUser { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string BankCode { get; set; }
        public Nullable<int> AttemptCount { get; set; }
        public Nullable<int> PasswordValidityInDays { get; set; }
        public Nullable<System.DateTime> LastPasswordChangeDate { get; set; }
        public Nullable<System.DateTime> LasUpdateDate { get; set; }
        public bool IsLogin { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string IPAddress { get; set; }
        public string HostName { get; set; }

        public virtual BranchInfo BranchInfo { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }

    }
}
