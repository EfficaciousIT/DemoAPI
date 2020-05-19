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
    public class LibraryController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string intSchool_id)
        {
            Library library = new Library();
            library.intschool_id = Convert.ToInt32(intSchool_id);
            DataSet ds = record.LibraryData(command, library);
            return ds; 
        }
        public DataSet Get(string command, string intSchool_id, string intStandard_id)
        {
            Library library = new Library();
            library.intStandard_id = Convert.ToInt32(intStandard_id);
            library.intschool_id = Convert.ToInt32(intSchool_id);
            DataSet ds = record.LibraryData(command,library);
            return ds; 
        }
        public DataSet Get(string command, string intSchool_id,string intCategory_id,string intStandard_id, string intBookLanguage_id)
        {
            Library library = new Library();
            library.intschool_id = Convert.ToInt32(intSchool_id);
            library.intCategory_id = Convert.ToInt32(intCategory_id);
            library.intStandard_id = Convert.ToInt32(intStandard_id);
            library.intBookLanguage_id = Convert.ToInt32(intBookLanguage_id);
            DataSet ds = record.LibraryData(command, library);
            return ds; 
        }
        public DataSet Get(string command, int intSchool_id, string intstandard_id, string intStudent_id,string dtAssigned_Date,string dtReturn_date, string intBookLanguage_id)
        {
            Library library = new Library();
            library.intschool_id = Convert.ToInt32(intSchool_id);
            library.intStandard_id = Convert.ToInt32(intstandard_id);
            library.intStudent_id = Convert.ToInt32(intStudent_id);
            library.intBookLanguage_id = Convert.ToInt32(intBookLanguage_id);
            library.dtAssigned_Date = Convert.ToDateTime(dtAssigned_Date).ToString("yyyy/MM/dd");
            library.dtReturn_date=Convert.ToDateTime(dtReturn_date).ToString("yyyy/MM/dd");
            DataSet ds = record.LibraryData(command, library);
            return ds; 
        }

        public DataSet Get(string command, string intSchool_id, string intTeacher_id, string intDepartment_id, string dtAssigned_Date, string dtReturn_date,string intBookLanguage_id)
        {
            Library library = new Library();
            library.intschool_id = Convert.ToInt32(intSchool_id);
            library.intTeacher_id = Convert.ToInt32(intTeacher_id);
            library.intDepartment_id = Convert.ToInt32(intDepartment_id);
            library.intBookLanguage_id = Convert.ToInt32(intBookLanguage_id);
            library.dtAssigned_Date = Convert.ToDateTime(dtAssigned_Date).ToString("yyyy/MM/dd");
            library.dtReturn_date = Convert.ToDateTime(dtReturn_date).ToString("yyyy/MM/dd");
            DataSet ds = record.LibraryData(command, library);
            return ds; 
        }

        public DataSet Get(string command, string intSchool_id, string intStandard_id, string intStudent_id)
        {
            Library library = new Library();
            library.intschool_id = Convert.ToInt32(intSchool_id);
            library.intStandard_id = Convert.ToInt32(intStandard_id);
            library.intStudent_id = Convert.ToInt32(intStudent_id);
            DataSet ds = record.LibraryDetail(command, library);
            return ds;
        }
    }
}
