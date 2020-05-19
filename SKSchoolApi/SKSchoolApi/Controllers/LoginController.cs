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
    public class LoginController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string intUserType_id, string vchUser_name, string vchPassword, string intAcademic_id, string intSchool_id)
        {
            Login login = new Login();
            login.intUserType_id = Convert.ToInt32(intUserType_id);
            login.vchUser_name = vchUser_name;
            login.vchPassword = vchPassword;
            login.intAcademic_id = Convert.ToInt32(intAcademic_id);
            login.intSchool_id = Convert.ToInt32(intSchool_id);
            DataSet ds = record.LoginDetails(command, login);
            return ds;
        }
        //public DataSet Get(string command, string GalleryId)
        //{
        //    DataSet ds = record.SchoolDetails(command, GalleryId);
        //    return ds;
        //}
        public DataSet Get(string command, string GalleryId)
        {
            DataSet ds = record.SchoolDetails(command, GalleryId);
            return ds;
        }
        public HttpResponseMessage Post(string command, Login logindetails)
        {
            try
            {
                DataSet ds = record.FCMTokenUpdate(command, logindetails);
                var message = Request.CreateResponse(HttpStatusCode.Created);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
