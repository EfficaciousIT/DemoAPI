using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SKSchoolApi.Models;

namespace SKSchoolApi.Controllers
{
    public class OnlineClassTimetableController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string intusertype_id, string intSchool_id)
        {
            OnlineClassTimetable onlineClassTimetable = new OnlineClassTimetable();
            onlineClassTimetable.intusertype_id = Convert.ToInt32(intusertype_id);
            onlineClassTimetable.intSchool_id = Convert.ToInt32(intSchool_id);
            DataSet ds = record.OnlineClassTimetable(command, onlineClassTimetable);
            return ds;
        }
        public DataSet Get(string command, string intStandard_id, string intAcademic_id, string intSchool_id, string dtLecture_date)
        {
            OnlineClassTimetable onlineClassTimetable = new OnlineClassTimetable();
            onlineClassTimetable.intStandard_id = Convert.ToInt32(intStandard_id);
            onlineClassTimetable.intAcademic_id = Convert.ToInt32(intAcademic_id);
            onlineClassTimetable.intSchool_id = Convert.ToInt32(intSchool_id);
            onlineClassTimetable.dtLecture_date = Convert.ToString(dtLecture_date);
            DataSet ds = record.OnlineClassTimetable(command, onlineClassTimetable);
            return ds;
        }
        public DataSet Get(string command, int intTeacher_id, string intAcademic_id, string intSchool_id, string dtLecture_date, string intStandard_id)
        {
            OnlineClassTimetable onlineClassTimetable = new OnlineClassTimetable();
            onlineClassTimetable.intTeacher_id = Convert.ToInt32(intTeacher_id);
            onlineClassTimetable.intStandard_id = Convert.ToInt32(intStandard_id);
            onlineClassTimetable.intAcademic_id = Convert.ToInt32(intAcademic_id);
            onlineClassTimetable.intSchool_id = Convert.ToInt32(intSchool_id);
            onlineClassTimetable.dtLecture_date = Convert.ToString(dtLecture_date);
            DataSet ds = record.OnlineClassTimetable(command, onlineClassTimetable);
            return ds;  
        }
        //public DataSet Get(string command, string intAcademic_id, string intSchool_id, string dtLecture_date)
        //{
        //    OnlineClassTimetable onlineClassTimetable = new OnlineClassTimetable();
        //    onlineClassTimetable.intAcademic_id = Convert.ToInt32(intAcademic_id);
        //    onlineClassTimetable.intSchool_id = Convert.ToInt32(intSchool_id);
        //    onlineClassTimetable.dtLecture_date = Convert.ToString(dtLecture_date);
        //    DataSet ds = record.OnlineClassTimetable(command, onlineClassTimetable);
        //    return ds;
        //}
        public DataSet Get(string command, int intAcademic_id, int intSchool_id, string intOnlinelecture_id)
        {
            OnlineClassTimetable onlineClassTimetable = new OnlineClassTimetable();
            onlineClassTimetable.intAcademic_id = Convert.ToInt32(intAcademic_id);
            onlineClassTimetable.intSchool_id = Convert.ToInt32(intSchool_id);
            onlineClassTimetable.intOnlinelecture_id = Convert.ToInt32(intOnlinelecture_id);
            DataSet ds = record.OnlineClassTimetable(command, onlineClassTimetable);
            return ds;
        }
    }
}
