using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;

namespace RMS.Web.Models.ViewModel
{
    public partial class vwShipEmployed
    {
        public DateTime? fromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ShipName { get; set; }
        public int RDay { get; set; }
        public string Reportname { get; set; }
        public string SubName { get; set; }
        public string LetterNo { get; set; }
        public string Quarter { get; set; }
    }
}