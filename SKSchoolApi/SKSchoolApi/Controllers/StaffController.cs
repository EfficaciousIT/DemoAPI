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
    public class StaffController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string intSchool_id)
        {
            TeacherDetail teacherdetail = new TeacherDetail();
            teacherdetail.intSchool_id = Convert.ToInt32(intSchool_id);
            DataSet ds = record.StaffDetails(command, teacherdetail);
            return ds;
        }
    }
}
