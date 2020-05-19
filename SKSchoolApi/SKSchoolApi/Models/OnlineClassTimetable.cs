using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSchoolApi.Models
{
    public class OnlineClassTimetable
    {
        public int intOnlinelecture_id { get; set; }
        public int vchLecture_name { get; set; }
        public int intStandard_id { get; set; }
        public int intDivision_id { get; set; }
        public string dtLecture_date { get; set; }
        public string dtFromTime { get; set; }
        public string dtToTime { get; set; }
        public int intTeacher_id { get; set; }
        public int intSubject_id { get; set; }
        public int intSchool_id { get; set; }
        public int intAcademic_id { get; set; }
    }
}