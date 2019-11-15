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

public partial class dealerledger : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);

    common ocommon = new common();
    DataTable dtTransactions = new DataTable();
    DataTable dtCustomers = new DataTable("Customers");
    DataTable dtOrderDetails = new DataTable();
    DataSet dsDealerOrderAndTransactions = new DataSet();

    Decimal grandtotal = 0, granddue = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String ticks = DateTime.Now.Ticks.ToString();
            ticks = ticks.Substring(ticks.Length - 2, 2);

            BindCustomers();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Customer Ledger";
        }


    }
    private void BindCustomers()
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealer_SelectAllAdmin";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtCustomers);


            if (dtCustomers != null)
            {
                if (dtCustomers.Rows.Count > 0)
                {
                    ddlDealer.DataSource = dtCustomers;
                    ddlDealer.DataTextField = "name";
                    ddlDealer.DataValueField = "did";
                    ddlDealer.DataBind();
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Customer--", "0");
                    ddlDealer.Items.Insert(0, objListItem);
                }
                else
                {
                    ddlDealer.DataSource = dtCustomers;
                    ddlDealer.DataTextField = "name";
                    ddlDealer.DataValueField = "did";
                    ddlDealer.DataBind();
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Customer--", "0");
                    ddlDealer.Items.Insert(0, objListItem);
                }
            }
            {
                ddlDealer.DataSource = dtCustomers;
                ddlDealer.DataTextField = "name";
                ddlDealer.DataValueField = "did";
                ddlDealer.DataBind();
                System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Customer--", "0");
                ddlDealer.Items.Insert(0, objListItem);
            }
        }
        catch { }
        finally { con.Close(); }

    }



    protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "getDealerOrdersAndTransactions",
                CommandType = CommandType.StoredProcedure
            };
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            cmd.Parameters.AddWithValue("@did", ddlDealer.SelectedValue);
            con.Open();
            sda.Fill(dsDealerOrderAndTransactions);

            //SqlCommand pocmd = new SqlCommand
            //{
            //    CommandText = "getPurchaseOrderDetails",
            //    CommandType = CommandType.StoredProcedure
            //};
            //SqlDataAdapter posda = new SqlDataAdapter();
            //pocmd.Connection = con;
            //posda.SelectCommand = pocmd;
            //pocmd.Parameters.AddWithValue("@vendorid", ddlDealer.SelectedValue);
            //posda.Fill(dtPoDetails);


        }
        catch { }
        finally { con.Close(); }


        if (dsDealerOrderAndTransactions != null)
        {
            if (dsDealerOrderAndTransactions.Tables[0] != null)
            {
                if (dsDealerOrderAndTransactions.Tables[0].Rows.Count > 0)
                {

                    ViewState["Transactions"] = dsDealerOrderAndTransactions.Tables[0];
                    repCategory.DataSource = dsDealerOrderAndTransactions.Tables[0];
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
            if (dsDealerOrderAndTransactions.Tables[1] != null)
            {
                if (dsDealerOrderAndTransactions.Tables[1] != null)
                {
                    if (dsDealerOrderAndTransactions.Tables[1].Rows.Count > 0)
                    {

                        ViewState["OrderDetails"] = dsDealerOrderAndTransactions.Tables[1];
                        reppodetails.DataSource = dsDealerOrderAndTransactions.Tables[1];
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