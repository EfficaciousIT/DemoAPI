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
    public class OutstandingFeeController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string intschool_id, string intAcademic_id)
        {
            OutstandingFee outstandingfee = new OutstandingFee();
            outstandingfee.intSchool_id = Convert.ToInt32(intschool_id);
            outstandingfee.intAcademic_id = Convert.ToInt32(intAcademic_id);
            DataSet ds = record.OutstandingFeeDetails(command, outstandingfee);
            return ds;
        }
        public DataSet Get(string command, string intschool_id, string intAcademic_id, string intstandard_id)
        {
            OutstandingFee outstandingfee = new OutstandingFee();
            outstandingfee.intSchool_id = Convert.ToInt32(intschool_id);
            outstandingfee.intAcademic_id = Convert.ToInt32(intAcademic_id);
            outstandingfee.intstandard_id = Convert.ToInt32(intstandard_id);
            DataSet ds = record.OutstandingFeeDetails(command, outstandingfee);
            return ds;
        }
        public DataSet Get(string command, string intschool_id, string intAcademic_id, string intstandard_id, string intDivision_id)
        {
            OutstandingFee outstandingfee = new OutstandingFee();
            outstandingfee.intSchool_id = Convert.ToInt32(intschool_id);
            outstandingfee.intAcademic_id = Convert.ToInt32(intAcademic_id);
            outstandingfee.intstandard_id = Convert.ToInt32(intstandard_id);
            outstandingfee.intDivision_id = Convert.ToInt32(intDivision_id);
            DataSet ds = record.OutstandingFeeDetails(command, outstandingfee);
            return ds;
        }
        public DataSet Get(string command, string intschool_id, string intAcademic_id, string intstandard_id, string intDivision_id, string intStudent_id)
        {
            OutstandingFee outstandingfee = new OutstandingFee();
            outstandingfee.intSchool_id = Convert.ToInt32(intschool_id);
            outstandingfee.intAcademic_id = Convert.ToInt32(intAcademic_id);
            outstandingfee.intstandard_id = Convert.ToInt32(intstandard_id);
            outstandingfee.intDivision_id = Convert.ToInt32(intDivision_id);
            outstandingfee.intStudent_id = Convert.ToInt32(intStudent_id);
            DataSet ds = record.OutstandingFeeDetails(command, outstandingfee);
            return ds;
        }
    }
}