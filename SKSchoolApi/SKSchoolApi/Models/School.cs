using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSchoolApi.Models
{
    public class School
    {
        public int intSchool_id { get; set; }
        public int intAcademic_id { get; set; }
        public string vchSchool_name { get; set; }
        public string AcademicYear { get; set; }
    }
}