using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class manageEmployee : System.Web.UI.Page
{
    string agentFrontPath = "~/uploads/emp/front/";
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindEmployee();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Manage Employee";
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Employee Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Employee Inserted Successfully";
        }
    }

    private void BindEmployee()
    {
        DataTable dtEmployee = (new Cls_Employee_b ().SelectAll());
        if (dtEmployee != null)
        {
            if (dtEmployee.Rows.Count > 0)
            {
                repemployee.DataSource = dtEmployee;
                repemployee.DataBind();
            }
            else
            {
                repemployee.DataSource = null;
                repemployee.DataBind();
            }
        }
        else
        {
            repemployee.DataSource = null;
            repemployee.DataBind();
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditEmployee.aspx"));
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        //  Int32 BankCount = int.Parse((item.FindControl("lblBankCount") as Label).Text);
        spnMessage.Visible = true;
        //if (BankCount.ToString() == "0")
        //{
        Int32 AgentId = int.Parse((item.FindControl("lblAgentId") as Label).Text);
        bool yes = (new Cls_Employee_b().Delete(AgentId));

        if (yes)
        {
            BindEmployee ();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Employee Deleted Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Employee Not Deleted";
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
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addeditEmployee.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "id").ToString(), true));

            Image imgCategory = (Image)e.Item.FindControl("imgProfile");
            imgCategory.ImageUrl = agentFrontPath + DataBinder.Eval(e.Item.DataItem, "img").ToString();

        }
    }


}