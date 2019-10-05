using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class changespassword : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            if (Session["userid"] != null)
            {
                string UserID = Session["userid"].ToString();
                string s = "update AdminLogin set Password='" + txtPassword.Text.Trim() + "' where AdminID=" + UserID + "";
                SqlCommand cmd = new SqlCommand(s, con);
                int t = cmd.ExecuteNonQuery();
                if (t > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Password Change Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Password Not Change')", true);
                }
                txtPassword.Text = string.Empty;
                txtConfirmPassword.Text = string.Empty;
              }
        }
        catch { }
        finally { con.Close(); }
    }
}