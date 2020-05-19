using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSchoolApi.Models
{
    public class TimeTable
    {
        public int intTeacher_id { get; set; }
        public int intAcademic_id { get; set; }
        public int intSchool_id { get; set; }
        public String Day { get; set; }
        public int intStandard_id { get; set; }
        public int intDivision_id { get; set; }
    }
}