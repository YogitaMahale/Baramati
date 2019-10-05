using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ManageVendor : System.Web.UI.Page
{
    string agentFrontPath = "~/uploads/vendor/front/";
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindAgent();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Manage Vendor";
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Vendor Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Vendor Inserted Successfully";
        }
    }

    private void BindAgent()
    {
        DataTable dtVendor = (new Cls_VendorMaster_b().SelectAll());
        if (dtVendor != null)
        {
            if (dtVendor.Rows.Count > 0)
            {
                repVendor.DataSource = dtVendor;
                repVendor.DataBind();
            }
            else
            {
                repVendor.DataSource = null;
                repVendor.DataBind();
            }
        }
        else
        {
            repVendor.DataSource = null;
            repVendor.DataBind();
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditVendor.aspx"));
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        //  Int32 BankCount = int.Parse((item.FindControl("lblBankCount") as Label).Text);
        spnMessage.Visible = true;
        //if (BankCount.ToString() == "0")
        //{
        Int32 AgentId = int.Parse((item.FindControl("lblAgentId") as Label).Text);
        bool yes = (new Cls_VendorMaster_b().Delete(AgentId));

        if (yes)
        {
            BindAgent();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Vendor Deleted Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Vendor Not Deleted";
        }
        //}
        //else
        //{
        //    spnMessage.Style.Add("color", "red");
        //    spnMessage.InnerText = "In this bank category added..so you can not delete.";
        //}
    }


    protected void repBank_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addeditVendor.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "vid").ToString(), true));

            //Image imgCategory = (Image)e.Item.FindControl("imgProfile");
            //imgCategory.ImageUrl = agentFrontPath + DataBinder.Eval(e.Item.DataItem, "img").ToString();

        }
    }


}