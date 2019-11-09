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
using BusinessLayer;
public partial class editdealerprofile : System.Web.UI.Page
{
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        if (!Page.IsPostBack)
        {
            BindUser();
            Bindstate();
            if (Request.QueryString["id"] != null)
            {
                hPageTitle.InnerText = "Update Dealer";
                Page.Title = "Update Dealer";
                btnUpdate.Text = "Update";
                BindDealer(ocommon.Decrypt(Convert.ToString(Request.QueryString["id"]), true));
                txtbalance.ReadOnly = true;

            }
            else
            {
                hPageTitle.InnerText = "New Dealer";
                Page.Title = "New Dealer";
                btnUpdate.Text = "Add";
                //BindDealer(ocommon.Decrypt(Convert.ToString(Request.QueryString["id"]), true));

            }
        }
    }

    private void BindDealer(string DealerId)
    {
        dealermaster objdealermaster = (new Cls_dealermaster_b().SelectById(Convert.ToInt64(DealerId)));
        if (objdealermaster != null)
        {
            txtDealerName.Text = objdealermaster.name;
            txtUserLoginNo.Text = objdealermaster.userloginmobileno;
            //txtUserLoginNo.Enabled = false;
            txtPassword.Text = objdealermaster.password;
            txtPassword.Enabled = false;
            txtWhatappNo.Text = objdealermaster.whatappno;
            txtEmailId.Text = objdealermaster.email;
            txtGSTNO.Text = objdealermaster.gstno;
            txtaadharno.Text = objdealermaster.aadharno;
            txtpanno.Text = objdealermaster.panno;
            txtAddress1.Text = objdealermaster.address1;
            txtAddress2.Text = objdealermaster.address2;
            txtCity.Text = objdealermaster.city;
            txtSate.SelectedValue = objdealermaster.state;
            txtSate_SelectedIndexChanged(null, null);
            ddldistrict.SelectedValue = objdealermaster.district.ToString();
            ddlUser.SelectedValue = objdealermaster.agentid.ToString();
            txtbalance.Text = objdealermaster.balance.ToString();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        dealermaster objdealermaster = new dealermaster();
        objdealermaster.name = txtDealerName.Text;
        objdealermaster.userloginmobileno = txtUserLoginNo.Text;
        objdealermaster.password = txtPassword.Text;
        objdealermaster.whatappno = txtWhatappNo.Text;
        objdealermaster.email = txtEmailId.Text;
        objdealermaster.gstno = txtGSTNO.Text;
        objdealermaster.aadharno = txtaadharno.Text;
        objdealermaster.panno = txtpanno.Text;
        objdealermaster.address1 = txtAddress1.Text;
        objdealermaster.address2 = txtAddress2.Text;
        objdealermaster.city = txtCity.Text;
        objdealermaster.state = txtSate.SelectedValue.ToString();
        objdealermaster.district = Convert.ToInt64(ddldistrict.SelectedValue.ToString());
        objdealermaster.agentid = Convert.ToInt64(ddlUser.SelectedValue.ToString());
        objdealermaster.balance = Convert.ToDecimal(txtbalance.Text.Trim());
        if (Request.QueryString["id"] != null)
        {
            objdealermaster.did = Convert.ToInt64(ocommon.Decrypt(Convert.ToString(Request.QueryString["id"]), true));
            Int64 DID = Update(objdealermaster);
            if (DID > 0)
            {
                Response.Redirect(Page.ResolveUrl("~/managedealer.aspx?mode=u"));
            }
            else
            {
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Dealer Not Updated";
            }
        }
        else {
            Int64 Result = (new Cls_dealermaster_b().Insert(objdealermaster));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/managedealer.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Dealer Information Not Saved...";
            }

        }
    }

    private void Clear()
    {
        txtDealerName.Text = String.Empty;
        txtUserLoginNo.Text = String.Empty;
        //txtUserLoginNo.Enabled = false;
        txtPassword.Text = String.Empty;
        //txtPassword.Enabled = String.Empty;
        txtWhatappNo.Text = String.Empty;
        txtEmailId.Text = String.Empty;
        txtGSTNO.Text = String.Empty;
        txtaadharno.Text = String.Empty;
        txtpanno.Text = String.Empty;
        txtAddress1.Text = String.Empty;
        txtAddress2.Text = String.Empty;
        txtCity.Text = String.Empty;
        txtSate.SelectedIndex = 0;
        ddlUser.SelectedIndex = 0;
        txtbalance.Text = "0";
    }

    public Int64 Update(dealermaster objdealermaster)
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealermaster_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@did";
            param.Value = objdealermaster.did;
            param.SqlDbType = SqlDbType.BigInt;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);

            cmd.Parameters.AddWithValue("@name", objdealermaster.name);
            cmd.Parameters.AddWithValue("@userloginmobileno", objdealermaster.userloginmobileno);
            cmd.Parameters.AddWithValue("@whatappno", objdealermaster.whatappno);
            cmd.Parameters.AddWithValue("@email", objdealermaster.email);
            cmd.Parameters.AddWithValue("@gstno", objdealermaster.gstno);
            cmd.Parameters.AddWithValue("@aadharno", objdealermaster.aadharno);
            cmd.Parameters.AddWithValue("@panno", objdealermaster.panno);
            cmd.Parameters.AddWithValue("@address1", objdealermaster.address1);
            cmd.Parameters.AddWithValue("@address2", objdealermaster.address2);
            cmd.Parameters.AddWithValue("@city", objdealermaster.city);
            cmd.Parameters.AddWithValue("@state", objdealermaster.state);
            cmd.Parameters.AddWithValue("@FK_agentId", objdealermaster.agentid);
            cmd.Parameters.AddWithValue("@district", objdealermaster.district);
            
            ConnectionString.Open();
            cmd.ExecuteNonQuery();
            result = Convert.ToInt64(param.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
        finally
        {
            ConnectionString.Close();
        }
        return result;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/managedealer.aspx"));
    }


    private void BindUser()
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        //SqlCommand cmd = new SqlCommand("select usertype,lower(usertype) as userid from usertypes");
        //SqlDataAdapter sda = new SqlDataAdapter();
        //DataTable dtUser = new Cls_userregistration_b().SelectAll();
        //con.Open();
        //cmd.Connection = con;
        //sda.SelectCommand = cmd;
        //sda.Fill(dtUserType);

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "WebsiteUser_SelectAllAdmin";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        DataTable dtUser = new DataTable();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtUser);
        if (dtUser != null)
        {
            if (dtUser.Rows.Count > 0)
            {
                ddlUser.DataSource = dtUser;
                ddlUser.DataTextField = "name";
                ddlUser.DataValueField = "AdminID";
                ddlUser.DataBind();
                ListItem objListItem = new ListItem("--Select User--", "-1");
                ddlUser.Items.Insert(0, objListItem);
            }
            else
            {
                ddlUser.DataSource = dtUser;
                ddlUser.DataTextField = "UserName";
                ddlUser.DataValueField = "AdminID"; 
                ddlUser.DataBind();
                ListItem objListItem = new ListItem("--Select User--", "-1");
                ddlUser.Items.Insert(0, objListItem);
            }
        }
        else
        {
            ddlUser.DataSource = dtUser;
            ddlUser.DataTextField = "UserName";
            ddlUser.DataValueField = "AdminID"; 
            ddlUser.DataBind();
            ListItem objListItem = new ListItem("--Select User--", "-1");
            ddlUser.Items.Insert(0, objListItem);
        }
    }
    private void Bindstate()
    {
       

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "getState_byCountryId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@countryid", Convert.ToInt64("1"));
        cmd.Connection = con;
        DataTable dtUser = new DataTable();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtUser);
        if (dtUser != null)
        {
            if (dtUser.Rows.Count > 0)
            {
                txtSate .DataSource = dtUser;
                txtSate.DataTextField = "statename";
                txtSate.DataValueField = "id";
                txtSate.DataBind();
                ListItem objListItem = new ListItem("--Select State--", "-1");
                txtSate.Items.Insert(0, objListItem);
            }
            else
            {
                txtSate.DataSource = dtUser;
                txtSate.DataTextField = "statename";
                txtSate.DataValueField = "id";
                txtSate.DataBind();
                ListItem objListItem = new ListItem("--Select State--", "-1");
                txtSate.Items.Insert(0, objListItem);
            }
        }
        else
        {
            txtSate.DataSource = dtUser;
            txtSate.DataTextField = "statename";
            txtSate.DataValueField = "id";
            txtSate.DataBind();
            ListItem objListItem = new ListItem("--Select State--", "-1");
            txtSate.Items.Insert(0, objListItem);
        }
    }
    private void BindDistrict(Int64 stateId)
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "getDistrict_byStateId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@stateid", Convert.ToInt64(stateId));
        cmd.Connection = con;
        DataTable dtUser = new DataTable();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtUser);
        if (dtUser != null)
        {
            if (dtUser.Rows.Count > 0)
            {
                ddldistrict .DataSource = dtUser;
                ddldistrict.DataTextField = "cityname";
                ddldistrict.DataValueField = "id";
                ddldistrict.DataBind();
                ListItem objListItem = new ListItem("--Select District--", "-1");
                ddldistrict.Items.Insert(0, objListItem);
            }
            else
            {
                ddldistrict.DataSource = dtUser;
                ddldistrict.DataTextField = "cityname";
                ddldistrict.DataValueField = "id";
                ddldistrict.DataBind();
                ListItem objListItem = new ListItem("--Select District--", "-1");
                ddldistrict.Items.Insert(0, objListItem);
            }
        }
        else
        {
            ddldistrict.DataSource = dtUser;
            ddldistrict.DataTextField = "cityname";
            ddldistrict.DataValueField = "id";
            ddldistrict.DataBind();
            ListItem objListItem = new ListItem("--Select District--", "-1");
            ddldistrict.Items.Insert(0, objListItem);
        }
    }
    protected void txtSate_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDistrict(Convert.ToInt64(txtSate.SelectedValue.ToString()));
    }
}