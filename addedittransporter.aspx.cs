using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class addedittransporter : System.Web.UI.Page
{
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["id"] != null)
            {
                BindTransporter(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "UPDATE";
                hPageTitle.InnerText = "Update Transporter";
                Page.Title = "Update Transporter";
            }
            else
            {
                hPageTitle.InnerText = "New Transporter";
                Page.Title = "New Transporter";
                btnSave.Text = "ADD";
            }
        }
    }

    private void Clear()
    {
        txtname.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtmobileno.Text = string.Empty;
        txtphoneno.Text = string.Empty;
        txtemail.Text = string.Empty;
        txtgstno.Text = string.Empty;
        ddlgsttype.SelectedIndex = 0;
        txtaadharno.Text = string.Empty;
        txtpanno.Text = string.Empty;
        txtremark.Text = string.Empty;
    }

    public void BindTransporter(Int64 Id)
    {
        transporter objTransporter = (new Cls_transporter_b().SelectById(Id));
        if (objTransporter != null)
        {
            txtname.Text = objTransporter.name;
            txtAddress.Text = objTransporter.address;
            txtmobileno.Text = objTransporter.mobileno;
            txtphoneno.Text = objTransporter.phoneno;
            txtemail.Text = objTransporter.email;
            txtgstno.Text = objTransporter.gstno;
            ddlgsttype.SelectedValue = objTransporter.gsttype;
            txtaadharno.Text = objTransporter.aadharno;
            txtpanno.Text = objTransporter.panno;
            txtremark.Text = objTransporter.remark;

        }
        else {
            Clear();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/managetransporters.aspx"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        transporter objtransporter = new transporter();
        objtransporter.name = txtname.Text.Trim();
        objtransporter.mobileno = txtmobileno.Text.Trim();
        objtransporter.phoneno = txtphoneno.Text.Trim();
        objtransporter.email = txtemail.Text.Trim();
        objtransporter.aadharno = txtaadharno.Text.Trim();
        objtransporter.panno = txtpanno.Text.Trim();
        objtransporter.gstno = txtgstno.Text.Trim();
        objtransporter.gsttype = ddlgsttype.SelectedValue;
        objtransporter.address = txtAddress.Text.Trim();
        objtransporter.remark = txtremark.Text.Trim();
        
        if (Request.QueryString["id"] != null)
        {
            objtransporter.id = Convert.ToInt32(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_transporter_b().Update(objtransporter));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/managetransporters.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Transporter Not Updated";
                BindTransporter(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_transporter_b().Insert(objtransporter));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/managetransporters.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Transporter Not Saved";

            }
        }
    }
    
    
}