using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Configuration;
using System.Xml;
using SKSchoolApi.Models;

namespace SKSchoolApi.Database
{
    public class DB
    {
        string constr = System.Configuration.ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        string SqlString = "";

        public DataSet LoginDetails(string command, Login Login)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_Mob_usermaster";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    com.Parameters.AddWithValue("@usertype", Login.intUserType_id);
                    com.Parameters.AddWithValue("@username", Login.vchUser_name);
                    com.Parameters.AddWithValue("@intschool_id", Login.intSchool_id);
                    com.Parameters.AddWithValue("@intAcademic_id", Login.intAcademic_id);
                    com.Parameters.AddWithValue("@password", Login.vchPassword);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "LoginDetails");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet SchoolDetails(string command, string GalleryId)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_Mob_School";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    com.Parameters.AddWithValue("@GalleryId", GalleryId);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "SchoolDetails");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet DashboardDetails(string command, Dashboard dashboard)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "";
                    if (command == "HolidaysAndVacation")
                    {
                        query = "usp_Mob_StatndardApi";
                    }
                    else
                    {
                        query = "usp_NewAdminDashboard";
                    }

                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    if (command == "HolidaysAndVacation")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intAcademic_id", dashboard.intAcademic_id);
                    }
                    else if (command == "genderwiseStudent" || command == "NoticeBoard" || command == "AllMessageToStudent" || command == "NoticeBoardTeacher" || command == "NoticeBoardStaff")
                    {
                        com.Parameters.AddWithValue("@type", command);
                        com.Parameters.AddWithValue("@AcademicID", dashboard.intAcademic_id);
                        com.Parameters.AddWithValue("@SchoolId", dashboard.intSchool_id);

                    }
                    else if (command == "NoticeBoardStudent")
                    {
                        com.Parameters.AddWithValue("@type", command);
                        com.Parameters.AddWithValue("@AcademicID", dashboard.intAcademic_id);
                        com.Parameters.AddWithValue("@SchoolId", dashboard.intSchool_id);
                        com.Parameters.AddWithValue("@intStandard_id", dashboard.intstanderd_id);
                    }
                    else if (command == "AllMessageToDivision")
                    {
                        com.Parameters.AddWithValue("@type", command);
                        com.Parameters.AddWithValue("@AcademicID", dashboard.intAcademic_id);
                        com.Parameters.AddWithValue("@STD", dashboard.intstanderd_id);
                        com.Parameters.AddWithValue("@DIV", dashboard.intDivision_id);

                    }
                    else
                    {
                        com.Parameters.AddWithValue("@type", command);
                        com.Parameters.AddWithValue("@AcademicID", dashboard.intAcademic_id);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DashboardDetails");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet StandardDetails(string command, Standard standard)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "";
                    query = "usp_Mob_StatndardApi";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    if (command == "select")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intSchool_id", standard.intSchool_id);
                    }
                    else if (command == "selectStandardByPrincipal")
                    {
                        com.Parameters.AddWithValue("@command", command);
                    }
                    else if (command == "GetDivision")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intSchool_id", standard.intSchool_id);
                        com.Parameters.AddWithValue("@intStandard_id", standard.intStandard_id);
                    }
                    else if (command == "selectStandradByLectures")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intAcademic_id", standard.intAcademic_id);
                        com.Parameters.AddWithValue("@intTeacher_id", standard.intTeacher_id);
                    }
                    else if (command == "selectDivisionByLectures")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intAcademic_id", standard.intAcademic_id);
                        com.Parameters.AddWithValue("@intTeacher_id", standard.intTeacher_id);
                        com.Parameters.AddWithValue("@intSchool_id", standard.intSchool_id);
                        com.Parameters.AddWithValue("@intStandard_id", standard.intStandard_id);
                    }
                    else if (command == "SelectSubjectByLectures")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intAcademic_id", standard.intAcademic_id);
                        com.Parameters.AddWithValue("@intTeacher_id", standard.intTeacher_id);
                        com.Parameters.AddWithValue("@intSchool_id", standard.intSchool_id);
                        com.Parameters.AddWithValue("@intStandard_id", standard.intStandard_id);
                        com.Parameters.AddWithValue("@intDivision_id", standard.intDivision_id);
                    }
                    else if (command == "DailyDairyTeacher")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intAcademic_id", standard.intAcademic_id);
                        com.Parameters.AddWithValue("@intTeacher_id", standard.intTeacher_id);
                        com.Parameters.AddWithValue("@vchType", standard.vchType);
                    }
                    else if (command == "DailyDairyAdmin" || command == "DailyDairyAdminApproved" || command == "DailyDairyAdminRejected")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intAcademic_id", standard.intAcademic_id);
                        com.Parameters.AddWithValue("@intSchool_id", standard.intSchool_id);
                        com.Parameters.AddWithValue("@vchType", standard.vchType);
                    }
                    else if (command == "DailyDairyStudent")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intAcademic_id", standard.intAcademic_id);
                        com.Parameters.AddWithValue("@intSchool_id", standard.intSchool_id);
                        com.Parameters.AddWithValue("@vchType", standard.vchType);
                        com.Parameters.AddWithValue("@intStandard_id", standard.intStandard_id);
                        com.Parameters.AddWithValue("@intDivision_id", standard.intDivision_id);
                    }
                    else if (command == "DailyDairyPrincipal" || command == "DailyDairyPrincipalApproved" || command == "DailyDairyPrincipalRejected")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intAcademic_id", standard.intAcademic_id);
                        com.Parameters.AddWithValue("@vchType", standard.vchType);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StandardDetails");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet StudentStandardwiseDetail(string command, StudentStandardWise studentstandardwise)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_Mob_Student";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    com.Parameters.AddWithValue("@Academic_id", studentstandardwise.Academic_id);
                    com.Parameters.AddWithValue("@intschool_id", studentstandardwise.intschool_id);
                    com.Parameters.AddWithValue("@Standard_id", studentstandardwise.Standard_id);
                    com.Parameters.AddWithValue("@Division_id", studentstandardwise.Division_id);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StudentStandardwiseDetail");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet AttendanceDetail(string command, Attendance attendance)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "";
                    if (command == "BackDate" || command == "BackDateTeacher" || command == "BackDateTeacherBYPrincipal" || command == "BackDateStaff")
                    {
                        query = "usp_Mob_GetMarkAttendance";
                    }
                    else if (command == "Insert" || command == "InsertTeacher")
                    {
                        query = "usp_Mob_MarkAttendance";
                    }
                    else if (command == "selectStudent")
                    {
                        query = "usp_Mob_StudentAttendanceSummery";
                    }
                    else if (command == "select" || command == "selectStaff")
                    {
                        query = "usp_Mob_TeacherAttendanceSummery";
                    }
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    if (command == "BackDate")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@dtDate", attendance.dtDate);
                        com.Parameters.AddWithValue("@intstanderd_id", attendance.Standard_id);
                        com.Parameters.AddWithValue("@intdivision_id", attendance.intDivision_id);
                        com.Parameters.AddWithValue("@intschool_id", attendance.intSchool_id);
                        com.Parameters.AddWithValue("@intAcademic_id", attendance.intAcademic_id);
                        com.Parameters.AddWithValue("@intUserType_id", attendance.intUserType_id);
                    }
                    else if (command == "BackDateTeacher" || command == "BackDateStaff")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@dtDate", attendance.dtDate);
                        com.Parameters.AddWithValue("@intschool_id", attendance.intSchool_id);
                        com.Parameters.AddWithValue("@intUserType_id", attendance.intUserType_id);
                    }
                    else if (command == "BackDateTeacherBYPrincipal")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@dtDate", attendance.dtDate);
                        com.Parameters.AddWithValue("@intUserType_id", attendance.intUserType_id);
                    }
                    else if (command == "Insert")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intUserType_id", attendance.intUserType_id);
                        com.Parameters.AddWithValue("@intUser_id", attendance.userId);
                        com.Parameters.AddWithValue("@dtDate", attendance.dtDate);
                        com.Parameters.AddWithValue("@status", attendance.status);
                        com.Parameters.AddWithValue("@intschool_id", attendance.intSchool_id);
                        com.Parameters.AddWithValue("@intAcademic_id", attendance.intAcademic_id);
                        com.Parameters.AddWithValue("@intstanderd_id", attendance.Standard_Id);
                        com.Parameters.AddWithValue("@intdivision_id", attendance.intDivision_id);
                    }
                    else if (command == "InsertTeacher")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intUserType_id", attendance.intUserType_id);
                        com.Parameters.AddWithValue("@intUser_id", attendance.userId);
                        com.Parameters.AddWithValue("@dtDate", attendance.dtDate);
                        com.Parameters.AddWithValue("@status", attendance.status);
                        com.Parameters.AddWithValue("@intSchool_Id", attendance.intSchool_id);
                        com.Parameters.AddWithValue("@intAcademic_id", attendance.intAcademic_id);
                    }
                    else if (command == "selectStudent")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intAcademic_id", attendance.intAcademic_id);
                        com.Parameters.AddWithValue("@intSchool_Id", attendance.intSchool_id);
                        com.Parameters.AddWithValue("@Student_id", attendance.userId);
                    }
                    else if (command == "select" || command == "selectStaff")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intAcademic_id", attendance.intAcademic_id);
                        com.Parameters.AddWithValue("@intSchool_Id", attendance.intSchool_id);
                        com.Parameters.AddWithValue("@Teacher_id", attendance.userId);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "AttendanceDetail");
                    if (command == "Insert" || command == "InsertTeacher")
                    {
                        string regId = attendance.FCMToken.ToString().Trim();
                        var applicationID = "AIzaSyCHfQSjFsEybdNRibLORHTMVVp6CKoI5TQ"; // PbojmyrmspdqkZ0pINni7DyqvhY2

                        var value = "";
                        string title = "Message Center";
                        var SENDER_ID = "574926706382";
                        // 73064704159
                        if (attendance.status == "Present")
                        {
                            value = "You are marked Present as on " + attendance.dtDate;
                        }
                        else
                        {
                            value = "You are marked Absent as on " + attendance.dtDate;
                        }

                        WebRequest tRequest;
                        tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                        tRequest.Method = "post";
                        tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));

                        tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                        string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
                            + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId + "&data.title=" + title + "";

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

                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet ProfileDetails(string command, Profile profile)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_Mob_profiler";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    com.Parameters.AddWithValue("@intUser_id", profile.intUser_id);
                    com.Parameters.AddWithValue("@intschool_id", profile.intschool_id);
                    com.Parameters.AddWithValue("@intAcademic_id", profile.intAcademic_id);
                    if (command == "InsertTeacherProfile" || command == "InsertStudentProfile" || command == "InsertSTaffProfile" || command == "InsertPrincipalProfile" || command == "InsertManagerProfile" || command == "InsertAdminProfile")
                    {
                        com.Parameters.AddWithValue("@vchImageURL", profile.vchProfile);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ProfileDetails");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet StaffDetails(string command, TeacherDetail teacherdetail)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_Mob_Staff";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    com.Parameters.AddWithValue("@intSchool_id", teacherdetail.intSchool_id);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "TeacherDetail");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet TeacherDetails(string command, TeacherDetail teacherdetail)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_Mob_Teacher";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    com.Parameters.AddWithValue("@intSchool_id", teacherdetail.intSchool_id);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "TeacherDetail");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet SyllabusDetails(string command, Syllabus syllabus)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_TopicMaster";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@type", command);
                    com.Parameters.AddWithValue("@intSchool_id", syllabus.intSchool_id);
                    com.Parameters.AddWithValue("@intSubject_id", syllabus.intSubject_id);
                    com.Parameters.AddWithValue("@intSTD_id", syllabus.intSTD_id);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "SyllabusDetail");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet SyllabusDetailsPDF(string command, Syllabus syllabus)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_Syllabus_Master";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@type", command);
                    com.Parameters.AddWithValue("@intSchool_id", syllabus.intSchool_id);
                    com.Parameters.AddWithValue("@intSubject_id", syllabus.intSubject_id);
                    com.Parameters.AddWithValue("@intStandard_id", syllabus.intSTD_id);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "SyllabusDetailPDF");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet TimeTableDetails(string command, TimeTable timetable)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "";
                    if (command == "select")
                    {
                        query = "usp_Mob_TeacherTimeTable";
                    }
                    else if (command == "selectStudentTT")
                    {
                        query = "usp_Mob_StudentTimeTable";
                    }
                    else if (command == "selectExamTT")
                    {
                        query = "usp_Mob_StudentExamTimetable";
                    }
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    com.Parameters.AddWithValue("@intAcademic_id", timetable.intAcademic_id);
                    com.Parameters.AddWithValue("@intSchool_id", timetable.intSchool_id);
                    if (command == "select")
                    {
                        com.Parameters.AddWithValue("@Teacher_id", timetable.intTeacher_id);
                        com.Parameters.AddWithValue("@Day", timetable.Day);
                    }
                    else if (command == "selectStudentTT")
                    {
                        com.Parameters.AddWithValue("@Std", timetable.intStandard_id);
                        com.Parameters.AddWithValue("@Day", timetable.Day);
                        com.Parameters.AddWithValue("@div", timetable.intDivision_id);

                    }
                    else if (command == "selectExamTT")
                    {
                        com.Parameters.AddWithValue("@intstandard_id", timetable.intStandard_id);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "TimeTableDetail");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet LibraryDetail(string command, Library library)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_Mob_BookDetails";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    com.Parameters.AddWithValue("@intschool_id", library.intschool_id);
                    com.Parameters.AddWithValue("@intStandard_id", library.intStandard_id);
                    com.Parameters.AddWithValue("@intStudent_id", library.intStudent_id);

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "LibraryDetail");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        public DataSet LibraryData(string command, Library library)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "";
                    if (command == "GetCategoryWiseData" || command == "GetStandardWiseBookList")
                    {
                        query = "usp_BookDetails";
                    }
                    else if (command == "AssignBookDetails" || command == "TeacherAssignBookDetails")
                    {
                        query = "usp_tblBookAssign";
                    }
                    else
                    {
                        query = "usp_BookCategoryMaster";
                    }
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    com.Parameters.AddWithValue("@intschool_id", library.intschool_id);

                    if (command == "GetCategoryWiseData")
                    {
                        com.Parameters.AddWithValue("@intCategory_id", library.intCategory_id);
                        com.Parameters.AddWithValue("@intstandard_id", library.intStandard_id);
                        com.Parameters.AddWithValue("@intBookLanguage_id", library.intBookLanguage_id);
                    }
                    else if (command == "GetStandardWiseBookList")
                    {
                        com.Parameters.AddWithValue("@intStandard_id", library.intStandard_id);
                    }
                    else if (command == "AssignBookDetails")
                    {
                        com.Parameters.AddWithValue("@intstandard_id", library.intStandard_id);
                        com.Parameters.AddWithValue("@intStudent_id", library.intStudent_id);
                        com.Parameters.AddWithValue("@dtAssigned_Date", library.dtAssigned_Date);
                        com.Parameters.AddWithValue("@dtReturn_date", library.dtReturn_date);
                        com.Parameters.AddWithValue("@intBookLanguage_id", library.intBookLanguage_id);
                    }
                    else if (command == "TeacherAssignBookDetails")
                    {
                        com.Parameters.AddWithValue("@intTeacher_id", library.intTeacher_id);
                        com.Parameters.AddWithValue("@intDepartment_id", library.intDepartment_id);
                        com.Parameters.AddWithValue("@dtAssigned_Date", library.dtAssigned_Date);
                        com.Parameters.AddWithValue("@dtReturn_date", library.dtReturn_date);
                        com.Parameters.AddWithValue("@intBookLanguage_id", library.intBookLanguage_id);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "LibraryDetail");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet DiaryHomeworkDetail(string command, Standard standard)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_Mob_DailyDiary";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    com.Parameters.AddWithValue("@vchComment", standard.vchComment);
                    com.Parameters.AddWithValue("@intTeacher_id", standard.intTeacher_id);
                    com.Parameters.AddWithValue("@intSubject_id", standard.intSubject_id);
                    com.Parameters.AddWithValue("@intStandard_id", standard.intStandard_id);
                    com.Parameters.AddWithValue("@intDivision_id", standard.intDivision_id);
                    com.Parameters.AddWithValue("@dtDatetime", standard.dtDatetime);
                    com.Parameters.AddWithValue("@intAcademic_id", standard.intAcademic_id);
                    com.Parameters.AddWithValue("@intSchool_id", standard.intSchool_id);
                    com.Parameters.AddWithValue("@vchFileName", standard.vchFileName);
                    com.Parameters.AddWithValue("@vchFilePath", standard.vchFilePath);
                    com.Parameters.AddWithValue("@int_Approval", standard.int_Approval);
                    com.Parameters.AddWithValue("@vchFilePath2", standard.vchFilePath2);
                    com.Parameters.AddWithValue("@vchFilePath3", standard.vchFilePath3);
                    com.Parameters.AddWithValue("@intMy_id", standard.intMy_id);
                    com.Parameters.AddWithValue("@vchType", standard.vchType);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StandardDetails");
                    SqlConnection conn = new SqlConnection();
                    conn = new SqlConnection(constr);
                    string SqlString = "", message = "";

                    if (command == "Insert" || command == "Update" || standard.vchType == "HomeWork" || standard.int_Approval == 1 || standard.int_Approval == 2 || standard.messageStatus == "1" || standard.vchType == "DailyDiary")
                    {
                        if (command == "Insert" && standard.vchType == "HomeWork")
                        {
                            SqlString = "select vchFCMToken from tblAdmin_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and intSchool_id='" + standard.intSchool_id + "'";
                            message = "A new Home Work is added by " + standard.TeacherName + " for Standard " + standard.vchStandard_name + " Division " + standard.vchDivisionName + " for Subject " + standard.vchSubjectName + "";
                        }
                        else if (command == "Update" && standard.int_Approval == 1)
                        {
                            SqlString = " select vchFCMToken from Student_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and intstanderd_id=" + standard.intStandard_id + " and intDivision_id=" + standard.intDivision_id + " ";
                            standard.vchType = "HomeWork";
                            message = "A new Home Work is added for " + standard.vchSubjectName + "";
                        }
                        else if (command == "Update" && standard.int_Approval == 2 && standard.messageStatus == "1")
                        {
                            SqlString = " select vchFCMToken from Student_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and intstanderd_id=" + standard.intStandard_id + " and intDivision_id=" + standard.intDivision_id + " ";
                            standard.vchType = "HomeWork";
                            message = " Home Work is Cancelled for " + standard.vchSubjectName + "";
                        }
                        else if (command == "Insert" && standard.vchType == "DailyDiary")
                        {
                            SqlString = "select vchFCMToken from tblAdmin_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and intSchool_id='" + standard.intSchool_id + "' union  select vchFCMToken from tblPrincipal_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' union  select vchFCMToken from tblManager_Master where vchFCMToken IS NOT NULL and vchFCMToken<>''";
                            message = "A new DailyDiary is added by " + standard.TeacherName + " for Standard " + standard.vchStandard_name + " Division " + standard.vchDivisionName + " for Subject " + standard.vchDivisionName + "";
                        }
                        else
                        {

                        }

                        SqlDataAdapter sda = new SqlDataAdapter(SqlString, conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string regId = dt.Rows[i]["vchFCMToken"].ToString();
                            var applicationID = "AIzaSyCHfQSjFsEybdNRibLORHTMVVp6CKoI5TQ"; // PbojmyrmspdqkZ0pINni7DyqvhY2

                            string title = standard.vchType;

                            var SENDER_ID = "574926706382";
                            var value = message.Trim();
                            WebRequest tRequest;
                            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                            tRequest.Method = "post";
                            tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                            tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                            string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
                                + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId + "&data.title=" + title + "";
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
                    }
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet LeaveDetail(string command, Leave leave)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_LeaveApply";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    com.Parameters.AddWithValue("@intUserType_id", leave.intUserType_id);
                    com.Parameters.AddWithValue("@intType_id", leave.intType_id);
                    com.Parameters.AddWithValue("@intUser_id", leave.intUser_id);
                    com.Parameters.AddWithValue("@dtFrom_date", leave.dtFrom_date);
                    com.Parameters.AddWithValue("@dtTo_Date", leave.dtTo_Date);
                    com.Parameters.AddWithValue("@vchReason", leave.vchReason);
                    com.Parameters.AddWithValue("@intTotalDays", leave.intTotalDays);
                    com.Parameters.AddWithValue("@bitAdminApproval", leave.bitAdminApproval);
                    com.Parameters.AddWithValue("@intSchool_id", leave.intSchool_id);
                    com.Parameters.AddWithValue("@intLeaveType_id", leave.intLeaveType_id);
                    com.Parameters.AddWithValue("@intAcademic_id", leave.intAcademic_id);
                    com.Parameters.AddWithValue("@vchLeaveType", leave.vchLeaveType);
                    com.Parameters.AddWithValue("@intCL", leave.intCL);
                    com.Parameters.AddWithValue("@intML", leave.intML);
                    com.Parameters.AddWithValue("@intTeacher_id", leave.intTeacher_id);
                    com.Parameters.AddWithValue("@intLeaveApplocation_id", leave.intLeaveApplocation_id);

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "LeaveDetail");
                    SqlConnection conn = new SqlConnection();
                    conn = new SqlConnection(constr);
                    string SqlString = "";
                    if (command == "insert")
                    {
                        SqlString = "select vchFCMToken from tblAdmin_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and intSchool_id='" + leave.intSchool_id + "' union  select vchFCMToken from tblPrincipal_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' union  select vchFCMToken from tblManager_Master where vchFCMToken IS NOT NULL and vchFCMToken<>''";

                        SqlDataAdapter sda = new SqlDataAdapter(SqlString, conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string regId = dt.Rows[i]["vchFCMToken"].ToString();
                            var applicationID = "AIzaSyCHfQSjFsEybdNRibLORHTMVVp6CKoI5TQ"; // PbojmyrmspdqkZ0pINni7DyqvhY2

                            string title = "Leave Apply";

                            var SENDER_ID = "574926706382";
                            String message = "" + leave.Name + " applied For Leave ";
                            var value = message.Trim();
                            WebRequest tRequest;
                            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                            tRequest.Method = "post";
                            tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                            tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                            string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
                                + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId + "&data.title=" + title + "";
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
                    }
                    else if (command == "UpdateStatus")
                    {
                        string msg;
                        if (leave.intUserType_id == 3)
                        {
                            if (leave.bitAdminApproval == 2)
                            {
                                SqlString = "select vchFCMToken from tblTeacher_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and intTeacher_id='" + leave.intUser_id + "'";
                                msg = "Your Leave has been Rejected";
                            }
                            else
                            {
                                msg = "Your Leave has been Approved";
                                SqlString = "select vchFCMToken from tblTeacher_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and intTeacher_id='" + leave.intUser_id + "'";
                            }
                        }
                        else
                        {

                            if (leave.bitAdminApproval == 2)
                            {
                                SqlString = "select vchFCMToken from Student_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and intStudent_id='" + leave.intUser_id + "'";
                                msg = "Your Leave has been Rejected";
                            }
                            else
                            {
                                msg = "Your Leave has been Approved";
                                SqlString = "select vchFCMToken from Student_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and intStudent_id='" + leave.intUser_id + "'";
                            }

                        }


                        SqlDataAdapter sda = new SqlDataAdapter(SqlString, conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string regId = dt.Rows[i]["vchFCMToken"].ToString();
                            var applicationID = "AIzaSyCHfQSjFsEybdNRibLORHTMVVp6CKoI5TQ"; // PbojmyrmspdqkZ0pINni7DyqvhY2

                            string title = "Leave Approved";

                            var SENDER_ID = "574926706382";
                            String message = msg;
                            var value = message.Trim();
                            WebRequest tRequest;
                            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                            tRequest.Method = "post";
                            tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                            tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                            string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
                                + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId + "&data.title=" + title + "";
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
                    }
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet EventDetail(string command, Event eventdetail)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "";
                    if (command == "Insert")
                    {
                        query = "usp_Mob_Event";
                    }
                    else if (command == "SelectStudentApi" || command == "SelectTeacherApi" || command == "SelectAdmin" || command == "SelectPrincipal" || command == "SelectEventParticipatedByTeacher" || command == "SelectEventParticipatedByStudent")
                    {
                        query = "usp_Mob_EventList";
                    }
                    else if (command == "InsertParticipation")
                    {
                        query = "usp_Mob_EventParticipation";
                    }
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    if (command == "SelectStudentApi" || command == "SelectTeacherApi" || command == "SelectAdmin" || command == "SelectPrincipal" || command == "SelectEventParticipatedByTeacher" || command == "SelectEventParticipatedByStudent")
                    {
                        com.Parameters.AddWithValue("@vchStandard_id", eventdetail.vchStandard_id);
                        com.Parameters.AddWithValue("@intAcademic_id", eventdetail.intAcademic_id);
                        com.Parameters.AddWithValue("@intSchool_id", eventdetail.intSchool_id);
                        com.Parameters.AddWithValue("@intUser_id", eventdetail.intUser_id);
                    }
                    else if (command == "Insert")
                    {
                        com.Parameters.AddWithValue("@vchStandard_id", eventdetail.vchStandard_id);
                        com.Parameters.AddWithValue("@intUser_id", eventdetail.intUser_id);
                        com.Parameters.AddWithValue("@intAcademic_id", eventdetail.intAcademic_id);
                        com.Parameters.AddWithValue("@intUserType_id", eventdetail.intUserType_id);
                        com.Parameters.AddWithValue("@intSchool_id", eventdetail.intSchool_id);
                        com.Parameters.AddWithValue("@dtRegistrartionStartDate", eventdetail.dtRegistrartionStartDate);
                        com.Parameters.AddWithValue("@dtRegistrationEndDate", eventdetail.dtRegistrationEndDate);
                        com.Parameters.AddWithValue("@dtEventStartDate", eventdetail.dtEventStartDate);
                        com.Parameters.AddWithValue("@dtEventEndDate", eventdetail.dtEventEndDate);
                        com.Parameters.AddWithValue("@vchEventName", eventdetail.vchEventName);
                        com.Parameters.AddWithValue("@vchEventFees", eventdetail.vchEventFees);
                        com.Parameters.AddWithValue("@vchEventDescription", eventdetail.vchEventDescription);
                    }
                    else if (command == "InsertParticipation")
                    {
                        com.Parameters.AddWithValue("@intEvent_id", eventdetail.intEvent_id);
                        com.Parameters.AddWithValue("@intStandard_id", eventdetail.intStandard_id);
                        com.Parameters.AddWithValue("@intDivision_id", eventdetail.intDivision_id);
                        com.Parameters.AddWithValue("@intUser_id", eventdetail.intUser_id);
                        com.Parameters.AddWithValue("@intUserType_id", eventdetail.intUserType_id);
                        com.Parameters.AddWithValue("@intAcademic_id", eventdetail.intAcademic_id);
                        com.Parameters.AddWithValue("@intSchool_id", eventdetail.intSchool_id);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "EventDetail");
                    SqlConnection conn = new SqlConnection();
                    conn = new SqlConnection(constr);

                    if (command == "Insert" || eventdetail.vchStandard_id == "AllStudent & AllTeacher" || eventdetail.vchStandard_id == "AllStudent" || eventdetail.vchStandard_id == "AllTeacher")
                    {
                        if (command == "Insert" && eventdetail.vchStandard_id == "AllStudent & AllTeacher")
                        {
                            SqlString = "select vchFCMToken from Student_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and  intSchool_id='" + eventdetail.intSchool_id + "' union select vchFCMToken from tblTeacher_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and intSchool_id='" + eventdetail.intSchool_id + "'";

                        }
                        else if (command == "Insert" && eventdetail.vchStandard_id == "AllStudent")
                        {
                            SqlString = " select vchFCMToken from Student_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and  intSchool_id='" + eventdetail.intSchool_id + "'";

                        }
                        else if (command == "Insert" && eventdetail.vchStandard_id == "AllTeacher")
                        {
                            SqlString = "select vchFCMToken from tblTeacher_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and  intSchool_id='" + eventdetail.intSchool_id + "' ";

                        }
                        else
                        {
                            SqlString = "select vchFCMToken from Student_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and intstanderd_id=" + eventdetail.vchStandard_id + " ";
                        }

                        SqlDataAdapter sda = new SqlDataAdapter(SqlString, conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string regId = dt.Rows[i]["vchFCMToken"].ToString();
                            var applicationID = "AIzaSyCHfQSjFsEybdNRibLORHTMVVp6CKoI5TQ"; // PbojmyrmspdqkZ0pINni7DyqvhY2

                            string title = "Event";
                            string message = "A new Event has been Organized";
                            var SENDER_ID = "574926706382";
                            var value = message.Trim();
                            WebRequest tRequest;
                            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                            tRequest.Method = "post";
                            tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                            tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                            string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
                                + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId + "&data.title=" + title + "";
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
                    }
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet NoticeboardDetail(string command, Noticeboard noticeboard)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_NocticeBoard";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    com.Parameters.AddWithValue("@intschool_id", noticeboard.intSchool_id);
                    com.Parameters.AddWithValue("@intUserType_id", noticeboard.intUserType_id);
                    com.Parameters.AddWithValue("@intStandard_id", noticeboard.intStandard_id);
                    com.Parameters.AddWithValue("@intDepartment_id", noticeboard.intDepartment_id);
                    com.Parameters.AddWithValue("@intTeacher_id", noticeboard.intTeacher_id);
                    com.Parameters.AddWithValue("@dtIssue_date", noticeboard.dtIssue_date);
                    com.Parameters.AddWithValue("@dtEnd_date", noticeboard.dtEnd_date);
                    com.Parameters.AddWithValue("@vchSubject", noticeboard.vchSubject);
                    com.Parameters.AddWithValue("@vchNotice", noticeboard.vchNotice);
                    com.Parameters.AddWithValue("@intInserted_by", noticeboard.intInserted_by);
                    com.Parameters.AddWithValue("@InsertIP", noticeboard.InsertIP);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "NoticeboardDetail");

                    string SchoolId = "1";
                    if (command == "insert")
                    {
                        SqlConnection conn = new SqlConnection();
                        conn = new SqlConnection(constr);
                        string SqlString = "";
                        if (noticeboard.intUserType_id == 1 && noticeboard.intStandard_id == 0)
                        {
                            SqlString = "select vchFCMToken from Student_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and  intSchool_id='" + SchoolId + "' ";

                        }
                        else if (noticeboard.intUserType_id == 1 && noticeboard.intStandard_id != 0)
                        {
                            SqlString = " select vchFCMToken from Student_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and intstanderd_id=" + noticeboard.intStandard_id + "";

                        }
                        else if (noticeboard.intUserType_id == 3)
                        {
                            SqlString = "select vchFCMToken from tblTeacher_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and  intSchool_id='" + SchoolId + "' ";

                        }
                        else if (noticeboard.intUserType_id == 5)
                        {
                            SqlString = "select vchFCMToken from tblAdmin_Master where vchFCMToken IS NOT NULL and vchFCMToken<>'' and  intSchool_id='" + SchoolId + "'";
                        }
                        else
                        {
                        }
                        if (SqlString != "")
                        {
                            SqlDataAdapter sda = new SqlDataAdapter(SqlString, conn);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string regId = dt.Rows[i]["vchFCMToken"].ToString();
                                var applicationID = "AIzaSyCHfQSjFsEybdNRibLORHTMVVp6CKoI5TQ"; // PbojmyrmspdqkZ0pINni7DyqvhY2

                                string title = "NoticeBoard";
                                string message = "A new Notice For you";
                                var SENDER_ID = "574926706382";
                                var value = message.Trim();
                                WebRequest tRequest;
                                tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                                tRequest.Method = "post";
                                tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
                                    + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId + "&data.title=" + title + "";
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
                        }
                    }

                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet GalleryDetail(string extension, string EventDescription, string Folder_id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_Mob_galleryData";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", "Insert");
                    com.Parameters.AddWithValue("@FileName", extension);
                    com.Parameters.AddWithValue("@EventDescription", EventDescription);
                    com.Parameters.AddWithValue("@Uploadedfrom", "Mobile");
                    com.Parameters.AddWithValue("@Filetype", "Gallery");
                    com.Parameters.AddWithValue("@GalleryId", Folder_id);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "GalleryDetail");
                    return (ds);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet Gallerydelete(string id, string filename)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_Mob_galleryData";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", "Delete");
                    com.Parameters.AddWithValue("@id", id);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "GalleryDetail");
                    var filePath = HttpContext.Current.Server.MapPath("~/image/" + filename);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        public DataSet AnnualFeeDetails(string command, feecollectiondetails feedetail)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    //String query = "";
                    //if (command == "AnnualfeeOutstandingstudent")
                    //{
                    //    query = "usp_SchoolFeeCollectionNew_SP";
                    //}
                    //else if (command == "MonthlyOutstandingstudent")
                    //{
                    //    query = "usp_SchoolFeeCollectionNew_SP";
                    //}
                    String query = "usp_SchoolFeeCollectionNew_SP";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@type", command);
                    com.Parameters.AddWithValue("@intStandard_id", feedetail.intstandard_id);
                    com.Parameters.AddWithValue("@intDivision_id ", feedetail.intDivision_id);
                    //com.Parameters.AddWithValue("@Academic_id", feedetail.intAcademic_id);
                    //com.Parameters.AddWithValue("@intschool_id", feedetail.intSchool_id);
                    com.Parameters.AddWithValue("@intstudent_id", feedetail.Student_id);
                    if (command == "selectstudent")
                    {
                        com.Parameters.AddWithValue("@intschool_id", feedetail.intSchool_id);
                        com.Parameters.AddWithValue("@FromDate", feedetail.fromdate);
                        com.Parameters.AddWithValue("@ToDate", feedetail.todate);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "FeeDetails");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        public DataSet FeeDetails(string command, feecollectiondetails feedetail)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    //String query = "";
                    //if (command == "AnnualfeeOutstandingstudent")
                    //{
                    //    query = "usp_SchoolFeeCollectionNew_SP";
                    //}
                    //else if (command == "MonthlyOutstandingstudent")
                    //{
                    //    query = "usp_SchoolFeeCollectionNew_SP";
                    //}
                    String query = "usp_Mob_getAllFeePaidDetailsBetweenDates";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    com.Parameters.AddWithValue("@intschool_id", feedetail.intSchool_id);
                    com.Parameters.AddWithValue("@FromDate", feedetail.fromdate);
                    com.Parameters.AddWithValue("@ToDate", feedetail.todate);
                    //AllDivisions
                    if (command == "AllDivision")
                    {
                        com.Parameters.AddWithValue("@intstanderd_id", feedetail.intstandard_id);
                    }
                    else if (command == "selectstudent")
                    {
                        com.Parameters.AddWithValue("@intstanderd_id", feedetail.intstandard_id);
                        com.Parameters.AddWithValue("@intDivision_id", feedetail.intDivision_id);
                        com.Parameters.AddWithValue("@intStudent_id", feedetail.intStudent_id);
                    }
                    else if (command == "select")
                    {
                        com.Parameters.AddWithValue("@intstanderd_id", feedetail.intstandard_id);
                        com.Parameters.AddWithValue("@intDivision_id", feedetail.intDivision_id);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "FeeDetails");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet OtherFee(string command, OtherFee otherFee)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {

                    String query = "usp_Mob_SchoolFeeCollectionNew_SP";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@type", command);
                    com.Parameters.AddWithValue("@intschool_id", otherFee.intSchool_id);
                    com.Parameters.AddWithValue("@intAcademic_id", otherFee.intAcademic_id);
                    if (command == "DueTransportMonthstudent")
                    {
                        com.Parameters.AddWithValue("@intDivision_id", otherFee.intDivision_id);
                        com.Parameters.AddWithValue("@intStandard_id", otherFee.intstandard_id);
                        com.Parameters.AddWithValue("@intstudent_id", otherFee.intStudent_id);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "OtherFeeDetails");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet OutstandingFeeDetails(string command, OutstandingFee outstandingfee)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {

                    String query = "usp_Mob_SchoolFeeCollectionNew_SP";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@type", command);
                    com.Parameters.AddWithValue("@intschool_id", outstandingfee.intSchool_id);
                    com.Parameters.AddWithValue("@intAcademic_id", outstandingfee.intAcademic_id);
                    if (command == "StandardWiseOutstandingFee")
                    {
                        com.Parameters.AddWithValue("@intStandard_id", outstandingfee.intstandard_id);
                    }
                    else if (command == "DivisionWiseOutstandingFee")
                    {
                        com.Parameters.AddWithValue("@intStandard_id", outstandingfee.intstandard_id);
                        com.Parameters.AddWithValue("@intDivision_id", outstandingfee.intDivision_id);
                    }
                    else if (command == "StudentWiseOutstandingFee")
                    {
                        com.Parameters.AddWithValue("@intStandard_id", outstandingfee.intstandard_id);
                        com.Parameters.AddWithValue("@intDivision_id", outstandingfee.intDivision_id);
                        com.Parameters.AddWithValue("@intstudent_id", outstandingfee.intStudent_id);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "OutstandingFeeDetails");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        public DataSet Feecollection(string command, feecollectiondetails feedetail)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {

                    String query = "usp_SchoolFeeCollectionNew_SP";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@type", command);
                    com.Parameters.AddWithValue("@intStandard_id", feedetail.intstandard_id);
                    com.Parameters.AddWithValue("@intDivision_id ", feedetail.intDivision_id);
                    //com.Parameters.AddWithValue("@Academic_id", feedetail.intAcademic_id);
                    //com.Parameters.AddWithValue("@intschool_id", feedetail.intSchool_id);
                    com.Parameters.AddWithValue("@intstudent_id", feedetail.Student_id);

                    //if (command == "DueTransportMonthstudent")
                    //{

                    //    com.Parameters.AddWithValue("@intStandard_id", feedetail.intstandard_id );
                    //    com.Parameters.AddWithValue("@intDivision_id", feedetail.intDivision_id );

                    //}

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Feecollection");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        public DataSet ChatDetails(string command, ChatDetail chatdetail)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_Mob_ChatDetails";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    com.Parameters.AddWithValue("@Standard_id", chatdetail.intstandard_id);
                    com.Parameters.AddWithValue("@Division_id", chatdetail.intDivision_id);
                    com.Parameters.AddWithValue("@Academic_id", chatdetail.intAcademic_id);
                    com.Parameters.AddWithValue("@intschool_id", chatdetail.intSchool_id);
                    com.Parameters.AddWithValue("@intUser_id", chatdetail.User_id);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ChatDetails");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet FCMTokenUpdate(string command, Login Login)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query;
                    if (command == "updateStudent" || command == "updateTeacher" || command == "updateAdmin" || command == "updatePrincipal" || command == "updateManager")
                    {
                        query = "usp_Mob_Studentchangepassword";
                    }
                    else
                    {
                        query = "usp_LoginDetails";
                    }
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", command);
                    if (command == "updateStudent" || command == "updateTeacher" || command == "updateAdmin" || command == "updatePrincipal" || command == "updateManager")
                    {
                        com.Parameters.AddWithValue("@userName", Login.vchUser_name);
                        com.Parameters.AddWithValue("@password", Login.vchPassword);
                        com.Parameters.AddWithValue("@User_id", Login.intUser_id);
                        com.Parameters.AddWithValue("@intschool_id", Login.intSchool_id);
                        com.Parameters.AddWithValue("@intUserType_id", Login.intUserType_id);
                        com.Parameters.AddWithValue("@intAcademic_id", Login.intAcademic_id);
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@vchFCMToken", Login.vchFCMToken);
                        com.Parameters.AddWithValue("@vchEmail", Login.vchEmail);
                        com.Parameters.AddWithValue("@intUser_id", Login.intUser_id);
                        com.Parameters.AddWithValue("@intSchool_id", Login.intSchool_id);
                        com.Parameters.AddWithValue("@intAcademic_id", Login.intAcademic_id);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "LoginDetails");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        public DataSet OnlineClassTimetable(string command, OnlineClassTimetable onlineClassTimetable)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_onlineLecture";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    if (command == "StandardWiseList")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intAcademic_id", onlineClassTimetable.intAcademic_id);
                        com.Parameters.AddWithValue("@intSchool_id", onlineClassTimetable.intSchool_id);
                        com.Parameters.AddWithValue("@intStandard_id", onlineClassTimetable.intStandard_id);
                        com.Parameters.AddWithValue("@dtLecture_date", onlineClassTimetable.dtLecture_date);
                    }
                    else if (command == "TeacherWiseList")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intAcademic_id", onlineClassTimetable.intAcademic_id);
                        com.Parameters.AddWithValue("@intSchool_id", onlineClassTimetable.intSchool_id);
                        com.Parameters.AddWithValue("@intTeacher_id", onlineClassTimetable.intTeacher_id);
                        com.Parameters.AddWithValue("@dtLecture_date", onlineClassTimetable.dtLecture_date);
                    }
                    else if (command == "AdminWiseList")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intAcademic_id", onlineClassTimetable.intAcademic_id);
                        com.Parameters.AddWithValue("@intSchool_id", onlineClassTimetable.intSchool_id);
                        com.Parameters.AddWithValue("@dtLecture_date", onlineClassTimetable.dtLecture_date);
                    }
                    else if (command == "GetOnliceClassDetails")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intSchool_id", onlineClassTimetable.intSchool_id);
                        com.Parameters.AddWithValue("@intOnlinelecture_id", onlineClassTimetable.intOnlinelecture_id);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "OnlineTimetable");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        public DataSet OnlineClassSchedule(string command, OnlineClassSchedule onlineClassSchedule)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_OnlineNocticeBoard";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    if (command == "StudentNotification")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        // com.Parameters.AddWithValue("@intAcademic_id", onlineClassSchedule.intAcademic_id);
                        com.Parameters.AddWithValue("@intSchool_id", onlineClassSchedule.intSchool_id);
                        com.Parameters.AddWithValue("@intStandard_id", onlineClassSchedule.intStandard_id);
                        com.Parameters.AddWithValue("@dtLecture_date", onlineClassSchedule.dtLecture_date);
                    }
                    else if (command == "TeacherNotification")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        //  com.Parameters.AddWithValue("@intAcademic_id", onlineClassSchedule.intAcademic_id);
                        com.Parameters.AddWithValue("@intSchool_id", onlineClassSchedule.intSchool_id);
                        com.Parameters.AddWithValue("@intTeacher_id", onlineClassSchedule.intTeacher_id);
                        com.Parameters.AddWithValue("@dtLecture_date", onlineClassSchedule.dtLecture_date);
                    }
                    else if (command == "AdminNotification")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        //  com.Parameters.AddWithValue("@intAcademic_id", onlineClassSchedule.intAcademic_id);
                        com.Parameters.AddWithValue("@intSchool_id", onlineClassSchedule.intSchool_id);
                        com.Parameters.AddWithValue("@dtLecture_date", onlineClassSchedule.dtLecture_date);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "OnlineSchedule");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        public DataSet APKVersionDetails(string command, APKVersion aPKVersion)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_APkVersion";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    if (command == "SelectVersion")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intSchool_id", aPKVersion.intschool_id);
                    }
                    else if (command == "insertVersion")
                    {
                        com.Parameters.AddWithValue("@command", command);
                        com.Parameters.AddWithValue("@intSchool_id", aPKVersion.intschool_id);
                        com.Parameters.AddWithValue("@vchVersion_name", aPKVersion.vchVersion_name);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "APKVersion");
                    return (ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
    }
}