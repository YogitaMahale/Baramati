using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Manage_OrderStatus : System.Web.UI.Page
{
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindOrderStatus();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Manage Order Status";
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Status Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Status Inserted Successfully";
        }
    }

    private void BindOrderStatus()
    {
        DataTable dtOrderStatus = (new Cls_OrderStatusmaster_b().SelectAll());
        if (dtOrderStatus != null)
        {
            if (dtOrderStatus.Rows.Count > 0)
            {
                repOrderStatus.DataSource = dtOrderStatus;
                repOrderStatus.DataBind();
            }
            else
            {
                repOrderStatus.DataSource = null;
                repOrderStatus.DataBind();
            }
        }
        else
        {
            repOrderStatus.DataSource = null;
            repOrderStatus.DataBind();
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addEditOrderStatus.aspx"));
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        //Int32 BankCount = int.Parse((item.FindControl("lblBankCount") as Label).Text);
        spnMessage.Visible = true;
        //if (BankCount.ToString() == "0")
        //{
        Int32 OrderStatusId = int.Parse((item.FindControl("lblorderId") as Label).Text);
        bool yes = (new Cls_OrderStatusmaster_b().Delete(OrderStatusId));

        if (yes)
        {
            BindOrderStatus();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Type Deleted Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Order Type Not Deleted";
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
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addEditOrderStatus.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "id").ToString(), true));
        }
    }
}