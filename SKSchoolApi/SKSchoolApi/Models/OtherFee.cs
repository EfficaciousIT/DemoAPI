using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSchoolApi.Models
{
    public class OtherFee
    {
        public int intSchool_id { get; set; }
        public int Student_id { get; set; }
        public int intstandard_id { get; set; }
        public int intDivision_id { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int intStudent_id { get; set; }
        public int intAcademic_id { get; set; }
    }
}