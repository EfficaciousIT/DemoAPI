using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSchoolApi.Models
{
    public class Standard
    {
        public string vchStandard_name { get; set; }
        public string vchDivisionName { get; set; }
        public string vchSubjectName { get; set; }
        public string TeacherName { get; set; }
        public string messageStatus { get; set; }
        public int intSchool_id { get; set; }
        public int intAcademic_id { get; set; }
        public int intDivision_id { get; set; }
        public int intTeacher_id { get; set; }
        public string vchType { get; set; }
        public int intStandard_id { get; set; }

        public string vchComment { get; set; }
        public int intSubject_id { get; set; }
        public string dtDatetime { get; set; }
        public string vchFileName { get; set; }
        public string vchFilePath { get; set; }
        public int int_Approval { get; set; }
        public string vchFilePath2 { get; set; }
        public string vchFilePath3 { get; set; }
        public int intMy_id { get; set; }
    }
}