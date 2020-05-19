using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSchoolApi.Models
{
    public class Attendance
    {
        public int intSchool_id { get; set; }
        public int intUserType_id { get; set; }
        public int Standard_id { get; set; }
        public String Standard_Id { get; set; }
        public int intDivision_id { get; set; }
        public int intAcademic_id { get; set; }
        public string dtDate { get; set; }
        public string status { get; set; }
        public int userId { get; set; }

        public String FCMToken { get; set; }
    }
}