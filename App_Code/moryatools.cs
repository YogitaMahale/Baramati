using System;
using System.Web;
using System.Net;
using System.Linq;
using System.Text;
using System.Data;
using BusinessLayer;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System.Globalization;

public class moryatools : System.Web.Services.WebService
{
    public moryatools()
    { }

    #region "User Regsitration"

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void UserLogin(string mobile, string password)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtUser = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "userdealer_Login";
            cmd.Parameters.AddWithValue("@mobileno", mobile);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dtUser);
            con.Close();
            if (dtUser != null)
            {
                if (dtUser.Rows.Count > 0)
                {
                    bool Status = Convert.ToBoolean(dtUser.Rows[0]["isactive"]);
                    if (Status)
                    {
                        //UpdateLatitudeLongitudeUsingUserId(Convert.ToInt64(dtUser.Rows[0]["uid"]), Convert.ToString(dtUser.Rows[0]["type"]), Latitude, Longitude);
                        string output = DataTableToJSONWithJavaScriptSerializer(dtUser);
                        finalResult = "{\"success\" : 1, \"message\" : \"Login Successfully\", \"data\" :" + output + "}";
                    }
                    else
                    {
                        finalResult = "{\"success\" : 0, \"message\" : \"Your Account Under Admin Observation.Please wait for admin confirmation\", \"data\" : \"\"}";
                    }
                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"Incorrect User Name & Password\", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Incorrect User Name & Password\", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }




    private void UpdateLatitudeLongitudeUsingUserId(Int64 UserId, string UserType, Decimal Latitude, Decimal Longitude)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            string query = string.Empty;
            if (UserType.ToLower() == "dealer".ToLower())
            {
                query = "update dealermaster SET latitude=" + Latitude + " , longitude=" + Longitude + " Where did=" + UserId;
            }
            else if (UserType.ToLower() == "user".ToLower())
            {
                query = "update userregistration SET latitude=" + Latitude + " , longitude=" + Longitude + " Where uid=" + UserId;
            }
            if (!string.IsNullOrEmpty(query))
            {
                con.Open();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            con.Close();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ChangePassword(string UserId, string UserType, string NewPassword)
    {
        string finalResult = string.Empty;
        Int64 Result = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "userdelaer_changepassword";
        cmd.Parameters.AddWithValue("@userid", UserId);
        cmd.Parameters.AddWithValue("@usertype", UserType);
        cmd.Parameters.AddWithValue("@password", NewPassword);
        con.Open();
        try
        {
            Result = Convert.ToInt64(cmd.ExecuteNonQuery());
            if (Result > 0)
            {
                finalResult = "{\"success\" : 1, \"message\" : \"Password Sucessfully Change \", \"data\" : \"\"}";
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" Erro in Password Change \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \" Erro in Password Change \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }



         
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ForgetPassword(string mobile)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtUser = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "userdealer_ForgetPassword";
            cmd.Parameters.AddWithValue("@mobileno", mobile);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dtUser);
            con.Close();
            if (dtUser != null)
            {
                if (dtUser.Rows.Count > 0)
                {
                    bool send = SendMail(dtUser.Rows[0]["name"].ToString(), dtUser.Rows[0]["email"].ToString(), dtUser.Rows[0]["password"].ToString());
                    if (send)
                    {
                        finalResult = "{\"success\" : 1, \"message\" : \"Password Send Successfully\",  \"data\" : \"\"}";
                    }
                    else
                    {
                        finalResult = "{\"success\" : 0, \"message\" : \"Incorrect Mobile No\", \"data\" : \"\"}";
                    }
                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"Incorrect Mobile No\", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Incorrect Mobile No \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    private bool SendMail(string Name, string Email, string Password)
    {
        bool send = false;
        MailMessage mail = new MailMessage();
        mail.To.Add(Email);
        mail.From = new MailAddress("demo@moryatools.com", "Morya Tools App");
        mail.Subject = "Morya App Forgot Password Details";
        StringBuilder strBul = new StringBuilder("<div>");
        strBul = strBul.Append("<div>Dear " + Name + ",</div>");
        strBul = strBul.Append("<br />");
        strBul = strBul.Append("<div>Your Account Details</div>");
        strBul = strBul.Append("<br />");
        strBul = strBul.Append("<div>Email -: &nbsp; " + Email + "</div>");
        strBul = strBul.Append("<br />");
        strBul = strBul.Append("<div>Password -: &nbsp; " + Password + "</div>");
        strBul = strBul.Append("<br />");
        strBul = strBul.Append("<div>Thank you,</div>");
        strBul = strBul.Append("<div>Morya App - Support Team.</div>");
        strBul = strBul.Append("</div>");
        mail.Body = strBul.ToString();
        mail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "103.250.184.62";
        smtp.Port = 25;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new System.Net.NetworkCredential("demo@moryatools.com", "vsys@2017");
        try
        {
            smtp.Send(mail);
            send = true;
        }
        catch (Exception ex)
        {
            send = false;
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        return send;
    }

    [WebMethod]
    public void UserDeviceIdUpdate(Int64 UserId, string UserType, string DeviceId)
    {
        Int64 result = 0;
        string finalResult = string.Empty;
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "userdealerlogin_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            cmd.Parameters.AddWithValue("@uid", UserId);
            cmd.Parameters.AddWithValue("@utype", UserType);
            if (!string.IsNullOrEmpty(DeviceId))
            {
                cmd.Parameters.AddWithValue("@deviceid", DeviceId);
            }
            else
            {
                cmd.Parameters.AddWithValue("@deviceid", string.Empty);
            }

            ConnectionString.Open();
            result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                finalResult = "{\"success\" : 1, \"message\" : \"Device Id Updated successfully\", \"data\" : \"\"}";
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Device Id Not Updated \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"Device Id Not Updated \", \"data\" : \"\"}";
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    public void UserLoginStatus(Int64 UserId, string UserType, string Status)
    {
        Int64 result = 0;
        string finalResult = string.Empty;
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "userdealerlogin_status";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            cmd.Parameters.AddWithValue("@uid", UserId);
            cmd.Parameters.AddWithValue("@utype", UserType);
            if (Status == "0")
            {
                cmd.Parameters.AddWithValue("@status", false);
            }
            else
            {
                cmd.Parameters.AddWithValue("@status", true);
            }
            ConnectionString.Open();
            result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                finalResult = "{\"success\" : 1, \"message\" : \"Login Status Updated successfully\", \"data\" : \"\"}";
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Login Status Not Updated \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"Login Status Not Updated \", \"data\" : \"\"}";
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void UserRegistration(Int64 UserId, string FirstName, string MiddleName, string LastName, string Email, string Mobile, string Password, string Address, string DOB, bool IsGuest, string RegDate)
    {
        string finalResult = string.Empty;
        if (CheckMobileNumberExitOrNot(Mobile))
        {
            finalResult = "{\"success\" : 0, \"message\" : \"Mobile Number Already Exits\", \"data\" : \"\"}";
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Flush();
            Context.Response.Write(finalResult);
            Context.Response.End();
        }
        else
        {
            if (CheckMobileNumberDealerExitOrNot(Mobile))
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Mobile Number Already Exits\", \"data\" : \"\"}";
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(finalResult);
                Context.Response.End();
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                try
                {
                    userregistration objuserregistration = new userregistration();
                    objuserregistration.fname = FirstName;
                    objuserregistration.mname = MiddleName;
                    objuserregistration.lname = LastName;
                    objuserregistration.email = Email;
                    objuserregistration.phone = Mobile;
                    objuserregistration.password = Password;
                    objuserregistration.address1 = Address;
                    objuserregistration.address2 = string.Empty;
                    //objuserregistration.dob = DOB;
                    objuserregistration.dob = string.Empty;
                    objuserregistration.isguest = IsGuest;
                    objuserregistration.registrationdate = RegDate;
                    objuserregistration.uguid = Guid.NewGuid().ToString();
                    objuserregistration.isactive = true;
                    objuserregistration.isdelete = false;

                    Int64 Result = (new Cls_userregistration_b().Insert(objuserregistration));
                    if (Result > 0)
                    {
                        DataTable dtUser = new DataTable();
                        dtUser.Columns.Add("UserId", typeof(string));
                        dtUser.Columns.Add("UserType", typeof(string));
                        dtUser.Rows.Add(Result.ToString(), "U");
                        string output = DataTableToJSONWithJavaScriptSerializer(dtUser);
                        finalResult = "{\"success\" : 1, \"message\" : \"User Registration inserted successfully\", \"data\" : " + output + "}";
                    }
                    else
                    {
                        finalResult = "{\"success\" : 1, \"message\" : \"User Registration not inserted successfully\", \"data\" : \"\"}";
                    }
                }
                catch (Exception ex)
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
                }
                finally
                {
                    con.Close();
                }
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(finalResult);
                Context.Response.End();
            }
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void UserRegistrationGuest(string Name, string Email, string Mobile, string Address)
    {
        string finalResult = string.Empty;
        if (CheckMobileNumberExitOrNot(Mobile))
        {
            Int64 Result = CheckMobileNumberExitOrNotGuest(Mobile);
            if (Result > 0)
            {
                DataTable dtUser = new DataTable();
                dtUser.Columns.Add("UserId", typeof(string));
                dtUser.Columns.Add("UserType", typeof(string));
                dtUser.Rows.Add(Result.ToString(), "U");
                string output = DataTableToJSONWithJavaScriptSerializer(dtUser);
                finalResult = "{\"success\" : 1, \"message\" : \"User Information \", \"data\" : " + output + "}";
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(finalResult);
                Context.Response.End();
            }
            else
            {
                finalResult = "{\"success\" : 1, \"message\" : \"User Creation Fail \", \"data\" }";
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(finalResult);
                Context.Response.End();
            }
        }
        else
        {
            if (CheckMobileNumberDealerExitOrNot(Mobile))
            {
                Int64 Result = CheckMobileNumberDealerExitOrNotGuest(Mobile);
                if (Result > 0)
                {
                    DataTable dtUser = new DataTable();
                    dtUser.Columns.Add("DealerId", typeof(string));
                    dtUser.Columns.Add("UserType", typeof(string));
                    dtUser.Rows.Add(Result.ToString(), "D");
                    string output = DataTableToJSONWithJavaScriptSerializer(dtUser);
                    finalResult = "{\"success\" : 1, \"message\" : \"Dearler Information \", \"data\" : " + output + "}";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Flush();
                    Context.Response.Write(finalResult);
                    Context.Response.End();
                }
                else
                {
                    finalResult = "{\"success\" : 1, \"message\" : \"Dearler Creation Fail \", \"data\" }";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Flush();
                    Context.Response.Write(finalResult);
                    Context.Response.End();
                }
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                try
                {
                    userregistration objuserregistration = new userregistration();
                    objuserregistration.fname = Name;
                    objuserregistration.mname = string.Empty;
                    objuserregistration.lname = string.Empty;
                    objuserregistration.email = Email;
                    objuserregistration.phone = Mobile;
                    objuserregistration.password = string.Empty;
                    objuserregistration.address1 = Address;
                    objuserregistration.address2 = string.Empty;
                    objuserregistration.dob = string.Empty;
                    objuserregistration.isguest = false;
                    objuserregistration.registrationdate = DateTime.Now.ToShortDateString();
                    objuserregistration.uguid = Guid.NewGuid().ToString();
                    objuserregistration.isactive = true;
                    objuserregistration.isdelete = false;

                    Int64 Result = (new Cls_userregistration_b().Insert(objuserregistration));
                    if (Result > 0)
                    {
                        DataTable dtUser = new DataTable();
                        dtUser.Columns.Add("UserId", typeof(string));
                        dtUser.Columns.Add("UserType", typeof(string));
                        dtUser.Rows.Add(Result.ToString(), "U");
                        string output = DataTableToJSONWithJavaScriptSerializer(dtUser);
                        finalResult = "{\"success\" : 1, \"message\" : \"User Registration inserted successfully\", \"data\" : " + output + "}";
                    }
                    else
                    {
                        finalResult = "{\"success\" : 1, \"message\" : \"User Registration not inserted successfully\", \"data\" : \"\"}";
                    }
                }
                catch (Exception ex)
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
                }
                finally
                {
                    con.Close();
                }
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(finalResult);
                Context.Response.End();
            }

        }
    }

    public bool CheckMobileNumberExitOrNot(string MobileNumber)
    {
        bool IsExit = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtMobile = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "userregistration_CheckMobileNumberExit";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mobileno", MobileNumber);
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dtMobile);
            con.Close();
            if (dtMobile != null)
            {
                if (dtMobile.Rows.Count > 0)
                {
                    IsExit = true;
                }
                else
                {
                    IsExit = false;
                }
            }
            else
            {
                IsExit = false;
            }
        }
        catch (Exception)
        {
            IsExit = false;
        }
        finally
        {
            con.Close();
        }
        return IsExit;
    }

    public Int64 CheckMobileNumberExitOrNotGuest(string MobileNumber)
    {
        Int64 IsExit = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtMobile = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "userregistration_CheckMobileNumberExit";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mobileno", MobileNumber);
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dtMobile);
            con.Close();
            if (dtMobile != null)
            {
                if (dtMobile.Rows.Count > 0)
                {
                    IsExit = Convert.ToInt64(dtMobile.Rows[0]["uid"]);
                }
                else
                {
                    IsExit = 0;
                }
            }
            else
            {
                IsExit = 0;
            }
        }
        catch (Exception)
        {
            IsExit = 0;
        }
        finally
        {
            con.Close();
        }
        return IsExit;
    }

    public bool CheckMobileNumberDealerExitOrNot(string MobileNumber)
    {
        bool IsExit = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtMobile = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealer_CheckMobileNumberExit";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mobileno", MobileNumber);
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dtMobile);
            con.Close();
            if (dtMobile != null)
            {
                if (dtMobile.Rows.Count > 0)
                {
                    IsExit = true;
                }
                else
                {
                    IsExit = false;
                }
            }
            else
            {
                IsExit = false;
            }
        }
        catch (Exception)
        {
            IsExit = false;
        }
        finally
        {
            con.Close();
        }
        return IsExit;
    }

    public Int64 CheckMobileNumberDealerExitOrNotGuest(string MobileNumber)
    {
        Int64 IsExit = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtMobile = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealer_CheckMobileNumberExit";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mobileno", MobileNumber);
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dtMobile);
            con.Close();
            if (dtMobile != null)
            {
                if (dtMobile.Rows.Count > 0)
                {
                    IsExit = Convert.ToInt64(dtMobile.Rows[0]["did"]);
                }
                else
                {
                    IsExit = 0;
                }
            }
            else
            {
                IsExit = 0;
            }
        }
        catch (Exception)
        {
            IsExit = 0;
        }
        finally
        {
            con.Close();
        }
        return IsExit;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void UserProfileUpdate(Int64 UserId, string FirstName, string MiddleName, string LastName, string Email, string Address, string DOB)
    {
        string finalResult = string.Empty;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            userregistration objuserregistration = new userregistration();
            objuserregistration.fname = FirstName;
            objuserregistration.mname = MiddleName;
            objuserregistration.lname = LastName;
            objuserregistration.email = Email;
            objuserregistration.address1 = Address;
            objuserregistration.address2 = string.Empty;
            objuserregistration.dob = DOB;
            objuserregistration.uid = UserId;
            objuserregistration.isactive = true;
            objuserregistration.isdelete = false;

            //objuserregistration.phone = Mobile;
            //objuserregistration.password = Password;
            //objuserregistration.isguest = IsGuest;
            //objuserregistration.registrationdate = RegDate;
            //objuserregistration.uguid = Guid.NewGuid().ToString();
            Int64 Result = (new Cls_userregistration_b().Update(objuserregistration));
            if (Result > 0)
            {
                finalResult = "{\"success\" : 1, \"message\" : \"User Updated Successfully\", \"data\" : \"\"}";
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"User Registration Not Updated \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    #endregion "User Regsitration"

    #region "Category Section"

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SelectAllBank()
    {
        string finalResult = string.Empty;
        try
        {
            DataTable dtBanks = (new Cls_bankmaster_b().SelectAll());
            if (dtBanks != null)
            {
                if (dtBanks.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtBanks);
                    finalResult = "{\"success\" : 1, \"message\" : \" Bank All Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Bank \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Bank \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SelectAllCategory_byMainCategoryId(string mainCategoryId)
    {
        string finalResult = string.Empty;
        try
        {
            SqlConnection ConnectionString=new SqlConnection (ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString .ToString());
           // DataTable dtCategory = (new Cls_category_b().category_WSSelectAll());
            DataTable dtCategory=new DataTable ();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "getSubCategory_fromMaincategory";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@id", Convert.ToInt64(mainCategoryId));
                ConnectionString.Open();
             SqlDataAdapter  da = new SqlDataAdapter(cmd);
                da.Fill(dtCategory);

            
            if (dtCategory != null)
            {
                if (dtCategory.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtCategory);
                    finalResult = "{\"success\" : 1, \"message\" : \" Category All Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Category \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SelectAllMainCategory()
    {
        string finalResult = string.Empty;
        try
        {
            DataTable dtCategory = (new Cls_maincategory_b().SelectAll());
            if (dtCategory != null)
            {
                if (dtCategory.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtCategory);
                    finalResult = "{\"success\" : 1, \"message\" : \" Main Category All Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Main Category \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Main Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SelectAllBrand()
    {
        string finalResult = string.Empty;
        try
        {
            DataTable dtCategory = (new Cls_brand_b().SelectAll());
            if (dtCategory != null)
            {
                if (dtCategory.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtCategory);
                    finalResult = "{\"success\" : 1, \"message\" : \" Brand All Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"Brand Category \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" Brand Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SelectCategoryDetailsUsingCategoryId(Int64 CategoryId)
    {
        string finalResult = string.Empty;
        try
        {
            DataTable dtCategory = (new Cls_category_b().category_WSSelectById(CategoryId));
            if (dtCategory != null)
            {
                if (dtCategory.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtCategory);
                    finalResult = "{\"success\" : 1, \"message\" : \" Category All Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Category \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    #endregion "Category Section"

    #region "Product Section"

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SelectProductsUsingCategoryId(Int64 CategoryId)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtProducts = (new Cls_product_b().Product_WSSelectAllProductUsingCategoryId(CategoryId));
            if (dtProducts != null)
            {
                if (dtProducts.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtProducts);
                    finalResult = "{\"success\" : 1, \"message\" : \" Category Wise Product Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Products Exits This Category \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Products Exits This Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Products Exits This Category \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SelectProductsUsingCategoryIdandBrandId(Int64 CategoryId,Int64 brandId)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtProducts = (new Cls_product_b().Product_WSSelectAllProductUsingCategoryIdandBrandId(CategoryId, brandId));
            if (dtProducts != null)
            {
                if (dtProducts.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtProducts);
                    finalResult = "{\"success\" : 1, \"message\" : \" Category Wise Product Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Products Exits This Category \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Products Exits This Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Products Exits This Category \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SearchProductUsingProductName(string Productname)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtProducts = (new Cls_product_b().SearchProductUsingProductName(Productname));
            if (dtProducts != null)
            {
                if (dtProducts.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtProducts);
                    finalResult = "{\"success\" : 1, \"message\" : \" Product Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Products Data Not Exits  \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Products Exits This Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Products Exits This Category \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SelectProductDetailsUsingProductId(Int64 ProductId)
    {
        string finalResult = string.Empty;
        string multipleImage = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtProduct = (new Cls_product_b().SelectProductDetailsUsingProductId(ProductId));
            if (dtProduct != null)
            {
                if (dtProduct.Rows.Count > 0)
                {
                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("pid", typeof(Int64));
                    dt2.Columns.Add("cid", typeof(Int64));
                    dt2.Columns.Add("Categoryname", typeof(string));
                    dt2.Columns.Add("productname", typeof(string));
                    dt2.Columns.Add("imagename", typeof(string));
                    dt2.Columns.Add("sku", typeof(string));
                    dt2.Columns.Add("customerprice", typeof(string));
                    dt2.Columns.Add("dealerprice", typeof(string));
                    dt2.Columns.Add("discountprice", typeof(string));
                    dt2.Columns.Add("superwholesaleprice", typeof(string));
                    dt2.Columns.Add("wholesaleprice", typeof(string));
                    dt2.Columns.Add("quantites", typeof(string));
                    dt2.Columns.Add("isstock", typeof(string));
                    dt2.Columns.Add("shortdescp", typeof(string));
                    dt2.Columns.Add("longdescp", typeof(string));
                    dt2.Columns.Add("imagevideopath", typeof(string));
                    dt2.Columns.Add("gst", typeof(string));
                    dt2.Columns.Add("video1", typeof(string));
                    dt2.Columns.Add("video2", typeof(string));
                    dt2.Columns.Add("video3", typeof(string));
                    dt2.Columns.Add("video4", typeof(string));
                    dt2.Columns.Add("video_name_1", typeof(string));
                    dt2.Columns.Add("video_name_2", typeof(string));
                    dt2.Columns.Add("video_name_3", typeof(string));
                    dt2.Columns.Add("video_name_4", typeof(string));

                    DataRow dr;
                    DataTable dt3 = new DataTable();
                    for (int i = 0; i < dtProduct.Rows.Count; i++)
                    {
                        dt3 = new DataTable();
                        SqlCommand cmd = new SqlCommand("SELECT 'http://moryaapp.moryatools.com/uploads/product/front/' +  imagevideopath as imagevideopath FROM productimagesvideos Where isdelete=0 and pid=" + ProductId, con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        cmd.Connection = con;
                        da.SelectCommand = cmd;
                        da.Fill(dt3);
                        if (dt3 != null)
                        {
                            if (dt3.Rows.Count > 0)
                            {
                                dr = dt2.NewRow();
                                dr["pid"] = Convert.ToInt32(dtProduct.Rows[i]["pid"]);
                                dr["cid"] = Convert.ToString(dtProduct.Rows[i]["cid"]);
                                dr["Categoryname"] = Convert.ToString(dtProduct.Rows[i]["Categoryname"]);
                                dr["productname"] = Convert.ToString(dtProduct.Rows[i]["productname"]);
                                dr["imagename"] = Convert.ToString(dtProduct.Rows[i]["imagename"]);
                                dr["sku"] = Convert.ToString(dtProduct.Rows[i]["sku"]);
                                dr["customerprice"] = Convert.ToString(dtProduct.Rows[i]["customerprice"]);
                                dr["dealerprice"] = Convert.ToString(dtProduct.Rows[i]["dealerprice"]);
                                dr["discountprice"] = Convert.ToString(dtProduct.Rows[i]["discountprice"]);
                                dr["wholesaleprice"] = Convert.ToString(dtProduct.Rows[i]["wholesaleprice"]);
                                dr["superwholesaleprice"] = Convert.ToString(dtProduct.Rows[i]["superwholesaleprice"]);
                                dr["quantites"] = Convert.ToString(dtProduct.Rows[i]["quantites"]);
                                dr["isstock"] = Convert.ToString(dtProduct.Rows[i]["isstock"]);
                                dr["shortdescp"] = Convert.ToString(dtProduct.Rows[i]["shortdescp"]);
                                dr["longdescp"] = Convert.ToString(dtProduct.Rows[i]["longdescp"]);
                                dr["gst"] = Convert.ToString(dtProduct.Rows[i]["gst"]);
                                dr["video1"] = Convert.ToString(dtProduct.Rows[i]["video1"]);
                                dr["video2"] = Convert.ToString(dtProduct.Rows[i]["video2"]);
                                dr["video3"] = Convert.ToString(dtProduct.Rows[i]["video3"]);
                                dr["video4"] = Convert.ToString(dtProduct.Rows[i]["video4"]);
                                dr["video_name_1"] = Convert.ToString(dtProduct.Rows[i]["video_name_1"]);
                                dr["video_name_2"] = Convert.ToString(dtProduct.Rows[i]["video_name_2"]);
                                dr["video_name_3"] = Convert.ToString(dtProduct.Rows[i]["video_name_3"]);
                                dr["video_name_4"] = Convert.ToString(dtProduct.Rows[i]["video_name_4"]);

                                for (int j = 0; j < dt3.Rows.Count; j++)
                                {
                                    multipleImage += "," + Convert.ToString(dt3.Rows[j]["imagevideopath"]);
                                }
                                dr["imagevideopath"] = multipleImage.TrimStart(',');
                                dt2.Rows.Add(dr);
                            }
                            else
                            {
                                dr = dt2.NewRow();
                                dr["pid"] = Convert.ToInt32(dtProduct.Rows[i]["pid"]);
                                dr["cid"] = Convert.ToString(dtProduct.Rows[i]["cid"]);
                                dr["Categoryname"] = Convert.ToString(dtProduct.Rows[i]["Categoryname"]);
                                dr["productname"] = Convert.ToString(dtProduct.Rows[i]["productname"]);
                                dr["imagename"] = Convert.ToString(dtProduct.Rows[i]["imagename"]);
                                dr["sku"] = Convert.ToString(dtProduct.Rows[i]["sku"]);
                                dr["customerprice"] = Convert.ToString(dtProduct.Rows[i]["customerprice"]);
                                dr["dealerprice"] = Convert.ToString(dtProduct.Rows[i]["dealerprice"]);
                                dr["wholesaleprice"] = Convert.ToString(dtProduct.Rows[i]["wholesaleprice"]);
                                dr["superwholesaleprice"] = Convert.ToString(dtProduct.Rows[i]["superwholesaleprice"]);
                                dr["discountprice"] = Convert.ToString(dtProduct.Rows[i]["discountprice"]);
                                dr["quantites"] = Convert.ToString(dtProduct.Rows[i]["quantites"]);
                                dr["isstock"] = Convert.ToString(dtProduct.Rows[i]["isstock"]);
                                dr["shortdescp"] = Convert.ToString(dtProduct.Rows[i]["shortdescp"]);
                                dr["longdescp"] = Convert.ToString(dtProduct.Rows[i]["longdescp"]);
                                dr["gst"] = Convert.ToString(dtProduct.Rows[i]["gst"]);
                                dr["video1"] = Convert.ToString(dtProduct.Rows[i]["video1"]);
                                dr["video2"] = Convert.ToString(dtProduct.Rows[i]["video2"]);
                                dr["video3"] = Convert.ToString(dtProduct.Rows[i]["video3"]);
                                dr["video4"] = Convert.ToString(dtProduct.Rows[i]["video4"]);
                                dr["video_name_1"] = Convert.ToString(dtProduct.Rows[i]["video_name_1"]);
                                dr["video_name_2"] = Convert.ToString(dtProduct.Rows[i]["video_name_2"]);
                                dr["video_name_3"] = Convert.ToString(dtProduct.Rows[i]["video_name_3"]);
                                dr["video_name_4"] = Convert.ToString(dtProduct.Rows[i]["video_name_4"]);
                                dt2.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            dr = dt2.NewRow();
                            dr["pid"] = Convert.ToInt32(dtProduct.Rows[i]["pid"]);
                            dr["cid"] = Convert.ToString(dtProduct.Rows[i]["cid"]);
                            dr["Categoryname"] = Convert.ToString(dtProduct.Rows[i]["Categoryname"]);
                            dr["productname"] = Convert.ToString(dtProduct.Rows[i]["productname"]);
                            dr["imagename"] = Convert.ToString(dtProduct.Rows[i]["imagename"]);
                            dr["sku"] = Convert.ToString(dtProduct.Rows[i]["sku"]);
                            dr["customerprice"] = Convert.ToString(dtProduct.Rows[i]["customerprice"]);
                            dr["dealerprice"] = Convert.ToString(dtProduct.Rows[i]["dealerprice"]);
                            dr["wholesaleprice"] = Convert.ToString(dtProduct.Rows[i]["wholesaleprice"]);
                            dr["superwholesaleprice"] = Convert.ToString(dtProduct.Rows[i]["superwholesaleprice"]);
                            dr["discountprice"] = Convert.ToString(dtProduct.Rows[i]["discountprice"]);
                            dr["quantites"] = Convert.ToString(dtProduct.Rows[i]["quantites"]);
                            dr["isstock"] = Convert.ToString(dtProduct.Rows[i]["isstock"]);
                            dr["shortdescp"] = Convert.ToString(dtProduct.Rows[i]["shortdescp"]);
                            dr["longdescp"] = Convert.ToString(dtProduct.Rows[i]["longdescp"]);
                            dr["gst"] = Convert.ToString(dtProduct.Rows[i]["gst"]);
                            dr["video1"] = Convert.ToString(dtProduct.Rows[i]["video1"]);
                            dr["video2"] = Convert.ToString(dtProduct.Rows[i]["video2"]);
                            dr["video3"] = Convert.ToString(dtProduct.Rows[i]["video3"]);
                            dr["video4"] = Convert.ToString(dtProduct.Rows[i]["video4"]);
                            dr["video_name_1"] = Convert.ToString(dtProduct.Rows[i]["video_name_1"]);
                            dr["video_name_2"] = Convert.ToString(dtProduct.Rows[i]["video_name_2"]);
                            dr["video_name_3"] = Convert.ToString(dtProduct.Rows[i]["video_name_3"]);
                            dr["video_name_4"] = Convert.ToString(dtProduct.Rows[i]["video_name_4"]);
                            dt2.Rows.Add(dr);
                        }
                    }
                    if (dt2 != null)
                    {
                        if (dt2.Rows.Count > 0)
                        {
                            string output = DataTableToJSONWithJavaScriptSerializer(dt2);
                            finalResult = "{\"success\" : 1, \"message\" : \" Product Details\", \"data\" :" + output + "}";
                        }
                        else
                        {
                            finalResult = "{\"success\" : 0, \"message\" : \"No Product Details \", \"data\" : \"\"}";
                        }
                    }
                    else
                    {
                        finalResult = "{\"success\" : 0, \"message\" : \"No Product Details \", \"data\" : \"\"}";
                    }
                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Product Details \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    #endregion "Product Section"

    #region "Products Update"

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SelectAllNewProductArrival()
    {
        string finalResult = string.Empty;
        try
        {
            DataTable dtProductOffer = (new Cls_newsupdate_b().newsupdate_WSSelectAll());
            if (dtProductOffer != null)
            {
                if (dtProductOffer.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtProductOffer);
                    finalResult = "{\"success\" : 1, \"message\" : \" Product Update  Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Product Update Found \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Product Update Found \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    #endregion "Products Update"

    #region "Offer's"

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SelectAllOffers()
    {
        string finalResult = string.Empty;
        try
        {
            DataTable dtOffers = (new Cls_offers_b().offers_WSSelectAll());
            if (dtOffers != null)
            {
                if (dtOffers.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtOffers);
                    finalResult = "{\"success\" : 1, \"message\" : \" Offers All Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Offers Found \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Offers Found \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    #endregion "Offer's"

    #region "FeebBack"

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Insert_FeedBack(string Name, string Phone, string FeedBack)
    {
        string finalResult = string.Empty;
        try
        {
            feedback objfeedback = new feedback();
            objfeedback.name = Name;
            objfeedback.phone = Phone;
            objfeedback.feedbackmessage = FeedBack;
            objfeedback.feedbackdt = DateTime.Now;

            Int64 Result = (new Cls_feedback_b().Insert(objfeedback));
            if (Result > 0)
            {
                finalResult = "{\"success\" : 1, \"message\" : \"Feedback inserted successfully\", \"data\" : \"\"}";
            }
            else
            {
                finalResult = "{\"success\" : 1, \"message\" : \"Feedback not inserted successfully\", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    #endregion "FeebBack"

    #region "Order Section"

    //[WebMethod]
    //public void Order_InsertOrderandOrderProduct(string OrderProducts_JSONString, string UserId, string ProductQuantites, string BillPaidorNot, string OrderAmount, string Discount, string Tax, string TotalAmount, string OrderDate, string UserType)
    //{
    //    string finalResult = string.Empty;
    //    orders objorders = new orders();
    //    if (UserId == string.Empty)
    //    {
    //        objorders.uid = 0;
    //    }
    //    else
    //    {
    //        objorders.uid = Convert.ToInt64(UserId);
    //    }
    //    if (ProductQuantites == string.Empty)
    //    {
    //        objorders.productquantites = 0;
    //    }
    //    else
    //    {
    //        objorders.productquantites = Convert.ToInt32(ProductQuantites);
    //    }
    //    if (BillPaidorNot == string.Empty)
    //    {
    //        objorders.billpaidornot = false;
    //    }
    //    else
    //    {
    //        objorders.billpaidornot = false;
    //    }
    //    if (OrderAmount == string.Empty)
    //    {
    //        objorders.amount = 0;
    //    }
    //    else
    //    {
    //        objorders.amount = Convert.ToDecimal(OrderAmount);
    //    }
    //    if (Discount == string.Empty)
    //    {
    //        objorders.discount = 0;
    //    }
    //    else
    //    {
    //        objorders.discount = Convert.ToDecimal(Discount);
    //    }
    //    if (Tax == string.Empty)
    //    {
    //        objorders.tax = 0;
    //    }
    //    else
    //    {
    //        objorders.tax = Convert.ToDecimal(Tax);
    //    }
    //    if (TotalAmount == string.Empty)
    //    {
    //        objorders.totalamount = 0;
    //    }
    //    else
    //    {
    //        objorders.totalamount = Convert.ToDecimal(TotalAmount);
    //    }
    //    if (OrderDate == string.Empty)
    //    {
    //        objorders.orderdate = DateTime.Now;
    //    }
    //    else
    //    {
    //        objorders.orderdate = DateTime.Now;
    //    }
    //    if (UserType == string.Empty)
    //    {
    //        objorders.usertype = "U";
    //    }
    //    else
    //    {
    //        objorders.usertype = UserType;
    //    }

    //    Int64 OrderProductAdd = 0;
    //    Int64 OrderId = 0;
    //    var dtOrderProducts = JsonConvert.DeserializeObject<DataTable>(OrderProducts_JSONString);
    //    if (dtOrderProducts != null)
    //    {
    //        if (dtOrderProducts.Rows.Count > 0)
    //        {
    //            OrderId = (new Cls_orders_b().Insert(objorders));
    //            if (OrderId > 0)
    //            {
    //                for (int i = 0; i < dtOrderProducts.Rows.Count; i++)
    //                {
    //                    OrderProductAdd = 0;
    //                    orderproducts objorderproducts = new orderproducts();
    //                    objorderproducts.oid = OrderId;
    //                    objorderproducts.uid = Convert.ToInt64(dtOrderProducts.Rows[i]["userid"]);
    //                    objorderproducts.pid = Convert.ToInt64(dtOrderProducts.Rows[i]["productid"]);
    //                    objorderproducts.productprice = Convert.ToDecimal(dtOrderProducts.Rows[i]["productprice"]);
    //                    objorderproducts.discount = Convert.ToDecimal(dtOrderProducts.Rows[i]["discount"]);
    //                    objorderproducts.productafterdiscountprice = Convert.ToDecimal(dtOrderProducts.Rows[i]["productafterdiscountprice"]);
    //                    objorderproducts.quantites = Convert.ToInt32(dtOrderProducts.Rows[i]["quantites"]);
    //                    objorderproducts.gst = Convert.ToDecimal(dtOrderProducts.Rows[i]["gst"]);
    //                    OrderProductAdd = (new Cls_orderproducts_b().Insert(objorderproducts));
    //                    #region " Stock Update "
    //                    Product_StockUpdate(objorderproducts.pid, objorderproducts.quantites);
    //                    #endregion " Stock Update "

    //                    //[{"userid":"15","productid":"783","productprice":"500","discount":"10","quantites":10,"productafterdiscountprice":"450","gst":"12"}
    //                    //,{"userid":"15","productid":"10783","productprice":"500","discount":"10","quantites":10,"productafterdiscountprice":"450","gst":"12"}]


    //                }
    //            }
    //        }
    //    }

    //    if ((OrderId > 0) && (OrderProductAdd > 0))
    //    {
    //        SendOrderMail(OrderId);
    //        finalResult = "{\"success\" : 1, \"message\" : \"Order Created Sucessfully\", \"data\" : \"\"}";
    //    }
    //    else
    //    {
    //        finalResult = "{\"success\" : 0, \"message\" : \"Order Not Created \", \"data\" : \"\"}";
    //    }
    //    Context.Response.Clear();
    //    Context.Response.ContentType = "application/json";
    //    Context.Response.Flush();
    //    Context.Response.Write(finalResult);
    //    Context.Response.End();
    //}
    [WebMethod]
    public void Order_InsertOrderandOrderProduct(string OrderProducts_JSONString, string UserId, string ProductQuantites, string BillPaidorNot, string OrderAmount, string Discount, string Tax, string TotalAmount, string OrderDate, string UserType,string customerType)
    {
        string finalResult = string.Empty;
        Customer_orders objorders = new Customer_orders();
        if (UserId == string.Empty)
        {
            objorders.uid = 0;
        }
        else
        {
            objorders.uid = Convert.ToInt64(UserId);
        }
        if (ProductQuantites == string.Empty)
        {
            objorders.productquantites = 0;
        }
        else
        {
            objorders.productquantites = Convert.ToInt32(ProductQuantites);
        }
        if (BillPaidorNot == string.Empty)
        {
            objorders.billpaidornot = false;
        }
        else
        {
            objorders.billpaidornot = false;
        }
        if (OrderAmount == string.Empty)
        {
            objorders.amount = 0;
        }
        else
        {
            objorders.amount = Convert.ToDecimal(OrderAmount);
        }
        if (Discount == string.Empty)
        {
            objorders.discount = 0;
        }
        else
        {
            objorders.discount = Convert.ToDecimal(Discount);
        }
        if (Tax == string.Empty)
        {
            objorders.tax = 0;
        }
        else
        {
            objorders.tax = Convert.ToDecimal(Tax);
        }
        if (TotalAmount == string.Empty)
        {
            objorders.totalamount = 0;
        }
        else
        {
            objorders.totalamount = Convert.ToDecimal(TotalAmount);
        }
        if (OrderDate == string.Empty)
        {
            objorders.orderdate = DateTime.Now;
        }
        else
        {
            objorders.orderdate = DateTime.Now;
        }
        if (UserType == string.Empty)
        {
            objorders.usertype = "U";
        }
        else
        {
            objorders.usertype = UserType;
        }

        int year = int.Parse(DateTime.Now.Year.ToString());
        string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
        int day = int.Parse(DateTime.Now.Day.ToString());
        int min = int.Parse(DateTime.Now.Minute.ToString());
        int hour = int.Parse(DateTime.Now.Hour.ToString());

        string orderno1 = year + "_" + month.Substring(0, 3).ToUpper() + "_" + day + "_" + hour + min;

       // objorders.orderno = orderno1;
        //objorders.ordertype = customerType.ToString();

        Int64 OrderProductAdd = 0;
        Int64 OrderId = 0;
        var dtOrderProducts = JsonConvert.DeserializeObject<DataTable>(OrderProducts_JSONString);
        if (dtOrderProducts != null)
        {
            if (dtOrderProducts.Rows.Count > 0)
            {
                OrderId = (new Cls_Customer_order_b().Insert(objorders));
                if (OrderId > 0)
                {
                    for (int i = 0; i < dtOrderProducts.Rows.Count; i++)
                    {
                        OrderProductAdd = 0;
                        Customer_orderproducts objorderproducts = new Customer_orderproducts();
                        objorderproducts.oid = OrderId;
                        objorderproducts.uid = Convert.ToInt64(dtOrderProducts.Rows[i]["userid"]);
                        objorderproducts.pid = Convert.ToInt64(dtOrderProducts.Rows[i]["productid"]);
                        objorderproducts.productprice = Convert.ToDecimal(dtOrderProducts.Rows[i]["productprice"]);
                        objorderproducts.discount = Convert.ToDecimal(dtOrderProducts.Rows[i]["discount"]);
                        objorderproducts.productafterdiscountprice = Convert.ToDecimal(dtOrderProducts.Rows[i]["productafterdiscountprice"]);
                        objorderproducts.quantites = Convert.ToInt32(dtOrderProducts.Rows[i]["quantites"]);
                        objorderproducts.gst = Convert.ToDecimal(dtOrderProducts.Rows[i]["gst"]);
                        OrderProductAdd = (new Cls_Customer_orderproducts_b().Insert(objorderproducts));
                        #region " Stock Update "
                        Product_StockUpdate(objorderproducts.pid, objorderproducts.quantites);
                        #endregion " Stock Update "

                        //[{"userid":"15","productid":"783","productprice":"500","discount":"10","quantites":10,"productafterdiscountprice":"450","gst":"12"},{"userid":"15","productid":"10783","productprice":"500","discount":"10","quantites":10,"productafterdiscountprice":"450","gst":"12"}]


                    }
                }
            }
        }

        if ((OrderId > 0) && (OrderProductAdd > 0))
        {
            SendOrderMail(OrderId);
            finalResult = "{\"success\" : 1, \"message\" : \"Order Created Sucessfully\", \"data\" : \"\"}";
        }
        else
        {
            finalResult = "{\"success\" : 0, \"message\" : \"Order Not Created \", \"data\" : \"\"}";
        }
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    private bool SendOrderMail(Int64 OrderId)
    {
        common ocommon = new common();
        string oSB = string.Empty;
        bool send = false;
        MailMessage mail = new MailMessage();
        mail.To.Add("kshatriya.enterprises@gmail.com");
        
        mail.CC.Add("Acnts.moryatools@gmail.com");
        mail.From = new MailAddress("demo@moryatools.com", "Morya Tools App");
        mail.Subject = "Customer Generate New Order - InvoiceNo - " + OrderId;
        mail.Body = OrderMailCreate(OrderId);
        mail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "103.250.184.62";
        smtp.Port = 25;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new System.Net.NetworkCredential("demo@moryatools.com", "vsys@2017");
        try
        {
            smtp.Send(mail);
            send = true;
        }
        catch (Exception ex)
        {
            send = false;
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        return send;
    }

    private string OrderMailCreate(Int64 OrderId)
    {
        common ocommon = new common();
        string OrderLink = "http://moryaapp.moryatools.com/orderinvoice.aspx?oid=" + ocommon.Encrypt(OrderId.ToString(), true);
        string oSB = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = "order_invoice";
            cmd.CommandText = "Customer_order_invoice";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@oid", OrderId);
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            oSB += "<div>Hello Admin,</div";
            if (ds.Tables != null)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    oSB += "<br/>";
                    oSB += "<table><tr><td><b><u>Customer Details - </u></b></td></tr><tr><td> Name - " + ds.Tables[2].Rows[0]["name"].ToString() + "</td></tr><tr><td>Phone: <span>" + ds.Tables[2].Rows[0]["phone"].ToString() + "</span></td></tr><tr><td>GST NO: <span>" + ds.Tables[2].Rows[0]["gstno"].ToString() + "</span></td></tr><tr><td>Address: <span>" + ds.Tables[2].Rows[0]["address"].ToString() + "</span></td></tr><tr><td>Email: <span>" + ds.Tables[2].Rows[0]["email"].ToString() + "</span></td></tr></table>";
                    oSB += "<hr/>";
                }

                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        oSB += "<br/>";
                        oSB += "<table><tr><td><b><u>Order Details -</u></b></td></tr><tr><td> Invoice No - #" + ds.Tables[0].Rows[0]["oid"].ToString() + "</td></tr><tr><td>Order Date: <span>" + ds.Tables[0].Rows[0]["orderdate"].ToString() + "</span></td></tr><tr><td>Total Amount: <span>" + ds.Tables[0].Rows[0]["totalamount"].ToString() + "</span></td></tr></table>";
                        oSB += "<hr/>";
                    }
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    oSB += "<br/>";
                    oSB += "<table><tr><td><b><u>Order Products Details - </u></b></td></tr></table>";
                    oSB += "<br/>";
                    oSB += "<table style='border: 1px solid black'><thead><tr style='border: 1px solid black'><th style='border: 1px solid black'>Product Name</th><th style='border: 1px solid black'>SKU</th><th style='text-align: center;border: 1px solid black'>Product Price</th><th style='text-align: center;border: 1px solid black'>Quantites</th><th style='text-align: center;border: 1px solid black'>GST</th><th style='text-align: center;border: 1px solid black'>Product Basic Price</th><th style='text-align: center;border: 1px solid black'>Product Total Price</th></tr></thead><tbody>";
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        oSB += "<tr style='border: 1px solid black'>";
                        oSB += "<td style='border: 1px solid black'><span>" + ds.Tables[1].Rows[i]["productname"].ToString() + "</span></td>";
                        oSB += "<td style='text-align: center;border: 1px solid black'><span>" + ds.Tables[1].Rows[i]["sku"].ToString() + "</span></td>";
                        oSB += "<td style='text-align: center;border: 1px solid black'><span>" + ds.Tables[1].Rows[i]["productprice"].ToString() + "</span></td>";
                        oSB += "<td style='text-align: center;border: 1px solid black'><span>" + ds.Tables[1].Rows[i]["quantites"].ToString() + "</span></td>";
                        oSB += "<td style='text-align: center;border: 1px solid black'><span>" + ds.Tables[1].Rows[i]["gst"].ToString() + "</span></td>";
                        oSB += "<td style='text-align: center;border: 1px solid black'><span>" + ds.Tables[1].Rows[i]["ProductBasicPrice"].ToString() + "</span></td>";
                        oSB += "<td style='text-align: center;border: 1px solid black'><span>" + ds.Tables[1].Rows[i]["producttotalprice"].ToString() + "</span></td>";
                        oSB += "</tr>";
                    }
                    oSB += "</tbody></table>";
                    oSB += "<br/>";
                    oSB += "<b><u>Website Page Link -</u>  <a href=" + OrderLink + ">" + OrderLink + "</a></b>";
                    oSB += "<br/>";
                    oSB += "<br/>";
                    oSB += "<hr/>";
                    oSB += "<div>Thank you,</div>";
                    oSB += "<div>Morya App - Support Team.</div>";
                }

            }
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return null;
        }
        finally
        {
            con.Close();
        }
        return oSB;
    }

    [WebMethod]
    public void Stock_CheckProductQuantites(string ProductId)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        DataTable dtStock = new DataTable();
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand("select quantites from product Where pid=" + ProductId);
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(dtStock);
            if (dtStock != null)
            {
                if (dtStock.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtStock);
                    finalResult = "{\"success\" : 1, \"message\" : \" Product Quantites  Data\", \"data\" :" + output + "}";
                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \" 0 Product Quantites \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" 0 Product Quantites \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    public void OrderHistroy_UsingUserID(Int64 UserId, string UserType)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtOrder = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Order_SelectOrderHistroyUsingUserId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userid", UserId);
            cmd.Parameters.AddWithValue("@usertype", UserType);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dtOrder);
            con.Close();
            if (dtOrder != null)
            {
                if (dtOrder.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtOrder);
                    finalResult = "{\"success\" : 1, \"message\" : \"User Order History\", \"data\" :" + output + "}";
                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Data Found\", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Data Found\", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Data Found\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    public void OrderDetailsUsingOrderID(Int64 OrderId)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtOrderHistroy = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Order_SelectOrderDetailsUsingOrderId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@orderid", OrderId);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dtOrderHistroy);
            con.Close();
            if (dtOrderHistroy != null)
            {
                if (dtOrderHistroy.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtOrderHistroy);
                    finalResult = "{\"success\" : 1, \"message\" : \"User Order Details History\", \"data\" :" + output + "}";
                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Data Found\", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Data Found\", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Data Found\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    private void Product_StockUpdate(Int64 ProductId, int Quantites)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "product_stockupdate";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@productid", ProductId);
            cmd.Parameters.AddWithValue("@quantites", Quantites);
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            con.Close();
        }
    }

    #endregion "Order Section"

    #region "Tax Amount"

    [WebMethod]
    public void GetTaxAmount()
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtTax = new DataTable();
            con.Open();
            string query = "select taxamount from tax";
            SqlCommand cmd = new SqlCommand(query);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dtTax);
            con.Close();
            if (dtTax != null)
            {
                if (dtTax.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtTax);
                    finalResult = "{\"success\" : 1, \"message\" : \"Tax Amount Data\", \"data\" :" + output + "}";
                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Data Found\", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Data Found\", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Data Found\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    #endregion "Tax Amount"

    #region " Dealer Section"

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DealerRegistration(Int64 DealerId, string Name, string UserLoginMobileNo, string Password, string WhatAppNo, string Email, string GstNo, string Address1, string Address2, string City, string State, string district)
    {
        string finalResult = string.Empty;
        if (CheckMobileNumberExitOrNot(UserLoginMobileNo))
        {
            finalResult = "{\"success\" : 0, \"message\" : \"Mobile Number Already Exits\", \"data\" : \"\"}";
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Flush();
            Context.Response.Write(finalResult);
            Context.Response.End();
        }
        else
        {
            if (CheckMobileNumberDealerExitOrNot(UserLoginMobileNo))
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Mobile Number Already Exits\", \"data\" : \"\"}";
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(finalResult);
                Context.Response.End();
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                try
                {
                    dealermaster objdealermaster = new dealermaster();
                    objdealermaster.did = 0;
                    objdealermaster.name = Name;
                    objdealermaster.userloginmobileno = UserLoginMobileNo;
                    objdealermaster.password = Password;
                    objdealermaster.whatappno = WhatAppNo;
                    objdealermaster.email = Email;
                    objdealermaster.gstno = GstNo;
                    objdealermaster.address1 = Address1;
                    objdealermaster.address2 = Address2;
                    objdealermaster.city = City;
                    objdealermaster.state = State;
                    objdealermaster.guid = Guid.NewGuid().ToString();
                    objdealermaster.district = Convert.ToInt64(district);
                    Int64 Result = (new Cls_dealermaster_b().Insert(objdealermaster));
                    if (Result > 0)
                    {
                        DataTable dtUser = new DataTable();
                        dtUser.Columns.Add("UserId", typeof(string));
                        dtUser.Columns.Add("UserType", typeof(string));
                        dtUser.Rows.Add(Result.ToString(), "D");
                        string output = DataTableToJSONWithJavaScriptSerializer(dtUser);
                        finalResult = "{\"success\" : 1, \"message\" : \"Dealer Registration inserted successfully\", \"data\" : " + output + "}";
                    }
                    else
                    {
                        finalResult = "{\"success\" : 1, \"message\" : \"Dealer Registration not inserted successfully\", \"data\" : \"\"}";
                    }
                }
                catch (Exception ex)
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
                }
                finally
                {
                    con.Close();
                }
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(finalResult);
                Context.Response.End();
            }

        }
    }

    public string DealerProfileImage(string img, string ext)
    {
        string FinalFileName = string.Empty;
        string fileName = Guid.NewGuid().ToString();
        byte[] bytes = Convert.FromBase64String(img);
        BinaryWriter Writer = null;
        Writer = new BinaryWriter(File.OpenWrite(System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/DealerImg/") + fileName + "." + ext));
        Writer.Write(bytes);
        Writer.Flush();
        Writer.Close();
        FinalFileName = fileName + "." + ext;
        return FinalFileName;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DealerProfileUpdate(Int64 DealerId, string Name, string WhatAppNo, string Email, string GstNo, string Address1, string Address2, string City, string State, string image1, string image1_extension,string districtId)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            dealermaster objdealermaster = new dealermaster();
            objdealermaster.did = DealerId;
            objdealermaster.name = Name;
            objdealermaster.whatappno = WhatAppNo;
            objdealermaster.email = Email;
            objdealermaster.gstno = GstNo;
            objdealermaster.address1 = Address1;
            objdealermaster.address2 = Address2;
            objdealermaster.city = City;
            objdealermaster.state = State;
            objdealermaster.district = Convert.ToInt64(districtId);

            if (!string.IsNullOrEmpty(image1))
            {
                objdealermaster.Img = DealerProfileImage(image1, image1_extension);
                 
            }
            //else
            //{
            //    objdealermaster.Img = string.Empty;
             
            //}
            //objdealermaster.userloginmobileno = UserLoginMobileNo;
            //objdealermaster.password = Password;
            //objdealermaster.guid = Guid.NewGuid().ToString();

            Int64 Result = (new Cls_dealermaster_b().Update(objdealermaster));
            if (Result > 0)
            {
                finalResult = "{\"success\" : 1, \"message\" : \"Dealer Information Updated successfully\", \"data\" : \"\"}";
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Dealer Information Not Updated  \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void UserDealerLatitudeLongitudeUpdate(Int64 UserId, string UserType, string Latitude, string Longitude)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "updatelatlong";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@userid", UserId);
            cmd.Parameters.AddWithValue("@usertype", UserType);
            cmd.Parameters.AddWithValue("@lat", Latitude);
            cmd.Parameters.AddWithValue("@long", Longitude);
            con.Open();
            int Result = cmd.ExecuteNonQuery();
            if (Result > 0)
            {
                finalResult = "{\"success\" : 1, \"message\" : \"User Information Updated successfully\", \"data\" : \"\"}";
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"User Information Not Updated  \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    #endregion " Dealer Section"

    #region " Live Count Download Count "

    [WebMethod]
    public void LiveDownloadCount()
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtCount = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select_LiveDownloadCount";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dtCount);
            con.Close();
            if (dtCount != null)
            {
                if (dtCount.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtCount);
                    finalResult = "{\"success\" : 1, \"message\" : \"Live & Download Count Data\", \"data\" :" + output + "}";
                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Data Found\", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Data Found\", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Data Found\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    #endregion " Live Count Download Count "

    #region " Order Image Add "

    [WebMethod]
    public void Insert_OrderInvoiceImage(Int64 OrderId, Int64 UserId, string UserType, string Title, string Image, string ImageExtension)
    {
        Int64 result = 0;
        string finalResult = string.Empty;
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "orderimagemaster_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@orderimageid";
            param.Value = 0;
            param.SqlDbType = SqlDbType.BigInt;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);

            cmd.Parameters.AddWithValue("@oid", OrderId);
            cmd.Parameters.AddWithValue("@uid", UserId);
            cmd.Parameters.AddWithValue("@utype", UserType);
            cmd.Parameters.AddWithValue("@title", Title);
            if (!string.IsNullOrEmpty(Image))
            {
                cmd.Parameters.AddWithValue("@image", OrderImage(Image, ImageExtension));
            }
            else
            {
                cmd.Parameters.AddWithValue("@image", string.Empty);
            }

            ConnectionString.Open();
            cmd.ExecuteNonQuery();
            result = Convert.ToInt64(param.Value);
            if (result > 0)
            {
                finalResult = "{\"success\" : 1, \"message\" : \"Order image inserted successfully\", \"data\" : \"\"}";
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Order image not inserted \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"Order image not inserted \", \"data\" : \"\"}";
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    public void Select_OrderTrackImageUsingOrderId(Int64 OrderId)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "Select top 2 'http://moryaapp.moryatools.com/uploads/orderimage/'+adminimage from orderimagemaster where adminimage is not null and oid=" + OrderId + " order by createddatetime desc ";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        con.Open();
        DataTable dtOrderTrackImage = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter();
        cmd.Connection = con;
        sda.SelectCommand = cmd;
        try
        {
            sda.Fill(dtOrderTrackImage);
            if (dtOrderTrackImage != null)
            {
                if (dtOrderTrackImage.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtOrderTrackImage);
                    finalResult = "{\"success\" : 1, \"message\" : \"Order Track Image\", \"data\" :" + output + "}";
                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Data Found\", \"data\" : \"\"}";
                }
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Data Found\", \"data\" : \"\"}";
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            con.Close();
        }
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    public string OrderImage(string img, string ext)
    {
        string FinalFileName = string.Empty;
        string fileName = Guid.NewGuid().ToString();
        byte[] bytes = Convert.FromBase64String(img);
        BinaryWriter Writer = null;
        Writer = new BinaryWriter(File.OpenWrite(System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/orderimage/") + fileName + "." + ext));
        Writer.Write(bytes);
        Writer.Flush();
        Writer.Close();
        FinalFileName = fileName + "." + ext;
        return FinalFileName;
    }

    #endregion " Order Image Add "

    #region "Product Feedback Images Add"

    [WebMethod]
    public void Insert_ProductRealtedFeedback(Int64 UserId, string UserType, string Title, string Descprition, string Image_1, string Image1_Extension, string Image_2, string Image2_Extension)
    {
        Int64 result = 0;
        string finalResult = string.Empty;
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "productfeedback_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@productfeedid";
            param.Value = 0;
            param.SqlDbType = SqlDbType.BigInt;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);

            cmd.Parameters.AddWithValue("@uid", UserId);
            cmd.Parameters.AddWithValue("@utype", UserType);
            cmd.Parameters.AddWithValue("@title", Title);
            cmd.Parameters.AddWithValue("@descp", Descprition);
            if (!string.IsNullOrEmpty(Image_1))
            {
                cmd.Parameters.AddWithValue("@image1", ProductFeedbackImage(Image_1, Image1_Extension));
            }
            else
            {
                cmd.Parameters.AddWithValue("@image1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Image_2))
            {
                cmd.Parameters.AddWithValue("@image2", ProductFeedbackImage(Image_2, Image2_Extension));
            }
            else
            {
                cmd.Parameters.AddWithValue("@image2", string.Empty);
            }
            ConnectionString.Open();
            cmd.ExecuteNonQuery();
            result = Convert.ToInt64(param.Value);
            if (result > 0)
            {
                finalResult = "{\"success\" : 1, \"message\" : \"Product feedback inserted successfully\", \"data\" : \"\"}";
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Product feedback not inserted \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"Product feedback not inserted \", \"data\" : \"\"}";
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    public string ProductFeedbackImage(string img, string ext)
    {
        string FinalFileName = string.Empty;
        string fileName = Guid.NewGuid().ToString();
        byte[] bytes = Convert.FromBase64String(img);
        BinaryWriter Writer = null;
        Writer = new BinaryWriter(File.OpenWrite(System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/productfeedbackimage/") + fileName + "." + ext));
        Writer.Write(bytes);
        Writer.Flush();
        Writer.Close();
        FinalFileName = fileName + "." + ext;
        return FinalFileName;
    }

    #endregion "Product Feedback Images Add"

    #region " Test Notification"

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SendTestNotification(string DeviceId, string Message)
    {
        String sResponseFromServer = string.Empty;
        try
        {
            var applicationID = "AIzaSyBHxPITxegYgcgMmhdJ0ceRJCqz8wxJyUM";
            var senderId = "315798993928";
            string Title = "Test";
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            var data1 = new
            {
                to = DeviceId,
                data = new
                {
                    Title = Title,
                    Message = Message,
                    StatusCode = "301",
                },
                priority = "high"
            };
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data1);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
            tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            tRequest.ContentLength = byteArray.Length;

            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            sResponseFromServer = tReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(sResponseFromServer);
        Context.Response.End();
    }

    #endregion " Test Notification"

    #region " PDF "
    [WebMethod]
    public void CreatePDF(Int64 CategoryId)
    {
        //string finalResult = string.Empty;
        Paragraph paragraph = new Paragraph();
        paragraph.Add(("Date: " + DateTime.Now.ToString("dd/MM/yyyy")).Replace('-', '/'));
        paragraph.Alignment = Element.ALIGN_RIGHT;
        DataTable dtProducts = (new Cls_product_b().Product_WSSelectAllProductUsingCategoryId(CategoryId));
        Document documnent = new Document(iTextSharp.text.PageSize.A4, 20f, 20f, 20f, 20f);

        PdfPTable tableCategory = new PdfPTable(3);
        iTextSharp.text.Image myImg = iTextSharp.text.Image.GetInstance(Server.MapPath("Uploads/MooryaTools.png"));
        myImg.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
        myImg.ScaleAbsolute(220f, 75f);


        //tableCategory.AddCell("Category Name:" + dtProducts.Rows[0]["Categoryname"]);
        //tableCategory.AddCell(("Date: " + DateTime.Now.ToString("dd/MM/yyyy")).Replace('-', '/'));
        //tableCategory.SpacingAfter = 12.5f;
        documnent.Open();
        //documnent.Add(tableCategory);
        Chunk glue = new Chunk(new VerticalPositionMark());
        Paragraph para = new Paragraph();
        string finalResult = string.Empty;
        MemoryStream mstr = new MemoryStream();

        String path = Server.MapPath("uploads");
        string PDFName = Guid.NewGuid().ToString();
        // string PDFName = Convert.ToString(dtProducts.Rows[0]["Categoryname"]);
        PdfWriter writer = PdfWriter.GetInstance(documnent, new FileStream(path + "/" + PDFName + ".pdf", FileMode.Create));

        Paragraph paragraph1 = new Paragraph();
        paragraph1.Add("------------------------------------------------------------------");
        paragraph1.Add("\n");
        paragraph1.Add("Category:" + dtProducts.Rows[0]["Categoryname"]);
        paragraph1.Add("\n");
        paragraph1.Add("------------------------------------------------------------------");
        paragraph1.Alignment = Element.ALIGN_CENTER;
        paragraph1.Add("\n");
        paragraph1.Add("Below mention price including GST");
        paragraph1.SpacingAfter = 12.5f;

        paragraph.SetLeading(1.0f, 3.0f);
        documnent.Open();
        paragraph.Add("");



        Phrase ph1 = new Phrase();

        Paragraph mm = new Paragraph();
        PdfPTable table = new PdfPTable(5);

        table.SetWidths(new float[] { 2f, 2f, 2f, 1f, 2f });
        table.AddCell("Product Name");
        table.AddCell("Image");
        table.AddCell("Quantities");
        table.AddCell("GST");
        table.AddCell("Rate");
        for (int i = 0; i < dtProducts.Rows.Count; i++)
        {
            try
            {
                table.AddCell(dtProducts.Rows[i]["productname"].ToString());
                if (dtProducts.Rows[i]["imagename"] != System.DBNull.Value)
                {
                    iTextSharp.text.Image myImg1 = iTextSharp.text.Image.GetInstance(("" + dtProducts.Rows[i]["imagename"] + " "));
                    myImg1.ScaleAbsolute(100f, 100f);
                    table.AddCell(iTextSharp.text.Image.GetInstance(("" + dtProducts.Rows[i]["imagename"] + " ")));
                }
                else
                {
                    iTextSharp.text.Image myImg1 = iTextSharp.text.Image.GetInstance(Server.MapPath("Uploads/ImageNotFound.png"));
                    table.AddCell(iTextSharp.text.Image.GetInstance((Server.MapPath("Uploads/ImageNotFound.png"))));
                }

                table.AddCell(dtProducts.Rows[i]["quantites"].ToString());
                table.AddCell(dtProducts.Rows[i]["gst"].ToString());
                table.AddCell(dtProducts.Rows[i]["dealerprice"].ToString());
            }
            catch (Exception )
            {
            }
        }

        Paragraph paragraph2 = new Paragraph();

        paragraph2.Add("Above mention price including GST");
        paragraph2.Alignment = Element.ALIGN_CENTER;

        documnent.Add(paragraph);

        documnent.Add(myImg);
        documnent.Add(paragraph1);
        documnent.Add(table);
        documnent.Add(paragraph2);
        mm.Add(ph1);
        para.Add(mm);
        documnent.Add(para);
        Paragraph p2 = new Paragraph();
        p2.SetLeading(5f, 5f);
        documnent.Add(paragraph);
        documnent.Add(p2);
        documnent.Close();

        DataTable dtPDFPath = new DataTable();
        dtPDFPath.Columns.Add("PDFPath", typeof(string));
        dtPDFPath.Rows.Add(@"http://moryaapp.moryatools.com/uploads/" + PDFName + ".pdf");
        string output = DataTableToJSONWithJavaScriptSerializer(dtPDFPath);
        finalResult = "{\"success\" : 1, \"message\" : \" PDF Path \", \"data\" : " + output + "}";

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }
    #endregion " PDF "



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void getDealerDetails_usingDealerID(Int64 ID)
    {
        string finalResult = string.Empty;
        try
        {
            DataTable dtCategory = (new Cls_dealermaster_b().SelectAllDetails_usingID(ID));
              
            if (dtCategory != null)
            {
                if (dtCategory.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtCategory);
                    finalResult = "{\"success\" : 1, \"message\" : \" Category All Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Category \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }


    public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
    {
        var JSONString = new StringBuilder();
        if (table.Rows.Count > 0)
        {
            JSONString.Append("[");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                JSONString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j < table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString().Trim() + "\",");
                    }
                    else if (j == table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString().Trim() + "\"");
                    }
                }
                if (i == table.Rows.Count - 1)
                {
                    JSONString.Append("}");
                }
                else
                {
                    JSONString.Append("},");
                }
            }
            JSONString.Append("]");
        }
        return JSONString.ToString();
    }


    #region New Webservice

    public string dealer_DataTableToJSONWithJavaScriptSerializer(DataTable table, int start, int end)
    {
        int LastIndex = 0;
        if (table.Rows.Count < (start + end))
        {
            LastIndex = table.Rows.Count;
        }
        else
        {
            LastIndex = end;
        }


        var JSONString = new StringBuilder();
        //if (table.Rows.Count > 0)
        if (table.Rows.Count > 0)
        {
            JSONString.Append("[");
            for (int i = start; i < LastIndex; i++)
            //for (int i = start; i < end; i++)
            {
                JSONString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j < table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString().Trim() + "\",");
                    }
                    else if (j == table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString().Trim() + "\"");
                    }
                }
                if (i == end - 1)
                {
                    JSONString.Append("}");
                }
                else
                {
                    JSONString.Append("},");
                }
            }
            JSONString.Append("]");
        }
        return JSONString.ToString();
    }

   
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void dealer_SelectAllAdminPaging(int start, int end)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtDealer = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealer_SelectAllAdminPaging";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtDealer);
            int newEnd = start + end;

            if (dtDealer != null)
            {
                if (dtDealer.Rows.Count > 0)
                {
                    string output = dealer_DataTableToJSONWithJavaScriptSerializer(dtDealer, start, newEnd);
                    finalResult = "{\"success\" : 1, \"message\" : \" Product Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Products Data Not Exits  \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Products Exits This Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Products Exits This Category \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Admin_Login(string Username, string password)
        {
            string finalResult = string.Empty;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            try
            {
                DataTable dtUser = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Admin_Login";
                cmd.Parameters.AddWithValue("@UserName", Username);
                cmd.Parameters.AddWithValue("@Password", password);                 

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter();

                cmd.Connection = con;
                sda.SelectCommand = cmd;
                sda.Fill(dtUser);
                con.Close();
                if (dtUser != null)
                {
                    if (dtUser.Rows.Count > 0)
                    {                       
                            string output = DataTableToJSONWithJavaScriptSerializer(dtUser);
                            finalResult = "{\"success\" : 1, \"message\" : \"Login Successfully\", \"data\" :" + output + "}";
                    }
                    else
                    {
                        finalResult = "{\"success\" : 0, \"message\" : \"Incorrect User Name & Password\", \"data\" : \"\"}";
                    }
                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"Incorrect User Name & Password\", \"data\" : \"\"}";
                }
            }
            catch (Exception ex)
            {
                finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
            }
            finally
            {
                con.Close();
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Flush();
            Context.Response.Write(finalResult);
            Context.Response.End();
        }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void dealer_SelectAllAdminActiveUserPaging()
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtDealer = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealer_SelectAllAdminActivePaging";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtDealer);
           // int newEnd = start + end;

            if (dtDealer != null)
            {
                if (dtDealer.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtDealer);
                    //string output = dealer_DataTableToJSONWithJavaScriptSerializer(dtDealer, start, newEnd);
                    finalResult = "{\"success\" : 1, \"message\" : \" Product Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Products Data Not Exits  \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Products Exits This Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Products Exits This Category \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }
    [WebMethod]

    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void dealer_SelectAllAdminNotActiveUserPaging()//(int start, int end)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtDealer = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealer_SelectAllAdminNotActivePaging";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtDealer);
            //int newEnd = start + end;

            if (dtDealer != null)
            {
                if (dtDealer.Rows.Count > 0)
                {
                   
                    string output = DataTableToJSONWithJavaScriptSerializer(dtDealer); 
                   // string output = dealer_DataTableToJSONWithJavaScriptSerializer(dtDealer, start, newEnd);
                    finalResult = "{\"success\" : 1, \"message\" : \" Product Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Products Data Not Exits  \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Products Exits This Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Products Exits This Category \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void WebsiteUser_SelectAllPaging(int start, int end)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtDealer = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealer_SelectAllWebsiteUserPaging";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtDealer);
            int newEnd = start + end;

            if (dtDealer != null)
            {
                if (dtDealer.Rows.Count > 0)
                {
                    string output = dealer_DataTableToJSONWithJavaScriptSerializer(dtDealer, start, newEnd);
                    finalResult = "{\"success\" : 1, \"message\" : \" Record Found\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"Record Data Not Exits  \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Record Exits   \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Record Exits  \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }


    private DataTable SelectAllDealers()
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select did,name,deviceid,userloginmobileno from dealermaster Where isdeleted=0";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = ConnectionString;
            ConnectionString.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return null;
        }
        finally
        {
            ConnectionString.Close();
        }
        return ds.Tables[0];
    }

    private void SendNotification(string OfferTitle, string OfferImage, string OfferDescp, string OfferValid, string OfferTo)
    {
        String sResponseFromServer = string.Empty;
        DataTable dtDealerList = SelectAllDealers();
        if (dtDealerList != null)
        {
            //for (int i = 0; i < dtDealerList.Rows.Count; i++)
            for (int i = 0; i < 5; i++)
            {
                if (!string.IsNullOrEmpty(dtDealerList.Rows[i]["deviceid"].ToString()))
                {
                    try
                    {
                        sResponseFromServer = string.Empty;


                         

                        var applicationID = "AIzaSyBHxPITxegYgcgMmhdJ0ceRJCqz8wxJyUM";
                        var senderId = "315798993928";
                        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                        tRequest.Method = "post";
                        tRequest.ContentType = "application/json";
                        var data1 = new
                        {
                            to = dtDealerList.Rows[i]["deviceid"].ToString(),
                            data = new
                            {
                                StatusCode = "301",
                                Image = "http://moryaapp.moryatools.com/uploads/offer/front/" + OfferImage,
                                Title = OfferTitle,
                                Description = OfferDescp + ' ' + OfferValid + ' ' + OfferTo,
                                //Sender_Name = "Admin",
                                //Sender_Id = "1",
                                //Sender_type = "A",
                                //Receiver_Name = dtDealerList.Rows[i]["name"].ToString(),
                                //Receiver_id = dtDealerList.Rows[i]["did"].ToString(),
                                //Receiver_type = "D",                                
                            },
                            priority = "high"
                        };
                        var serializer = new JavaScriptSerializer();
                        var json = serializer.Serialize(data1);
                        Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                        tRequest.ContentLength = byteArray.Length;

                        using (Stream dataStream = tRequest.GetRequestStream())
                        {
                            dataStream.Write(byteArray, 0, byteArray.Length);

                            using (WebResponse tResponse = tRequest.GetResponse())
                            {
                                using (Stream dataStreamResponse = tResponse.GetResponseStream())
                                {
                                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                    {
                                        sResponseFromServer = tReader.ReadToEnd();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Flush();
                    Context.Response.Write(sResponseFromServer);
                }
            }
        }
        Context.Response.End();
    }

    public string InsertOfferImage(string img, string ext)
    {
        string FinalFileName = string.Empty;
        string fileName = Guid.NewGuid().ToString();
        byte[] bytes = Convert.FromBase64String(img);
        BinaryWriter Writer = null;
        Writer = new BinaryWriter(File.OpenWrite(System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/offer/") + fileName + "." + ext));
        Writer.Write(bytes);
        Writer.Flush();
        Writer.Close();
        //------------------------
        BinaryWriter Writer1 = null;
        Writer1 = new BinaryWriter(File.OpenWrite(System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/offer/front/") + fileName + "." + ext));
        Writer1.Write(bytes);
        Writer1.Flush();
        Writer1.Close();
        //-----------------------------
        FinalFileName = fileName + "." + ext;
        return FinalFileName;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void insertOffer(String Title, string Description, string fromDate, string Todate, string Image1, string Image1Extension)
    {
        string finalResult = string.Empty;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            Int64 Result = 0;
            offers objoffers = new offers();
            objoffers.title = Title;
            objoffers.descp = Description;
            objoffers.validfrom = fromDate;
            objoffers.validto = Todate;
            if (!string.IsNullOrEmpty(Image1))
            {
                objoffers.imagename = InsertOfferImage(Image1, Image1Extension);

            }
            else
            {
                objoffers.imagename = string.Empty;

            }
            Result = (new Cls_offers_b().Insert(objoffers));
            if (Result > 0)
            {
                SendNotification(objoffers.title, objoffers.imagename, objoffers.descp, objoffers.validfrom, objoffers.validto);

                finalResult = "{\"success\" : 1, \"message\" : \"User Registration inserted successfully\", \"data\" : \"\"}";

            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"User Registration not inserted successfully\", \"data\" : \"\"}";

            }



        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();


    }

    public string InsertNewProductImage(string img, string ext)
    {

        string FinalFileName = string.Empty;
        string fileName = Guid.NewGuid().ToString();
        byte[] bytes = Convert.FromBase64String(img);
        BinaryWriter Writer = null;
        Writer = new BinaryWriter(File.OpenWrite(System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/news/") + fileName + "." + ext));
        Writer.Write(bytes);
        Writer.Flush();
        Writer.Close();
        //------------------------
        BinaryWriter Writer1 = null;
        Writer1 = new BinaryWriter(File.OpenWrite(System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/news/front/") + fileName + "." + ext));
        Writer1.Write(bytes);
        Writer1.Flush();
        Writer1.Close();
        //-----------------------------
        FinalFileName = fileName + "." + ext;
        return FinalFileName;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void insertManageNewProduct(String Title, string ShortDescription, string longDescription, string date, string Image1, string Image1Extension)
    {
        string finalResult = string.Empty;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            Int64 Result = 0;
            newsupdate objnewsupdate = new newsupdate();
            objnewsupdate.title = Title;
            objnewsupdate.shortdescp = ShortDescription;
            objnewsupdate.longdescp = longDescription;
            objnewsupdate.newsdate = date;
            if (!string.IsNullOrEmpty(Image1))
            {
                objnewsupdate.imagename = InsertNewProductImage(Image1, Image1Extension);

            }
            else
            {
                objnewsupdate.imagename = string.Empty;

            }

            Result = (new Cls_newsupdate_b().Insert(objnewsupdate));
            if (Result > 0)
            {
                SendNotification_NewProduct(objnewsupdate.title, objnewsupdate.imagename, objnewsupdate.shortdescp, objnewsupdate.newsdate);
                finalResult = "{\"success\" : 1, \"message\" : \"Record inserted successfully\", \"data\" : \"\"}";

            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Record not inserted successfully\", \"data\" : \"\"}";
            }



        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();


    }
    private void SendNotification_NewProduct(string NewsTitle, string NewsImage, string NewsDescp, string NewsDate)
    {
        String sResponseFromServer = string.Empty;
        DataTable dtDealerList = SelectAllDealers();
        if (dtDealerList != null)
        {
            //for (int i = 0; i < dtDealerList.Rows.Count; i++)
            for (int i = 0; i < 5; i++)
            {
                if (!string.IsNullOrEmpty(dtDealerList.Rows[i]["deviceid"].ToString()))
                {
                    try
                    {
                        sResponseFromServer = string.Empty;
                        var applicationID = "AIzaSyBHxPITxegYgcgMmhdJ0ceRJCqz8wxJyUM";
                        var senderId = "315798993928";
                        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                        tRequest.Method = "post";
                        tRequest.ContentType = "application/json";
                        var data1 = new
                        {
                            to = dtDealerList.Rows[i]["deviceid"].ToString(),
                            data = new
                            {
                                StatusCode = "302",
                                Image = "http://moryaapp.moryatools.com/uploads/news/front/" + NewsImage,
                                Title = NewsTitle,
                                Description = NewsDescp + ' ' + NewsDate,
                                //Sender_Name = "Admin",
                                //Sender_Id = "1",
                                //Sender_type = "O",
                                //Receiver_Name = dtDealerList.Rows[i]["name"].ToString(),
                                //Receiver_id = dtDealerList.Rows[i]["did"].ToString(),
                                //Receiver_type = "D",                                
                            },
                            priority = "high"
                        };
                        var serializer = new JavaScriptSerializer();
                        var json = serializer.Serialize(data1);
                        Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                        tRequest.ContentLength = byteArray.Length;

                        using (Stream dataStream = tRequest.GetRequestStream())
                        {
                            dataStream.Write(byteArray, 0, byteArray.Length);

                            using (WebResponse tResponse = tRequest.GetResponse())
                            {
                                using (Stream dataStreamResponse = tResponse.GetResponseStream())
                                {
                                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                    {
                                        sResponseFromServer = tReader.ReadToEnd();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Flush();
                    Context.Response.Write(sResponseFromServer);
                }
            }
        }
        Context.Response.End();
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void WebSiteUser_SelectAll(int start, int end)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtDealer = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "WebSiteUser_SelectAll";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtDealer);
            int newEnd = start + end;

            if (dtDealer != null)
            {
                if (dtDealer.Rows.Count > 0)
                {
                    string output = dealer_DataTableToJSONWithJavaScriptSerializer(dtDealer, start, newEnd);
                    finalResult = "{\"success\" : 1, \"message\" : \" Record Found\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No  Data Not Exits  \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Data Exits This Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Data Exits This Category \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void WebSiteUser_SelectAllNotActive(int start, int end)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtDealer = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "WebSiteUser_SelectAllNotActive";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtDealer);
            int newEnd = start + end;

            if (dtDealer != null)
            {
                if (dtDealer.Rows.Count > 0)
                {
                    string output = dealer_DataTableToJSONWithJavaScriptSerializer(dtDealer, start, newEnd);
                    finalResult = "{\"success\" : 1, \"message\" : \" Record Found\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No  Data Not Exits  \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Data Exits This Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Data Exits This Category \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void WebSiteUser_SelectAllActive(int start, int end)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtDealer = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "WebSiteUser_SelectAllActive";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtDealer);
            int newEnd = start + end;

            if (dtDealer != null)
            {
                if (dtDealer.Rows.Count > 0)
                {
                    string output = dealer_DataTableToJSONWithJavaScriptSerializer(dtDealer, start, newEnd);
                    finalResult = "{\"success\" : 1, \"message\" : \" Record Found\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No  Data Not Exits  \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Data Exits This Category \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Data Exits This Category \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void pageaccess_SelectAll()
    {
        string finalResult = string.Empty;
        try
        {
            DataTable ds = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "pageaccess_SelectAll";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@feedbackid", feedbackid);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(ds);
                    finalResult = "{\"success\" : 1, \"message\" : \" Record Found\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Data Found \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Data Found \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void orderSelectAll_TodaysOrder()
    {
        string finalResult = string.Empty;
        try
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Order_SelectAll";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@feedbackid", feedbackid);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(ds.Tables[0]);
                    finalResult = "{\"success\" : 1, \"message\" : \" Record Found\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Data Found \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Data Found \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void orderSelectAll_YesterdayOrder()
    {
        string finalResult = string.Empty;
        try
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Order_SelectAll";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@feedbackid", feedbackid);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (ds.Tables[1] != null)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(ds.Tables[1]);
                    finalResult = "{\"success\" : 1, \"message\" : \" Record Found\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Data Found \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Data Found \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void orderSelectAll_Remaining()
    {
        string finalResult = string.Empty;
        try
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Order_SelectAll";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@feedbackid", feedbackid);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (ds.Tables[2] != null)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(ds.Tables[2]);
                    finalResult = "{\"success\" : 1, \"message\" : \" Record Found\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Data Found \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Data Found \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void get_InvoieDetails(Int64 OrderID)
    {
        string finalResult = string.Empty;
        try
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "showMobileorder_invoice";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@oid", OrderID);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(ds.Tables[0]);
                    finalResult = "{\"success\" : 1, \"message\" : \" Record Found\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Data Found \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Data Found \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }





    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void dealermaster_IsActive(Int64 UserID, Boolean IsActive)
    {
        string finalResult = string.Empty;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            Int64 Result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealermaster_IsActive";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@uid", UserID);
            cmd.Parameters.AddWithValue("@isactive", IsActive);
            con.Open();
            Result = cmd.ExecuteNonQuery();
            if (Result > 0)
            {

                finalResult = "{\"success\" : 1, \"message\" : \"Dealer Status Updated Successfully\", \"data\" : \"\"}";

            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Dealer Status Not Updated Successfully\", \"data\" : \"\"}";

            }



        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();


    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void WebsiteUser_selectedbyID(Int64 UserID)
    {
        string finalResult = string.Empty;
        try
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "WebsiteUser_SelectById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@UserID", UserID);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(ds.Tables[0]);
                    finalResult = "{\"success\" : 1, \"message\" : \" Record Found\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Data Found \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Data Found \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void WebsiteUserDetails_update(Int64 UserID, String Username, string LoginID, string Password, string Email, string Phone, string pageAccessID)
    {
        string finalResult = string.Empty;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            Int64 Result = 0;
            string userType = "user";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = "Update adminlogin SET UserName='" + LoginID + "',name='" + Username + "',Password='" + Password + "', usertype='" + userType + "',email='" + Email + "',phone='" + Phone + "' Where AdminId=" + UserID;
            con.Open();
            Result = cmd.ExecuteNonQuery();
            con.Close();
            DeletePageAuthority(UserID);
            string[] pageId = pageAccessID.ToString().Split(',');
            for (int i = 0; i < pageId.Length; i++)
            {
                if (pageId[i].ToString().Trim() == "")
                {
                }
                else
                {
                    InsertPageAuthority(UserID, Convert.ToInt64(pageId[i].ToString()));
                }
            }



            if (Result > 0)
            {

                finalResult = "{\"success\" : 1, \"message\" : \"User Registration inserted successfully\", \"data\" : \"\"}";

            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"User Registration not inserted successfully\", \"data\" : \"\"}";

            }



        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();


    }
    private void DeletePageAuthority(Int64 AdminId)
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "Delete FROM pageauthority Where adminid=" + AdminId;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = ConnectionString;
        try
        {
            ConnectionString.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }
    }
    private void InsertPageAuthority(Int64 AdminId, Int64 PageId)
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "pageauthority_Insert";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = ConnectionString;
        cmd.Parameters.AddWithValue("@adminid", AdminId);
        cmd.Parameters.AddWithValue("@pageid", PageId);
        try
        {
            ConnectionString.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }
    }


    public string InsertUserImage(string img, string ext)
    {
        string FinalFileName = string.Empty;
        string fileName = Guid.NewGuid().ToString();
        byte[] bytes = Convert.FromBase64String(img);
        BinaryWriter Writer = null;
        Writer = new BinaryWriter(File.OpenWrite(System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/news/") + fileName + "." + ext));
        Writer.Write(bytes);
        Writer.Flush();
        Writer.Close();
        //----------------
        BinaryWriter Writer1 = null;
        Writer1 = new BinaryWriter(File.OpenWrite(System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/news/front/") + fileName + "." + ext));
        Writer1.Write(bytes);
        Writer1.Flush();
        Writer1.Close();

        FinalFileName = fileName + "." + ext;
        return FinalFileName;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void insertNewArrivalProduct(String Title, string ShortDescription, string longDescription, string Image1, string Image1Extension)
    {
        string finalResult = string.Empty;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            Int64 Result = 0;
            newsupdate objnewsupdate = new newsupdate();
            objnewsupdate.title = Title;
            objnewsupdate.shortdescp = ShortDescription;
            objnewsupdate.longdescp = longDescription;
            objnewsupdate.newsdate = DateTime.Now.ToString();

            if (!string.IsNullOrEmpty(Image1))
            {
                objnewsupdate.imagename = InsertUserImage(Image1, Image1Extension);

            }
            else
            {
                objnewsupdate.imagename = string.Empty;

            }


            Result = (new Cls_newsupdate_b().Insert(objnewsupdate));
            if (Result > 0)
            {
                SendNotification(objnewsupdate.title, objnewsupdate.imagename, objnewsupdate.shortdescp, objnewsupdate.newsdate);
                finalResult = "{\"success\" : 1, \"message\" : \"User Registration inserted successfully\", \"data\" : \"\"}";

            }
            else
            {

                finalResult = "{\"success\" : 0, \"message\" : \"User Registration not inserted successfully\", \"data\" : \"\"}";

            }



        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 1, \"message\" : \"User Registration inserted successfully\", \"data\" : \"\"}";

            //finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);

        Context.Response.End();


    }
    private void SendNotification(string NewsTitle, string NewsImage, string NewsDescp, string NewsDate)
    {
        String sResponseFromServer = string.Empty;
        DataTable dtDealerList = SelectAllDealers();
        if (dtDealerList != null)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!string.IsNullOrEmpty(dtDealerList.Rows[i]["deviceid"].ToString()))
                {
                    try
                    {
                        sResponseFromServer = string.Empty;
                        var applicationID = "AIzaSyBHxPITxegYgcgMmhdJ0ceRJCqz8wxJyUM";
                        var senderId = "315798993928";
                        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                        tRequest.Method = "post";
                        tRequest.ContentType = "application/json";
                        var data1 = new
                        {
                            to = dtDealerList.Rows[i]["deviceid"].ToString(),
                            data = new
                            {
                                StatusCode = "302",
                                Image = "http://moryaapp.moryatools.com/uploads/news/front/" + NewsImage,
                                Title = NewsTitle,
                                Description = NewsDescp + ' ' + NewsDate,
                                //Sender_Name = "Admin",
                                //Sender_Id = "1",
                                //Sender_type = "O",
                                //Receiver_Name = dtDealerList.Rows[i]["name"].ToString(),
                                //Receiver_id = dtDealerList.Rows[i]["did"].ToString(),
                                //Receiver_type = "D",                                
                            },
                            priority = "high"
                        };
                        var serializer = new JavaScriptSerializer();
                        var json = serializer.Serialize(data1);
                        Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                        tRequest.ContentLength = byteArray.Length;

                        using (Stream dataStream = tRequest.GetRequestStream())
                        {
                            dataStream.Write(byteArray, 0, byteArray.Length);

                            using (WebResponse tResponse = tRequest.GetResponse())
                            {
                                using (Stream dataStreamResponse = tResponse.GetResponseStream())
                                {
                                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                    {
                                        sResponseFromServer = tReader.ReadToEnd();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Flush();
                    Context.Response.Write(sResponseFromServer);
                }
            }
        }
        Context.Response.End();
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Order_CountList()
    {
        string finalResult = string.Empty;
        try
        {
            DataTable ds = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Order_CountList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@feedbackid", feedbackid);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(ds);
                    finalResult = "{\"success\" : 1, \"message\" : \" Record Found\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Data Found \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Data Found \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    #endregion
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void  MYSSKfollowupUPSelectall()
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtDealer = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[MySSKTblFollowupSelectAll]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtDealer);
          

            if (dtDealer != null)
            {
                if (dtDealer.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtDealer);
                    finalResult = "{\"success\" : 1, \"message\" : \" Folloup Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Followup Data Not Exits  \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Followup Exits  \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Followup Exits  \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }




    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void EZACUSfollowupUPSelectall()
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtDealer = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[EZACUSFolloupSelectAll]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtDealer);


            if (dtDealer != null)
            {
                if (dtDealer.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtDealer);
                    finalResult = "{\"success\" : 1, \"message\" : \" Folloup Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Followup Data Not Exits  \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Followup Exits  \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Followup Exits  \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MoryafollowupUPSelectall()
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtDealer = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[MoryaFolloupSelectAll]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtDealer);


            if (dtDealer != null)
            {
                if (dtDealer.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtDealer);
                    finalResult = "{\"success\" : 1, \"message\" : \" Folloup Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Followup Data Not Exits  \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Followup Exits  \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Followup Exits  \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void WebsiteUser_Status(string Username,string password, Boolean IsActive)
    {
        string finalResult = string.Empty;        
        try
        {
            Cls_userregistration_b obj = new Cls_userregistration_b();
           Int64  Result = obj.WebsiteUser_Status(Username, password, IsActive);
            if (Result > 0)
            {

                finalResult = "{\"success\" : 1, \"message\" : \"  Status Updated Successfully\", \"data\" : \"\"}";

            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"  Status Not Updated Successfully\", \"data\" : \"\"}";

            }



        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }
        finally
        {
             
        }
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();


    }

    [WebMethod]
    public void Update_MySSKTblFollowup(Int64 ID, Int64 AdminID)
    {
        Int64 result = 0;
        string finalResult = string.Empty;
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Update_MySSKTblFollowup";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            cmd.Parameters.AddWithValue("@PKId", ID);
            cmd.Parameters.AddWithValue("@Remark", AdminID);
            

            ConnectionString.Open();
            result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                finalResult = "{\"success\" : 1, \"message\" : \"Device Id Updated successfully\", \"data\" : \"\"}";
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Device Id Not Updated \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"Device Id Not Updated \", \"data\" : \"\"}";
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }


    [WebMethod]
    public void Update_MoryaTblFollowup(Int64 ID, Int64 AdminID)
    {
        Int64 result = 0;
        string finalResult = string.Empty;
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Update_MoryaTblFollowup";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            cmd.Parameters.AddWithValue("@PKId", ID);
            cmd.Parameters.AddWithValue("@Remark", AdminID);


            ConnectionString.Open();
            result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                finalResult = "{\"success\" : 1, \"message\" : \"Device Id Updated successfully\", \"data\" : \"\"}";
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Device Id Not Updated \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"Device Id Not Updated \", \"data\" : \"\"}";
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }
    [WebMethod]
    public void Update_EZACUSTblFollowup(Int64 ID, Int64 AdminID, string enquityFor, string nextTimetoCall, string lead, string other)
    {
        Int64 result = 0;
        string finalResult = string.Empty;
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Update_EZACUSTblFollowup";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            cmd.Parameters.AddWithValue("@PKId", ID);
            cmd.Parameters.AddWithValue("@Remark", AdminID);

              cmd.Parameters.AddWithValue("@EnquiryFor", enquityFor);
              cmd.Parameters.AddWithValue("@NextTimetoCall", nextTimetoCall);
              cmd.Parameters.AddWithValue("@Lead", lead);
              cmd.Parameters.AddWithValue("@Other", other);
              
            
            ConnectionString.Open();
            result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                finalResult = "{\"success\" : 1, \"message\" : \"Device Id Updated successfully\", \"data\" : \"\"}";
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Device Id Not Updated \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"Device Id Not Updated \", \"data\" : \"\"}";
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void customerLedger(string userType, string userId,string fromDate,string toDate)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtUser = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "WS_CustomerLedger";
            cmd.Parameters.AddWithValue("@id", Convert.ToInt64(userId) );
            cmd.Parameters.AddWithValue("@userType", userType );
            cmd.Parameters.AddWithValue("@from", fromDate);
            cmd.Parameters.AddWithValue("@to", toDate);         

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dtUser);
            con.Close();
            if (dtUser != null)
            {
                if (dtUser.Rows.Count > 0)
                {
                     
                        string output = DataTableToJSONWithJavaScriptSerializer(dtUser);
                        object Total = dtUser.Compute("Sum(totalamount)", string.Empty);
                        object paid = dtUser.Compute("Sum(paidamount)", string.Empty);
                        object remaining = dtUser.Compute("Sum(remaining)", string.Empty);
                        string t = "Total :" + Total.ToString() + ",paid :" + paid + ",Remaiining :" + remaining;
                        finalResult = "{\"success\" : 1, \"message\" : \"Data Found\", \"Total\" : \"" + Total.ToString() + "\", \"paid\" : \"" + paid.ToString() + "\", \"remaining\" : \"" + remaining.ToString() + "\", \"data\" :" + output + "}";
                     
                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"Incorrect User Name & Password\", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Incorrect User Name & Password\", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void selectAllState()
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtState = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "getState_byCountryId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@countryid", Convert.ToInt64("1"));
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtState);


            if (dtState != null)
            {
                if (dtState.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtState);
                    finalResult = "{\"success\" : 1, \"message\" : \" State Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No   Data Not Exits  \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Data  Exits  \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Data Exits  \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void selectAllDistrict_bystateId(string stateId)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtState = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "getDistrict_byStateId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stateid", Convert.ToInt64(stateId));
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtState);


            if (dtState != null)
            {
                if (dtState.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtState);
                    finalResult = "{\"success\" : 1, \"message\" : \" District Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No   Data Not Exits  \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"No Data  Exits  \", \"data\" : \"\"}";
            }
        }
        catch (Exception)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"No Data Exits  \", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SelectAllSlider()
    {
        string finalResult = string.Empty;
        try
        {
            DataTable dtBanks = (new Cls_slider_b().SelectAll());
            if (dtBanks != null)
            {
                if (dtBanks.Rows.Count > 0)
                {
                    string output = DataTableToJSONWithJavaScriptSerializer(dtBanks);
                    finalResult = "{\"success\" : 1, \"message\" : \" Slider All Data\", \"data\" :" + output + "}";

                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"No Slider \", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \" No Slider \", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SalesmanLogin(string username, string password)
    {
        string finalResult = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtUser = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SalesmanLogin";
            cmd.Parameters.AddWithValue("@UserName", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dtUser);
            con.Close();
            if (dtUser != null)
            {
                if (dtUser.Rows.Count > 0)
                {
                    //bool Status = Convert.ToBoolean(dtUser.Rows[0]["isactive"]);
                    //if (Status)
                    //{
                        //UpdateLatitudeLongitudeUsingUserId(Convert.ToInt64(dtUser.Rows[0]["uid"]), Convert.ToString(dtUser.Rows[0]["type"]), Latitude, Longitude);
                        string output = DataTableToJSONWithJavaScriptSerializer(dtUser);
                        finalResult = "{\"success\" : 1, \"message\" : \"Login Successfully\", \"data\" :" + output + "}";
                    //}
                    //else
                    //{
                    //    finalResult = "{\"success\" : 0, \"message\" : \"Your Account Under Admin Observation.Please wait for admin confirmation\", \"data\" : \"\"}";
                    //}
                }
                else
                {
                    finalResult = "{\"success\" : 0, \"message\" : \"Incorrect User Name & Password\", \"data\" : \"\"}";
                }
            }
            else
            {
                finalResult = "{\"success\" : 0, \"message\" : \"Incorrect User Name & Password\", \"data\" : \"\"}";
            }
        }
        catch (Exception ex)
        {
            finalResult = "{\"success\" : 0, \"message\" : \"" + ex.Message + "\", \"data\" : \"\"}";
        }
        finally
        {
            con.Close();
        }

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();
    }


}
