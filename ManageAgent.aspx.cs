using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ManageAgent : System.Web.UI.Page
{
    string agentFrontPath = "~/uploads/agent/front/";
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindAgent();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Manage Agent";
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Agent Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Agent Inserted Successfully";
        }
    }

    private void BindAgent()
    {
        DataTable dtAgent = (new Cls_agentmaster_b().SelectAll());
        if (dtAgent != null)
        {
            if (dtAgent.Rows.Count > 0)
            {
                repAgent.DataSource = dtAgent;
                repAgent.DataBind();
            }
            else
            {
                repAgent.DataSource = null;
                repAgent.DataBind();
            }
        }
        else
        {
            repAgent.DataSource = null;
            repAgent.DataBind();
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditAgent.aspx"));
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        //  Int32 BankCount = int.Parse((item.FindControl("lblBankCount") as Label).Text);
        spnMessage.Visible = true;
        //if (BankCount.ToString() == "0")
        //{
        Int32 AgentId = int.Parse((item.FindControl("lblAgentId") as Label).Text);
        bool yes = (new Cls_agentmaster_b().Delete(AgentId));

        if (yes)
        {
            BindAgent();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Agent Deleted Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Agent Not Deleted";
        }
        //}
        //else
        //{
        //    spnMessage.Style.Add("color", "red");
        //    spnMessage.InnerText = "In this bank category added..so you can not delete.";
        //}
    }

    //protected void IsActive_CheckedChanged(object sender, EventArgs e)
    //{
    //    RepeaterItem item = (sender as CheckBox).Parent as RepeaterItem;
    //    Int32 BankId = int.Parse((item.FindControl("lblBankId") as Label).Text);
    //    bool chkSelected = Convert.ToBoolean((item.FindControl("IsActive") as CheckBox).Checked);
    //    bool yes = (new Cls_bankmaster_b().IsActive(BankId, chkSelected));
    //    if (yes)
    //    {
    //        BindBank();
    //        spnMessage.Style.Add("color", "green");
    //        spnMessage.InnerText = "Bank Updated Successfully";
    //    }
    //    else
    //    {
    //        spnMessage.Style.Add("color", "red");
    //        spnMessage.InnerText = "Bank Not Updated";
    //    }
    //}

    protected void repBank_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addeditAgent.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "aid").ToString(), true));

            Image imgCategory = (Image)e.Item.FindControl("imgProfile");
            imgCategory.ImageUrl = agentFrontPath + DataBinder.Eval(e.Item.DataItem, "img").ToString();

        }
    }

}