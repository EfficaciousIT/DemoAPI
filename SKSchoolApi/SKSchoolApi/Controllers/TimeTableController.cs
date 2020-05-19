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
    public class TimeTableController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string intTeacher_id, string intAcademic_id, string intSchool_id, string Day, string intStandard_id,string intDivision_id)
        {
            TimeTable timetable = new TimeTable();
           timetable.intTeacher_id = Convert.ToInt32(intTeacher_id);
            timetable.intAcademic_id = Convert.ToInt32(intAcademic_id);
            timetable.intSchool_id = Convert.ToInt32(intSchool_id);
            timetable.Day = Day;
            timetable.intStandard_id = Convert.ToInt32(intStandard_id);
            timetable.intDivision_id = Convert.ToInt32(intDivision_id);

            DataSet ds = record.TimeTableDetails(command, timetable);
            return ds;
        }
    }
}
