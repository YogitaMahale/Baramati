using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class addeditExpenseMaster : System.Web.UI.Page
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
                BindExpenes(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "UPDATE";
                hPageTitle.InnerText = "Update Expense";
                Page.Title = "Update Expense";
            }
            else
            {
                hPageTitle.InnerText = "Add Expense";
                Page.Title = "Add Expense";
                btnSave.Text = "ADD";

            }
        }
    }

    private void Clear()
    {
        txtExpenseName.Text = string.Empty;

    }



    public void BindExpenes(Int64 CategoryId)
    {
        ExpenseMaster objcategory = (new Cls_ExpenseMaster_b().SelectById(CategoryId));
        if (objcategory != null)
        {

            txtExpenseName.Text = objcategory.Expensename;


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
        ExpenseMaster objcategory = new ExpenseMaster();
        objcategory.Expensename = txtExpenseName.Text.Trim();

        if (Request.QueryString["id"] != null)
        {
            objcategory.eid = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_ExpenseMaster_b().Update(objcategory));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/Manage_expenseMaster.aspx?mode=u"));
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
            Result = (new Cls_ExpenseMaster_b().Insert(objcategory));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/Manage_expenseMaster.aspx?mode=i"));
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
        Response.Redirect(Page.ResolveUrl("~/Manage_expenseMaster.aspx"));
    }
}