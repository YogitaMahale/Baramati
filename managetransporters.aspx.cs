using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class managetransporters : System.Web.UI.Page
{
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindTransporter();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Manage Transporter";
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Transporter Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Transporter Saved Successfully";
        }
    }

    private void BindTransporter()
    {

        DataTable dtTransporter = (new Cls_transporter_b().SelectAll());
        if (dtTransporter != null)
        {
            if (dtTransporter.Rows.Count > 0)
            {
                repAgent.DataSource = dtTransporter;
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
        Response.Redirect(Page.ResolveUrl("~/addedittransporter.aspx"));
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        spnMessage.Visible = true;
        Int32 Id = int.Parse((item.FindControl("lblid") as Label).Text);
        bool yes = (new Cls_transporter_b().Delete(Id));

        if (yes)
        {
            BindTransporter();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Transporter Deleted Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Transporter Not Deleted";
        }
        
    }
    
    protected void repBank_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addedittransporter.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "id").ToString(), true));

            
        }
    }

}