using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SKSchoolApi.Models;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Web.Script.Services;
namespace SKSchoolApi.Database
{
    public class ChatMessage
    {
        string constr = System.Configuration.ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        public Boolean ChatMessages(ChatDetail chatdetail)
        {

            try
            {
                string regId = chatdetail.FCMToken.ToString().Trim();
                var applicationID = "AIzaSyCHfQSjFsEybdNRibLORHTMVVp6CKoI5TQ"; // PbojmyrmspdqkZ0pINni7DyqvhY2
                var value = chatdetail.Message.Trim();
                string userid = chatdetail.User_id.ToString();
                string title = "IndividualChat";
                var SENDER_ID = "574926706382";
                string ReceivrName = chatdetail.ReceiverName.Trim();
                string RecieverFCMTOken = chatdetail.FCMToken.Trim();
                string Sendername = chatdetail.SenderName.Trim();
                string senderFCMToken = chatdetail.SenderFCMToken.Trim();
                string UserTypeid = chatdetail.intUserType_id.ToString();
                WebRequest tRequest;
                tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
                    + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId + "&data.title=" + title + "&data.userid=" + userid + "&data.ReceivrName=" + ReceivrName + "&data.RecieverFCMTOken=" + RecieverFCMTOken + "&data.Sendername=" + Sendername + "&data.senderFCMToken=" + senderFCMToken + "&data.UserTypeid=" + UserTypeid + "";

                Console.WriteLine(postData);
                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();

                //txtResponse.Text = sResponseFromServer; //printing response from GCM server.
                //txtStream.Text = postData.ToString().Trim();
                tReader.Close();
                dataStream.Close();
                tResponse.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public Boolean ChatGroupMessageForTeacher(ChatDetail chatdetail)
        {

            try
            {

                SqlConnection conn = new SqlConnection();
                conn = new SqlConnection(constr);
                string SqlString = "";
                SqlString = "select vchFCMToken from Student_Master where vchFCMToken IS NOT NULL and vchFCMToken!='' and intstanderd_id='" + chatdetail.intstandard_id.ToString() + "' and intDivision_id='" + chatdetail.intDivision_id.ToString() + "' and intschool_id='" + chatdetail.intSchool_id.ToString() + "' and intAcademic_id='" + chatdetail.intAcademic_id.ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(SqlString, conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);


                string s = chatdetail.Message.Trim();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string regId = dt.Rows[i]["vchFCMToken"].ToString();
                    var applicationID = "AIzaSyCHfQSjFsEybdNRibLORHTMVVp6CKoI5TQ"; // PbojmyrmspdqkZ0pINni7DyqvhY2
                    string message = chatdetail.Message.Trim();
                    string title = "Group";
                    string Sendername = chatdetail.SenderName;
                    string GroupName = chatdetail.GRoupName;
                    var SENDER_ID = "574926706382";
                    var value = chatdetail.Message.Trim();
                    WebRequest tRequest;
                    tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                    tRequest.Method = "post";
                    tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                    tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                    tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                    string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
                        + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId + "&data.title=" + title + "&data.Sendername=" + Sendername + "&data.GroupName=" + GroupName + "&data.Standard_id=" + chatdetail.intstandard_id.ToString() + "&data.Division_id=" + chatdetail.intDivision_id.ToString() + "";
                    Console.WriteLine(postData);
                    Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    tRequest.ContentLength = byteArray.Length;
                    Stream dataStream = tRequest.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    WebResponse tResponse = tRequest.GetResponse();
                    dataStream = tResponse.GetResponseStream();
                    StreamReader tReader = new StreamReader(dataStream);
                    String sResponseFromServer = tReader.ReadToEnd();
                    //txtResponse.Text = sResponseFromServer; //printing response from GCM server.
                    //txtStream.Text = postData.ToString().Trim();
                    tReader.Close();
                    dataStream.Close();
                    tResponse.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Boolean ChatGroupMessageForStudent(ChatDetail chatdetail)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn = new SqlConnection(constr);
                string SqlString = "";
                SqlString = "select vchFCMToken from Student_Master where vchFCMToken IS NOT NULL and vchFCMToken!='' and intstanderd_id='" + chatdetail.intstandard_id.ToString() + "' and intDivision_id='" + chatdetail.intDivision_id.ToString() + "' and intStudent_id not in('" + chatdetail.User_id.ToString() + "') and intschool_id='" + chatdetail.intSchool_id.ToString() + "' and intAcademic_id='" + chatdetail.intAcademic_id.ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(SqlString, conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                string s = chatdetail.Message.Trim();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string regId = dt.Rows[i]["vchFCMToken"].ToString();
                    var applicationID = "AIzaSyCHfQSjFsEybdNRibLORHTMVVp6CKoI5TQ"; // PbojmyrmspdqkZ0pINni7DyqvhY2
                    string message = chatdetail.Message.Trim();
                    string title = "Group";
                    string Sendername = chatdetail.SenderName;
                    string GroupName = chatdetail.GRoupName;
                    var SENDER_ID = "574926706382";
                    var value = chatdetail.Message.Trim();
                    WebRequest tRequest;
                    tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                    tRequest.Method = "post";
                    tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                    tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                    tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                    string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
                        + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId + "&data.title=" + title + "&data.Sendername=" + Sendername + "&data.GroupName=" + GroupName + "&data.Standard_id=" + chatdetail.intstandard_id.ToString() + "&data.Division_id=" + chatdetail.intDivision_id.ToString() + "";
                    Console.WriteLine(postData);
                    Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    tRequest.ContentLength = byteArray.Length;
                    Stream dataStream = tRequest.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    WebResponse tResponse = tRequest.GetResponse();
                    dataStream = tResponse.GetResponseStream();
                    StreamReader tReader = new StreamReader(dataStream);
                    String sResponseFromServer = tReader.ReadToEnd();
                    //txtResponse.Text = sResponseFromServer; //printing response from GCM server.
                    //txtStream.Text = postData.ToString().Trim();
                    tReader.Close();
                    dataStream.Close();
                    tResponse.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public Boolean ChatGroupMessageFromStudentToTeacher(ChatDetail chatdetail)
        {

            try
            {

                SqlConnection conn = new SqlConnection();
                conn = new SqlConnection(constr);
                string SqlString = "";
                SqlString = "select  vchFCMToken from tblTeacher_Master where intTeacher_id='" + chatdetail.intTeacher_id.ToString() + "' and vchFCMToken IS NOT NULL and vchFCMToken!='' and intSchool_id='" + chatdetail.intSchool_id.ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(SqlString, conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);


                string s = chatdetail.Message.Trim();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string regId = dt.Rows[i]["vchFCMToken"].ToString();
                    var applicationID = "AIzaSyCHfQSjFsEybdNRibLORHTMVVp6CKoI5TQ"; // PbojmyrmspdqkZ0pINni7DyqvhY2
                    string message = chatdetail.Message.Trim();
                    string title = "Group";
                    string Sendername = chatdetail.SenderName;
                    string GroupName = chatdetail.GRoupName;
                    var SENDER_ID = "574926706382";
                    var value = chatdetail.Message.Trim();
                    WebRequest tRequest;
                    tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                    tRequest.Method = "post";
                    tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                    tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                    tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                    string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
                        + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId + "&data.title=" + title + "&data.Sendername=" + Sendername + "&data.GroupName=" + GroupName + "&data.Standard_id=" + chatdetail.intstandard_id.ToString() + "&data.Division_id=" + chatdetail.intDivision_id.ToString() + "";
                    Console.WriteLine(postData);
                    Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    tRequest.ContentLength = byteArray.Length;
                    Stream dataStream = tRequest.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    WebResponse tResponse = tRequest.GetResponse();
                    dataStream = tResponse.GetResponseStream();
                    StreamReader tReader = new StreamReader(dataStream);
                    String sResponseFromServer = tReader.ReadToEnd();
                    //txtResponse.Text = sResponseFromServer; //printing response from GCM server.
                    //txtStream.Text = postData.ToString().Trim();
                    tReader.Close();
                    dataStream.Close();
                    tResponse.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
