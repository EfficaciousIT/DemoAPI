using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SKSchoolApi.Models;
using System.Data;

namespace SKSchoolApi.Controllers
{
    public class APKVersionController : ApiController
    {
        SKSchoolApi.Database.DB record = new SKSchoolApi.Database.DB();
        public DataSet Get(string command, string intschool_id)
        {
            APKVersion aPKVersion = new APKVersion();
            aPKVersion.intschool_id = Convert.ToInt32(intschool_id);
            DataSet ds = record.APKVersionDetails(command, aPKVersion);
            return ds;
        }
        [HttpPost]
        public DataSet Post(string command, APKVersion aPKVersion)
        {
            DataSet ds = record.APKVersionDetails(command, aPKVersion);
            var message = Request.CreateResponse(HttpStatusCode.Created);
            return ds;
        }
    }
}
