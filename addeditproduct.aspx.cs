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

public partial class addeditproduct : System.Web.UI.Page
{
    int productImageFrontWidth = 1000;
    int productImageFrontHeight = 900;
    string productMainPath = "~/uploads/product/";
    string productFrontPath = "~/uploads/product/water/";
    string productWaterFrontPath = "~/uploads/product/front/";
    common ocommon = new common();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCategory();
            //BindColor();
            //BindSize();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["id"] != null)
            {
                BindProducts(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "Update";
                hPageTitle.InnerText = "Product Update";
                Page.Title = "Product Update";
            }
            else
            {
                hPageTitle.InnerText = "Product Add";
                Page.Title = "Product Add";
            }
        }
    }

    public void BindProducts(Int64 ProductId)
    {
        product objproduct = (new Cls_product_b().SelectById(ProductId));
        if (objproduct != null)
        {
            ddlCategory.SelectedValue = objproduct.cid.ToString();
            //ddlSize .SelectedValue = objproduct.fk_sizeId.ToString();
            //ddlColor.SelectedValue = objproduct.fk_colorId .ToString();

            txtProductName.Text = objproduct.productname;
            txtSKU.Text = objproduct.sku;
            txtCustomerProductPrice.Text = objproduct.customerprice.ToString();
            txtDealerPrice.Text = objproduct.dealerprice.ToString();
            txtDiscountProductPrice.Text = objproduct.discountprice.ToString();
            txtGST.Text = objproduct.gst.ToString();
            txtQuantites.Text = objproduct.quantites.ToString();
            txtAlertQuantites.Text = objproduct.alertquantites.ToString();
            cbIsStock.Checked = objproduct.isstock;
            txtProductShortDescription.Text = objproduct.shortdescp;
            txtProductDescription.Text = objproduct.longdescp;
            txtYouTubeVideo1.Text = objproduct.video1;
            txtYouTubeVideo2.Text = objproduct.video2;
            txtYouTubeVideo3.Text = objproduct.video3;
            txtYouTubeVideo4.Text = objproduct.video4;
            txtWholesalePrice.Text = objproduct.wholesaleprice.ToString();
            txtSuperWholesalePrice.Text = objproduct.superwholesaleprice.ToString();
            txtYoutubeName1.Text = objproduct.video_name_1;
            txtYoutubeName2.Text = objproduct.video_name_2;
            txtYoutubeName3.Text = objproduct.video_name_3;
            txtYoutubeName4.Text = objproduct.video_name_4;
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
                btnImageUpload.Visible = true;
            }
            txt_Hsncode.Text = objproduct.HSNCode;
            txt_landingprice.Text = objproduct.LandingPrice.ToString();
            txt_RealStock.Text = objproduct.RealStock.ToString();
            cbIsHotproduct.Checked = objproduct.isHotproduct;
            txtpack.Text = objproduct.packing.ToString();

        }
    }

    private void BindCategory()
    {
        DataTable dtCategory = (new Cls_category_b().SelectAll());
        ddlCategory.Items.Clear();
        if (dtCategory != null)
        {
            if (dtCategory.Rows.Count > 0)
            {
               
                ddlCategory.DataSource = dtCategory;
                ddlCategory.DataTextField = "categoryname";
                ddlCategory.DataValueField = "cid";
                ddlCategory.DataBind();
                ListItem objListItem = new ListItem("--Select Category--", "0");
                ddlCategory.Items.Insert(0, objListItem);
            }
        }
    }
    /*
     * private void BindSize()
    {
        DataTable dtSize= (new Cls_size_b ().SelectAll ());
        if (dtSize != null)
        {
            if (dtSize.Rows.Count > 0)
            {

                ddlSize .DataSource = dtSize;
                ddlSize.DataTextField = "sizeName";
                ddlSize.DataValueField = "cid";
                ddlSize.DataBind();
                ListItem objListItem = new ListItem("--Select Size--", "0");
                ddlSize.Items.Insert(0, objListItem);
            }
        }
    }
    private void BindColor()
    {
        DataTable dtColor = (new Cls_color_b ().SelectAll());
        if (dtColor != null)
        {
            if (dtColor.Rows.Count > 0)
            {

                ddlColor .DataSource = dtColor;
                ddlColor.DataTextField = "colorname";
                ddlColor.DataValueField = "cid";
                ddlColor.DataBind();
                ListItem objListItem = new ListItem("--Select Color--", "0");
                ddlColor.Items.Insert(0, objListItem);
            }
        }
    }
    */
    private void Clear()
    {
        txt_Hsncode.Text = string.Empty;
        txt_RealStock.Text = string.Empty;
        txt_landingprice.Text = string.Empty;
        txtpack.Text = string.Empty;
        txtProductName.Text = string.Empty;
        txtSKU.Text = string.Empty;
        txtCustomerProductPrice.Text = string.Empty;
        txtDealerPrice.Text = string.Empty;
        txtDiscountProductPrice.Text = string.Empty;
        txtProductDescription.Text = string.Empty;
        txtProductShortDescription.Text = string.Empty;
        cbIsStock.Checked = false;
        cbIsHotproduct.Checked = false;
        imgProduct.Visible = false;
        ViewState["fileName"] = null;
        btnImageUpload.Visible = true;
        btnRemove.Visible = false;
        txtWholesalePrice.Text = string.Empty;
        txtYoutubeName1.Text = string.Empty;
        txtYoutubeName2.Text = string.Empty;
        txtYoutubeName3.Text = string.Empty;
        txtYoutubeName4.Text = string.Empty;
        BindCategory();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        product objproduct = new product();
        objproduct.productname = txtProductName.Text.Trim();
        objproduct.sku = txtSKU.Text.Trim();
        objproduct.customerprice = Convert.ToDecimal(txtCustomerProductPrice.Text.Trim());
        objproduct.dealerprice = Convert.ToDecimal(txtDealerPrice.Text.Trim());
        objproduct.discountprice = Convert.ToDecimal(txtDiscountProductPrice.Text.Trim());
        objproduct.gst = Convert.ToDecimal(txtGST.Text.Trim());
        objproduct.quantites = Convert.ToInt32(txtQuantites.Text.Trim());
        objproduct.alertquantites = Convert.ToInt32(txtAlertQuantites.Text.Trim());
        objproduct.isstock = cbIsStock.Checked;
        objproduct.shortdescp = txtProductShortDescription.Text;
        objproduct.longdescp = txtProductDescription.Text;
        objproduct.cid = Convert.ToInt64(ddlCategory.SelectedValue);
        objproduct.video1 = txtYouTubeVideo1.Text.Trim();
        objproduct.video2 = txtYouTubeVideo2.Text.Trim();
        objproduct.video3 = txtYouTubeVideo3.Text.Trim();
        objproduct.video4 = txtYouTubeVideo4.Text.Trim();

        objproduct.wholesaleprice = Convert.ToDecimal(txtWholesalePrice.Text);
        objproduct.superwholesaleprice = Convert.ToDecimal(txtSuperWholesalePrice.Text);

        objproduct.video_name_1 = txtYoutubeName1.Text;
        objproduct.video_name_2 = txtYoutubeName2.Text;
        objproduct.video_name_3 = txtYoutubeName3.Text;
        objproduct.video_name_4 = txtYoutubeName4.Text;


        objproduct.HSNCode = txt_Hsncode.Text;
        objproduct.RealStock = Convert.ToInt32(txt_RealStock.Text.Trim());
        objproduct.LandingPrice = Convert.ToDecimal(txt_landingprice.Text);
        objproduct.isHotproduct = cbIsHotproduct.Checked;
        if (ViewState["fileName"] != null)
        {
            objproduct.mainimage = ViewState["fileName"].ToString();
        }
        objproduct.fk_colorId = 0;
        objproduct.fk_sizeId = 0;
        //objproduct.fk_colorId  = Convert.ToInt64 (ddlColor.SelectedValue.ToString());
        //objproduct.fk_sizeId  = Convert.ToInt64(ddlSize.SelectedValue.ToString());
        objproduct.packing = Convert.ToInt32(txtpack.Text.Trim());

        if (Request.QueryString["id"] != null)
        {
            objproduct.pid = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_product_b().Update(objproduct));
            if (Result > 0)
            {
                Clear();
                //if (Request.QueryString["page"].ToString() == "price")
                //{
                //    Response.Redirect(Page.ResolveUrl("~/manageproductprice.aspx?mode=u&catid=" + objproduct.cid.ToString()));
                //}
                //else if (Request.QueryString["page"].ToString() == "stock")
                //{
                //    Response.Redirect(Page.ResolveUrl("~/manageproductstock.aspx?mode=u&catid=" + objproduct.cid.ToString()));
                //}
                //else
                //{
                //    Response.Redirect(Page.ResolveUrl("~/manageproduct.aspx?mode=u&catid=" + objproduct.cid.ToString()));
                //}
                Response.Redirect(Page.ResolveUrl("~/manageproduct.aspx?mode=u&catid=" + objproduct.cid.ToString()));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Product Not Updated";
                BindProducts(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_product_b().Insert(objproduct));
            if (Result > 0)
            {
                Clear();
                //if (Request.QueryString["page"].ToString() == "price")
                //{
                //    Response.Redirect(Page.ResolveUrl("~/manageproductprice.aspx?mode=i&catid=" + objproduct.cid.ToString()));
                //}
                //else if (Request.QueryString["page"].ToString() == "stock")
                //{
                //    Response.Redirect(Page.ResolveUrl("~/manageproductstock.aspx?mode=i&catid=" + objproduct.cid.ToString()));
                //}
                //else
                //{
                //    Response.Redirect(Page.ResolveUrl("~/manageproduct.aspx?mode=i&catid=" + objproduct.cid.ToString()));
                //}
                Response.Redirect(Page.ResolveUrl("~/manageproduct.aspx?mode=i&catid=" + objproduct.cid.ToString()));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Product Not Inserted";
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
                ocommon.CreateThumbnail1("uploads\\product\\", productImageFrontWidth, productImageFrontHeight, "~/Uploads/product/water/", fileName);
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
        string watermarkText = "";
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
        var filePath = Server.MapPath("~/uploads/product/" + ViewState["fileName"].ToString());
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        var filePath1 = Server.MapPath("~/uploads/product/water/" + ViewState["fileName"].ToString());
        if (File.Exists(filePath1))
        {
            File.Delete(filePath1);
        }

        var filePath2 = Server.MapPath("~/uploads/product/front/" + ViewState["fileName"].ToString());
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
        if (String.IsNullOrEmpty(Request.QueryString["page"]))
        {
            Response.Redirect(Page.ResolveUrl("~/manageproduct.aspx"));
        }
        else if (Request.QueryString["page"].ToString() == "stock")
        {
            Response.Redirect(Page.ResolveUrl("~/manageproductstock.aspx"));
        }
        else if(Request.QueryString["page"].ToString() == "price")
        {
            Response.Redirect(Page.ResolveUrl("~/manageproductprice.aspx"));

        }
    }
}