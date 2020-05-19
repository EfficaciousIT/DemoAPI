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
    public class StandardController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string intSchool_id, string intStandard_id, string intAcademic_id, string intTeacher_id, string vchType, string intDivision_id)
        {
            Standard standard = new Standard();
            standard.intSchool_id = Convert.ToInt32(intSchool_id);
            standard.intStandard_id = Convert.ToInt32(intStandard_id);
            standard.intAcademic_id = Convert.ToInt32(intAcademic_id);
            standard.intTeacher_id = Convert.ToInt32(intTeacher_id);
            standard.vchType = vchType;
            standard.intDivision_id = Convert.ToInt32(intDivision_id);
            DataSet ds = record.StandardDetails(command, standard);
            return ds;
        }
        public HttpResponseMessage Post(string command, Standard standard)
        {
            try
            {
                DataSet ds = record.DiaryHomeworkDetail(command, standard);
                var message = Request.CreateResponse(HttpStatusCode.Created);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Put(string command, Standard standard)
        {
            try
            {
                DataSet ds = record.DiaryHomeworkDetail(command, standard);
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
