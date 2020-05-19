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
    public class OnlineClassScheduleController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string intStandard_id, string intAcademic_id, string intSchool_id, string dtLecture_date)
        {
            OnlineClassSchedule onlineClassSchedule = new OnlineClassSchedule();
            onlineClassSchedule.intStandard_id = Convert.ToInt32(intStandard_id);
            onlineClassSchedule.intAcademic_id = Convert.ToInt32(intAcademic_id);
            onlineClassSchedule.intSchool_id = Convert.ToInt32(intSchool_id);
            onlineClassSchedule.dtLecture_date = dtLecture_date;
            DataSet ds = record.OnlineClassSchedule(command, onlineClassSchedule);
            return ds;
        }
        public DataSet Get(string command, int intTeacher_id, string intAcademic_id, string intSchool_id, string dtLecture_date)
        {
            OnlineClassSchedule onlineClassSchedule = new OnlineClassSchedule();
            onlineClassSchedule.intTeacher_id = Convert.ToInt32(intTeacher_id);
            onlineClassSchedule.intAcademic_id = Convert.ToInt32(intAcademic_id);
            onlineClassSchedule.intSchool_id = Convert.ToInt32(intSchool_id);
            onlineClassSchedule.dtLecture_date = dtLecture_date;
            DataSet ds = record.OnlineClassSchedule(command, onlineClassSchedule);
            return ds;
        }
        public DataSet Get(string command, string intAcademic_id, string intSchool_id, string dtLecture_date)
        {
            OnlineClassSchedule onlineClassSchedule = new OnlineClassSchedule();
            onlineClassSchedule.intAcademic_id = Convert.ToInt32(intAcademic_id);
            onlineClassSchedule.intSchool_id = Convert.ToInt32(intSchool_id);
            onlineClassSchedule.dtLecture_date = dtLecture_date;
            DataSet ds = record.OnlineClassSchedule(command, onlineClassSchedule);
            return ds;
        }
    }
}
