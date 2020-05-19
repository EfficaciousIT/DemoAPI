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
    public class StudentStandardWiseController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string intSchool_id, string intStandard_id, string intAcademic_id, string intDivision_id)
        {
            StudentStandardWise studentstandardwise = new StudentStandardWise();
            studentstandardwise.intschool_id = Convert.ToInt32(intSchool_id);
            studentstandardwise.Standard_id = Convert.ToInt32(intStandard_id);
            studentstandardwise.Academic_id = Convert.ToInt32(intAcademic_id);
            studentstandardwise.Division_id = Convert.ToInt32(intDivision_id);
            DataSet ds = record.StudentStandardwiseDetail(command, studentstandardwise);
            return ds;
        }
    }
}
