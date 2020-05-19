using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSchoolApi.Models
{
    public class ChatDetail
    {
        public int intSchool_id { get; set; }
        public int User_id { get; set; }
        public int intstandard_id { get; set; }
        public int intDivision_id { get; set; }
        public int intAcademic_id { get; set; }

        public string FCMToken { get; set; }
        public string SenderFCMToken { get; set; }
        public string Message { get; set; }
        public string ReceiverName { get; set; }
        public string SenderName { get; set; }
        public int intUserType_id { get; set; }
        public string GRoupName { get; set; }
        public int intTeacher_id { get; set; }
    }
}