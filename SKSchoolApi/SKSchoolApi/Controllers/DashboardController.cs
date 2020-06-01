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
    public class DashboardController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string intAcademic_id)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.intAcademic_id = Convert.ToInt32(intAcademic_id);
            DataSet ds = record.DashboardDetails(command, dashboard);
            return ds;
        }
        public DataSet Get(string command, string intAcademic_id, string intSchool_id)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.intAcademic_id = Convert.ToInt32(intAcademic_id);
            dashboard.intSchool_id = Convert.ToInt32(intSchool_id);
            DataSet ds = record.DashboardDetails(command, dashboard);
            return ds;
        }
        public DataSet Get(string command, string intAcademic_id, string intDivision_id, string intstanderd_id)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.intAcademic_id = Convert.ToInt32(intAcademic_id);
            dashboard.intDivision_id = Convert.ToInt32(intDivision_id);
            dashboard.intstanderd_id = Convert.ToInt32(intstanderd_id);
            DataSet ds = record.DashboardDetails(command, dashboard);
            return ds;
        }
        public DataSet Get(string command, string intstanderd_id, int intSchool_id, string intAcademic_id)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.intstanderd_id = Convert.ToInt32(intstanderd_id);
            dashboard.intSchool_id = Convert.ToInt32(intSchool_id);
            dashboard.intAcademic_id = Convert.ToInt32(intAcademic_id);
            DataSet ds = record.DashboardDetails(command, dashboard);
            return ds;
        }
    }
}
