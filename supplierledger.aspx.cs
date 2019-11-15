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

public partial class supplierledger : System.Web.UI.Page
{
    common ocommon = new common();
    DataTable dtTransactions = new DataTable();
    DataTable dtSuppliers = new DataTable("Suppliers");
    DataTable dtPoDetails = new DataTable();

    Decimal grandtotal = 0, granddue = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindVendors();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Supplier Ledger";
        }


    }
    private void BindVendors()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            Cls_VendorMaster_b clsbrand = new Cls_VendorMaster_b();
            dtSuppliers = clsbrand.SelectAll();


            if (dtSuppliers != null)
            {
                if (dtSuppliers.Rows.Count > 0)
                {
                    ddlVendor.DataSource = dtSuppliers;
                    ddlVendor.DataTextField = "vendorName";
                    ddlVendor.DataValueField = "vid";
                    ddlVendor.DataBind();
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Vendor--", "0");
                    ddlVendor.Items.Insert(0, objListItem);
                }
                else
                {
                    ddlVendor.DataSource = dtSuppliers;
                    ddlVendor.DataTextField = "vendorName";
                    ddlVendor.DataValueField = "vid";
                    ddlVendor.DataBind();
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Vendor--", "0");
                    ddlVendor.Items.Insert(0, objListItem);
                }
            }
            {
                ddlVendor.DataSource = dtSuppliers;
                ddlVendor.DataTextField = "vendorName";
                ddlVendor.DataValueField = "vid";
                ddlVendor.DataBind();
                System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Vendor--", "0");
                ddlVendor.Items.Insert(0, objListItem);
            }
        }
        catch { }
        finally { con.Close(); }
        
    }

    protected void repCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
    }



    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "getSupplierTransactions",
                CommandType = CommandType.StoredProcedure
            };
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            cmd.Parameters.AddWithValue("@vendorid", ddlVendor.SelectedValue);
            con.Open();
            sda.Fill(dtTransactions);

            SqlCommand pocmd = new SqlCommand
            {
                CommandText = "getPurchaseOrderDetails",
                CommandType = CommandType.StoredProcedure
            };
            SqlDataAdapter posda = new SqlDataAdapter();
            pocmd.Connection = con;
            posda.SelectCommand = pocmd;
            pocmd.Parameters.AddWithValue("@vendorid", ddlVendor.SelectedValue);
            posda.Fill(dtPoDetails);


        }
        catch { }
        finally { con.Close(); }


        if (dtTransactions != null)
        {
            if (dtTransactions.Rows.Count > 0)
            {

                ViewState["Transactions"] = dtTransactions;
                repCategory.DataSource = dtTransactions;
                repCategory.DataBind();
            }
            else
            {
                repCategory.DataSource = null;
                repCategory.DataBind();
            }
        }
        else
        {
            repCategory.DataSource = null;
            repCategory.DataBind();
        }

        if (dtPoDetails != null)
        {
            if (dtPoDetails.Rows.Count > 0)
            {

                ViewState["PoDetails"] = dtPoDetails;
                reppodetails.DataSource = dtPoDetails;
                reppodetails.DataBind();
            }
            else
            {
                reppodetails.DataSource = null;
                reppodetails.DataBind();
            }
        }
        else
        {
            reppodetails.DataSource = null;
            reppodetails.DataBind();
        }



    }

    protected void reppodetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Label gt = (Label)e.Item.FindControl("lbltotalamount");
            Label gd = (Label)e.Item.FindControl("lblpendingamount");

            grandtotal += Convert.ToDecimal(gt.Text);
            granddue += Convert.ToDecimal(gd.Text);
            //HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");



        }

        lblgrandtotal.Text = grandtotal.ToString();
        lblgranddue.Text = granddue.ToString();
    }
}