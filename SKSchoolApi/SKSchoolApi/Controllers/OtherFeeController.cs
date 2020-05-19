using SKSchoolApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SKSchoolApi.Controllers
{
    public class OtherFeeController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string intschool_id, string intAcademic_id)
        {
            OtherFee otherFee = new OtherFee();
            otherFee.intSchool_id = Convert.ToInt32(intschool_id);
            otherFee.intAcademic_id = Convert.ToInt32(intAcademic_id);
            DataSet ds = record.OtherFee(command, otherFee);
            return ds;
        }
        public DataSet Get(string command, string intstandard_id, string intDivision_id, string intStudent_id)
        {
            OtherFee otherFee = new OtherFee();

            otherFee.intstandard_id = Convert.ToInt32(intstandard_id);
            otherFee.intDivision_id = Convert.ToInt32(intDivision_id);
            otherFee.intStudent_id = Convert.ToInt32(intStudent_id);
            DataSet ds = record.OtherFee(command, otherFee);
            return ds;
        }
    }
}