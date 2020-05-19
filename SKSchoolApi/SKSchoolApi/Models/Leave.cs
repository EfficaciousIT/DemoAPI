using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSchoolApi.Models
{
    public class Leave
    {
        public string Name { get; set; }
        public int intUserType_id { get; set; }
        public int intType_id { get; set; }
        public int intUser_id { get; set; }
        public string dtFrom_date { get; set; }
        public string dtTo_Date { get; set; }
        public string vchReason { get; set; }
        public int intTotalDays { get; set; }
        public int bitAdminApproval { get; set; }
        public int intSchool_id { get; set; }
        public int intLeaveType_id { get; set; }
        public int intAcademic_id { get; set; }
        public string vchLeaveType { get; set; }
        public int intCL { get; set; }
        public int intML { get; set; }
        public int intTeacher_id { get; set; }
        public int intLeaveApplocation_id { get; set; }
    }
}