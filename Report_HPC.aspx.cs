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

public partial class Report_HPC : System.Web.UI.Page
{
    common ocommon = new common();
    DataTable dtCategory = new DataTable();
    DataTable dtCustomers = new DataTable("Customers");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCategory();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Highest Paying Customers";
        }

        //if (Request.QueryString["mode"] == "u")
        //{
        //    spnMessage.Visible = true;
        //    spnMessage.Style.Add("color", "green");
        //    spnMessage.InnerText = "Purchase Order Updated Successfully";
        //}
        //else if (Request.QueryString["mode"] == "i")
        //{
        //    spnMessage.Visible = true;
        //    spnMessage.Style.Add("color", "green");
        //    spnMessage.InnerText = "Purchase Order Raised!!!";
        //}
    }
    private void BindCategory()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);




        try
        {

            con.Open();


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "getHighestPayingCustomers";
            // cmd.Parameters.AddWithValue("@ProdBrand", ProdBrand);
            //cmd.Parameters.AddWithValue("@password", password);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dtCustomers);
        }
        catch { }
        finally { con.Close(); }


        if (dtCustomers != null)
        {
            if (dtCustomers.Rows.Count > 0)
            {

                ViewState["Customers"] = dtCustomers;
                repCategory.DataSource = dtCustomers;
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
        //foreach (RepeaterItem item in repCategory.Items)
        //{
        //    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
        //    {

        //        Button grn = (Button)item.FindControl("btnGRN");
        //        Button view = (Button)item.FindControl("btnView");
        //        Label status = (Label)item.FindControl("lblStatus");
        //        if (status.Text == "True")
        //        {
        //            view.Text = "CLOSED";
        //            view.CssClass = "btn btn-sm btn-danger";
        //            //view.Enabled = false;

        //            grn.Visible = false;
        //        }

        //    }
        //}
    }

    protected void repCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");



        }
    }


}