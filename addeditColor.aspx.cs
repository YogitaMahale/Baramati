using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
public partial class addeditColor : System.Web.UI.Page
{
    //int categoryImageFrontWidth = 500;
    //int categoryImageFrontHeight = 605;
    //string categoryMainPath = "~/uploads/category/";
    //string categoryFrontPath = "~/uploads/category/front/";
    common ocommon = new common();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //BindBank();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["id"] != null)
            {
                BindColor(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "Update";
                hPageTitle.InnerText = "Color Update";
                Page.Title = "Color Update";
            }
            else
            {
                hPageTitle.InnerText = "Color Add";
                Page.Title = "Color Add";
                btnSave.Text = "Add";
                
            }
        }
    }

    private void Clear()
    {
        txtColorName.Text = string.Empty;
      
       
    }

   

    public void BindColor(Int64 CategoryId)
    {
        ColorMaster objcategory = (new Cls_color_b().SelectById(CategoryId));
        if (objcategory != null)
        {
            //ddlBank.SelectedValue = objcategory.bankid.ToString();
            txtColorName.Text = objcategory.colorname;
             
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
        ColorMaster  objcategory = new ColorMaster ();
        objcategory.colorname = txtColorName.Text.Trim();
        
        if (Request.QueryString["id"] != null)
        {
            objcategory.cid = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_color_b ().Update(objcategory));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/manageColor.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Color Not Updated";
                BindColor(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_color_b ().Insert(objcategory));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/manageColor.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Color Not Inserted";

            }
        }
    }

    

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/manageColor.aspx"));
    }
}