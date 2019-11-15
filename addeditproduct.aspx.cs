using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            BindMainCategoryBrand();
            BindProcessCombo();
            //BindColor();
            //BindSize();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["id"] != null)
            {
                BindProducts(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                BindArticle(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)) );
                btnSave.Text = "Update";
                hPageTitle.InnerText = "Update Article";
                Page.Title = "Update Article";
            }
            else
            {
                 BindArticle(0);
                hPageTitle.InnerText = "New Article";
                Page.Title = "New Article";
            }
        }
    }
    

    public void BindProducts(Int64 ProductId)
    {
        product objproduct = (new Cls_product_b().SelectById(ProductId));
        if (objproduct != null)
        {
            ddlMain.SelectedValue = objproduct.maincategoryid.ToString();
            ddlMain_SelectedIndexChanged(null, null);
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
            ddlBrand.SelectedValue = objproduct.brandid.ToString();
            cbIsgstType.Checked = objproduct.gsttype;
        }
    }

    private void BindMainCategoryBrand()
    {
        DataTable dtBrands = new Cls_brand_b().SelectAll();
        
        /*
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
        */
        

        DataTable dtMainCategory = (new Cls_maincategory_b().SelectAll());
        ddlMain.Items.Clear();
        if (dtMainCategory != null)
        {
            if (dtMainCategory.Rows.Count > 0)
            {

                ddlMain.DataSource = dtMainCategory;
                ddlMain.DataTextField = "name";
                ddlMain.DataValueField = "id";
                ddlMain.DataBind();
                ListItem objListItem = new ListItem("--Select Category--", "0");
                ddlMain.Items.Insert(0, objListItem);
            }
        }

        if (dtBrands != null)
        {
            if (dtBrands.Rows.Count > 0)
            {

                ddlBrand.DataSource = dtBrands;
                ddlBrand.DataTextField = "brandName";
                ddlBrand.DataValueField = "bid";
                ddlBrand.DataBind();
                ListItem objListItem = new ListItem("--Select Brand--", "0");
                ddlBrand.Items.Insert(0, objListItem);
            }
        }
    }
    private void BindProcessCombo()
    {
        DataTable dtProcess = new Cls_process_b ().SelectAll ();

        

 
        ddlProcess .Items.Clear();
        if (dtProcess != null)
        {
            if (dtProcess.Rows.Count > 0)
            {

                ddlProcess.DataSource = dtProcess;
                ddlProcess.DataTextField = "particular";
                ddlProcess.DataValueField = "id";
                ddlProcess.DataBind();
                ListItem objListItem = new ListItem("--Select Process--", "0");
                ddlProcess.Items.Insert(0, objListItem);
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
        txtDiscountProductPrice.Text = "0";
        txtSuperWholesalePrice.Text = "0";
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
        ddlBrand.SelectedIndex = 0;
        BindMainCategoryBrand();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        product objproduct = new product();
        objproduct.productname = txtProductName.Text.Trim();
        //objproduct.sku = txtSKU.Text.Trim();
        objproduct.sku = "0";
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
        objproduct.brandid = Convert.ToInt64(ddlBrand.SelectedValue);
        objproduct.maincategoryid = Convert.ToInt64(ddlMain.SelectedValue.ToString());
        objproduct.gsttype = cbIsgstType.Checked;


        DataTable dtproduct = new DataTable();
        dtproduct = (DataTable)ViewState["dtprod"];
        
        if (Request.QueryString["id"] != null)
        {
            objproduct.pid = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_product_b().Update(objproduct));
            //-----details part---------------------
            ConnectionString.Open();
            foreach (DataRow dr in dtproduct.Rows)
            {
                Int64 idd = 0;
                Int64 particulars1 = 0;
                if (dr["particulars"].ToString().ToLower().Trim() != "")
                {
                    if (dr["id"].ToString().ToLower().Trim() == "")
                    {
                    }
                    else
                    {
                        idd = Convert.ToInt64(dr["id"].ToString().ToLower().Trim());
                    }
                    particulars1 = Convert.ToInt64(dr["particulars"].ToString().ToLower().Trim());
                }


                string s = "select * from productparticulars where productid = " + Result.ToString() + " and particulars = " + particulars1 + "";
                //string s = "select * from productparticulars where productid = " + Result.ToString() + " and id = " + idd + "";
                
                SqlCommand cmd = new SqlCommand(s, ConnectionString);
                SqlDataReader dr1 = cmd.ExecuteReader();
                if (dr1.HasRows)
                {
                    dr1.Close();
                    string s1 = " update productparticulars set   particulars=@particulars ,wages=@wages where productid=@productid and id=@id ";

                    SqlCommand cmddeta = new SqlCommand(s1, ConnectionString);
                    cmddeta.Parameters.AddWithValue("@id", idd);
                    cmddeta.Parameters.AddWithValue("@productid", Result);
                    cmddeta.Parameters.AddWithValue("@particulars", dr["particulars"].ToString());
                    cmddeta.Parameters.AddWithValue("@wages", dr["wages"].ToString());

                    Int64 a = cmddeta.ExecuteNonQuery();
                }
                else
                {
                    dr1.Close();
                    string s1 = " INSERT INTO productparticulars(productid, particulars, wages, quantity, isdeleted)  VALUES (@productid, @particulars, @wages, @quantity, @isdeleted) ";

                    SqlCommand cmddeta = new SqlCommand(s1, ConnectionString);
                    cmddeta.Parameters.AddWithValue("@productid", Result);
                    cmddeta.Parameters.AddWithValue("@particulars", dr["particulars"].ToString());
                    cmddeta.Parameters.AddWithValue("@wages", dr["wages"].ToString());
                    cmddeta.Parameters.AddWithValue("@quantity", "0");
                    cmddeta.Parameters.AddWithValue("@isdeleted", "0");
                   Int64 a = cmddeta.ExecuteNonQuery();

                }


            }

            ConnectionString.Close();
            //-----------------------------
            if (Result > 0)
            {
                Clear();
               
                Response.Redirect(Page.ResolveUrl("~/manageproduct.aspx?mode=u&catid=" + objproduct.cid.ToString()));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Article Not Updated";
                BindProducts(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_product_b().Insert(objproduct));
            int i = 0;
            ConnectionString.Open();
            foreach (DataRow dr in dtproduct.Rows)
            {
                string s1 = " INSERT INTO productparticulars(productid, particulars, wages, quantity, isdeleted)  VALUES (@productid, @particulars, @wages, @quantity, @isdeleted) ";

                SqlCommand cmddeta = new SqlCommand(s1, ConnectionString );
                cmddeta.Parameters.AddWithValue("@productid", Result.ToString());
                cmddeta.Parameters.AddWithValue("@particulars", dr["particulars"].ToString());
                cmddeta.Parameters.AddWithValue("@wages", dr["wages"].ToString());
                cmddeta.Parameters.AddWithValue("@quantity", "0");
                cmddeta.Parameters.AddWithValue("@isdeleted", "0");
                i = cmddeta.ExecuteNonQuery();
            }
            ConnectionString.Close();
            if (Result > 0)
            {
                Clear();
                
                Response.Redirect(Page.ResolveUrl("~/manageproduct.aspx?mode=i&catid=" + objproduct.cid.ToString()));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Article Saved";
            }
        }
    }

    protected void btnImageUpload_Click(object sender, EventArgs e)
    {
        if (fpProduct1.HasFile)
        {

            decimal size = Math.Round(((decimal)fpProduct1.PostedFile.ContentLength / (decimal)1024), 2);
            if (size <= 2000)
            {
                string fileName = Path.GetFileNameWithoutExtension(fpProduct1.FileName.Replace(' ', '_')) + DateTime.Now.Ticks.ToString() + Path.GetExtension(fpProduct1.FileName);
                fpProduct1.SaveAs(MapPath(productMainPath + fileName));
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

    protected void ddlMain_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtCategory = new DataTable();
        ddlCategory.Items.Clear();

        try
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "getSubCategory_fromMaincategory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@id", ddlMain.SelectedValue);
            //cmd.Parameters.AddWithValue("@isactive", IsActive);
            ConnectionString.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtCategory);
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
        catch (Exception ex)
        {
            //return false;
            
        }
        finally
        {
            ConnectionString.Close();
        }


    }
    //*************
     
    public void BindArticle(Int64 pid)
    {
        try
        {
            ConnectionString.Open();
            DataTable ds = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "getArticleDetailsby_productId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@productid", pid);
            //con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);


            repProcess.DataSource = ds;
            repProcess.DataBind();
            ViewState["dtprod"] = ds;




            ConnectionString.Close();
        }
        catch { }
        finally { ConnectionString.Close(); }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlProcess.SelectedIndex==0 || txt_price.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Please Enter All Fields ');", true);
                return;
            }

            DataTable dtprodn = new DataTable();
            dtprodn = (DataTable)ViewState["dtprod"];

            DataRow dr = dtprodn.NewRow();
            int srr = 0;
            srr = dtprodn.Rows.Count + 1;
            dr["sr"] = srr.ToString();
            dr["particulars"] = ddlProcess.SelectedValue.ToString();
            dr["wages"] = txt_price.Text;
            dr["processName"] = ddlProcess.SelectedItem .ToString();

            dtprodn.Rows.Add(dr);
            repProcess.DataSource = dtprodn;
            repProcess.DataBind();
            ViewState["dtprod"] = dtprodn;
            ddlProcess.SelectedIndex = 0;
            txt_price.Text = string.Empty;


        }
        catch (Exception ex)
        {

            Response.Write(ex.Message + ex.StackTrace);
        }
    }
    protected void Remove_member1(object sender, EventArgs e)
    {
        try
        {
            ConnectionString.Open();

            GridViewRow gr = (GridViewRow)(sender as Control).Parent.Parent;
            int ind = gr.RowIndex;
            //string pid = (gvproduct.Rows[ind].Cells[0].Text);        
            string pid = "";
            DataTable dtprodn = new DataTable();
            dtprodn = (DataTable)ViewState["dtprod"];
            pid = dtprodn.Rows[ind]["id"].ToString();
            dtprodn.Rows.RemoveAt(ind);

            //-----------------
            for (int i = 0; i < dtprodn.Rows.Count; i++)
            {
                dtprodn.Rows[i]["sr"] = (i + 1).ToString();
            }
            //-----------------
            dtprodn.AcceptChanges();
            repProcess.DataSource = dtprodn;
            repProcess.DataBind();

            if (Request.QueryString["id"] != null)
            {
                Int64 OrderID = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
                string s = "update productparticulars set isdeleted =1  where id=" + pid + "";
                SqlCommand cmd = new SqlCommand(s, ConnectionString);
                int t = cmd.ExecuteNonQuery();


            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Process Remove Successfully');", true);
            ConnectionString.Close();
        }
        catch (Exception p)
        { }
        finally { ConnectionString.Close(); }
    }


}