using SKSchoolApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SKSchoolApi.Controllers
{
    public class EventController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string vchStandard_id, string intAcademic_id, string intSchool_id, string intUser_id)
        {
            Event eventdetail = new Event();
            eventdetail.intAcademic_id = Convert.ToInt32(intAcademic_id);
            eventdetail.intSchool_id = Convert.ToInt32(intSchool_id);
            eventdetail.intUser_id = Convert.ToInt32(intUser_id);
            eventdetail.vchStandard_id = vchStandard_id;
            DataSet ds = record.EventDetail(command, eventdetail);
            return ds;
        }
        public HttpResponseMessage Post(string command, Event eventdetail)
        {
            try
            {
                DataSet ds = record.EventDetail(command, eventdetail);
                var message = Request.CreateResponse(HttpStatusCode.Created);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
