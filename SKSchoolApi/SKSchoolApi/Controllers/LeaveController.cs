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
    public class LeaveController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string intAcademic_id, string intUserType_id, string intUser_id, string intSchool_id, string intTeacher_id)
        {
            Leave leave = new Leave();
            leave.intAcademic_id = Convert.ToInt32(intAcademic_id);
            leave.intUserType_id = Convert.ToInt32(intUserType_id);
            leave.intUser_id = Convert.ToInt32(intUser_id);
            leave.intSchool_id = Convert.ToInt32(intSchool_id);
            leave.intTeacher_id = Convert.ToInt32(intTeacher_id);
            DataSet ds = record.LeaveDetail(command, leave);
            return ds;
        }
        public HttpResponseMessage Post(string command, Leave leave)
        {
            try
            {
                DataSet ds = record.LeaveDetail(command, leave);
                var message = Request.CreateResponse(HttpStatusCode.Created);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Put(string command, Leave leave)
        {
            try
            {
                DataSet ds = record.LeaveDetail(command, leave);
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
