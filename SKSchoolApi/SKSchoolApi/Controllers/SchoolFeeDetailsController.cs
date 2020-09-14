using SKSchoolApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SKSchoolApi.Controllers
{
    public class SchoolFeeDetailsController : ApiController
    {
        Database.SchoolFees record = new Database.SchoolFees();
        public DataSet Get(string command,string intStudentID_Number,string intstandard_id,string intStudentID, string intAcademic_id,string intSchool_id)
        {
            FeeCollection feeCollection = new FeeCollection();
            feeCollection.intStudentID_Number = Convert.ToInt32(intStudentID_Number);
            feeCollection.intstandard_id = Convert.ToInt32(intstandard_id);
            feeCollection.intStudentID = Convert.ToInt32(intStudentID);
            feeCollection.intAcademic_id = Convert.ToInt32(intAcademic_id);
            feeCollection.intSchool_id = Convert.ToInt32(intSchool_id);
            DataSet ds = record.GetFeeMonthWise(command,feeCollection);
            return ds;
        }
        public DataSet Get(string command, string intStudentID_Number, string intstandard_id, string intStudentID, string intAcademic_id, string intSchool_id,string month)
        {
            FeeCollection feeCollection = new FeeCollection();
            feeCollection.intStudentID_Number = Convert.ToInt32(intStudentID_Number);
            feeCollection.intstandard_id = Convert.ToInt32(intstandard_id);
            feeCollection.intStudentID = Convert.ToInt32(intStudentID);
            feeCollection.intAcademic_id = Convert.ToInt32(intAcademic_id);
            feeCollection.intSchool_id = Convert.ToInt32(intSchool_id);
            feeCollection.month = Convert.ToString(month);
            DataSet ds = record.GetFeeMonthWise(command, feeCollection);
            return ds;
        }
       [HttpPost]
        public HttpResponseMessage Post(string command, string intStudentID_Number, string intstandard_id, string intStudentID, string intAcademic_id, string intSchool_id, string month,string intDivision_id, string intRoll_no)
        {
            try
            {
                FeeCollection feeCollection = new FeeCollection();
                feeCollection.intStudentID_Number = Convert.ToInt32(intStudentID_Number);
                feeCollection.intstandard_id = Convert.ToInt32(intstandard_id);
                feeCollection.intStudentID = Convert.ToInt32(intStudentID);
                feeCollection.intAcademic_id = Convert.ToInt32(intAcademic_id);
                feeCollection.intSchool_id = Convert.ToInt32(intSchool_id);
                feeCollection.intDivision_id = Convert.ToInt32(intDivision_id);
                feeCollection.intRoll_no = Convert.ToInt32(intRoll_no);
                feeCollection.month = Convert.ToString(month);
                int Records = record.InsertFeeRecords(command, feeCollection);
                var message = Request.CreateResponse(HttpStatusCode.Created,"Record Inserted Successfully");
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }
       
        public DataSet Get(string command, string intStudentID_Number, string intstandard_id, string intStudentID, string intAcademic_id, int intSchool_id, string intDivision_id)
        {
            FeeCollection feeCollection = new FeeCollection();
            feeCollection.intStudentID_Number = Convert.ToInt32(intStudentID_Number);
            feeCollection.intstandard_id = Convert.ToInt32(intstandard_id);
            feeCollection.intStudentID = Convert.ToInt32(intStudentID);
            feeCollection.intAcademic_id = Convert.ToInt32(intAcademic_id);
            feeCollection.intSchool_id = Convert.ToInt32(intSchool_id);
            DataSet ds = record.GetPaidFeeMonthWise(command, feeCollection);
            return ds;
        }

        public DataSet Get(string command, string intStudentID_Number, string intstandard_id, string intStudentID, string intAcademic_id, int intSchool_id, string intDivision_id,string Month)
        {
            FeeCollection feeCollection = new FeeCollection();
            feeCollection.intStudentID_Number = Convert.ToInt32(intStudentID_Number);
            feeCollection.intstandard_id = Convert.ToInt32(intstandard_id);
            feeCollection.intStudentID = Convert.ToInt32(intStudentID);
            feeCollection.month = Convert.ToString(Month);
            feeCollection.intAcademic_id = Convert.ToInt32(intAcademic_id);
            feeCollection.intSchool_id = Convert.ToInt32(intSchool_id);
            DataSet ds = record.GetPaidFeeMonthWise(command, feeCollection);
            return ds;
        }
        [HttpPost]
        public DataSet Post(string command, string feeId, string intStudentId, string trasactionId)
        {
            try
            {
                FeeCollection feeCollection = new FeeCollection();
                feeCollection.intStudentID = Convert.ToInt32(intStudentId);
                feeCollection.intStudFee_id = Convert.ToInt32(feeId);
                feeCollection.trasactionID = trasactionId;
                DataSet ds = record.UpdatetransactionId(command, feeCollection);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
