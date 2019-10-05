using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["logout"] == "yes")
        {
            AllSessionNull();
        }
    }

    public void Clear()
    {
        txtUserName.Text = string.Empty;
        txtPassword.Text = string.Empty;
    }

    private void AllSessionNull()
    {
        Session["userid"] = null;
        Session["usertype"] = null;

    }

    private bool SendMail(string Name)
    {
        bool send = false;
        MailMessage mail = new MailMessage();
        //mail.To.Add(ConfigurationManager.AppSettings["LoginToEmail"].ToString());
        mail.To.Add("Swapnil.kshatriyask@gmail.com");

        mail.From = new MailAddress("demo@moryatools.com", "Morya Tools App");
        mail.Subject = Name + " Login Morya Tool Website  7 PM Details";
        StringBuilder strBul = new StringBuilder("<div>");
        strBul = strBul.Append("<div>Dear Sir,</div>");
        strBul = strBul.Append("<br />");
        strBul = strBul.Append("<br />");
        strBul = strBul.Append("<div>" + Name + " Login Morya Tool Website After 7 PM , Login Date & Time " + System.DateTime.Now.ToString() + "</div>");
        strBul = strBul.Append("<br />");
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

    protected void btnLogin_Click(object sender, EventArgs e)
    {
       
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            string query = string.Empty;
            if (txtUserName.Text != string.Empty && txtPassword.Text != string.Empty)
            {
                query = "select * from AdminLogin where isdelete=0 and lower(username)=lower('" + txtUserName.Text.Trim() + "') and password='" + txtPassword.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(query);
                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dtUser = new DataTable();
                con.Open();
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                sda.Fill(dtUser);
                if (dtUser != null)
                {
                    if (dtUser.Rows.Count > 0)
                    {
                        //if (System.DateTime.Now.Hour <= 19)
                        //  { 
                        //	}
                        // else
                        // {

                        
                      // SendMail(Convert.ToString(dtUser.Rows[0]["name"].ToString().ToUpper()));
                       

                        //}
                        //---update Status----


                        Cls_userregistration_b obj = new Cls_userregistration_b();
                        Int64 Result = obj.WebsiteUser_Status(txtUserName.Text, txtPassword.Text, true);


                        Session.Timeout = 120;
                        Session["userid"] = Convert.ToString(dtUser.Rows[0]["adminid"]);
                        Session["usertype"] = Convert.ToString(dtUser.Rows[0]["usertype"]);
                        Session["nameuser"] = Convert.ToString(dtUser.Rows[0]["name"]);
                        Session["usermail"] = Convert.ToString(dtUser.Rows[0]["email"]);
                        Response.Redirect(Page.ResolveUrl("~/dashboard.aspx"));
                    }
                    else
                    {
                        bMsg.InnerText = "Please enter correct user name & password !!!";
                        Clear();
                    }
                }
                else
                {
                    bMsg.InnerText = "Please enter correct user name & password !!!";
                    Clear();
                }
            }
            else
            {
                bMsg.InnerText = "Please enter user name & password !!!";
            }
        }
        catch { }
        finally { con.Close(); }
    }

}