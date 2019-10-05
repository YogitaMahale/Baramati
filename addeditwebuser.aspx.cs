using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class addeditwebuser : System.Web.UI.Page
{
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        if (!Page.IsPostBack)
        {
            BindPageAccess();
            if (Request.QueryString["id"] != null)
            {
                hPageTitle.InnerText = "Update User";
                Page.Title = "Update User";
                btnSave.Text = "Update";
                BindUser(ocommon.Decrypt(Convert.ToString(Request.QueryString["id"]), true));
            }
            else
            {
                hPageTitle.InnerText = "Add User";
                Page.Title = "Add User";
                btnSave.Text = "Add";
            }
        }
    }

    private void Clear()
    {
        txtUserName.Text = string.Empty;
        txtUserId.Text = string.Empty;
        txtPassword.Text = string.Empty;
        ddlUserType.SelectedValue = "-1";
        txtEmail.Text = string.Empty;
        txtPhone.Text = string.Empty;
    }

    private void BindPageAccess()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        //SqlCommand cmd = new SqlCommand("select id,pagename from pageaccess");
        SqlCommand cmd = new SqlCommand("select Id as id, SubMenuName as pagename  from SubMenu");
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dtPageAccess = new DataTable();
        con.Open();
        cmd.Connection = con;
        sda.SelectCommand = cmd;
        sda.Fill(dtPageAccess);
        if (dtPageAccess != null)
        {
            if (dtPageAccess.Rows.Count > 0)
            {
                chkList.DataSource = dtPageAccess;
                chkList.DataTextField = "pagename";
                chkList.DataValueField = "id";
                chkList.DataBind();
            }
            else
            {
                chkList.DataSource = dtPageAccess;
                chkList.DataTextField = "pagename";
                chkList.DataValueField = "id";
                chkList.DataBind();
            }
        }
        else
        {
            chkList.DataSource = dtPageAccess;
            chkList.DataTextField = "pagename";
            chkList.DataValueField = "id";
            chkList.DataBind();
        }
    }

    private void BindUserType()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand("select usertype,lower(usertype) as userid from usertypes");
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dtUserType = new DataTable();
        con.Open();
        cmd.Connection = con;
        sda.SelectCommand = cmd;
        sda.Fill(dtUserType);
        if (dtUserType != null)
        {
            if (dtUserType.Rows.Count > 0)
            {
                ddlUserType.DataSource = dtUserType;
                ddlUserType.DataTextField = "usertype";
                ddlUserType.DataValueField = "userid";
                ddlUserType.DataBind();
                ListItem objListItem = new ListItem("--Select User Type--", "-1");
                ddlUserType.Items.Insert(0, objListItem);
            }
            else
            {
                ddlUserType.DataSource = dtUserType;
                ddlUserType.DataTextField = "usertype";
                ddlUserType.DataValueField = "userid";
                ddlUserType.DataBind();
                ListItem objListItem = new ListItem("--Select User Type--", "-1");
                ddlUserType.Items.Insert(0, objListItem);
            }
        }
        else
        {
            ddlUserType.DataSource = dtUserType;
            ddlUserType.DataTextField = "usertype";
            ddlUserType.DataValueField = "userid";
            ddlUserType.DataBind();
            ListItem objListItem = new ListItem("--Select User Type--", "-1");
            ddlUserType.Items.Insert(0, objListItem);
        }
    }

    private void BindUser(string Userid)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand("select * from AdminLogin where AdminId='" + Userid + "'");
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
                txtUserName.Text = dtUser.Rows[0]["name"].ToString();
                txtUserId.Text = dtUser.Rows[0]["UserName"].ToString();
                txtPassword.Text = dtUser.Rows[0]["Password"].ToString();
                //ddlUserType.SelectedValue = dtUser.Rows[0]["usertype"].ToString().ToLower();
                txtEmail.Text = dtUser.Rows[0]["email"].ToString();
                txtPhone.Text = dtUser.Rows[0]["phone"].ToString();
                BindPageAuthority();
            }
            else
            {
                Clear();
            }
        }
        else
        {
            Clear();
        }
    }

    private void BindPageAuthority()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "select * from pageauthority Where adminid=" + ocommon.Decrypt(Convert.ToString(Request.QueryString["id"]), true);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                foreach (ListItem item in chkList.Items)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (item.Value.ToString() == dt.Rows[i]["pageid"].ToString())
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 result = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        if (Request.QueryString["id"] != null)
        {
            string userType = "user";
            cmd.CommandText = "Update adminlogin SET UserName='" + txtUserId.Text.Trim() + "',name='" + txtUserName.Text.Trim() + "',Password='" + txtPassword.Text.Trim() + "', usertype='" + userType + "',email='" + txtEmail.Text.Trim() + "',phone='" + txtPhone.Text.Trim() + "' Where AdminId=" + ocommon.Decrypt(Convert.ToString(Request.QueryString["id"]), true);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            DeletePageAuthority(Convert.ToInt64(ocommon.Decrypt(Convert.ToString(Request.QueryString["id"]), true)));
            foreach (ListItem item in chkList.Items)
            {
                if (item.Selected)
                {
                    InsertPageAuthority(Convert.ToInt64(ocommon.Decrypt(Convert.ToString(Request.QueryString["id"]), true)), Convert.ToInt64(item.Value));
                }
            }
        }
        else
        {
            cmd.CommandText = "AdminLogin_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@adminid";
            param.Value = 0;
            param.SqlDbType = SqlDbType.BigInt;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@UserName", txtUserId.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
            cmd.Parameters.AddWithValue("@usertype", "user");
            cmd.Parameters.AddWithValue("@name", txtUserName.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
            con.Open();
            cmd.ExecuteNonQuery();
            result = Convert.ToInt64(param.Value);
            con.Close();
            foreach (ListItem item in chkList.Items)
            {
                if (item.Selected)
                {
                    InsertPageAuthority(result, Convert.ToInt64(item.Value));
                }
            }
        }
        try
        {
            if (Request.QueryString["id"] != null)
            {
                if (result > 0)
                {
                    Clear();
                    Response.Redirect(Page.ResolveUrl("~/managewebsiteusers.aspx?mode=u"));
                }
                else
                {
                    Clear();
                    spnMessgae.Style.Add("color", "red");
                    spnMessgae.InnerText = "User Not Updated";
                    BindUser(ocommon.Decrypt(Convert.ToString(Request.QueryString["id"]), true));
                }
            }
            else
            {
                if (result > 0)
                {
                    Clear();
                    Response.Redirect(Page.ResolveUrl("~/managewebsiteusers.aspx?mode=i"));
                }
                else
                {
                    Clear();
                    spnMessgae.Style.Add("color", "red");
                    spnMessgae.InnerText = "User Not Inserted";
                }
            }
        }
        catch (Exception ex)
        {
            result = 0;
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/managewebsiteusers.aspx"));
    }
}