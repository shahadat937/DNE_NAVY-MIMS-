using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public partial class BooksAndBr
    {
        public long BooksAndBrIdentity { get; set; }
        public string BooksAndBrName { get; set; }
        public string UrlLink { get; set; }
        public string UserId { get; set; }
        public System.DateTime LastUpdate { get; set; }
    }
}
