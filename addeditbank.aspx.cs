using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class addeditbank : System.Web.UI.Page
{
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["id"] != null)
            {
                BindBank(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "UPDATE";
                hPageTitle.InnerText = "Update Bank";
                Page.Title = "Update Bank";
            }
            else
            {
                hPageTitle.InnerText = "New Bank";
                Page.Title = "New Bank";
                btnSave.Text = "ADD";
            }
        }
    }

    private void Clear()
    {
        txtBankName.Text = string.Empty;
        txtIFSC.Text = string.Empty;
        txtBankBranch.Text = string.Empty;
        txtAccountNo.Text = string.Empty;
        txtAccountHolderName.Text = string.Empty;
    }

    public void BindBank(Int64 BankId)
    {
        bankmaster objbankmaster = (new Cls_bankmaster_b().SelectById(BankId));
        if (objbankmaster != null)
        {
            txtBankName.Text = objbankmaster.bankname;
            txtIFSC.Text = objbankmaster.bankifsccode;
            txtBankBranch.Text = objbankmaster.bankbranch;
            txtAccountNo.Text = objbankmaster.accountno;
            txtAccountHolderName.Text = objbankmaster.accountholdername;
            txtbalance.Text = objbankmaster.balance.ToString();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/managebank.aspx"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        bankmaster objbankmaster = new bankmaster();
        objbankmaster.bankname = txtBankName.Text.Trim();
        objbankmaster.bankifsccode = txtIFSC.Text.Trim();
        objbankmaster.bankbranch = txtBankBranch.Text.Trim();
        objbankmaster.accountno = txtAccountNo.Text.Trim();
        objbankmaster.accountholdername = txtAccountHolderName.Text.Trim();
        objbankmaster.balance = Convert.ToDecimal(txtbalance.Text.Trim());

        if (Request.QueryString["id"] != null)
        {
            objbankmaster.bankid = Convert.ToInt32(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_bankmaster_b().Update(objbankmaster));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/managebank.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Bank Not Updated";
                BindBank(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_bankmaster_b().Insert(objbankmaster));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/managebank.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Bank Not Inserted";

            }
        }
    }
}