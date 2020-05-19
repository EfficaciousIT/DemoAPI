using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSchoolApi.Models
{
    public class Noticeboard
    {
        public int intUserType_id { get; set; }
        public int intStandard_id { get; set; }
        public int intDepartment_id { get; set; }
        public int intTeacher_id { get; set; }
        public string dtIssue_date { get; set; }
        public string dtEnd_date { get; set; }
        public string vchSubject { get; set; }
        public string vchNotice { get; set; }
        public int intInserted_by { get; set; }
        public String InsertIP { get; set; }
        public int intSchool_id { get; set; }
    }
}