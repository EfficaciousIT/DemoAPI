using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSchoolApi.Models
{
    public class OnlineClassSchedule
    {
        public int intOnlineNotice_id { get; set; }
        public int intUserType_id { get; set; }
        public int intStandard_id { get; set; }
        public int intDivision_id { get; set; }
        public int intStudent_id { get; set; }
        public int intDepartment_id { get; set; }
        public int intTeacher_id { get; set; }
        public int dtIssue_date { get; set; }
        public int dtEnd_date { get; set; }
        public string vchSubject { get; set; }
        public string vchonlineNotice { get; set; }
        public int vchLectureBy { get; set; }
        public string vchMeetingId { get; set; }
        public string vchpassword { get; set; }
        public int intSchool_id { get; set; }
        public int intAcademic_id { get; set; }
        public int intOnlinelecture_id { get; set; }
        public string dtLecture_date { get; set; }
    }
}