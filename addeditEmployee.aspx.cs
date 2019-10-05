using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class addeditEmployee : System.Web.UI.Page
{
    int categoryImageFrontWidth = 500;
    int categoryImageFrontHeight = 605;
    string categoryMainPath = "~/uploads/emp/";
    string categoryFrontPath = "~/uploads/emp/front/";
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            // BindAgent();
           
            if (Request.QueryString["id"] != null)
            {
                BindEmployee(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "UPDATE";
                hPageTitle.InnerText = "Update Employee";
                Page.Title = "Update Employee";
            }
            else
            { 
                hPageTitle.InnerText = "New Employee";
                Page.Title = "New Employee";
                btnSave.Text = "ADD";
            }
        }
    }

    private void Clear()
    {
        txtEmployeeName .Text = string.Empty;
        txtAddress1.Text = string.Empty;
         
        txtMobileNo1.Text = string.Empty;
       
        txtLandline.Text = string.Empty;
        txtEmail.Text = string.Empty;
        imgCategory.Visible = false;
        ViewState["fileName"] = null;
    }

    public void BindEmployee(Int64 BankId)
    {
        employeeMaster  objVendorMaster = (new Cls_Employee_b ().SelectById(BankId));
        if (objVendorMaster != null)
        {
           

            txtEmployeeName .Text = objVendorMaster.employeeName;
            txtAddress1.Text = objVendorMaster.Address1;
           
            txtMobileNo1.Text = objVendorMaster.MobileNo1;
          
            txtLandline.Text = objVendorMaster.landline;
            txtEmail.Text = objVendorMaster.email;

            if (!string.IsNullOrEmpty(objVendorMaster.img))
            {
                imgCategory.Visible = true;
                ViewState["fileName"] = objVendorMaster.img;
                imgCategory.ImageUrl = categoryFrontPath + objVendorMaster.img;
                btnImageUpload.Visible = false;
                btnRemove.Visible = true;
            }
            else
            {
                btnImageUpload.Visible = true;
            }


        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/manageEmployee.aspx"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        employeeMaster objemployeeMaster = new employeeMaster();
        objemployeeMaster.employeeName = txtEmployeeName.Text.Trim();
        objemployeeMaster.Address1 = txtAddress1.Text.Trim();
        objemployeeMaster.MobileNo1 = txtMobileNo1.Text.Trim();
        objemployeeMaster.landline = txtLandline.Text.Trim();
        objemployeeMaster.email = txtEmail.Text.Trim();
        if (ViewState["fileName"] != null)
        {
            objemployeeMaster.img = ViewState["fileName"].ToString();
        }


        if (Request.QueryString["id"] != null)
        {
            objemployeeMaster.id = Convert.ToInt32(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_Employee_b().Update(objemployeeMaster));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/manageEmployee.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Employee Not Updated";
                BindEmployee (Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_Employee_b().Insert(objemployeeMaster));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/manageEmployee.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Employee Not Inserted";

            }
        }
    }

    protected void btnImageUpload_Click(object sender, EventArgs e)
    {
        if (fpCategory.HasFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(fpCategory.FileName.Replace(' ', '_')) + DateTime.Now.Ticks.ToString() + Path.GetExtension(fpCategory.FileName);
            fpCategory.SaveAs(MapPath(categoryMainPath + fileName));
            ocommon.CreateThumbnail1("uploads\\emp\\", categoryImageFrontWidth, categoryImageFrontHeight, "~/Uploads/emp/front/", fileName);
            imgCategory.Visible = true;
            imgCategory.ImageUrl = categoryFrontPath + fileName;
            ViewState["fileName"] = fileName;
            btnRemove.Visible = true;
            btnImageUpload.Visible = false;
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        var filePath = Server.MapPath("~/uploads/emp/" + ViewState["fileName"].ToString());
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        var filePath1 = Server.MapPath("~/uploads/emp/front/" + ViewState["fileName"].ToString());
        if (File.Exists(filePath1))
        {
            File.Delete(filePath1);
        }

        btnImageUpload.Visible = true;
        btnRemove.Visible = false;
        ViewState["fileName"] = string.Empty;
        imgCategory.Visible = false;
    }

     

    
}