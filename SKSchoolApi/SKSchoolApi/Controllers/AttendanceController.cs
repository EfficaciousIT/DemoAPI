using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using SKSchoolApi.Models;

namespace SKPSchool.Controllers
{
    public class AttendanceController : ApiController
    {
        SKSchoolApi.Database.DB record = new SKSchoolApi.Database.DB();
        public DataSet Get(string command, string intschool_id, string intUserType_id, string intstanderd_id, string intdivision_id, string intAcademic_id, string dtDate, string status, string intUser_id)
        {
            Attendance attendance = new Attendance();
            attendance.intSchool_id = Convert.ToInt32(intschool_id);
            attendance.intUserType_id = Convert.ToInt32(intUserType_id);
            attendance.Standard_id = Convert.ToInt32(intstanderd_id);
            attendance.intDivision_id = Convert.ToInt32(intdivision_id);
            attendance.intAcademic_id = Convert.ToInt32(intAcademic_id);
            attendance.dtDate = dtDate;
            attendance.status = status;
            attendance.userId = Convert.ToInt32(intUser_id);
            DataSet ds = record.AttendanceDetail(command, attendance);
            return ds;
        }
        public HttpResponseMessage Post(string command, Attendance attendance)
        {
            try
            {
                DataSet ds = record.AttendanceDetail(command, attendance);
                var message = Request.CreateResponse(HttpStatusCode.Created);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public DataSet Get(string command, string intschool_id, string intUser_id, string intAcademic_id)
        {
            Attendance attendance = new Attendance();
            attendance.intSchool_id = Convert.ToInt32(intschool_id);
            attendance.intAcademic_id = Convert.ToInt32(intAcademic_id);
            attendance.userId = Convert.ToInt32(intUser_id);
            DataSet ds = record.AttendanceDetail(command, attendance);
            return ds;
        }
    }
}
