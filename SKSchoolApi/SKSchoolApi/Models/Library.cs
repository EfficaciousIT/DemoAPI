using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSchoolApi.Models
{
    public class Library
    {
        public int intschool_id { get; set; }
        public int intStandard_id { get; set; }

        public int intStudent_id { get; set; }

        public int intCategory_id { get; set; }
        public string dtAssigned_Date { get; set; }
        public string dtReturn_date { get; set; }
        public int intTeacher_id { get; set; }
        public int intDepartment_id { get; set; }
        public int intBookLanguage_id { get; set; }
    }
}