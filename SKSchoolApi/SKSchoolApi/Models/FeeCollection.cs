using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSchoolApi.Models
{
    public class FeeCollection
    {
        public int intStudFee_id { get; set; }
        public int intSchool_id { get; set; }
        public int intAcademic_id { get; set; }
        public int intStudentID_Number { get; set; }
        public int intstandard_id { get; set; }
        public int intStudentID { get; set; }
        public string month { get; set; }
        public int intDivision_id { get; set; }
        public int intRoll_no { get; set; }
        public string vchInserted_ip { get; set; }
        //public string Status { get; set; }
        public string trasactionID { get; set; }
    }
}