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
    public class ChatController : ApiController
    {
        Database.DB record = new Database.DB();
        Database.ChatMessage chatmessage = new Database.ChatMessage();
        public DataSet Get(string command, string intSchool_id, string intUserid, string intStandard_id, string intDivision_id, string intAcademic_id)
        {
            ChatDetail chatdetail = new ChatDetail();
            chatdetail.intSchool_id = Convert.ToInt32(intSchool_id);
            chatdetail.User_id = Convert.ToInt32(intUserid);
            chatdetail.intstandard_id = Convert.ToInt32(intStandard_id);
            chatdetail.intDivision_id = Convert.ToInt32(intDivision_id);
            chatdetail.intAcademic_id = Convert.ToInt32(intAcademic_id);
            DataSet ds = record.ChatDetails(command, chatdetail);
            return ds;
        }
        public HttpResponseMessage Post(string command, ChatDetail chatdetail)
        {
            try
            {
                if (command == "ChatMessage")
                {
                    Boolean ds = chatmessage.ChatMessages(chatdetail);
                }
                else if (command == "ChatGroupMessageForTeacher")
                {
                    Boolean ds = chatmessage.ChatGroupMessageForTeacher(chatdetail);
                }
                else if (command == "ChatGroupMessageForStudent")
                {
                    Boolean ds = chatmessage.ChatGroupMessageForStudent(chatdetail);
                }
                else if (command == "ChatGroupMessageFromStudentToTeacher")
                {
                    Boolean ds = chatmessage.ChatGroupMessageFromStudentToTeacher(chatdetail);
                }

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
