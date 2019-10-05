using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class addeditExpenseDetails : System.Web.UI.Page
{
    int categoryImageFrontWidth = 500;
    int categoryImageFrontHeight = 605;
    string categoryMainPath = "~/uploads/category/";
    string categoryFrontPath = "~/uploads/category/front/";
    common ocommon = new common();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["id"] != null)
            {
                BindExpenseType();
                BindExpenes(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "UPDATE";
                hPageTitle.InnerText = "Update Expense";
                Page.Title = "Update Expense";
            }
            else
            {
                BindBanks();
                BindExpenseType();
                hPageTitle.InnerText = "Add Expense";
                Page.Title = "Add Expense";
                btnSave.Text = "ADD";

            }
        }
    }

    private void Clear()
    {
        txtExpenseAmt.Text = string.Empty;
        ddlExpenseType.SelectedIndex = 0;
        txtDescription.Text = string.Empty;
        ddlBank.SelectedIndex = 0;

    }
    private void BindExpenseType()
    {
        DataTable dtCategory = (new Cls_ExpenseMaster_b().SelectAll());
        if (dtCategory != null)
        {
            if (dtCategory.Rows.Count > 0)
            {
                ddlExpenseType.DataSource = dtCategory;
                ddlExpenseType.DataTextField = "Expensename";
                ddlExpenseType.DataValueField = "eid";
                ddlExpenseType.DataBind();
                ListItem objListItem = new ListItem("--Select Expense--", "0");
                ddlExpenseType.Items.Insert(0, objListItem);
            }
        }
    }

    private void BindBanks()
    {
        DataTable dtBanks = (new Cls_bankmaster_b().SelectAll());
        if (dtBanks != null)
        {
            if (dtBanks.Rows.Count > 0)
            {
                ddlBank.DataSource = dtBanks;
                ddlBank.DataTextField = "bankname";
                ddlBank.DataValueField = "bankid";
                ddlBank.DataBind();
                ListItem objListItem = new ListItem("--Select Bank--", "0");
                ddlBank.Items.Insert(0, objListItem);
            }
        }
    }


    public void BindExpenes(Int64 CategoryId)
    {
        ExpenseDetails objExpenseDetails = (new Cls_ExpenseDetails_b().SelectById(CategoryId));
        if (objExpenseDetails != null)
        {
            ddlExpenseType.SelectedValue = objExpenseDetails.FK_ExpenseID.ToString();
            txtExpenseAmt.Text = objExpenseDetails.amount.ToString();
            txtDescription.Text = objExpenseDetails.Description.ToString();


        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        string validatorOverrideScripts = "<script src=\"" + Page.ResolveUrl("~") + "js/validators.js\" type=\"text/javascript\"></script>";
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ValidatorOverrideScripts", validatorOverrideScripts, false);
        base.Render(writer);
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        ExpenseDetails objExpenseDetails = new ExpenseDetails();
        objExpenseDetails.FK_ExpenseID = Convert.ToInt64(ddlExpenseType.SelectedValue.ToString());
        objExpenseDetails.amount = Convert.ToDecimal(txtExpenseAmt.Text.Trim());
        objExpenseDetails.Description = txtDescription.Text.Trim();
        objExpenseDetails.bankid = Convert.ToInt64(ddlBank.SelectedValue.ToString());
        if (Request.QueryString["id"] != null)
        {
            objExpenseDetails.id = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_ExpenseDetails_b().Update(objExpenseDetails));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/Manage_expenseDetails.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Expense Not Updated";
                BindExpenes(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_ExpenseDetails_b().Insert(objExpenseDetails));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/Manage_expenseDetails.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Expense Not Inserted";

            }
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/Manage_expenseDetails.aspx"));
    }
}