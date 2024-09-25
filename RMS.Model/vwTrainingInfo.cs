using System;
using System.Collections.Generic;

namespace RMS.Model
{
    public partial class vwTrainingInfo
    {
        public int TrainingInfoId { get; set; }
        public string ONOorPNO { get; set; }
        public string TrainerName { get; set; }
        public string Ranks { get; set; }
        public string TrainingType { get; set; }
        public string NameOfCourse { get; set; }
        public string TrainingLocation { get; set; }
        public System.DateTime TrainingFrom { get; set; }
        public System.DateTime TrainingTo { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<long> CreateUserId { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<long> UpdateUserId { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public virtual CommonCode CommonCode1 { get; set; }

    }
}
