
namespace RMS.Model
{
    using System;   
    public partial class UserProfile
    {
        public long UserProfileId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }
        public string NationalId { get; set; }
        public Nullable<long> Designation { get; set; }
        public Nullable<long> Sex { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<System.DateTime> JoiningDate { get; set; }
        public long UserId { get; set; }
        public string PhotographUrl { get; set; }
        public string SignatureUrl { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public virtual CommonCode CommonCode1 { get; set; }
        public virtual User User { get; set; }
    }
}
