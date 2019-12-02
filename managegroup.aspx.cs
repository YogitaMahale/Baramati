using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class managegroup : System.Web.UI.Page
{
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGroup();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Manage Bank";
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Group Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Group Inserted Successfully";
        }
    }

    private void BindGroup()
    {
        DataTable dtGroup = (new Cls_groupmaster_b().SelectAllAdmin());
        if (dtGroup != null)
        {
            if (dtGroup.Rows.Count > 0)
            {
                repBank.DataSource = dtGroup;
                repBank.DataBind();
            }
            else
            {
                repBank.DataSource = null;
                repBank.DataBind();
            }
        }
        else
        {
            repBank.DataSource = null;
            repBank.DataBind();
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditgroup.aspx"));
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        Int32 GroupCount = int.Parse((item.FindControl("lblGroupCount") as Label).Text);
        spnMessage.Visible = true;
        if (GroupCount.ToString() == "0")
        {
            Int32 GroupId = int.Parse((item.FindControl("lblGroupId") as Label).Text);
            bool yes = (new Cls_groupmaster_b().Delete(GroupId));

            if (yes)
            {
                BindGroup();
                spnMessage.Style.Add("color", "green");
                spnMessage.InnerText = "Group Deleted Successfully";
            }
            else
            {
                spnMessage.Style.Add("color", "red");
                spnMessage.InnerText = "Group Not Deleted";
            }
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "In this group sizes are added..so you can not delete.";
        }
    }

    protected void IsActive_CheckedChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as CheckBox).Parent as RepeaterItem;
        Int32 GroupId = int.Parse((item.FindControl("lblGroupId") as Label).Text);
        bool chkSelected = Convert.ToBoolean((item.FindControl("IsActive") as CheckBox).Checked);
        bool yes = (new Cls_groupmaster_b().IsActive(GroupId, chkSelected));
        if (yes)
        {
            BindGroup();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Group Updated Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Group Not Updated";
        }
    }

    protected void repBank_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addeditgroup.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "id").ToString(), true));
        }
    }
}