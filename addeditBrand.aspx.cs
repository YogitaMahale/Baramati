using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class addeditBrand : System.Web.UI.Page
{
    common ocommon = new common();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //BindBank();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["id"] != null)
            {
                BindBrand(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "Update";
                hPageTitle.InnerText = "Update Brand";
                Page.Title = "Update Brand";
            }
            else
            {
                hPageTitle.InnerText = "New Brand";
                Page.Title = "New Brand";
                btnSave.Text = "Add";

            }
        }
    }

    private void Clear()
    {
        txtBrandName.Text = string.Empty;


    }



    public void BindBrand(Int64 bid)
    {
        brandMaster objcategory = (new Cls_brand_b().SelectById(bid));
        if (objcategory != null)
        {
            //ddlBank.SelectedValue = objcategory.bankid.ToString();
            txtBrandName.Text = objcategory.brandName;

        }
    }

    //protected override void Render(HtmlTextWriter writer)
    //{
    //    string validatorOverrideScripts = "<script src=\"" + Page.ResolveUrl("~") + "js/validators.js\" type=\"text/javascript\"></script>";
    //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ValidatorOverrideScripts", validatorOverrideScripts, false);
    //    base.Render(writer);
    //}



    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        brandMaster objcategory = new brandMaster();
        objcategory.brandName = txtBrandName.Text.Trim();

        if (Request.QueryString["id"] != null)
        {
            objcategory.bid = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_brand_b().Update(objcategory));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/manageBrand.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Brand Not Updated";
                BindBrand(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_brand_b().Insert(objcategory));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/manageBrand.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Brand Saved";

            }
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/manageBrand.aspx"));
    }
}