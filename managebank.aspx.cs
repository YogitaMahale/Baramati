using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class managebank : System.Web.UI.Page
{
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindBank();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Manage Bank";
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Bank Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Bank Inserted Successfully";
        }
    }

    private void BindBank()
    {
        DataTable dtBank = (new Cls_bankmaster_b().SelectAllAdmin());
        if (dtBank != null)
        {
            if (dtBank.Rows.Count > 0)
            {
                repBank.DataSource = dtBank;
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
        Response.Redirect(Page.ResolveUrl("~/addeditbank.aspx"));
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        Int32 BankCount = int.Parse((item.FindControl("lblBankCount") as Label).Text);
        spnMessage.Visible = true;
        if (BankCount.ToString() == "0")
        {
            Int32 BankId = int.Parse((item.FindControl("lblBankId") as Label).Text);
            bool yes = (new Cls_bankmaster_b().Delete(BankId));

            if (yes)
            {
                BindBank();
                spnMessage.Style.Add("color", "green");
                spnMessage.InnerText = "Bank Deleted Successfully";
            }
            else
            {
                spnMessage.Style.Add("color", "red");
                spnMessage.InnerText = "Bank Not Deleted";
            }
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "In this bank category added..so you can not delete.";
        }
    }

    protected void IsActive_CheckedChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as CheckBox).Parent as RepeaterItem;
        Int32 BankId = int.Parse((item.FindControl("lblBankId") as Label).Text);
        bool chkSelected = Convert.ToBoolean((item.FindControl("IsActive") as CheckBox).Checked);
        bool yes = (new Cls_bankmaster_b().IsActive(BankId, chkSelected));
        if (yes)
        {
            BindBank();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Bank Updated Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Bank Not Updated";
        }
    }

    protected void repBank_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addeditbank.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "bankid").ToString(), true));
        }
    }
}