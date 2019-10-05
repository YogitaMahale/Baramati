using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class addeditRawmaterial : System.Web.UI.Page
{
    int productImageFrontWidth = 1000;
    int productImageFrontHeight = 900;
    string productMainPath = "~/uploads/RawMaterial/";
    //string productFrontPath = "~/uploads/RawMaterial/water/";
    string productWaterFrontPath = "~/uploads/RawMaterial/front/";
    common ocommon = new common();

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!Page.IsPostBack)
        {
            //BindCategory();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["id"] != null)
            {
                BindProducts(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "Update";
                hPageTitle.InnerText = "Raw Material Update";
                Page.Title = "Raw Material Update";
            }
            else
            {
                hPageTitle.InnerText = "Raw Material Add";
                Page.Title = "Raw Material Add";
            }
        }
       
    }

    public void BindProducts(Int64 ProductId)
    {
       

        rawMaterialMaster  objproduct = (new Cls_Rawmaterial_b().SelectById(ProductId));
        if (objproduct != null)
        {
            
            txtProductName.Text = objproduct.productname;
          
            txtPrice .Text = objproduct.price.ToString();
           
            txtQuantites.Text = objproduct.quantity.ToString();
            txtAlertQuantites.Text = objproduct.alertquantites.ToString();
            cbIsStock.Checked = objproduct.isstock;
            txtProductShortDescription.Text = objproduct.shortdescp;
            txtProductDescription.Text = objproduct.longdescp;
            
            if (!string.IsNullOrEmpty(objproduct.mainimage))
            {
                imgProduct.Visible = true;
                ViewState["fileName"] = objproduct.mainimage;
                imgProduct.ImageUrl = productWaterFrontPath + objproduct.mainimage;
                btnImageUpload.Visible = false;
                btnRemove.Visible = true;
            }
            else
            {
                btnImageUpload.Visible = true;
            }
           

        }
          
    }

     
    private void Clear()
    {         
        txtProductName.Text = string.Empty;        
        txtProductDescription.Text = string.Empty;
        txtProductShortDescription.Text = string.Empty;
        cbIsStock.Checked = false;        
        imgProduct.Visible = false;
        ViewState["fileName"] = null;
        btnImageUpload.Visible = true;
        btnRemove.Visible = false;
         
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        Int64 Result = 0;
 //       id, productname, mainimage, price, quantity, alertquantites, isstock, shortdescp, longdescp,
 //isactive, isdelete, createddate, modifieddate, seqno


        rawMaterialMaster objrawMaterialMaster = new rawMaterialMaster();
        objrawMaterialMaster.productname = txtProductName.Text.Trim();
        objrawMaterialMaster.price = Convert.ToDecimal(txtPrice.Text.Trim());
        objrawMaterialMaster.quantity = Convert.ToInt32(txtQuantites.Text.Trim());
        objrawMaterialMaster.alertquantites = Convert.ToInt32(txtAlertQuantites.Text.Trim());
        objrawMaterialMaster.isstock = cbIsStock.Checked;
        objrawMaterialMaster.shortdescp = txtProductShortDescription.Text;
        objrawMaterialMaster.longdescp = txtProductDescription.Text;
        
        if (ViewState["fileName"] != null)
        {
            objrawMaterialMaster.mainimage = ViewState["fileName"].ToString();
        }
        if (Request.QueryString["id"] != null)
        {
            objrawMaterialMaster.id = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_Rawmaterial_b ().Update(objrawMaterialMaster));
            if (Result > 0)
            {
                Clear();
                
                Response.Redirect(Page.ResolveUrl("~/manageRawMaterial.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Raw Material Not Updated";
                BindProducts(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_Rawmaterial_b ().Insert(objrawMaterialMaster));
            if (Result > 0)
            {
                Clear();
               
                Response.Redirect(Page.ResolveUrl("~/manageRawMaterial.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Raw Material Not Inserted";
            }
        }
          
    }

    protected void btnImageUpload_Click(object sender, EventArgs e)
    {
        if (fpProduct.HasFile)
        {

            decimal size = Math.Round(((decimal)fpProduct.PostedFile.ContentLength / (decimal)1024), 2);
            if (size <= 2000)
            {
                string fileName = Path.GetFileNameWithoutExtension(fpProduct.FileName.Replace(' ', '_')) + DateTime.Now.Ticks.ToString() + Path.GetExtension(fpProduct.FileName);
                fpProduct.SaveAs(MapPath(productMainPath + fileName));
                ocommon.CreateThumbnail1("uploads\\RawMaterial\\", productImageFrontWidth, productImageFrontHeight, "~/Uploads/RawMaterial/front/", fileName);
               // WatermarkImageCreate(fileName);
                imgProduct.Visible = true;
                imgProduct.ImageUrl = productWaterFrontPath + fileName;
                ViewState["fileName"] = fileName;
                btnRemove.Visible = true;
                btnImageUpload.Visible = false;
            }
            else
            {
                Response.Write("<script>alert('Image size Must be less than 2MB')</script>");
            }
        }
    }

     
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        var filePath = Server.MapPath("~/uploads/RawMaterial/" + ViewState["fileName"].ToString());
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        var filePath1 = Server.MapPath("~/uploads/RawMaterial/water/" + ViewState["fileName"].ToString());
        if (File.Exists(filePath1))
        {
            File.Delete(filePath1);
        }

        var filePath2 = Server.MapPath("~/uploads/RawMaterial/front/" + ViewState["fileName"].ToString());
        if (File.Exists(filePath2))
        {
            File.Delete(filePath2);
        }

        btnImageUpload.Visible = true;
        btnRemove.Visible = false;
        ViewState["fileName"] = string.Empty;
        imgProduct.Visible = false;

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/manageRawMaterial.aspx"));
    }
}