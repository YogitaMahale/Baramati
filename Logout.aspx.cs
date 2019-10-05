using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    common ocommom = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        //spnAdmin.InnerHtml = common.projectAdmin;
        try
        {
            if (Session["userid"] != null)
            {
                string UserID = Session["userid"].ToString();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                string s = "select username,password from AdminLogin where AdminID=" + UserID + "";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(s, con);
                da.Fill(dt);
                string Username = "", pass = "";
                if (dt.Rows[0][0].ToString() != "")
                {
                    Username = dt.Rows[0]["username"].ToString();
                    pass = dt.Rows[0]["password"].ToString();
                }
                Cls_userregistration_b obj = new Cls_userregistration_b();
                Int64 Result = obj.WebsiteUser_Status(Username, pass, false);

            }
        }
        catch { }
        finally { }

        Response.Redirect(Page.ResolveUrl("~/Default.aspx?logout=yes"));
    }
}