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

public partial class addeditVendor : System.Web.UI.Page
{
    int categoryImageFrontWidth = 500;
    int categoryImageFrontHeight = 605;
    string categoryMainPath = "~/uploads/vendor/";
    string categoryFrontPath = "~/uploads/vendor/front/";
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
           // BindAgent();
            BindCountry();
            if (Request.QueryString["id"] != null)
            {
                BindVendor(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "UPDATE";
                hPageTitle.InnerText = "Update Vendor";
                Page.Title = "Update Vendor";
            }
            else
            {
                hPageTitle.InnerText = "New Vendor";
                Page.Title = "New Vendor";
                btnSave.Text = "ADD";
            }
        }
    }

    private void Clear()
    {
        txtvendorName.Text = string.Empty;
        txtAddress1.Text = string.Empty;
        txtAddress2.Text = string.Empty;
        txtMobileNo1.Text = string.Empty;
        txtMobileNo2.Text = string.Empty;
        txtLandline.Text = string.Empty;
        txtEmail.Text = string.Empty;
        imgCategory.Visible = false;
        ViewState["fileName"] = null;
    }

    public void BindVendor(Int64 BankId)
    {
        VendorMaster objVendorMaster = (new Cls_VendorMaster_b().SelectById(BankId));
        if (objVendorMaster != null)
        {
            //BindAgent();
            BindCountry();
            //ddlAgent.SelectedValue = objVendorMaster.fk_agentId.ToString();
            ddlCountry.SelectedValue = objVendorMaster.fk_countryId.ToString();
            ddlCountry_SelectedIndexChanged(null, null);
            ddlState.SelectedValue = objVendorMaster.fk_stateId.ToString();
            ddlState_SelectedIndexChanged(null, null);
            ddlCity.SelectedValue = objVendorMaster.fk_cityId.ToString();

            txtvendorName.Text = objVendorMaster.vendorName;
            txtAddress1.Text = objVendorMaster.Address1;
            txtAddress2.Text = objVendorMaster.Address2;
            txtMobileNo1.Text = objVendorMaster.MobileNo1;
            txtMobileNo2.Text = objVendorMaster.MobileNo2;
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
        Response.Redirect(Page.ResolveUrl("~/ManageVendor.aspx"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        VendorMaster objVendorMaster = new VendorMaster();
        //objVendorMaster.fk_agentId = Convert.ToInt64(ddlAgent.SelectedValue.ToString());
        objVendorMaster.fk_agentId = 0;
        objVendorMaster.fk_countryId = ddlCountry.SelectedValue.ToString();
        objVendorMaster.fk_stateId = ddlState.SelectedValue.ToString();
        objVendorMaster.fk_cityId = ddlCity.SelectedValue.ToString();



        objVendorMaster.vendorName = txtvendorName.Text.Trim();
        objVendorMaster.Address1 = txtAddress1.Text.Trim();
        objVendorMaster.Address2 = txtAddress2.Text.Trim(); 
        objVendorMaster.MobileNo1 = txtMobileNo1.Text.Trim();
        objVendorMaster.MobileNo2 = txtMobileNo2.Text.Trim();
        objVendorMaster.landline = txtLandline.Text.Trim();
        objVendorMaster.email = txtEmail.Text.Trim();




        if (ViewState["fileName"] != null)
        {
            objVendorMaster.img = ViewState["fileName"].ToString();
        }


        if (Request.QueryString["id"] != null)
        {
            objVendorMaster.vid = Convert.ToInt32(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_VendorMaster_b().Update(objVendorMaster));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/ManageVendor.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Vendor Not Updated";
                BindVendor(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_VendorMaster_b().Insert(objVendorMaster));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/ManageVendor.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Vendor Not Inserted";

            }
        }
    }

    protected void btnImageUpload_Click(object sender, EventArgs e)
    {
        if (fpCategory.HasFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(fpCategory.FileName.Replace(' ', '_')) + DateTime.Now.Ticks.ToString() + Path.GetExtension(fpCategory.FileName);
            fpCategory.SaveAs(MapPath(categoryMainPath + fileName));
            ocommon.CreateThumbnail1("uploads\\vendor\\", categoryImageFrontWidth, categoryImageFrontHeight, "~/Uploads/vendor/front/", fileName);
            imgCategory.Visible = true;
            imgCategory.ImageUrl = categoryFrontPath + fileName;
            ViewState["fileName"] = fileName;
            btnRemove.Visible = true;
            btnImageUpload.Visible = false;
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        var filePath = Server.MapPath("~/uploads/vendor/" + ViewState["fileName"].ToString());
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        var filePath1 = Server.MapPath("~/uploads/vendor/front/" + ViewState["fileName"].ToString());
        if (File.Exists(filePath1))
        {
            File.Delete(filePath1);
        }

        btnImageUpload.Visible = true;
        btnRemove.Visible = false;
        ViewState["fileName"] = string.Empty;
        imgCategory.Visible = false;
    }

    #region Bing Agent Names Not Using

    //private void BindAgent()
    //{
    //    DataTable dtCategory = (new Cls_agentmaster_b().SelectAll());

    //    ddlAgent.DataSource = null;
    //    ddlAgent.DataBind();

    //    if (dtCategory != null)
    //    {
    //        if (dtCategory.Rows.Count > 0)
    //        {
    //            ddlAgent.DataSource = dtCategory;
    //            ddlAgent.DataTextField = "Agentname";
    //            ddlAgent.DataValueField = "aid";
    //            ddlAgent.DataBind();
    //            ListItem objListItem = new ListItem("--Select Agent--", "0");
    //            ddlAgent.Items.Insert(0, objListItem);
    //        }
    //    }
    //}

    #endregion


    private void BindCountry()
    {
        DataTable dtCategory = (new Cls_VendorMaster_b().Country_SelectAll());

        ddlCountry.DataSource = null;
        ddlCountry.DataBind();

        if (dtCategory != null)
        {
            if (dtCategory.Rows.Count > 0)
            {
                ddlCountry.DataSource = dtCategory;
                ddlCountry.DataTextField = "countryname";
                ddlCountry.DataValueField = "id";
                ddlCountry.DataBind();
                ListItem objListItem = new ListItem("--Select Country--", "0");
                ddlCountry.Items.Insert(0, objListItem);
            }
        }
    }
    private void getState_byCountryId(Int64 countryId)
    {
        DataTable dtCategory = (new Cls_VendorMaster_b().getState_byCountryId(countryId));

        ddlState.DataSource = null;
        ddlState.DataBind();

        if (dtCategory != null)
        {
            if (dtCategory.Rows.Count > 0)
            {
                ddlState.DataSource = dtCategory;
                ddlState.DataTextField = "statename";
                ddlState.DataValueField = "id";
                ddlState.DataBind();
                ListItem objListItem = new ListItem("--Select State--", "0");
                ddlState.Items.Insert(0, objListItem);
            }
        }
    }
    private void getCity_byStateId(Int64 cityId)
    {
        DataTable dtCategory = (new Cls_VendorMaster_b().getCity_byStateId(cityId));

        ddlCity.DataSource = null;
        ddlCity.DataBind();

        if (dtCategory != null)
        {
            if (dtCategory.Rows.Count > 0)
            {
                ddlCity.DataSource = dtCategory;
                ddlCity.DataTextField = "cityname";
                ddlCity.DataValueField = "id";
                ddlCity.DataBind();
                ListItem objListItem = new ListItem("--Select City--", "0");
                ddlCity.Items.Insert(0, objListItem);
            }
        }
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Int64 countryId = Convert.ToInt64(ddlCountry.SelectedValue.ToString());
        getState_byCountryId(countryId);
    }
    protected void ddlState_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Int64 stateId = Convert.ToInt64(ddlState.SelectedValue.ToString());
        getCity_byStateId(stateId);
    }
}