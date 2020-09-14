using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SKSchoolApi.Models;
using System.Reflection;

namespace SKSchoolApi.Database
{
    public class SchoolFees
    {
        string constr = System.Configuration.ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        string SqlString = "";
        string strQry = "";
        DataSet dsObj = new DataSet();
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        DataSet ds3 = new DataSet();
        DataTable olddt = new DataTable();
        string FirstMonth = "";
        int loop = 0;
        string vchConcession_per = "";
        string vchConcession_amt = "";
        DataSet tempds = new DataSet();
        DataSet dsTransport = new DataSet();
        int loopcount = 0;

        public DataSet GetFeeMonthWise(string command,FeeCollection feeCollection)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_Addfee";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", "SearchStudentByID");
                    com.Parameters.AddWithValue("@intSchool_id", feeCollection.intSchool_id);
                    com.Parameters.AddWithValue("@intAcademic_id", feeCollection.intAcademic_id);
                    com.Parameters.AddWithValue("@intStudentID_Number", feeCollection.intStudentID_Number);

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StudentDetails");
                    if(ds.Tables["StudentDetails"].Rows.Count >0)
                    { 
                        string StudentStartDate = ds.Tables["StudentDetails"].Rows[0]["dtstart_date"].ToString();
                    string StudentEndDate = ds.Tables["StudentDetails"].Rows[0]["dtend_date"].ToString();

                    if (StudentStartDate != null && StudentStartDate != "")
                    {
                        StudentStartDate = Convert.ToDateTime(StudentStartDate).ToString("dd/MM/yyyy");
                    }
                    if (StudentEndDate != null && StudentEndDate != "")
                    {
                        StudentEndDate = Convert.ToDateTime(StudentEndDate).ToString("dd/MM/yyyy");
                    }

                    if (StudentStartDate != "" && StudentEndDate != "")
                    {
                        SqlCommand com1 = new SqlCommand(query, con);
                       
                        com1.CommandType = CommandType.StoredProcedure;
                        com1.Parameters.AddWithValue("@command", "GetAllFeesStandardWise");
                        com1.Parameters.AddWithValue("@intstandard_id", feeCollection.intstandard_id);
                        com1.Parameters.AddWithValue("@intSchool_id", feeCollection.intSchool_id);
                        com1.Parameters.AddWithValue("@dtfrom_date", Convert.ToDateTime(StudentStartDate).ToString("MM/dd/yyyy"));
                        com1.Parameters.AddWithValue("@dtto_date", Convert.ToDateTime(StudentEndDate).ToString("MM/dd/yyyy"));
                        SqlDataAdapter da1 = new SqlDataAdapter(com1);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1, "FeeDetails");

                        for (int i = 0; ds1.Tables[0].Rows.Count > i; i++)
                        {
                            if (Convert.ToString(ds1.Tables[0].Rows[i]["frequency"]) == "12")
                            {
                                BindItem(Convert.ToString(feeCollection.intstandard_id), Convert.ToString(ds1.Tables[0].Rows[i]["intFeemaster_id"]), Convert.ToString(feeCollection.intSchool_id), StudentStartDate, StudentEndDate, feeCollection.intStudentID);
                            }
                            else
                            {
                                strQry = "usp_Addfee @command='CheckRecordsExits',@intStudent_id='" + feeCollection.intStudentID + "',@intFeemaster_id='" + ds1.Tables[0].Rows[i]["intFeemaster_id"] + "'";
                                ds = sGetDataset(strQry);
                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    BindItem(Convert.ToString(feeCollection.intstandard_id), Convert.ToString(ds1.Tables[0].Rows[i]["intFeemaster_id"]), Convert.ToString(feeCollection.intSchool_id), StudentStartDate, StudentEndDate, feeCollection.intStudentID);
                                }
                            }

                        }

                    }



                    //Check Transport Fee
                    strQry = "usp_Addfee @command='CheckTransportFee',@intStudent_id='" + feeCollection.intStudentID + "'";
                    ds = sGetDataset(strQry);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //int fromMonth = dtfromdate.Month;
                        int fromMonth = Convert.ToDateTime(StudentStartDate).Month;
                        int month = 0;

                        int monthdiff = GetMonthDifference(Convert.ToDateTime(StudentStartDate), Convert.ToDateTime(StudentEndDate));
                        string[] GetMonths = GetMonthsBetweenDates(Convert.ToDateTime(StudentStartDate), monthdiff + 1);

                        int fromdate = Convert.ToDateTime(StudentStartDate).Day;

                        //int year = dtfromdate.Year;
                        int year = Convert.ToDateTime(StudentStartDate).Year;
                        for (int i = 0; monthdiff >= i; i++)
                        {
                            month = (fromMonth + i);
                            if (month > 12)
                            {
                                year = Convert.ToDateTime(StudentStartDate).Year + 1;
                                month = (fromMonth + i) - 12;
                            }
                            DateTime dtdate = new DateTime(year, month, fromdate);
                            string dtdate1 = Convert.ToDateTime(dtdate).ToString("MM/dd/yyyy");
                            string dtdate2 = Convert.ToDateTime(dtdate).ToString("dd/MM/yyyy");

                            strQry = "usp_Addfee @command='CheckTransportFee',@intStudent_id='" + feeCollection.intStudentID + "',@dtdate='" + dtdate1 + "'";
                            ds = sGetDataset(strQry);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strQry = "usp_Addfee @command='SelectTransportRecords',@intStudent_id='" + feeCollection.intStudentID + "',@dtdate='" + dtdate1 + "'";
                                dsTransport = sGetDataset(strQry);
                                if (dsTransport.Tables[0].Rows.Count > 0)
                                {
                                    //Check this record is already inserted or not in the fee table  
                                    strQry = "usp_Addfee @command='CheckRecordMonthWise',@intStudent_id='" + feeCollection.intStudentID + "',@intFeemaster_id='0',@intMonth_id='" + GetMonths[i] + "'";
                                    DataSet ds1 = sGetDataset(strQry);
                                    if (ds1.Tables[0].Rows.Count == 0)
                                    {
                                        if (Convert.ToDateTime(dtdate2) >= Convert.ToDateTime(StudentStartDate) && Convert.ToDateTime(dtdate2) <= Convert.ToDateTime(StudentEndDate))
                                        {
                                            CheckTransportLateFee(dtdate2, Convert.ToString(dsTransport.Tables[0].Rows[0]["intTransport_id"]));

                                            strQry = "usp_Addfee @command='selectConcessionDetails',@intStudent_id='" + feeCollection.intStudentID + "',@dtdate='" + dtdate1 + "'";

                                            ds = sGetDataset(strQry);
                                            if (ds.Tables[0].Rows.Count > 0)
                                            {
                                                vchConcession_per = ds.Tables[0].Rows[0]["vchConcession_per"].ToString();
                                                vchConcession_amt = ds.Tables[0].Rows[0]["vchConcession_amt"].ToString();
                                            }

                                            DataColumnCollection col = dsTransport.Tables[0].Columns;
                                            if (!col.Contains("Concession"))
                                                dsTransport.Tables[0].Columns.Add("Concession");

                                            DataColumnCollection col1 = dsTransport.Tables[0].Columns;
                                            if (!col1.Contains("NetAmount"))
                                                dsTransport.Tables[0].Columns.Add("NetAmount",typeof(int));

                                            string FeeAmount = dsTransport.Tables[0].Rows[0]["vchfee"].ToString();
                                            string conAmt = "0";

                                            if (vchConcession_per != "")
                                            {
                                                conAmt = Convert.ToString(Convert.ToInt32(FeeAmount) * Convert.ToInt32(vchConcession_per) / 100);
                                            }
                                            else if (vchConcession_amt != "")
                                            {
                                                conAmt = vchConcession_amt;
                                            }

                                            if (dsTransport.Tables[0].Rows[0]["vchconssion"].ToString() == "Yes")
                                                dsTransport.Tables[0].Rows[0]["Concession"] = conAmt;
                                            else
                                                dsTransport.Tables[0].Rows[0]["Concession"] = "0";

                                            int Rsconcession = (conAmt == "0") ? Rsconcession = 0 : Rsconcession = Convert.ToInt32(conAmt);

                                            int NetAmt = Convert.ToInt32(FeeAmount) - Rsconcession;

                                            dsTransport.Tables[0].Rows[0]["NetAmount"] = NetAmt;


                                            DataColumnCollection col2 = dsTransport.Tables[0].Columns;
                                            if (!col2.Contains("LateFee"))
                                                dsTransport.Tables[0].Columns.Add("LateFee");

                                            dsTransport.Tables[0].Rows[0]["LateFee"] = "0";

                                            AddData(dsTransport);
                                        }
                                    }
                                }
                                }
                            } 

                        }
                    }
                    dsObj.Tables.Add(olddt);
                    DataSet Monthfee = new DataSet();
                    
                    if (command == "MonthWiseTotalFee")
                    {
                        Monthfee.Tables.Add(MonthWiseCalculation(olddt, "MonthWiseTotalFee"));
                        Monthfee.Tables[0].TableName = "MonthWiseFee";
                    }
                    else if(command == "MonthFeeDetails")
                    {
                        Monthfee.Tables.Add(MonthWiseFeeDetails(olddt, feeCollection.month, "MonthFeeDetails","Paid"));
                        Monthfee.Tables[0].TableName = "MonthFeeDetails";
                    }
                    else if (command == "PayFeeMonthWise")
                    {
                        string Months =feeCollection.month;
                        string[] values = Months.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            values[i] = values[i].Trim();
                            Monthfee.Tables.Add(MonthWiseFeeDetails(olddt, values[i], "onthFeeDetails",""));
                            Monthfee.Tables[0].TableName = "MonthFeeDetails";
                            Monthfee.Tables["MonthFeeDetails"].Merge(Monthfee.Tables[i]);
                        }
                    }

                    return Monthfee;
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
        public void BindItem(string intstandard_id, string intFeemaster_id, string intschool_id, string StudentStartDate, string StudentEndDate,int intStudentID)
        {

            string strFromDate = "";
            string strToDate = "";
            
            strFromDate = Convert.ToDateTime(StudentStartDate).ToString("dd/MM/yyyy").Replace("-", "/");
            strToDate = Convert.ToDateTime(StudentEndDate).ToString("dd/MM/yyyy").Replace("-", "/");
            

            string sSQL = "exec usp_Addfee @command='GetDataFeeMasterWise',@intstandard_id='" + intstandard_id + "',@intFeemaster_id='" + intFeemaster_id + "',@intschool_id='" + intschool_id + "',@dtdate='" + Convert.ToDateTime(strFromDate).ToString("MM/dd/yyyy") + "'";

            dsObj = sGetDataset(sSQL);

            if ((Convert.ToString(dsObj.Tables[0].Rows[0]["frequency"])) == "12")
            {

                int monthdiff = GetMonthDifference(Convert.ToDateTime(StudentStartDate), Convert.ToDateTime(strToDate));

                string[] GetMonths = GetMonthsBetweenDates(Convert.ToDateTime(StudentStartDate), monthdiff + 1);

                string firstmonthindex = null;

                string StudStartDate = Convert.ToDateTime(StudentStartDate).ToString("dd/MM/yyyy").Replace("-", "/");

                for (int i = 0; monthdiff >= i; i++)
                {
                    strQry = "usp_Addfee @command='CheckRecordMonthWise',@intStudent_id='" + intStudentID + "',@intFeemaster_id='" + intFeemaster_id + "',@intMonth_id='" + GetMonths[i] + "'";
                    ds2 = sGetDataset(strQry);
                    if (ds2.Tables[0].Rows.Count == 0)
                    {
                        if (firstmonthindex == null)
                        {
                            FirstMonth = GetMonths[i];
                            firstmonthindex = GetMonths[i];
                            loop += 1;
                        }
                        if (Convert.ToDateTime(strFromDate) >= Convert.ToDateTime(StudentStartDate) && Convert.ToDateTime(strFromDate) <= Convert.ToDateTime(StudentEndDate))
                        {
                            CheckLateFee(StudStartDate, intstandard_id, intFeemaster_id, intschool_id);
                            dsObj.Tables[0].Rows[0]["month"] = GetMonths[i];
                            GetConcession(strFromDate, intStudentID);
                            AddData(dsObj);
                        }
                    }
                    StudStartDate = Convert.ToDateTime(StudStartDate).AddMonths(1).ToString();
                }
            }
            else
            {
                UpdateMonth(dsObj.Tables[0].Rows[0]["feestartdate"].ToString());
                CheckLateFee(strFromDate, intstandard_id, intFeemaster_id, intschool_id);
                GetConcession(Convert.ToDateTime(dsObj.Tables[0].Rows[0]["feestartdate"]).ToString("dd/MM/yyyy"), intStudentID);
                if (Convert.ToDateTime(strFromDate) >= Convert.ToDateTime(StudentStartDate) && Convert.ToDateTime(strFromDate) <= Convert.ToDateTime(StudentEndDate))
                {
                    AddData(dsObj);
                }
            }
        }

        public DataSet GetPaidFeeMonthWise(string command, FeeCollection feeCollection)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    String query = "usp_Addfee";
                    SqlCommand com = new SqlCommand(query, con);
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@command", "SearchStudentByID");
                    com.Parameters.AddWithValue("@intSchool_id", feeCollection.intSchool_id);
                    com.Parameters.AddWithValue("@intAcademic_id", feeCollection.intAcademic_id);
                    com.Parameters.AddWithValue("@intStudentID_Number", feeCollection.intStudentID_Number);

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StudentDetails");
                    if (ds.Tables["StudentDetails"].Rows.Count > 0)
                    {
                        string StudentStartDate = ds.Tables["StudentDetails"].Rows[0]["dtstart_date"].ToString();
                        string StudentEndDate = ds.Tables["StudentDetails"].Rows[0]["dtend_date"].ToString();

                        if (StudentStartDate != null && StudentStartDate != "")
                        {
                            StudentStartDate = Convert.ToDateTime(StudentStartDate).ToString("dd/MM/yyyy");
                        }
                        if (StudentEndDate != null && StudentEndDate != "")
                        {
                            StudentEndDate = Convert.ToDateTime(StudentEndDate).ToString("dd/MM/yyyy");
                        }

                        if (StudentStartDate != "" && StudentEndDate != "")
                        {
                            SqlCommand com1 = new SqlCommand(query, con);

                            com1.CommandType = CommandType.StoredProcedure;
                            com1.Parameters.AddWithValue("@command", "GetAllFeesStandardWise");
                            com1.Parameters.AddWithValue("@intstandard_id", feeCollection.intstandard_id);
                            com1.Parameters.AddWithValue("@intSchool_id", feeCollection.intSchool_id);
                            com1.Parameters.AddWithValue("@dtfrom_date", Convert.ToDateTime(StudentStartDate).ToString("MM/dd/yyyy"));
                            com1.Parameters.AddWithValue("@dtto_date", Convert.ToDateTime(StudentEndDate).ToString("MM/dd/yyyy"));
                            SqlDataAdapter da1 = new SqlDataAdapter(com1);
                            DataSet ds1 = new DataSet();        
                            da1.Fill(ds1, "FeeDetails");

                            for (int i = 0; ds1.Tables[0].Rows.Count > i; i++)
                            {
                                if (Convert.ToString(ds1.Tables[0].Rows[i]["frequency"]) == "12")
                                {
                                    BindDataWithStatus(Convert.ToString(feeCollection.intstandard_id), Convert.ToString(ds1.Tables[0].Rows[i]["intFeemaster_id"]), Convert.ToString(feeCollection.intSchool_id), StudentStartDate, StudentEndDate, feeCollection.intStudentID,"");
                                }
                                else
                                {
                                    strQry = "usp_Addfee @command='CheckRecordsExits',@intStudent_id='" + feeCollection.intStudentID + "',@intFeemaster_id='" + ds1.Tables[0].Rows[i]["intFeemaster_id"] + "'";
                                    ds = sGetDataset(strQry);
                                    if (ds.Tables[0].Rows.Count == 0)
                                    {
                                        BindDataWithStatus(Convert.ToString(feeCollection.intstandard_id), Convert.ToString(ds1.Tables[0].Rows[i]["intFeemaster_id"]), Convert.ToString(feeCollection.intSchool_id), StudentStartDate, StudentEndDate, feeCollection.intStudentID,"Unpaid");
                                    }
                                    else
                                    {
                                        BindDataWithStatus(Convert.ToString(feeCollection.intstandard_id), Convert.ToString(ds1.Tables[0].Rows[i]["intFeemaster_id"]), Convert.ToString(feeCollection.intSchool_id), StudentStartDate, StudentEndDate, feeCollection.intStudentID, "Paid");
                                    }
                                }

                            }

                        }



                        //Check Transport Fee
                        strQry = "usp_Addfee @command='CheckTransportFee',@intStudent_id='" + feeCollection.intStudentID + "'";
                        ds = sGetDataset(strQry);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //int fromMonth = dtfromdate.Month;
                            int fromMonth = Convert.ToDateTime(StudentStartDate).Month;
                            int month = 0;

                            int monthdiff = GetMonthDifference(Convert.ToDateTime(StudentStartDate), Convert.ToDateTime(StudentEndDate));
                            string[] GetMonths = GetMonthsBetweenDates(Convert.ToDateTime(StudentStartDate), monthdiff + 1);

                            int fromdate = Convert.ToDateTime(StudentStartDate).Day;

                            //int year = dtfromdate.Year;
                            int year = Convert.ToDateTime(StudentStartDate).Year;
                            for (int i = 0; monthdiff >= i; i++)
                            {
                                month = (fromMonth + i);
                                if (month > 12)
                                {
                                    year = Convert.ToDateTime(StudentStartDate).Year + 1;
                                    month = (fromMonth + i) - 12;
                                }
                                DateTime dtdate = new DateTime(year, month, fromdate);
                                string dtdate1 = Convert.ToDateTime(dtdate).ToString("MM/dd/yyyy");
                                string dtdate2 = Convert.ToDateTime(dtdate).ToString("dd/MM/yyyy");

                                strQry = "usp_Addfee @command='CheckTransportFee',@intStudent_id='" + feeCollection.intStudentID + "',@dtdate='" + dtdate1 + "'";
                                ds = sGetDataset(strQry);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    strQry = "usp_Addfee @command='SelectTransportRecords',@intStudent_id='" + feeCollection.intStudentID + "',@dtdate='" + dtdate1 + "'";
                                    dsTransport = sGetDataset(strQry);
                                    if (dsTransport.Tables[0].Rows.Count > 0)
                                    {
                                        //Check this record is already inserted or not in the fee table  
                                        strQry = "usp_Addfee @command='CheckRecordMonthWise',@intStudent_id='" + feeCollection.intStudentID + "',@intFeemaster_id='0',@intMonth_id='" + GetMonths[i] + "'";
                                        DataSet ds1 = sGetDataset(strQry);

                                        DataColumnCollection columns = dsTransport.Tables[0].Columns;
                                        if (!columns.Contains("Status"))
                                            dsTransport.Tables[0].Columns.Add("Status");

                                        if (ds1.Tables[0].Rows.Count == 0)
                                        {
                                            if (Convert.ToDateTime(dtdate2) >= Convert.ToDateTime(StudentStartDate) && Convert.ToDateTime(dtdate2) <= Convert.ToDateTime(StudentEndDate))
                                            {
                                                dsTransport.Tables[0].Rows[0]["Status"] = "Unpaid";

                                                CheckTransportLateFee(dtdate2, Convert.ToString(dsTransport.Tables[0].Rows[0]["intTransport_id"]));

                                                strQry = "usp_Addfee @command='selectConcessionDetails',@intStudent_id='" + feeCollection.intStudentID + "',@dtdate='" + dtdate1 + "'";

                                                ds = sGetDataset(strQry);
                                                if (ds.Tables[0].Rows.Count > 0)
                                                {
                                                    vchConcession_per = ds.Tables[0].Rows[0]["vchConcession_per"].ToString();
                                                    vchConcession_amt = ds.Tables[0].Rows[0]["vchConcession_amt"].ToString();
                                                }

                                                DataColumnCollection col = dsTransport.Tables[0].Columns;
                                                if (!col.Contains("Concession"))
                                                    dsTransport.Tables[0].Columns.Add("Concession");

                                                DataColumnCollection col1 = dsTransport.Tables[0].Columns;
                                                if (!col1.Contains("NetAmount"))
                                                    dsTransport.Tables[0].Columns.Add("NetAmount", typeof(int));

                                                string FeeAmount = dsTransport.Tables[0].Rows[0]["vchfee"].ToString();
                                                string conAmt = "0";

                                                if (vchConcession_per != "")
                                                {
                                                    conAmt = Convert.ToString(Convert.ToInt32(FeeAmount) * Convert.ToInt32(vchConcession_per) / 100);
                                                }
                                                else if (vchConcession_amt != "")
                                                {
                                                    conAmt = vchConcession_amt;
                                                }

                                                if (dsTransport.Tables[0].Rows[0]["vchconssion"].ToString() == "Yes")
                                                    dsTransport.Tables[0].Rows[0]["Concession"] = conAmt;
                                                else
                                                    dsTransport.Tables[0].Rows[0]["Concession"] = "0";

                                                int Rsconcession = (conAmt == "0") ? Rsconcession = 0 : Rsconcession = Convert.ToInt32(conAmt);

                                                int NetAmt = Convert.ToInt32(FeeAmount) - Rsconcession;

                                                dsTransport.Tables[0].Rows[0]["NetAmount"] = NetAmt;


                                                DataColumnCollection col2 = dsTransport.Tables[0].Columns;
                                                if (!col2.Contains("LateFee"))
                                                    dsTransport.Tables[0].Columns.Add("LateFee");

                                                dsTransport.Tables[0].Rows[0]["LateFee"] = "0";

                                                AddData(dsTransport);
                                            }
                                        }
                                        else
                                        {
                                            if (Convert.ToDateTime(dtdate2) >= Convert.ToDateTime(StudentStartDate) && Convert.ToDateTime(dtdate2) <= Convert.ToDateTime(StudentEndDate))
                                            {
                                                dsTransport.Tables[0].Rows[0]["Status"] = "Paid";

                                                CheckTransportLateFee(dtdate2, Convert.ToString(dsTransport.Tables[0].Rows[0]["intTransport_id"]));

                                                strQry = "usp_Addfee @command='selectConcessionDetails',@intStudent_id='" + feeCollection.intStudentID + "',@dtdate='" + dtdate1 + "'";

                                                ds = sGetDataset(strQry);
                                                if (ds.Tables[0].Rows.Count > 0)
                                                {
                                                    vchConcession_per = ds.Tables[0].Rows[0]["vchConcession_per"].ToString();
                                                    vchConcession_amt = ds.Tables[0].Rows[0]["vchConcession_amt"].ToString();
                                                }

                                                DataColumnCollection col = dsTransport.Tables[0].Columns;
                                                if (!col.Contains("Concession"))
                                                    dsTransport.Tables[0].Columns.Add("Concession");

                                                DataColumnCollection col1 = dsTransport.Tables[0].Columns;
                                                if (!col1.Contains("NetAmount"))
                                                    dsTransport.Tables[0].Columns.Add("NetAmount", typeof(int));

                                                string FeeAmount = dsTransport.Tables[0].Rows[0]["vchfee"].ToString();
                                                string conAmt = "0";

                                                if (vchConcession_per != "")
                                                {
                                                    conAmt = Convert.ToString(Convert.ToInt32(FeeAmount) * Convert.ToInt32(vchConcession_per) / 100);
                                                }
                                                else if (vchConcession_amt != "")
                                                {
                                                    conAmt = vchConcession_amt;
                                                }

                                                if (dsTransport.Tables[0].Rows[0]["vchconssion"].ToString() == "Yes")
                                                    dsTransport.Tables[0].Rows[0]["Concession"] = conAmt;
                                                else
                                                    dsTransport.Tables[0].Rows[0]["Concession"] = "0";

                                                int Rsconcession = (conAmt == "0") ? Rsconcession = 0 : Rsconcession = Convert.ToInt32(conAmt);

                                                int NetAmt = Convert.ToInt32(FeeAmount) - Rsconcession;

                                                dsTransport.Tables[0].Rows[0]["NetAmount"] = NetAmt;


                                                DataColumnCollection col2 = dsTransport.Tables[0].Columns;
                                                if (!col2.Contains("LateFee"))
                                                    dsTransport.Tables[0].Columns.Add("LateFee");

                                                dsTransport.Tables[0].Rows[0]["LateFee"] = "0";

                                                AddData(dsTransport);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                    dsObj.Tables.Add(olddt);
                    DataSet Monthfee = new DataSet();

                    if (command == "MonthWiseTotalFeeStatus")
                    {
                        Monthfee.Tables.Add(MonthWiseCalculation(olddt, "MonthWiseTotalFeeStatus"));
                        Monthfee.Tables[0].TableName = "MonthWiseFee";
                    }
                    else if (command == "MonthFeeDetailsStatus")
                    {
                        Monthfee.Tables.Add(MonthWiseFeeDetails(olddt, feeCollection.month, "MonthFeeDetailsStatus","Paid"));
                        Monthfee.Tables[0].TableName = "MonthFeeDetails";
                    }
                   
                    return Monthfee;
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

        public void BindDataWithStatus(string intstandard_id, string intFeemaster_id, string intschool_id, string StudentStartDate, string StudentEndDate, int intStudentID,string status)
        {

            string strFromDate = "";
            string strToDate = "";

            strFromDate = Convert.ToDateTime(StudentStartDate).ToString("dd/MM/yyyy").Replace("-", "/");
            strToDate = Convert.ToDateTime(StudentEndDate).ToString("dd/MM/yyyy").Replace("-", "/");


            string sSQL = "exec usp_Addfee @command='GetDataFeeMasterWise',@intstandard_id='" + intstandard_id + "',@intFeemaster_id='" + intFeemaster_id + "',@intschool_id='" + intschool_id + "',@dtdate='" + Convert.ToDateTime(strFromDate).ToString("MM/dd/yyyy") + "'";

            dsObj = sGetDataset(sSQL);

            DataColumnCollection col = dsObj.Tables[0].Columns;
            if (!col.Contains("Status"))
                dsObj.Tables[0].Columns.Add("Status");

            if ((Convert.ToString(dsObj.Tables[0].Rows[0]["frequency"])) == "12")
            {

                int monthdiff = GetMonthDifference(Convert.ToDateTime(StudentStartDate), Convert.ToDateTime(strToDate));

                string[] GetMonths = GetMonthsBetweenDates(Convert.ToDateTime(StudentStartDate), monthdiff + 1);

                string firstmonthindex = null;

                string StudStartDate = Convert.ToDateTime(StudentStartDate).ToString("dd/MM/yyyy").Replace("-", "/");

                for (int i = 0; monthdiff >= i; i++)
                {
                    strQry = "usp_Addfee @command='CheckRecordMonthWise',@intStudent_id='" + intStudentID + "',@intFeemaster_id='" + intFeemaster_id + "',@intMonth_id='" + GetMonths[i] + "'";
                    ds2 = sGetDataset(strQry);
                    if (ds2.Tables[0].Rows.Count == 0)
                    {
                        if (firstmonthindex == null)
                        {
                            FirstMonth = GetMonths[i];
                            firstmonthindex = GetMonths[i];
                            loop += 1;
                        }
                        if (Convert.ToDateTime(strFromDate) >= Convert.ToDateTime(StudentStartDate) && Convert.ToDateTime(strFromDate) <= Convert.ToDateTime(StudentEndDate))
                        {
                            CheckLateFee(StudStartDate, intstandard_id, intFeemaster_id, intschool_id);
                            dsObj.Tables[0].Rows[0]["month"] = GetMonths[i];
                            dsObj.Tables[0].Rows[0]["Status"] = "Unpaid";
                            GetConcession(strFromDate, intStudentID);
                            AddData(dsObj);
                        }
                    }
                    else
                    {
                        if (Convert.ToDateTime(strFromDate) >= Convert.ToDateTime(StudentStartDate) && Convert.ToDateTime(strFromDate) <= Convert.ToDateTime(StudentEndDate))
                        {
                            CheckLateFee(StudStartDate, intstandard_id, intFeemaster_id, intschool_id);
                            dsObj.Tables[0].Rows[0]["month"] = GetMonths[i];
                            dsObj.Tables[0].Rows[0]["Status"] = "Paid";
                            GetConcession(strFromDate, intStudentID);
                            AddData(dsObj);
                        }
                    }
                    StudStartDate = Convert.ToDateTime(StudStartDate).AddMonths(1).ToString();
                }
            }
            else
            {
                dsObj.Tables[0].Rows[0]["Status"] = status;
                UpdateMonth(dsObj.Tables[0].Rows[0]["feestartdate"].ToString());
                CheckLateFee(strFromDate, intstandard_id, intFeemaster_id, intschool_id);
                GetConcession(Convert.ToDateTime(dsObj.Tables[0].Rows[0]["feestartdate"]).ToString("dd/MM/yyyy"), intStudentID);
                if (Convert.ToDateTime(strFromDate) >= Convert.ToDateTime(StudentStartDate) && Convert.ToDateTime(strFromDate) <= Convert.ToDateTime(StudentEndDate))
                {
                    AddData(dsObj);
                }
            }
        }


        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        private string[] GetMonthsBetweenDates(DateTime deltaDate, int TotalMonths)
        {
            var monthsBetweenDates = Enumerable.Range(0, TotalMonths)
                                               .Select(i => deltaDate.AddMonths(i))
                                               .OrderBy(e => e)
                                               .AsEnumerable();

            return monthsBetweenDates.Select(e => e.ToString("MMM")).ToArray();
        }


        private void AddData(DataSet dsObj)
        {
            tempds = dsObj;
            DataTable dt = new DataTable();
            DataRow NewRow = null;

            if (tempds.Tables[0].Rows.Count > 0)
            {
                DataTable MyDT = new DataTable();
                MyDT.Clear();
                MyDT = tempds.Tables[0];
                DataTable newdt = new DataTable();
                

                   
                    if (loop != 0)
                    {
                        if (olddt.Rows[0]["frequency"].ToString() == "12")
                        {
                            olddt.Rows[loopcount]["month"] = FirstMonth;
                            loop += 1;
                        }
                    }
                    newdt = tempds.Tables[0]; //(DataTable)ViewState["DataTable"];
                    olddt.Merge(newdt);
                    
                }

            }
        

        private void GetConcession(string Date,int intStudent_id)
        {
            try
            {
                strQry = "usp_Addfee @command='selectConcessionDetails',@intStudent_id='" + intStudent_id + "',@dtdate='" + Convert.ToDateTime(Date).ToString("MM/dd/yyyy") + "'";

                ds = sGetDataset(strQry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    vchConcession_per = ds.Tables[0].Rows[0]["vchConcession_per"].ToString();
                    vchConcession_amt = ds.Tables[0].Rows[0]["vchConcession_amt"].ToString();
                }
                else
                {
                    vchConcession_per = "";
                    vchConcession_amt = "";
                }

                DataColumnCollection col = dsObj.Tables[0].Columns;
                if (!col.Contains("Concession"))
                    dsObj.Tables[0].Columns.Add("Concession");

                DataColumnCollection col1 = dsObj.Tables[0].Columns;
                if (!col1.Contains("NetAmount"))
                    dsObj.Tables[0].Columns.Add("NetAmount",typeof(int));

                string FeeAmount = dsObj.Tables[0].Rows[0]["vchfee"].ToString();
                string conAmt = "0";

                if (vchConcession_per != "")
                {
                    conAmt = Convert.ToString(Convert.ToInt32(FeeAmount) * Convert.ToInt32(vchConcession_per) / 100);
                }
                else if (vchConcession_amt != "")
                {
                    conAmt = vchConcession_amt;
                }

                if (dsObj.Tables[0].Rows[0]["vchconssion"].ToString() == "Yes" )
                dsObj.Tables[0].Rows[0]["Concession"] = conAmt;
                else
                dsObj.Tables[0].Rows[0]["Concession"] = "0";

                int Rsconcession = (conAmt == "0") ? Rsconcession = 0 : Rsconcession = Convert.ToInt32(conAmt);

                int NetAmt = Convert.ToInt32(FeeAmount) - Rsconcession;

                dsObj.Tables[0].Rows[0]["NetAmount"] = NetAmt;

                DataColumnCollection col2 = dsObj.Tables[0].Columns;
                if (!col2.Contains("LateFee"))
                    dsObj.Tables[0].Columns.Add("LateFee");

                dsObj.Tables[0].Rows[0]["LateFee"] = "0";
            }
            catch (Exception ex)
            {
            }
        }

        private void UpdateMonth(string FeeDate)
        {
            try
            {
                string[] GetMonths = GetMonthsBetweenDates(Convert.ToDateTime(FeeDate), 1);
                dsObj.Tables[0].Rows[0]["month"] = GetMonths[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CheckTransportLateFee(string strFromDate, string intTransport_id)
        {
            try
            {
                strQry = "usp_Addfee @command='CheckTransportDueFee',@intTransport_id='" + intTransport_id + "'";
                ds3 = sGetDataset(strQry);
                int startdate = Convert.ToInt32(ds3.Tables[0].Rows[0]["Startday"]);
                int duedate = Convert.ToInt32(ds3.Tables[0].Rows[0]["Dueday"]);

                if (Convert.ToDateTime(strFromDate).Month == DateTime.Now.Month)
                {
                    if (Convert.ToInt32(startdate) <= DateTime.Now.Day && Convert.ToInt32(duedate) >= DateTime.Now.Day)
                    {
                        dsTransport.Tables[0].Rows[0]["vchLateCharge"] = "No";
                    }
                    else
                    {
                        dsTransport.Tables[0].Rows[0]["vchLateCharge"] = "Yes";
                    }
                }
                else if (Convert.ToDateTime(strFromDate).Month < DateTime.Now.Month && Convert.ToDateTime(strFromDate).Year <= DateTime.Now.Year)
                {
                    dsTransport.Tables[0].Rows[0]["vchLateCharge"] = "Yes";
                }
                else if (Convert.ToDateTime(strFromDate).Month > DateTime.Now.Month && Convert.ToDateTime(strFromDate).Year >= DateTime.Now.Year)
                {
                    dsTransport.Tables[0].Rows[0]["vchLateCharge"] = "No";
                }

            }
            catch (Exception ex)
            {
            }
        }


        private void CheckLateFee(string strFromDate, string intstandard_id, string intFeemaster_id, string intschool_id)
        {
            try
            {
                strQry = "usp_Addfee @command='CheckMothwiseDueFee',@intstandard_id='" + intstandard_id + "',@intFeemaster_id='" + intFeemaster_id + "',@intschool_id='" + intschool_id + "'";
                ds3 = sGetDataset(strQry);
                int startdate = Convert.ToInt32(ds3.Tables[0].Rows[0]["dtstart_date"]);
                int duedate = Convert.ToInt32(ds3.Tables[0].Rows[0]["dtduetill_date"]);

                string Startdt = Convert.ToString(ds3.Tables[0].Rows[0]["start_date"]);
                string Duedt = Convert.ToString(ds3.Tables[0].Rows[0]["duetill_date"]);

                if ((Convert.ToString(dsObj.Tables[0].Rows[0]["frequency"])) != "12")
                {
                    if ((DateTime.Now >= Convert.ToDateTime(Startdt)) && (DateTime.Now <= Convert.ToDateTime(Duedt)))
                    {
                        dsObj.Tables[0].Rows[0]["vchLateCharge"] = "No";
                    }
                    else if ((DateTime.Now < Convert.ToDateTime(Startdt)) && (DateTime.Now < Convert.ToDateTime(Duedt)))
                    {
                        dsObj.Tables[0].Rows[0]["vchLateCharge"] = "No";
                    }
                    else
                    {
                        dsObj.Tables[0].Rows[0]["vchLateCharge"] = "Yes";
                    }
                }
                else if ((Convert.ToString(dsObj.Tables[0].Rows[0]["frequency"])) == "12")
                {
                    if (Convert.ToDateTime(strFromDate).Month == DateTime.Now.Month)
                    {
                        if (Convert.ToInt32(startdate) <= DateTime.Now.Day && Convert.ToInt32(duedate) >= DateTime.Now.Day)
                        {
                            dsObj.Tables[0].Rows[0]["vchLateCharge"] = "No";
                        }
                        else
                        {
                            dsObj.Tables[0].Rows[0]["vchLateCharge"] = "Yes";
                        }
                    }
                    else if (Convert.ToDateTime(strFromDate).Month < DateTime.Now.Month && Convert.ToDateTime(strFromDate).Year <= DateTime.Now.Year)
                    {
                        dsObj.Tables[0].Rows[0]["vchLateCharge"] = "Yes";
                    }
                    else if (Convert.ToDateTime(strFromDate).Month > DateTime.Now.Month && Convert.ToDateTime(strFromDate).Year >= DateTime.Now.Year)
                    {
                        dsObj.Tables[0].Rows[0]["vchLateCharge"] = "No";
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }


        private DataTable MonthWiseCalculation(DataTable dt,string command)
        {
            try
            {
                DataTable dtCloned = dt.Clone();
                dtCloned.Columns[9].DataType = typeof(Int32);
                foreach (DataRow row in dt.Rows)
                {
                    dtCloned.ImportRow(row);
                }

                DataTable data;

                if (command == "MonthWiseTotalFee")
                { 
                    var result = (from row in dtCloned.AsEnumerable()
                                       group row by new
                                       {
                                           Month = row.Field<string>("month")                                         
                                       }
                                        into g
                                       select new
                                       {
                                           g.Key.Month,
                                           Sum = g.Sum(x => x.Field<Int32>("NetAmount"))
                                       }).ToArray();
                    data = LINQResultToDataTable(result);
                }
                else
                {
                    var result = (from row in dtCloned.AsEnumerable()
                                  group row by new
                                  {
                                      Month = row.Field<string>("month"),
                                      Status = row.Field<string>("Status")
                                  }
                                       into g
                                  select new
                                  {
                                      g.Key.Month,
                                      g.Key.Status,
                                      Sum = g.Sum(x => x.Field<Int32>("NetAmount"))
                                  }).ToArray();
                    data = LINQResultToDataTable(result);
                }

                

            return data;
           
            }
            catch (Exception ex)
             {
                throw ex;
            }
        }

        private DataTable MonthWiseFeeDetails(DataTable dt,string month,string  command,string Status)
        {
            try
            {
                DataTable data;
                if (command == "MonthFeeDetails")
                { 
                var Result = from MonthData in dt.AsEnumerable()
                                where MonthData.Field<string>("month") == month
                             select new
                                {
                                 //Fee Name, Month, Amount, Late Fee, Concession Amount, Net Amount
                                 intFeemaster_id = MonthData.Field<int>("intFeemaster_id"),
                                 FeeName = MonthData.Field<string>("Fee_Name"),
                                    Month = MonthData.Field<string>("month"),
                                 vchfee = MonthData.Field<string>("vchfee"),
                                 LateFee = MonthData.Field<string>("LateFee"),
                                    ConcessionAmt = MonthData.Field<string>("Concession"),
                                    NetAmt = MonthData.Field<int>("NetAmount")
                             };
                     data = LINQResultToDataTable(Result);
                }
                else
                {
                    var Result = from MonthData in dt.AsEnumerable()
                                 where MonthData.Field<string>("month") == month &&  MonthData.Field<string>("Status") == Status
                                 select new
                                 {
                                     //Fee Name, Month, Amount, Late Fee, Concession Amount, Net Amount
                                     intFeemaster_id = MonthData.Field<int>("intFeemaster_id"),
                                     FeeName = MonthData.Field<string>("Fee_Name"),
                                     Month = MonthData.Field<string>("month"),
                                     vchfee = MonthData.Field<string>("vchfee"),
                                     LateFee = MonthData.Field<string>("LateFee"),
                                     ConcessionAmt = MonthData.Field<string>("Concession"),
                                     NetAmt = MonthData.Field<int>("NetAmount")
                                 };
                     data = LINQResultToDataTable(Result);
                }

               

                return data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();


            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static DataSet sGetDataset(string sQuery)
        {
            DataSet Retds = new DataSet();
            try
            {
                string sConStr = getConnectionString();
                using (SqlConnection con = new SqlConnection(sConStr))
                {
                    SqlDataAdapter da = new SqlDataAdapter(sQuery, sConStr);
                    if (da != null)
                    {
                        da.Fill(Retds);
                    }
                    else
                    {
                        Retds = null;
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Retds = null;
                //MessageBox.Show(ex.ToString());
            }

            return Retds;
        }

        //Get Connection String
        public static string getConnectionString()
        {
            string retval = "";
            try
            {
                retval = System.Configuration.ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
            }
            catch (Exception Ex)
            {
                throw Ex;
                retval = "";
            }
            return retval;
        }

        public static string sExecuteScalar(string sQuery)
        {
            string retVal = "";
            try
            {
                string sConStr = getConnectionString();
                using (SqlConnection con = new SqlConnection(sConStr))
                {
                    con.Open();
                    SqlCommand cmdExe = new SqlCommand(sQuery, con);
                    retVal = Convert.ToString(cmdExe.ExecuteScalar());
                    con.Close();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
                retVal = null;
            }
            return retVal;
        }

        //Execute NonQuery
        public static int sExecuteQuery(string sQuery)
        {
            //int retVal = 0;
            int retVal;
            try
            {
                string sConStr = getConnectionString();
                using (SqlConnection con = new SqlConnection(sConStr))
                {
                    con.Open();
                    SqlCommand cmdExe = new SqlCommand(sQuery, con);
                    retVal = cmdExe.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
                retVal = -1;
            }
            return retVal;
        }

        public int InsertFeeRecords(string command ,FeeCollection feeCollection)
        {
            try
            {
                DataSet dataset= GetFeeMonthWise("PayFeeMonthWise", feeCollection);


                int sum = dataset.Tables["MonthFeeDetails"].AsEnumerable().Sum(row => row.Field<int>("NetAmt"));


                strQry = "usp_Addfee @command='AddFeeinMaster',@intStudent_id='" + feeCollection.intStudentID + "',@intStandard_id='" + feeCollection.intstandard_id + "',@intDivision_id='" + feeCollection.intDivision_id + "',@intRoll_no='" + feeCollection.intRoll_no + "'" +
                    ",@vchDiscount='0',@vchNet_amt='" + sum + "',@vchRemark='Paid From Mobile APP',@ModeOfPayment='Mobile',@vchCheque_no='',@vchBank_name='',@intSchool_id='" + feeCollection.intSchool_id + "'" +
                        ",@intAcademic_id='" + feeCollection.intAcademic_id + "',@intInserted_by='" + feeCollection.intStudentID + "',@vchInserted_ip='" + feeCollection.vchInserted_ip + "'";
                string ExcecuteQur = sExecuteScalar(strQry);
                

                int ExeQuery = 0;
                for (int i = 0; i < (dataset.Tables[0].Rows.Count); i++)
                {
                    strQry = "usp_Addfee @command='InsertFeeDetails',@intStudFee_id='" + ExcecuteQur + "',@intFeemaster_id='" + dataset.Tables[0].Rows[i]["intFeemaster_id"] + "',@intNoOf_month=''" +
                       ",@intMonth_id='" + dataset.Tables[0].Rows[i]["month"] + "',@vchAmount='" + dataset.Tables[0].Rows[i]["vchfee"] + "',@vchLate_charges='" + dataset.Tables[0].Rows[i]["LateFee"] + "'" +
                       ",@intConcession_amt='" + dataset.Tables[0].Rows[i]["ConcessionAmt"] + "',@vchNetDetail_amt='" + dataset.Tables[0].Rows[i]["NetAmt"] + "',@intSchool_id='" + feeCollection.intSchool_id + "',@intAcademic_id='" + feeCollection.intAcademic_id + "'";
                    ExeQuery = sExecuteQuery(strQry);
                }
                return ExeQuery;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataSet UpdatetransactionId(string command, FeeCollection feeCollection)
        {
            try
            {
                strQry = "usp_Addfee @command='UpdateTransactionID',@TransationID='" + feeCollection.trasactionID + "',@intStudent_id='" + feeCollection.intStudentID + "',@intStudFee_id='" + feeCollection.intStudFee_id + "'";
                DataSet ds1 = sGetDataset(strQry);
                ds1.Tables[0].TableName = "PayFeeDetails";
                return ds1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}