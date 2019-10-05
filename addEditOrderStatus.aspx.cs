using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class addEditOrderStatus : System.Web.UI.Page
{
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["id"] != null)
            {
                BindOrderStatus(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "UPDATE";
                hPageTitle.InnerText = "Update Order Type";
                Page.Title = "Update Order Type";
            }
            else
            {
                hPageTitle.InnerText = "Add Order Type";
                Page.Title = "Add Order Type";
                btnSave.Text = "ADD";
            }
        }
    }

    private void Clear()
    {
        txtorderType.Text = string.Empty;
        txtNotificationmsg.Text = string.Empty;

    }

    public void BindOrderStatus(Int64 BankId)
    {
        OrderStatus objbankmaster = (new Cls_OrderStatusmaster_b().SelectById(BankId));
        if (objbankmaster != null)
        {
            txtorderType.Text = objbankmaster.type;
            txtNotificationmsg.Text = objbankmaster.NotificationMsg;

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/Manage_OrderStatus.aspx"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        OrderStatus objOrderStatus = new OrderStatus();
        objOrderStatus.type = txtorderType.Text.Trim();
        objOrderStatus.NotificationMsg = txtNotificationmsg.Text.Trim();


        if (Request.QueryString["id"] != null)
        {
            objOrderStatus.id = Convert.ToInt32(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_OrderStatusmaster_b().Update(objOrderStatus));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/Manage_OrderStatus.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Record Not Updated";
                BindOrderStatus(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_OrderStatusmaster_b().Insert(objOrderStatus));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/Manage_OrderStatus.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Record Not Inserted";

            }
        }
    }
}