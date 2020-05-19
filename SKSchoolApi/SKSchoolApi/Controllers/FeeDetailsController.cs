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
    public class FeeDetailsController : ApiController
    {
        Database.DB record = new Database.DB();

        public DataSet Get(string command, string intSchool_id, string fromdate, string todate)
        {
            feecollectiondetails feecollection = new feecollectiondetails();
            feecollection.intSchool_id = Convert.ToInt32(intSchool_id);
            string dateString = Convert.ToDateTime(fromdate).ToString("MM/dd/yyyy");
            feecollection.fromdate = dateString.Replace("-", "/");
            string dateString1 = Convert.ToDateTime(todate).ToString("MM/dd/yyyy");
            feecollection.todate = dateString1.Replace("-", "/");
            DataSet ds = record.FeeDetails(command, feecollection);
            return ds;
        }
        public DataSet Get(string command, string intSchool_id, string fromdate, string todate, string intstandard_id)
        {
            feecollectiondetails feecollection = new feecollectiondetails();
            feecollection.intSchool_id = Convert.ToInt32(intSchool_id);
            string dateString = Convert.ToDateTime(fromdate).ToString("MM/dd/yyyy");
            feecollection.fromdate = dateString.Replace("-", "/");
            string dateString1 = Convert.ToDateTime(todate).ToString("MM/dd/yyyy");
            feecollection.todate = dateString1.Replace("-", "/");
            feecollection.intstandard_id = Convert.ToInt32(intstandard_id);
            DataSet ds = record.FeeDetails(command, feecollection);
            return ds;
        }
        public DataSet Get(string command, string intSchool_id, string fromdate, string todate, string intstandard_id, string intDivision_id)
        {
            feecollectiondetails feecollection = new feecollectiondetails();
            feecollection.intSchool_id = Convert.ToInt32(intSchool_id);
            string dateString = Convert.ToDateTime(fromdate).ToString("MM/dd/yyyy");
            feecollection.fromdate = dateString.Replace("-", "/");
            string dateString1 = Convert.ToDateTime(todate).ToString("MM/dd/yyyy");
            feecollection.todate = dateString1.Replace("-", "/");
            feecollection.intstandard_id = Convert.ToInt32(intstandard_id);
            feecollection.intDivision_id = Convert.ToInt32(intDivision_id);
            DataSet ds = record.FeeDetails(command, feecollection);
            return ds;
        }

        public DataSet Get(string command, string intStudent_id, string intstanderd_id, string intdivision_id, string intSchool_id, string fromdate, string todate)
        {
            feecollectiondetails feecollection = new feecollectiondetails();
            feecollection.intStudent_id = Convert.ToInt32(intStudent_id);
            feecollection.intstandard_id = Convert.ToInt32(intstanderd_id);
            feecollection.intDivision_id = Convert.ToInt32(intdivision_id);
            string dateString = Convert.ToDateTime(fromdate).ToString("MM/dd/yyyy");
            feecollection.fromdate = dateString.Replace("-", "/");
            string dateString1 = Convert.ToDateTime(todate).ToString("MM/dd/yyyy");
            feecollection.todate = dateString1.Replace("-", "/");
            //feecollection.intAcademic_id = Convert.ToInt32(intAcademic_id);
            feecollection.intSchool_id = Convert.ToInt32(intSchool_id);
            DataSet ds = record.FeeDetails(command, feecollection);
            return ds;
        }
    }
}