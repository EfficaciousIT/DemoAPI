using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSchoolApi.Models
{
    public class Profile
    {
        public int intUser_id { get; set; }
        public string vchProfile { get; set; }
        public string vchStudentName { get; set; }
        public string vchLastName { get; set; }
        public string vchAdminName { get; set; }
        public string vchTeacherName { get; set; }
        public string vchStaffName { get; set; }
        public string vchEmailAddress { get; set; }
        public string vchMobileNo { get; set; }
        public int intschool_id { get; set; }
        public int intAcademic_id { get; set; }
        public string vchAddress { get; set; }
    }
}