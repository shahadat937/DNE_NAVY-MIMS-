using System;

namespace RMS.Utility
{
   public class ResponseModel
    {
       public object Data { get; set; }
       public string Message { get; set; }
       public int ResponsStatus{ get; set; }
       public Nullable<long> ShipId { get; set; }

       public long PrimaryKey { get; set; }

    }
}
