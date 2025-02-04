using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RMS.Web.Controllers
{
    public class TestdataController : ApiController
    {

        [HttpGet]
        public IHttpActionResult Index()
        {
            var data = new
            {
                Id = 5,
                Name = "Internals"
            };

            return this.Json(data);
        }
    }
}
