using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSchoolApi.Models
{
    public class Event
    {
        public string vchStandard_id { get; set; }
        public int intAcademic_id { get; set; }
        public int intSchool_id { get; set; }
        public int intStandard_id { get; set; }
        public int intDivision_id { get; set; }
        public int intEvent_id { get; set; }
        public int intUser_id { get; set; }
        public int intUserType_id { get; set; }
        public string dtRegistrartionStartDate { get; set; }
        public string dtRegistrationEndDate { get; set; }
        public string dtEventStartDate { get; set; }
        public string dtEventEndDate { get; set; }
        public string vchEventName { get; set; }
        public string vchEventFees { get; set; }
        public string vchEventDescription { get; set; }
    }
}