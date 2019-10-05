using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class managewebsiteusers : System.Web.UI.Page
{
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindWebUserList();
        }
        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "User Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "User Inserted Successfully";
        }
    }

    private void BindWebUserList()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "WebsiteUser_SelectAllAdmin";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        DataTable dtUserType = new DataTable();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtUserType);
        //SqlCommand cmd = new SqlCommand("select * from AdminLogin Where isdelete=0");
        //SqlDataAdapter sda = new SqlDataAdapter();
        //DataTable dtUserType = new DataTable();
        //con.Open();
        //cmd.Connection = con;
        //sda.SelectCommand = cmd;
        //sda.Fill(dtUserType);
        if (dtUserType != null)
        {
            if (dtUserType.Rows.Count > 0)
            {
                repWebSiteUser.DataSource = dtUserType;
                repWebSiteUser.DataBind();
            }
            else
            {
                repWebSiteUser.DataSource = null;
                repWebSiteUser.DataBind();
            }
        }
        else
        {
            repWebSiteUser.DataSource = null;
            repWebSiteUser.DataBind();
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        Int64 AdminId = int.Parse((item.FindControl("lblAdminId") as Label).Text);
        int yes = Deleteuser(AdminId);
        spnMessage.Visible = true;
        if (yes > 0)
        {
            BindWebUserList();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "User Deleted Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "User Not Deleted";
        }
    }

    private int Deleteuser(Int64 Adminid)
    {
        int result = 0;
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "Update adminlogin SET Isdelete=1 where adminid=" + Adminid;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = ConnectionString;
        try
        {
            ConnectionString.Open();
            result = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            result = 0;
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }

        return result;
    }

    protected void repWebSiteUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addeditwebuser.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "adminid").ToString(), true));
        }
    }
    protected void btnAddEditUser_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditwebuser.aspx"));
    }
}