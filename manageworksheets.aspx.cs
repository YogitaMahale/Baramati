using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class manageworksheets : System.Web.UI.Page
{
    //string agentFrontPath = "~/uploads/vendor/front/";
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindWorkSheets();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Worksheets";
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Worksheet Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Worksheet Created Successfully";
        }
        else if (Request.QueryString["mode"] == "a")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Final Production Saved Successfully";
        }
    }

    private void BindWorkSheets()
    {
        DataTable dtWorksheets = new Cls_worksheetmaster_b().SelectAll(new worksheetmaster());


        if (dtWorksheets != null)
        {
            if (dtWorksheets.Rows.Count > 0)
            {
                repWorksheet.DataSource = dtWorksheets;
                repWorksheet.DataBind();
            }
            else
            {
                repWorksheet.DataSource = null;
                repWorksheet.DataBind();
            }
        }
        else
        {
            repWorksheet.DataSource = null;
            repWorksheet.DataBind();
        }
    }

    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addWorksheet.aspx"));
    }

    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        //RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        ////  Int32 BankCount = int.Parse((item.FindControl("lblBankCount") as Label).Text);
        //spnMessage.Visible = true;
        ////if (BankCount.ToString() == "0")
        ////{
        //Int32 AgentId = int.Parse((item.FindControl("lblAgentId") as Label).Text);
        //bool yes = (new Cls_VendorMaster_b().Delete(AgentId));

        //if (yes)
        //{
        //    BindAgent();
        //    spnMessage.Style.Add("color", "green");
        //    spnMessage.InnerText = "Vendor Deleted Successfully";
        //}
        //else
        //{
        //    spnMessage.Style.Add("color", "red");
        //    spnMessage.InnerText = "Vendor Not Deleted";
        //}
        //}
        //else
        //{
        //    spnMessage.Style.Add("color", "red");
        //    spnMessage.InnerText = "In this bank category added..so you can not delete.";
        //}
    }


    protected void RepBank_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addWorksheet.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "vid").ToString(), true));



            //Image imgCategory = (Image)e.Item.FindControl("imgProfile");
            //imgCategory.ImageUrl = agentFrontPath + DataBinder.Eval(e.Item.DataItem, "img").ToString();

        }
    }

    




    protected void repWorksheet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //NavigateUrl = '<%# Eval("id","~/articleproduction.aspx?id={0}") %>'

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink lblAgentId = (HyperLink)e.Item.FindControl("lblAgentId");
            lblAgentId.NavigateUrl = Page.ResolveUrl("~/addarticleproduction.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "id").ToString(), true));

            HyperLink lblMobileno = (HyperLink)e.Item.FindControl("lblMobileno");
            lblMobileno.NavigateUrl = Page.ResolveUrl("~/addarticleproduction.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "id").ToString(), true));

            


        }

    }
}