using BusinessLayer;
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

public partial class vendorpayment : System.Web.UI.Page
{
    common ocommon = new common();
    DataTable dtProduct = new DataTable();

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    String usermail = String.Empty;
    String createdby = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["userid"]
        if (!String.IsNullOrEmpty(Session["userid"].ToString()))
            createdby = Session["userid"].ToString();
        //Bind();

        if (!IsPostBack)
        {
            Bind();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Supplier Payment";
            txt_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }
        
    }

    private void Clear() {
        ddlpaytype.SelectedIndex = 0;
        txtamount.Text = string.Empty;
        txtnote.Text = string.Empty;
        txt_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
        ddlVendor.SelectedIndex = 0;
    }

    private void Bind()
    {
        DataTable dtVendors = new DataTable();
        
        try
        {

            Cls_VendorMaster_b objVendors = new Cls_VendorMaster_b();
            dtVendors = objVendors.SelectAll();
        }
        catch { }
        finally { con.Close(); }

        if (dtVendors != null)
        {
            if (dtVendors.Rows.Count > 0)
            {
                ddlVendor.DataSource = dtVendors;
                ddlVendor.DataTextField = "vendorname";
                ddlVendor.DataValueField = "vid";
                ddlVendor.DataBind();
                System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Supplier--", "0");
                ddlVendor.Items.Insert(0, objListItem);

            }
            else
            {
                ddlVendor.DataSource = dtVendors;
                ddlVendor.DataTextField = "vendorname";
                ddlVendor.DataValueField = "vid";
                ddlVendor.DataBind();
            }
        }
        else
        {
            ddlVendor.DataSource = dtVendors;
            ddlVendor.DataTextField = "vendorname";
            ddlVendor.DataValueField = "vid";
            ddlVendor.DataBind();
        }

        
    }
    
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        DataTable dtProduct = new DataTable();

        

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "potransactions_insert",
                CommandType = CommandType.StoredProcedure,
                Connection = con
            };

            SqlParameter param = new SqlParameter
            {
                ParameterName = "@id",
                Value = 0,
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.InputOutput
            };
            cmd.Parameters.Add(param);

            cmd.Parameters.AddWithValue("@vendorid",ddlVendor.SelectedValue);
            cmd.Parameters.AddWithValue("@amount", txtamount.Text.Trim());
            cmd.Parameters.AddWithValue("@comment", txtnote.Text.Trim() );
            cmd.Parameters.AddWithValue("@paymenttype", ddlpaytype.SelectedValue);



            con.Open();
            cmd.ExecuteNonQuery();
            Result = Convert.ToInt64(param.Value);
            if (Result > 0) {
                Clear();
                spnMessgae.InnerText = "Payment Successfull !!!";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            //return result;
        }
        finally
        {
            con.Close();
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/");
    }
    
}