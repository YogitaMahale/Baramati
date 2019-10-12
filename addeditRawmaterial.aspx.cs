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
    string productFrontPath = "~/uploads/RawMaterial/water/";
    string productWaterFrontPath = "~/uploads/RawMaterial/front/";
    common ocommon = new common();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            //BindCategory();
            BindUnits();
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

    private void BindUnits()
    {
        DataTable dtUnits = new DataTable();
        
        try
        {



            //ProductMaster objProductMaster = new ProductMaster();
            Cls_unit_b clsbunit = new Cls_unit_b();
            dtUnits = clsbunit.SelectAll();

            if (dtUnits != null)
            {
                if (dtUnits.Rows.Count > 0)
                {
                    ddlunit.DataSource = dtUnits;
                    ddlunit.DataTextField = "unitname";
                    ddlunit.DataValueField = "id";
                    ddlunit.DataBind();
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Unit--", "0");
                    ddlunit.Items.Insert(0, objListItem);
                }
            }

        }
        catch { }
        finally {
            //con.Close(); 
        }

        
    }



    public void BindProducts(Int64 ProductId)
    {
       

        rawMaterialMaster  objproduct = (new Cls_Rawmaterial_b().SelectById(ProductId));
        if (objproduct != null)
        {
            
            txtProductName.Text = objproduct.productname;
            txthsncode.Text = objproduct.hsncode;
            txtgstno.Text = objproduct.gstno;
            txtPrice .Text = objproduct.price.ToString();
            ddlunit.SelectedIndex = objproduct.unitid;
            txtQuantites.Text = objproduct.quantity.ToString();
            txtAlertQuantites.Text = objproduct.alertquantites.ToString();
            cbIsStock.Checked = objproduct.isstock;
            txtProductShortDescription.Text = objproduct.shortdescp;
            txtProductDescription.Text = objproduct.longdescp;
            
            if (!string.IsNullOrEmpty(objproduct.mainimage))
            {
                imgProduct.Visible = true;
                ViewState["fileName"] = objproduct.mainimage;
                imgProduct.ImageUrl = productFrontPath + objproduct.mainimage;
                btnImageUpload.Visible = false;
                btnRemove.Visible = true;
            }
            else
            {
                //btnImageUpload.Visible = true;
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

        //btnImageUpload.Visible = true;
        //btnRemove.Visible = false;
         
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        /*
         private String _productname;
    private String _mainimage;
    private Decimal _price;
    private Int32 _quantity;
    private Int32 _alertquantites;
    private Boolean _isstock;
    private String _shortdescp;
    private String _longdescp;
    private Boolean _isactive;
    private Boolean _isdelete;
    private System.DateTime _createddate;
    private System.DateTime _modifieddate;
    private String _hsncode;
    private String _gstno;
    private Int32 _unitid; */
    Int64 Result = 0;
        //product objproduct = new product();
        rawMaterialMaster objrawmaterial = new rawMaterialMaster();
        objrawmaterial.productname = txtProductName.Text.Trim();
        objrawmaterial.price = Convert.ToDecimal(txtPrice.Text.Trim());
        objrawmaterial.quantity = Convert.ToInt32(txtQuantites.Text.Trim());
        objrawmaterial.alertquantites = Convert.ToInt32(txtAlertQuantites.Text.Trim());
        objrawmaterial.isstock = cbIsStock.Checked;
        objrawmaterial.shortdescp = txtProductShortDescription.Text;
        objrawmaterial.longdescp = txtProductDescription.Text;
        objrawmaterial.hsncode = txthsncode.Text.Trim();
        objrawmaterial.gstno = txtgstno.Text.Trim();
        objrawmaterial.unitid = Convert.ToInt32(ddlunit.SelectedValue);
        if (ViewState["fileName"] != null)
        {
            objrawmaterial.mainimage = ViewState["fileName"].ToString();
        }
        if (Request.QueryString["id"] != null)
        {
            objrawmaterial.id = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_Rawmaterial_b().Update(objrawmaterial));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/manageRawMaterial.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Update Failed...";
                BindProducts(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_Rawmaterial_b().Insert(objrawmaterial));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/manageRawMaterial.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Raw Material Not Saved";
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
                ocommon.CreateThumbnail1("uploads\\RawMaterial\\", productImageFrontWidth, productImageFrontHeight, "~/Uploads/RawMaterial/water/", fileName);
                WatermarkImageCreate(fileName);
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

    private void WatermarkImageCreate(string fileName)
    {
        string watermarkText = "© MORYATOOLS";
        using (Bitmap bmp = new Bitmap(HttpContext.Current.Server.MapPath(productFrontPath) + fileName, false))
        {
            using (Graphics grp = Graphics.FromImage(bmp))
            {
                Brush brush = new SolidBrush(Color.Gray);
                Font font = new System.Drawing.Font("Book Antiqua", 25, FontStyle.Regular, GraphicsUnit.Pixel);
                SizeF textSize = new SizeF();
                textSize = grp.MeasureString(watermarkText, font);

                #region " Top "

                Point positionLeftTop = new Point(0, 0);
                grp.DrawString(watermarkText, font, brush, positionLeftTop);

                Point positionCenterTop = new Point(((bmp.Width - ((int)textSize.Width)) / 2), 0);
                grp.DrawString(watermarkText, font, brush, positionCenterTop);


                Point positionRightTop = new Point((bmp.Width - ((int)textSize.Width)), 0);
                grp.DrawString(watermarkText, font, brush, positionRightTop);

                #endregion " Top "

                #region " Bottom "

                Point positionLeftBottom = new Point(0, ((bmp.Height - ((int)textSize.Height))));
                grp.DrawString(watermarkText, font, brush, positionLeftBottom);


                Point positionCenterBottom = new Point(((bmp.Width - ((int)textSize.Width)) / 2), ((bmp.Height - ((int)textSize.Height))));
                grp.DrawString(watermarkText, font, brush, positionCenterBottom);

                Point positionRightBottom = new Point((bmp.Width - ((int)textSize.Width)), (bmp.Height - ((int)textSize.Height)));
                grp.DrawString(watermarkText, font, brush, positionRightBottom);

                #endregion " Bottom "

                #region " Center "

                Point positionLeftCenter = new Point(0, ((bmp.Height - ((int)textSize.Height)) / 2));
                grp.DrawString(watermarkText, font, brush, positionLeftCenter);

                Point positionCenter = new Point(((bmp.Width - ((int)textSize.Width)) / 2), ((bmp.Height - ((int)textSize.Height)) / 2));
                grp.DrawString(watermarkText, font, brush, positionCenter);

                Point positionRightCenter = new Point((bmp.Width - ((int)textSize.Width)), ((bmp.Height - ((int)textSize.Height)) / 2));
                grp.DrawString(watermarkText, font, brush, positionRightCenter);

                #endregion " Center "

                #region " Top Middle "

                Point positionTopLeftMiddle = new Point(0, ((bmp.Height - ((int)textSize.Height)) / 4));
                grp.DrawString(watermarkText, font, brush, positionTopLeftMiddle);


                Point positionTopMiddleCenter = new Point(((bmp.Width - ((int)textSize.Width)) / 2), ((bmp.Height - ((int)textSize.Height)) / 4));
                grp.DrawString(watermarkText, font, brush, positionTopMiddleCenter);

                Point positionTopRightMiddle = new Point((bmp.Width - ((int)textSize.Width)), ((bmp.Height - ((int)textSize.Height)) / 4));
                grp.DrawString(watermarkText, font, brush, positionTopRightMiddle);

                Point positionBottomLeftMiddle = new Point(0, (((bmp.Height / 2) + (bmp.Height)) / 2));
                grp.DrawString(watermarkText, font, brush, positionBottomLeftMiddle);

                Point positionBottomRightMiddle = new Point((bmp.Width - ((int)textSize.Width)), (((bmp.Height / 2) + (bmp.Height)) / 2));
                grp.DrawString(watermarkText, font, brush, positionBottomRightMiddle);

                Point positionBottomCenterMiddle = new Point((((bmp.Width - ((int)textSize.Width)) / 2)), (((bmp.Height / 2) + (bmp.Height)) / 2));
                grp.DrawString(watermarkText, font, brush, positionBottomCenterMiddle);


                #endregion " Top Middle "

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    bmp.Save(HttpContext.Current.Server.MapPath(productWaterFrontPath) + fileName);
                    memoryStream.Position = 0;
                }
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