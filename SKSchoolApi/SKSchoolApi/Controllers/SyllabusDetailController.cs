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
    public class SyllabusDetailController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string intSTD_id, string intSubject_id, string intSchool_id)
        {
            Syllabus syllabus = new Syllabus();
            syllabus.intSchool_id = Convert.ToInt32(intSchool_id);
            syllabus.intSTD_id = Convert.ToInt32(intSTD_id);
            syllabus.intSubject_id = Convert.ToInt32(intSubject_id);
            DataSet ds = record.SyllabusDetails(command, syllabus);
            return ds;
        }
    }
}
