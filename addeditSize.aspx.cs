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
public partial class addeditSize : System.Web.UI.Page
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
            Bind();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["id"] != null)
            {
                BindSize(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "Update";
                hPageTitle.InnerText = "Size Update";
                Page.Title = "Size Update";
            }
            else
            {
                hPageTitle.InnerText = "Size Add";
                Page.Title = "Size Add";
                btnSave.Text = "Add";

            }
        }
    }

    private void Clear()
    {
        txtSizeName.Text = string.Empty;
        lstGroup.SelectedIndex = -1;


    }

    private void Bind()
    {
        DataTable dtSizegroup = new DataTable();
        
        try
        {
            Cls_groupmaster_b clsGroup = new Cls_groupmaster_b();
            dtSizegroup = clsGroup.SelectAll();
            
        }
        catch { }
        finally { }

        
        if (dtSizegroup != null)
        {
            if (dtSizegroup.Rows.Count > 0)
            {
                lstGroup.DataSource = dtSizegroup;
                lstGroup.DataTextField = "groupname";
                lstGroup.DataValueField = "id";
                lstGroup.DataBind();
                

            }
            else
            {
                lstGroup.DataSource = dtSizegroup;
                lstGroup.DataTextField = "groupname";
                lstGroup.DataValueField = "id";
                lstGroup.DataBind();


            }
        }
        else
        {
            lstGroup.DataSource = dtSizegroup;
            lstGroup.DataTextField = "groupname";
            lstGroup.DataValueField = "id";
            lstGroup.DataBind();


        }

    }


    public void BindSize(Int64 CategoryId)
    {
        sizeMaster  objcategory = (new Cls_size_b().SelectById(CategoryId));
        if (objcategory != null)
        {
            //ddlBank.SelectedValue = objcategory.bankid.ToString();
            txtSizeName.Text = objcategory.sizeName;
            lstGroup.SelectedValue = objcategory.groupid.ToString();


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
        sizeMaster objcategory = new sizeMaster();
        objcategory.sizeName = txtSizeName.Text.Trim();
        objcategory.groupid = Convert.ToInt64(hfgroupid.Value);

        if (Request.QueryString["id"] != null)
        {
            objcategory.cid = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_size_b().Update(objcategory));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/manageSize.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Size Not Updated";
                BindSize(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_size_b().Insert(objcategory));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/manageSize.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Size Not Inserted";

            }
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/manageSize.aspx"));
    }
}