using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public partial class FortNightly
    {
        public long FortNightlyIdentity { get; set; }
        public long ControlShipIdentity { get; set; }
        public Nullable<long> WorkCategoy { get; set; }
        public string WrNo { get; set; }
        public Nullable<long> DrType { get; set; }
        public string No { get; set; }
        public Nullable<System.DateTime> DrDate { get; set; }
        public string Description { get; set; }
        public Nullable<long> Docket { get; set; }
        public string PMH { get; set; }
        public bool TwentyFive { get; set; }
        public Nullable<System.DateTime> TwentyFiveDate { get; set; }
        public string TwentyFiveId { get; set; }
        public bool Fifty { get; set; }
        public Nullable<System.DateTime> FiftyDate { get; set; }
        public string FiftyId { get; set; }
        public bool SeventyFive { get; set; }
        public Nullable<System.DateTime> SeventyFiveDate { get; set; }
        public string SeventyFiveId { get; set; }
        public bool Complete { get; set; }
        public Nullable<System.DateTime> CompleteDate { get; set; }
        public string CompleteId { get; set; }
        public string Remarks { get; set; }
        public string CreateId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string LastUpdateId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public virtual CommonCode CommonCode1 { get; set; }
        public virtual ControlShipInfo ControlShipInfo { get; set; }
    }
}
