
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.pdf.draw;
using iTextSharp.text;
using System.Net;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class manageproduct : System.Web.UI.Page
{
    readonly PagedDataSource _pgsource = new PagedDataSource();
    int _firstIndex, _lastIndex;
    private int _pageSize = 20;
    //--------paging-----


    //string productFrontPath = "~/uploads/product/front/";
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCategory();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Manage Products";
            Page.Title = "Manage Product";
        }
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["mode"] == "u")
            {
                if (Request.QueryString["catid"] != null)
                {
                    //BindProduct(Convert.ToInt64(Request.QueryString["catid"].ToString()));
                    search();
                    ddlCategory.SelectedValue = Request.QueryString["catid"].ToString();
                }
                spnMessage.Visible = true;
                spnMessage.Style.Add("color", "green");
                spnMessage.InnerText = "Product Updated Successfully";

            }
            else if (Request.QueryString["mode"] == "i")
            {
                if (Request.QueryString["catid"] != null)
                {
                    //  BindProduct(Convert.ToInt64(Request.QueryString["catid"].ToString()));
                    search();
                    ddlCategory.SelectedValue = Request.QueryString["catid"].ToString();
                }
                spnMessage.Visible = true;
                spnMessage.Style.Add("color", "green");
                spnMessage.InnerText = "Product Inserted Successfully";
            }
        }

    }

    private void BindCategory()
    {
        DataTable dtCategory = (new Cls_category_b().SelectAll());
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

    private void BindProduct(Int64 CategoryId)
    {
        if (CategoryId > 0)
        {
            //  divProduct.Visible = true;
            DataTable dtProduct = (new Cls_product_b().SelectAllProductUsingCategoryIdAdmin(CategoryId));
            if (dtProduct != null)
            {
                // divSaveAll.Visible = true;
                // divExcel.Visible = true;
                repProduct.DataSource = dtProduct;
                repProduct.DataBind();
            }
            else
            {
                //  divExcel.Visible = false;
                //  divSaveAll.Visible = false;
                repProduct.DataSource = null;
                repProduct.DataBind();
            }
        }
        else
        {
            //   divSaveAll.Visible = false;
            repProduct.DataSource = null;
            repProduct.DataBind();
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        Int64 ProductId = int.Parse((item.FindControl("lblProductId") as Label).Text);
        Int64 CategoryId = int.Parse((item.FindControl("lblCategoryId") as Label).Text);

        bool yes = (new Cls_product_b().Delete(ProductId, CategoryId));
        spnMessage.Visible = true;
        if (yes)
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Product Deleted Successfully";
            //  BindProduct(Convert.ToInt64(ViewState["CategoryId"]));
            search();
        }
        else
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Product Not Deleted";
        }
    }

    protected void btnAddNewProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditproduct.aspx"));
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        // BindProduct(Convert.ToInt64(ddlCategory.SelectedValue));
        search();
        ViewState["CategoryId"] = ddlCategory.SelectedValue;
    }

    protected void repProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            DropDownList ddlSeqNo = (DropDownList)e.Item.FindControl("ddlSeqNo");
            HyperLink hlAddImageVideo = (HyperLink)e.Item.FindControl("hlAddImageVideo");
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addeditproduct.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "pid").ToString(), true));
            hlAddImageVideo.NavigateUrl = Page.ResolveUrl("~/addeditproductmultipleimages.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "pid").ToString(), true));
            //Image imgProduct = (Image)e.Item.FindControl("imgProduct");
            //imgProduct.ImageUrl = productFrontPath + DataBinder.Eval(e.Item.DataItem, "mainimage").ToString();
            Fill_SeqNo(Convert.ToInt64(DataBinder.Eval(e.Item.DataItem, "SeqNo")), Convert.ToInt64(DataBinder.Eval(e.Item.DataItem, "MaxSeqNo")), ref ddlSeqNo);


            HyperLink hlAddProcess = (HyperLink)e.Item.FindControl("hlAddProcess");
            hlAddProcess.NavigateUrl = Page.ResolveUrl("~/addeditArticle.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "pid").ToString(), true));

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool yes = false;
        foreach (RepeaterItem item in repProduct.Items)
        {
            CheckBox chkContainer = (CheckBox)item.FindControl("chkContainer");
            if (chkContainer.Checked)
            {
                string ProductId = chkContainer.Attributes["attr-ID"];
                TextBox txtDiscountPrice = (TextBox)item.FindControl("txtDiscountPrice");
                TextBox txtStockQuantites = (TextBox)item.FindControl("txtStockQuantites");
                TextBox txtStockAlertQuantites = (TextBox)item.FindControl("txtStockAlertQuantites");
                TextBox txtCustomerPrice = (TextBox)item.FindControl("txtCustomerPrice");
                TextBox txtDealerPrice = (TextBox)item.FindControl("txtDealerPrice");
                TextBox txtWholesaleprice = (TextBox)item.FindControl("txtWholesaleprice");
                TextBox txtSuperWholesaleprice = (TextBox)item.FindControl("txtSuperWholesaleprice");
                TextBox txtGST = (TextBox)item.FindControl("txtGST");
                CheckBox chbIsStock = (CheckBox)item.FindControl("chbIsStock");
                CheckBox chbIsActive = (CheckBox)item.FindControl("IsActive");
                CheckBox chkisHotproduct = (CheckBox)item.FindControl("isHotproduct");
                Decimal DiscountPrice = 0;
                int StockQuantites = 0;
                int StockAlertQuantites = 0;
                Decimal CustomerPrice = 0;
                Decimal DealerPrice = 0;
                Decimal Wholesaleprice = 0;
                Decimal SuperWholesaleprice = 0;
                Decimal GST = 0;
                if (!string.IsNullOrEmpty(txtDiscountPrice.Text))
                {
                    DiscountPrice = Convert.ToDecimal(txtDiscountPrice.Text);
                }
                else
                {
                    DiscountPrice = 0;
                }

                if (!string.IsNullOrEmpty(txtStockQuantites.Text))
                {
                    StockQuantites = Convert.ToInt32(txtStockQuantites.Text);
                }
                else
                {
                    StockQuantites = 0;
                }
                if (!string.IsNullOrEmpty(txtStockAlertQuantites.Text))
                {
                    StockAlertQuantites = Convert.ToInt32(txtStockAlertQuantites.Text);
                }
                else
                {
                    StockAlertQuantites = 0;
                }
                if (!string.IsNullOrEmpty(txtCustomerPrice.Text))
                {
                    CustomerPrice = Convert.ToDecimal(txtCustomerPrice.Text);
                }
                else
                {
                    CustomerPrice = 0;
                }
                if (!string.IsNullOrEmpty(txtDealerPrice.Text))
                {
                    DealerPrice = Convert.ToDecimal(txtDealerPrice.Text);
                }
                else
                {
                    DealerPrice = 0;
                }
                if (!string.IsNullOrEmpty(txtWholesaleprice.Text))
                {
                    Wholesaleprice = Convert.ToDecimal(txtWholesaleprice.Text);
                }
                else
                {
                    Wholesaleprice = 0;
                }
                if (!string.IsNullOrEmpty(txtSuperWholesaleprice.Text))
                {
                    SuperWholesaleprice = Convert.ToDecimal(txtSuperWholesaleprice.Text);
                }
                else
                {
                    SuperWholesaleprice = 0;
                }
                if (!string.IsNullOrEmpty(txtGST.Text))
                {
                    GST = Convert.ToDecimal(txtGST.Text);
                }
                else
                {
                    GST = 0;
                }

                yes = (new Cls_product_b().Product_UpdatePrice(Convert.ToInt64(ProductId), CustomerPrice, DealerPrice, DiscountPrice, GST, StockQuantites, chbIsStock.Checked, chbIsActive.Checked, Wholesaleprice, SuperWholesaleprice, StockAlertQuantites,chkisHotproduct.Checked));
            }
        }
        spnMessage.Visible = true;
        spnMessage.Style.Add("color", "green");
        spnMessage.InnerText = "Product Updated Successfully";
        if (ViewState["CategoryId"] != null)
        {
            // BindProduct(Convert.ToInt64(ViewState["CategoryId"]));
            search();
        }
        else
        {
            // BindProduct(Convert.ToInt64(ddlCategory.SelectedValue));
            search();

        }
    }

    protected void ddlSeqNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList DDLSeqNo = (DropDownList)sender;
        RepeaterItem item = (sender as DropDownList).Parent as RepeaterItem;
        Int64 ProductId = int.Parse((item.FindControl("lblProductId") as Label).Text);
        Int64 CategoryId = int.Parse((item.FindControl("lblCategoryId") as Label).Text);
        Int64 PreSeqNo = int.Parse((item.FindControl("lblSeqNo") as Label).Text);
        Update_SeqNo_Product_DB(ProductId, CategoryId, PreSeqNo, Convert.ToInt64(DDLSeqNo.SelectedValue));
    }

    public void Update_SeqNo_Product_DB(Int64 ProductId, Int64 CategoryId, Int64 PrevSeqNo, Int64 SeqNo)
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Update_SeqNo_Product";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Con;

            cmd.Parameters.AddWithValue("@ProductID", ProductId);
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            cmd.Parameters.AddWithValue("@PrevSeqNo", PrevSeqNo);
            cmd.Parameters.AddWithValue("@SeqNo", SeqNo);
            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Product Updated Successfully";
            BindProduct(CategoryId);
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            Con.Close();
        }
    }

    public void Fill_SeqNo(Int64 SelectedSeqNo, Int64 MaxSeqNo, ref DropDownList DDLSeqNo)
    {
        ListItem lst0;
        for (int i = 0; i < MaxSeqNo; i++)
        {
            lst0 = new ListItem((i + 1).ToString(), (i + 1).ToString());
            DDLSeqNo.Items.Add(lst0);
        }
        DDLSeqNo.DataBind();
        if (DDLSeqNo.Items.Count > 0)
            DDLSeqNo.SelectedValue = SelectedSeqNo.ToString();
    }

    protected void btnExcelExport_Click(object sender, EventArgs e)
    {
        try
        {
            SelectAllProductUsingCategoryIdAdmin();
            if (Session["dtProduct"] != null)
            {
                Response.Redirect("ExcelExport.aspx?filename=Product_Master_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SelectAllProductUsingCategoryIdAdmin()
    {
        DataTable dtTable = new DataTable();
        SqlConnection ConnectionString = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlDataAdapter da;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ProductExcelExportUsingCategoryId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@categoryid", ddlCategory.SelectedValue);
            ConnectionString.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(dtTable);
            if (dtTable != null)
            {
                if (dtTable.Rows.Count > 0)
                {
                    Session["dtProduct"] = dtTable;
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }
    }
    protected void btn_updateRealStock_Click(object sender, EventArgs e)
    {
        DataTable dtTable = new DataTable();
        SqlConnection ConnectionString = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlDataAdapter da;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "product_SelectAllAdmin";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            // cmd.Parameters.AddWithValue("@categoryid", ddlCategory.SelectedValue);
            ConnectionString.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(dtTable);
            if (dtTable != null)
            {
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    string pid1 = dtTable.Rows[i]["pid"].ToString();
                    string quantites1 = dtTable.Rows[i]["quantites"].ToString();
                    string RealStock1 = dtTable.Rows[i]["RealStock"].ToString();
                    string s = "update product set quantites=" + RealStock1 + " where pid=" + pid1 + "";
                    SqlCommand cmd1 = new SqlCommand(s, ConnectionString);
                    int t = cmd1.ExecuteNonQuery();


                }
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Stock Updated Successfully')", true);
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }
    }


    //paging------------------
    private int CurrentPage
    {
        get
        {
            if (ViewState["CurrentPage"] == null)
            {
                return 0;
            }
            return ((int)ViewState["CurrentPage"]);
        }
        set
        {
            ViewState["CurrentPage"] = value;
        }
    }
    // Bind PagedDataSource into Repeater
    private void BindDataIntoRepeater(DataTable dtData)
    {
        //var dt = SelectAllActiveUser();
        var dt = dtData;
        _pgsource.DataSource = dt.DefaultView;
        _pgsource.AllowPaging = true;
        // Number of items to be displayed in the Repeater
        _pgsource.PageSize = _pageSize;
        _pgsource.CurrentPageIndex = CurrentPage;
        // Keep the Total pages in View State
        ViewState["TotalPages"] = _pgsource.PageCount;
        // Example: "Page 1 of 10"
        lblpage.Text = "Page " + (CurrentPage + 1) + " of " + _pgsource.PageCount;
        // Enable First, Last, Previous, Next buttons
        lbPrevious.Enabled = !_pgsource.IsFirstPage;
        lbNext.Enabled = !_pgsource.IsLastPage;
        lbFirst.Enabled = !_pgsource.IsFirstPage;
        lbLast.Enabled = !_pgsource.IsLastPage;

        repProduct.DataSource = null;
        repProduct.DataBind();
        // Bind data into repeater
        repProduct.DataSource = _pgsource;
        repProduct.DataBind();

        // Call the function to do paging
        HandlePaging();
    }

    private void HandlePaging()
    {
        var dt = new DataTable();
        dt.Columns.Add("PageIndex"); //Start from 0
        dt.Columns.Add("PageText"); //Start from 1

        _firstIndex = CurrentPage - 5;
        if (CurrentPage > 5)
            _lastIndex = CurrentPage + 5;
        else
            _lastIndex = 10;

        // Check last page is greater than total page then reduced it to total no. of page is last index
        if (_lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
        {
            _lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
            _firstIndex = _lastIndex - 10;
        }

        if (_firstIndex < 0)
            _firstIndex = 0;

        // Now creating page number based on above first and last page index
        for (var i = _firstIndex; i < _lastIndex; i++)
        {
            var dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i + 1;
            dt.Rows.Add(dr);
        }

        rptPaging.DataSource = null;
        rptPaging.DataBind();
        rptPaging.DataSource = dt;
        rptPaging.DataBind();
    }

    protected void lbFirst_Click(object sender, EventArgs e)
    {
        CurrentPage = 0;
        //BindDataIntoRepeater(SelectAllActiveUser());
        //txtSearch_TextChanged(null, null);
        search();
    }
    protected void lbLast_Click(object sender, EventArgs e)
    {
        CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
        //BindDataIntoRepeater(SelectAllActiveUser());
        //txtSearch_TextChanged(null, null);
        search();
    }
    protected void lbPrevious_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        //BindDataIntoRepeater(SelectAllActiveUser());
        //  txtSearch_TextChanged(null, null);
        search();
    }
    protected void lbNext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        // txtSearch_TextChanged(null, null);
        //BindDataIntoRepeater(SelectAllActiveUser());
        search();
    }

    protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (!e.CommandName.Equals("newPage")) return;
        CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
        //  txtSearch_TextChanged(null, null);
        //BindDataIntoRepeater(SelectAllActiveUser());
        search();
    }

    protected void rptPaging_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        var lnkPage = (LinkButton)e.Item.FindControl("lbPaging");
        if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
        lnkPage.Enabled = false;
        lnkPage.BackColor = System.Drawing.Color.FromName("#00FF00");
    }
    public void search()
    {
        #region
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        DataTable ds = new DataTable();
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "product_SelectAllProductUsingCategoryIdAdminPaging";

            cmd.CommandType = CommandType.StoredProcedure;
            if (txtSearch.Text == "")
            {
                cmd.Parameters.Add("@seachtext", null);
            }
            else
            {
                cmd.Parameters.Add("@seachtext", txtSearch.Text);

            }
           
                cmd.Parameters.Add("@isactive", Convert.ToInt64(ddlActiveStatus.SelectedValue.ToString()));
          
            cmd.Parameters.Add("@cid", Convert.ToInt64(ddlCategory.SelectedValue.ToString()));
            cmd.Connection = ConnectionString;
            ConnectionString.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);


            lbPrevious.Enabled = true;
            lbNext.Enabled = false;
            lbFirst.Enabled = true;
            lbLast.Enabled = false;
            //CurrentPage = 0;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }
        BindDataIntoRepeater(ds);
        #endregion
    }


    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        CurrentPage = 0;
        search();
    }
    //protected void btn_pdfDownload_Click(object sender, EventArgs e)
    //{
    //    if (ddlCategory.SelectedIndex == 0)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Please Select Category')", true);
    //    }
    //    else
    //    {
    //        if (ListBox1.SelectedIndex == 0)
    //        {
    //            #region "Pdf "
    //            txtPath.Text = string.Empty;
    //            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //            try
    //            {
    //                con.Open();
    //                StringBuilder sb = new StringBuilder();
    //                string s = "";


    //                //string finalResult = string.Empty;
    //                Paragraph paragraph = new Paragraph();
    //                paragraph.Add(("Date: " + DateTime.Now.ToString("dd/MM/yyyy")).Replace('-', '/'));
    //                paragraph.Alignment = Element.ALIGN_RIGHT;
    //                Int64 c = Convert.ToInt64(ddlCategory.SelectedValue.ToString());

    //                DataTable dt = (new Cls_product_b().Product_WSSelectAllProductUsingCategoryId(c));



    //                iTextSharp.text.Document documnent = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 20f, 20f, 20f, 20f);

    //                PdfPTable tableCategory = new PdfPTable(3);
    //                iTextSharp.text.Image myImg = iTextSharp.text.Image.GetInstance(Server.MapPath("Uploads/MooryaTools.png"));
    //                myImg.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
    //                myImg.ScaleAbsolute(220f, 75f);


    //                //tableCategory.AddCell("Category Name:" + dtProducts.Rows[0]["Categoryname"]);
    //                //tableCategory.AddCell(("Date: " + DateTime.Now.ToString("dd/MM/yyyy")).Replace('-', '/'));
    //                //tableCategory.SpacingAfter = 12.5f;
    //                documnent.Open();
    //                //documnent.Add(tableCategory);
    //                Chunk glue = new Chunk(new VerticalPositionMark());
    //                Paragraph para = new Paragraph();
    //                string finalResult = string.Empty;
    //                MemoryStream mstr = new MemoryStream();

    //                String path = Server.MapPath("uploads");
    //                string PDFName = Guid.NewGuid().ToString();
    //                // string PDFName = Convert.ToString(dtProducts.Rows[0]["Categoryname"]);
    //                //PdfWriter writer = PdfWriter.GetInstance(documnent, new FileStream(path + "/" + PDFName + ".pdf", FileMode.Create));
    //                PdfWriter writer = PdfWriter.GetInstance(documnent, mstr);

    //                Paragraph paragraph1 = new Paragraph();
    //                paragraph1.Add("------------------------------------------------------------------");
    //                paragraph1.Add("\n");
    //                paragraph1.Add("Category:" + dt.Rows[0]["Categoryname"]);
    //                paragraph1.Add("\n");
    //                paragraph1.Add("------------------------------------------------------------------");
    //                paragraph1.Alignment = Element.ALIGN_CENTER;
    //                paragraph1.Add("\n");
    //                paragraph1.Add("Below mention price including GST");
    //                paragraph1.SpacingAfter = 12.5f;

    //                paragraph.SetLeading(1.0f, 3.0f);
    //                documnent.Open();
    //                paragraph.Add("");



    //                Phrase ph1 = new Phrase();

    //                Paragraph mm = new Paragraph();
    //                PdfPTable table = new PdfPTable(2);

    //                table.SetWidths(new float[] { 4f, 2f });
    //                table.AddCell("Product Name");
    //                table.AddCell("Image");

    //                for (int i = 0; i < dt.Rows.Count; i++)
    //                {
    //                    try
    //                    {
    //                        table.AddCell(dt.Rows[i]["productname"].ToString());
    //                        if (dt.Rows[i]["imagename"] != System.DBNull.Value)
    //                        {
    //                            //iTextSharp.text.Image myImg1 = iTextSharp.text.Image.GetInstance(Server.MapPath("Uploads/ImageNotFound.png"));
    //                            //table.AddCell(iTextSharp.text.Image.GetInstance((Server.MapPath("Uploads/ImageNotFound.png"))));
    //                            iTextSharp.text.Image myImg1 = iTextSharp.text.Image.GetInstance(("" + dt.Rows[i]["imagename"] + " "));
    //                            myImg1.ScaleAbsolute(100f, 100f);
    //                            table.AddCell(iTextSharp.text.Image.GetInstance(("" + dt.Rows[i]["imagename"] + " ")));
    //                        }
    //                        else
    //                        {
    //                            iTextSharp.text.Image myImg1 = iTextSharp.text.Image.GetInstance(Server.MapPath("Uploads/ImageNotFound.png"));
    //                            table.AddCell(iTextSharp.text.Image.GetInstance((Server.MapPath("Uploads/ImageNotFound.png"))));
    //                        }




    //                    }
    //                    catch (Exception)
    //                    {
    //                    }
    //                }

    //                Paragraph paragraph2 = new Paragraph();
    //                var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, Color.BLUE);
    //                string ssss = " ''Morya Tools'' ";
    //                paragraph2.Add("Above mention price including GST" + Environment.NewLine + "For iOS");
    //                paragraph2.Add(Environment.NewLine + Environment.NewLine + "To Know the Prices,Please Download our app" + ssss);
    //                paragraph2.Add(Environment.NewLine + "For Android" + Environment.NewLine);
    //                //   paragraph2.Add(Environment.NewLine + Environment.NewLine + "https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en");
    //                var c1 = new Chunk("https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en", titleFontBlue);

    //                c1.SetAnchor("https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en");
    //                paragraph2.Add(c1);

    //                //----
    //                var c11 = new Chunk("https://itunes.apple.com/in/app/morya-tools/id1315540530?mt=8", titleFontBlue);

    //                c11.SetAnchor("https://itunes.apple.com/in/app/morya-tools/id1315540530?mt=8");

    //                paragraph2.Add(Environment.NewLine + "For iOS" + Environment.NewLine);

    //                paragraph2.Alignment = Element.ALIGN_CENTER;
    //                paragraph2.Add(c11);

    //                // var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, Color.BLUE);
    //                // string ssss = " ''Morya Tools'' ";
    //                // paragraph2.Add("Above mention price including GST");
    //                // paragraph2.Add(Environment.NewLine + Environment.NewLine + "To Know the Prices,Please Download our app" + ssss);
    //                // //   paragraph2.Add(Environment.NewLine + Environment.NewLine + "https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en");
    //                // var c1 = new Chunk("https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en", titleFontBlue);
    //                // c1.SetAnchor("https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en");
    //                //paragraph2.Add(c1);

    //                paragraph2.Alignment = Element.ALIGN_CENTER;

    //                documnent.Add(paragraph);

    //                documnent.Add(myImg);
    //                documnent.Add(paragraph1);
    //                documnent.Add(table);
    //                documnent.Add(paragraph2);
    //                mm.Add(ph1);
    //                para.Add(mm);
    //                documnent.Add(para);
    //                Paragraph p2 = new Paragraph();
    //                p2.SetLeading(5f, 5f);
    //                documnent.Add(paragraph);
    //                documnent.Add(p2);
    //                documnent.Close();

    //                DataTable dtPDFPath = new DataTable();
    //                dtPDFPath.Columns.Add("PDFPath", typeof(string));
    //                dtPDFPath.Rows.Add(@"http://moryaapp.moryatools.com/uploads/" + PDFName + ".pdf");

    //                txtPath.Text = "http://moryaapp.moryatools.com/uploads/" + PDFName + ".pdf";

    //                DownloadPDF(mstr, ddlCategory.SelectedItem.ToString());



    //            }
    //            catch (Exception obj)
    //            {
    //            }
    //            finally { }
    //            #endregion
    //        }
    //        else
    //        {

    //            #region "Pdf "
    //            txtPath.Text = string.Empty;
    //            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //            try
    //            {
    //                con.Open();
    //                StringBuilder sb = new StringBuilder();
    //                string s = "";


    //                //string finalResult = string.Empty;
    //                Paragraph paragraph = new Paragraph();
    //                paragraph.Add(("Date: " + DateTime.Now.ToString("dd/MM/yyyy")).Replace('-', '/'));
    //                paragraph.Alignment = Element.ALIGN_RIGHT;
    //                Int64 c = Convert.ToInt64(ddlCategory.SelectedValue.ToString());
    //                DataTable dt = new DataTable();
    //                dt = (new Cls_product_b().Product_WSSelectAllProductUsingCategoryId(c));



    //                Document documnent = new Document(iTextSharp.text.PageSize.A4, 20f, 20f, 20f, 20f);



    //                PdfPTable tableCategory = new PdfPTable(3);
    //                iTextSharp.text.Image myImg = iTextSharp.text.Image.GetInstance(Server.MapPath("Uploads/MooryaTools.png"));
    //                myImg.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
    //                myImg.ScaleAbsolute(220f, 75f);


    //                //tableCategory.AddCell("Category Name:" + dtProducts.Rows[0]["Categoryname"]);
    //                //tableCategory.AddCell(("Date: " + DateTime.Now.ToString("dd/MM/yyyy")).Replace('-', '/'));
    //                //tableCategory.SpacingAfter = 12.5f;
    //                documnent.Open();
    //                //documnent.Add(tableCategory);
    //                Chunk glue = new Chunk(new VerticalPositionMark());
    //                Paragraph para = new Paragraph();
    //                string finalResult = string.Empty;
    //                MemoryStream mstr = new MemoryStream();

    //                String path = Server.MapPath("uploads");
    //                string PDFName = Guid.NewGuid().ToString();
    //                // string PDFName = Convert.ToString(dtProducts.Rows[0]["Categoryname"]);
    //                //PdfWriter writer = PdfWriter.GetInstance(documnent, new FileStream(path + "/" + PDFName + ".pdf", FileMode.Create));
    //                PdfWriter writer = PdfWriter.GetInstance(documnent, mstr);
    //                //PdfWriter writer = PdfWriter.GetInstance(documnent,mstr);


    //                Paragraph paragraph1 = new Paragraph();
    //                paragraph1.Add("------------------------------------------------------------------");
    //                paragraph1.Add("\n");
    //                paragraph1.Add("Category:" + dt.Rows[0]["Categoryname"]);
    //                paragraph1.Add("\n");
    //                paragraph1.Add("------------------------------------------------------------------");
    //                paragraph1.Alignment = Element.ALIGN_CENTER;
    //                paragraph1.Add("\n");
    //                paragraph1.Add("Below mention price including GST");
    //                paragraph1.SpacingAfter = 12.5f;

    //                paragraph.SetLeading(1.0f, 3.0f);
    //                documnent.Open();
    //                paragraph.Add("");



    //                Phrase ph1 = new Phrase();

    //                Paragraph mm = new Paragraph();
    //                PdfPTable table = new PdfPTable(3);

    //                table.SetWidths(new float[] { 2f, 2f, 2f });
    //                table.AddCell("Product Name");
    //                table.AddCell("Image");
    //                //table.AddCell("Quantities");
    //                //table.AddCell("GST");
    //                //table.AddCell("Rate");
    //                string hh = ListBox1.SelectedItem.ToString();
    //                table.AddCell(ListBox1.SelectedItem.ToString());
    //                //if (chk_super.Checked == true)
    //                //{
    //                //    table.AddCell("superwholesaleprice");

    //                //}
    //                //if (chk_whole.Checked == true)
    //                //{
    //                //    table.AddCell("wholesaleprice");
    //                //}
    //                //if (chk_dealer.Checked == true)
    //                //{
    //                //    table.AddCell("dealerprice");
    //                //}
    //                //if (chk_custmer.Checked == true)
    //                //{
    //                //    table.AddCell("customerprice");
    //                //}
    //                ////else
    //                ////{
    //                ////    table.AddCell("");
    //                ////}
    //                for (int i = 0; i < dt.Rows.Count; i++)
    //                {
    //                    try
    //                    {
    //                        table.AddCell(dt.Rows[i]["productname"].ToString());
    //                        if (dt.Rows[i]["imagename"] != System.DBNull.Value)
    //                        {
    //                            //iTextSharp.text.Image myImg1 = iTextSharp.text.Image.GetInstance(Server.MapPath("Uploads/ImageNotFound.png"));
    //                            //table.AddCell(iTextSharp.text.Image.GetInstance((Server.MapPath("Uploads/ImageNotFound.png"))));
    //                            iTextSharp.text.Image myImg1 = iTextSharp.text.Image.GetInstance(("" + dt.Rows[i]["imagename"] + " "));
    //                            myImg1.ScaleAbsolute(100f, 100f);
    //                            table.AddCell(iTextSharp.text.Image.GetInstance(("" + dt.Rows[i]["imagename"] + " ")));
    //                        }
    //                        else
    //                        {
    //                            iTextSharp.text.Image myImg1 = iTextSharp.text.Image.GetInstance(Server.MapPath("Uploads/ImageNotFound.png"));
    //                            table.AddCell(iTextSharp.text.Image.GetInstance((Server.MapPath("Uploads/ImageNotFound.png"))));
    //                        }

    //                        //table.AddCell(dtProducts.Rows[i]["quantites"].ToString());
    //                        //table.AddCell(dtProducts.Rows[i]["gst"].ToString());
    //                        //table.AddCell(dtProducts.Rows[i]["dealerprice"].ToString());

    //                        string price1 = ListBox1.SelectedValue.ToString();
    //                        ////if (chk_super.Checked == true)
    //                        ////{
    //                        table.AddCell(dt.Rows[i][price1].ToString());

    //                        ////}
    //                        ////if (chk_whole.Checked == true)
    //                        ////{
    //                        ////    table.AddCell(dtProducts.Rows[i]["wholesaleprice"].ToString());

    //                        ////}
    //                        ////if (chk_dealer.Checked == true)
    //                        ////{
    //                        ////    table.AddCell(dtProducts.Rows[i]["dealerprice"].ToString());

    //                        ////}
    //                        ////if (chk_custmer.Checked == true)
    //                        ////{
    //                        ////    table.AddCell(dtProducts.Rows[i]["customerprice"].ToString());

    //                        ////}
    //                        //else if (chk_custmer.Checked == true)
    //                        //{
    //                        //    table.AddCell("");

    //                        //}

    //                    }
    //                    catch (Exception)
    //                    {
    //                    }
    //                }

    //                Paragraph paragraph2 = new Paragraph();
    //                var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, Color.BLUE);
    //                string ssss = " ''Morya Tools'' ";
    //                paragraph2.Add("Above mention price including GST" + Environment.NewLine + "For iOS");
    //                paragraph2.Add(Environment.NewLine + Environment.NewLine + "To Know the Prices,Please Download our app" + ssss);
    //                paragraph2.Add(Environment.NewLine + "For Android" + Environment.NewLine);
    //                //   paragraph2.Add(Environment.NewLine + Environment.NewLine + "https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en");
    //                var c1 = new Chunk("https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en", titleFontBlue);

    //                c1.SetAnchor("https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en");
    //                paragraph2.Add(c1);

    //                //----
    //                var c11 = new Chunk("https://itunes.apple.com/in/app/morya-tools/id1315540530?mt=8", titleFontBlue);

    //                c11.SetAnchor("https://itunes.apple.com/in/app/morya-tools/id1315540530?mt=8");

    //                paragraph2.Add(Environment.NewLine + "For iOS" + Environment.NewLine);

    //                paragraph2.Alignment = Element.ALIGN_CENTER;
    //                paragraph2.Add(c11);

    //                paragraph2.Alignment = Element.ALIGN_CENTER;

    //                documnent.Add(paragraph);

    //                documnent.Add(myImg);
    //                documnent.Add(paragraph1);
    //                documnent.Add(table);
    //                documnent.Add(paragraph2);
    //                mm.Add(ph1);
    //                para.Add(mm);
    //                documnent.Add(para);
    //                Paragraph p2 = new Paragraph();
    //                p2.SetLeading(5f, 5f);
    //                documnent.Add(paragraph);
    //                documnent.Add(p2);
    //                documnent.Close();
    //                DataTable dtPDFPath = new DataTable();
    //                dtPDFPath.Columns.Add("PDFPath", typeof(string));
    //                dtPDFPath.Rows.Add(@"http://moryaapp.moryatools.com/uploads/" + PDFName + ".pdf");

    //                txtPath.Text = "http://moryaapp.moryatools.com/uploads/" + PDFName + ".pdf";
    //                DownloadPDF(mstr, ddlCategory.SelectedItem.ToString());

    //                //String po = "PDFName";
    //                //DownloadPDF(mstr, po);

    //                ////-----
    //                //Response.ContentType = "application/pdf";
    //                //Response.AppendHeader("Content-Disposition", "attachment; filename=MyFile.pdf");
    //                //Response.TransmitFile(Server.MapPath("~/uploads/" + PDFName + ".pdf"));
    //                //Response.End();
    //            }
    //            catch (Exception o)
    //            { }
    //            finally { }
    //            #endregion

    //        }
    //    }

    //}
    protected void btn_pdfDownload_Click(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Please Select Category')", true);
        }
        else
        {
            if (ListBox1.SelectedIndex == 0)
            {
                #region "Pdf "
                txtPath.Text = string.Empty;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                try
                {
                    con.Open();
                    StringBuilder sb = new StringBuilder();
                    string s = "";


                    //string finalResult = string.Empty;
                    Paragraph paragraph = new Paragraph();
                    paragraph.Add(("Date: " + DateTime.Now.ToString("dd/MM/yyyy")).Replace('-', '/'));
                    paragraph.Alignment = Element.ALIGN_RIGHT;
                    Int64 c = Convert.ToInt64(ddlCategory.SelectedValue.ToString());

                    DataTable dt = (new Cls_product_b().Product_WSSelectAllProductUsingCategoryId(c));



                    Document documnent = new Document(iTextSharp.text.PageSize.A4, 20f, 20f, 20f, 20f);

                    PdfPTable tableCategory = new PdfPTable(3);
                    iTextSharp.text.Image myImg = iTextSharp.text.Image.GetInstance(Server.MapPath("Uploads/MooryaTools.png"));
                    myImg.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    myImg.ScaleAbsolute(220f, 75f);


                    //tableCategory.AddCell("Category Name:" + dtProducts.Rows[0]["Categoryname"]);
                    //tableCategory.AddCell(("Date: " + DateTime.Now.ToString("dd/MM/yyyy")).Replace('-', '/'));
                    //tableCategory.SpacingAfter = 12.5f;
                    documnent.Open();
                    //documnent.Add(tableCategory);
                    Chunk glue = new Chunk(new VerticalPositionMark());
                    Paragraph para = new Paragraph();
                    string finalResult = string.Empty;
                    MemoryStream mstr = new MemoryStream();

                    String path = Server.MapPath("uploads");
                    string PDFName = Guid.NewGuid().ToString();
                    // string PDFName = Convert.ToString(dtProducts.Rows[0]["Categoryname"]);
                    PdfWriter writer = PdfWriter.GetInstance(documnent, new FileStream(path + "/" + PDFName + ".pdf", FileMode.Create));

                    //  PdfWriter writer = PdfWriter.GetInstance(documnent, mstr);

                    Paragraph paragraph1 = new Paragraph();
                    paragraph1.Add("------------------------------------------------------------------");
                    paragraph1.Add("\n");
                    paragraph1.Add("Category:" + dt.Rows[0]["Categoryname"]);
                    paragraph1.Add("\n");
                    paragraph1.Add("------------------------------------------------------------------");
                    paragraph1.Alignment = Element.ALIGN_CENTER;
                    paragraph1.Add("\n");
                    paragraph1.Add("Below mention price including GST");
                    paragraph1.SpacingAfter = 12.5f;

                    paragraph.SetLeading(1.0f, 3.0f);
                    documnent.Open();
                    paragraph.Add("");


                    var font = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 60, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.LIGHT_GRAY);
                    ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_CENTER, new Phrase("", font), 300, 400, 45);



                    Phrase ph1 = new Phrase();

                    Paragraph mm = new Paragraph();
                    PdfPTable table = new PdfPTable(2);

                    table.SetWidths(new float[] { 4f, 2f });
                    table.AddCell("Product Name");
                    table.AddCell("Image");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            table.AddCell(dt.Rows[i]["productname"].ToString());
                            if (dt.Rows[i]["imagename"] != System.DBNull.Value)
                            {
                                //iTextSharp.text.Image myImg1 = iTextSharp.text.Image.GetInstance(Server.MapPath("Uploads/ImageNotFound.png"));
                                //table.AddCell(iTextSharp.text.Image.GetInstance((Server.MapPath("Uploads/ImageNotFound.png"))));
                                iTextSharp.text.Image myImg1 = iTextSharp.text.Image.GetInstance(("" + dt.Rows[i]["imagename"] + " "));
                                myImg1.ScaleAbsolute(100f, 100f);
                                table.AddCell(iTextSharp.text.Image.GetInstance(("" + dt.Rows[i]["imagename"] + " ")));
                            }
                            else
                            {
                                iTextSharp.text.Image myImg1 = iTextSharp.text.Image.GetInstance(Server.MapPath("Uploads/ImageNotFound.png"));
                                table.AddCell(iTextSharp.text.Image.GetInstance((Server.MapPath("Uploads/ImageNotFound.png"))));
                            }




                        }
                        catch (Exception)
                        {
                        }
                    }

                    Paragraph paragraph2 = new Paragraph();

                    var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, Color.BLUE);
                    string ssss = " ''Morya Tools'' ";
                    paragraph2.Add("Above mention price including GST" + Environment.NewLine + "For iOS");
                    paragraph2.Add(Environment.NewLine + Environment.NewLine + "To Know the Prices,Please Download our app" + ssss);
                    paragraph2.Add(Environment.NewLine + "For Android" + Environment.NewLine);
                    //   paragraph2.Add(Environment.NewLine + Environment.NewLine + "https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en");
                    var c1 = new Chunk("https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en", titleFontBlue);

                    c1.SetAnchor("https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en");
                    paragraph2.Add(c1);

                    //----
                    var c11 = new Chunk("https://itunes.apple.com/in/app/morya-tools/id1315540530?mt=8", titleFontBlue);

                    c11.SetAnchor("https://itunes.apple.com/in/app/morya-tools/id1315540530?mt=8");

                    paragraph2.Add(Environment.NewLine + "For iOS" + Environment.NewLine);

                    paragraph2.Alignment = Element.ALIGN_CENTER;
                    paragraph2.Add(c11);

                    paragraph2.Alignment = Element.ALIGN_CENTER;

                    documnent.Add(paragraph);

                    documnent.Add(myImg);
                    documnent.Add(paragraph1);
                    documnent.Add(table);
                    documnent.Add(paragraph2);
                    mm.Add(ph1);
                    para.Add(mm);
                    documnent.Add(para);
                    Paragraph p2 = new Paragraph();
                    p2.SetLeading(5f, 5f);
                    documnent.Add(paragraph);
                    documnent.Add(p2);
                    documnent.Close();

                    DataTable dtPDFPath = new DataTable();
                    dtPDFPath.Columns.Add("PDFPath", typeof(string));
                    dtPDFPath.Rows.Add(@"http://moryaapp.moryatools.com/uploads/" + PDFName + ".pdf");

                    txtPath.Text = "http://moryaapp.moryatools.com/uploads/" + PDFName + ".pdf";
                    // DownloadPDF(mstr, ddlCategory.SelectedItem.ToString());

                    string[] a = new string[100];
                    for (int i = 0; i < 100; i++)
                    {
                        a[i] = " ";
                    }
                    for (int i = 0; i < 100; i = i + 4)
                    {
                        a[i] = " MORYATOOLS         MORYATOOLS          MORYATOOLS         MORYATOOLS         MORYATOOLS         MORYATOOLS ";
                    }
                    string srcpath = AppDomain.CurrentDomain.BaseDirectory + "uploads\\" + PDFName + ".pdf";
                    string destPath = AppDomain.CurrentDomain.BaseDirectory + "uploads\\WaterMarkPdf\\b.pdf";
                    AddWatermarkTextC(srcpath, destPath, a);
                    Response.ContentType = ContentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(ddlCategory.SelectedItem.ToString() + ".pdf"));
                    Response.WriteFile(destPath);
                    Response.End();



                }
                catch (Exception obj)
                {
                }
                finally { }
                #endregion
            }
            else
            {

                #region "Pdf "
                txtPath.Text = string.Empty;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                try
                {
                    con.Open();
                    StringBuilder sb = new StringBuilder();
                    string s = "";


                    //string finalResult = string.Empty;
                    Paragraph paragraph = new Paragraph();
                    paragraph.Add(("Date: " + DateTime.Now.ToString("dd/MM/yyyy")).Replace('-', '/'));
                    paragraph.Alignment = Element.ALIGN_RIGHT;
                    Int64 c = Convert.ToInt64(ddlCategory.SelectedValue.ToString());
                    DataTable dt = new DataTable();
                    dt = (new Cls_product_b().Product_WSSelectAllProductUsingCategoryId(c));



                    Document documnent = new Document(iTextSharp.text.PageSize.A4, 20f, 20f, 20f, 20f);



                    PdfPTable tableCategory = new PdfPTable(3);
                    iTextSharp.text.Image myImg = iTextSharp.text.Image.GetInstance(Server.MapPath("Uploads/MooryaTools.png"));
                    myImg.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    myImg.ScaleAbsolute(220f, 75f);


                    //tableCategory.AddCell("Category Name:" + dtProducts.Rows[0]["Categoryname"]);
                    //tableCategory.AddCell(("Date: " + DateTime.Now.ToString("dd/MM/yyyy")).Replace('-', '/'));
                    //tableCategory.SpacingAfter = 12.5f;
                    documnent.Open();
                    //documnent.Add(tableCategory);
                    Chunk glue = new Chunk(new VerticalPositionMark());
                    Paragraph para = new Paragraph();
                    string finalResult = string.Empty;
                    MemoryStream mstr = new MemoryStream();

                    String path = Context.Server.MapPath("uploads");
                    string PDFName = Guid.NewGuid().ToString();

                    PdfWriter writer = PdfWriter.GetInstance(documnent, new FileStream(path + "/" + PDFName + ".pdf", FileMode.Create));

                    // PdfWriter writer = PdfWriter.GetInstance(documnent,mstr);
                    //PdfWriter writer = PdfWriter.GetInstance(documnent,mstr);


                    Paragraph paragraph1 = new Paragraph();
                    paragraph1.Add("------------------------------------------------------------------");
                    paragraph1.Add("\n");
                    paragraph1.Add("Category:" + dt.Rows[0]["Categoryname"]);
                    paragraph1.Add("\n");
                    paragraph1.Add("------------------------------------------------------------------");
                    paragraph1.Alignment = Element.ALIGN_CENTER;
                    paragraph1.Add("\n");
                    paragraph1.Add("Below mention price including GST");
                    paragraph1.SpacingAfter = 12.5f;

                    paragraph.SetLeading(1.0f, 3.0f);
                    documnent.Open();
                    paragraph.Add("");


                    //var font = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 60, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.LIGHT_GRAY);
                    //ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_CENTER, new Phrase("MORYATOOLS", font), 300, 400, 45);



                    Phrase ph1 = new Phrase();

                    Paragraph mm = new Paragraph();
                    PdfPTable table = new PdfPTable(3);

                    table.SetWidths(new float[] { 2f, 2f, 2f });
                    table.AddCell("Product Name");
                    table.AddCell("Image");
                    //table.AddCell("Quantities");
                    //table.AddCell("GST");
                    //table.AddCell("Rate");
                    string hh = ListBox1.SelectedItem.ToString();
                    table.AddCell(ListBox1.SelectedItem.ToString());
                    //if (chk_super.Checked == true)
                    //{
                    //    table.AddCell("superwholesaleprice");

                    //}
                    //if (chk_whole.Checked == true)
                    //{
                    //    table.AddCell("wholesaleprice");
                    //}
                    //if (chk_dealer.Checked == true)
                    //{
                    //    table.AddCell("dealerprice");
                    //}
                    //if (chk_custmer.Checked == true)
                    //{
                    //    table.AddCell("customerprice");
                    //}
                    ////else
                    ////{
                    ////    table.AddCell("");
                    ////}
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            table.AddCell(dt.Rows[i]["productname"].ToString());
                            if (dt.Rows[i]["imagename"] != System.DBNull.Value)
                            {
                                //iTextSharp.text.Image myImg1 = iTextSharp.text.Image.GetInstance(Server.MapPath("Uploads/ImageNotFound.png"));
                                //table.AddCell(iTextSharp.text.Image.GetInstance((Server.MapPath("Uploads/ImageNotFound.png"))));
                                iTextSharp.text.Image myImg1 = iTextSharp.text.Image.GetInstance(("" + dt.Rows[i]["imagename"] + " "));
                                myImg1.ScaleAbsolute(100f, 100f);
                                table.AddCell(iTextSharp.text.Image.GetInstance(("" + dt.Rows[i]["imagename"] + " ")));
                            }
                            else
                            {
                                iTextSharp.text.Image myImg1 = iTextSharp.text.Image.GetInstance(Server.MapPath("Uploads/ImageNotFound.png"));
                                table.AddCell(iTextSharp.text.Image.GetInstance((Server.MapPath("Uploads/ImageNotFound.png"))));
                            }

                            //table.AddCell(dtProducts.Rows[i]["quantites"].ToString());
                            //table.AddCell(dtProducts.Rows[i]["gst"].ToString());
                            //table.AddCell(dtProducts.Rows[i]["dealerprice"].ToString());

                            string price1 = ListBox1.SelectedValue.ToString();
                            ////if (chk_super.Checked == true)
                            ////{
                            table.AddCell(dt.Rows[i][price1].ToString());

                            ////}
                            ////if (chk_whole.Checked == true)
                            ////{
                            ////    table.AddCell(dtProducts.Rows[i]["wholesaleprice"].ToString());

                            ////}
                            ////if (chk_dealer.Checked == true)
                            ////{
                            ////    table.AddCell(dtProducts.Rows[i]["dealerprice"].ToString());

                            ////}
                            ////if (chk_custmer.Checked == true)
                            ////{
                            ////    table.AddCell(dtProducts.Rows[i]["customerprice"].ToString());

                            ////}
                            //else if (chk_custmer.Checked == true)
                            //{
                            //    table.AddCell("");

                            //}

                        }
                        catch (Exception)
                        {
                        }
                    }

                    Paragraph paragraph2 = new Paragraph();

                    var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, Color.BLUE);
                    string ssss = " ''Morya Tools'' ";
                    paragraph2.Add("Above mention price including GST" + Environment.NewLine + "For iOS");
                    paragraph2.Add(Environment.NewLine + Environment.NewLine + "To Know the Prices,Please Download our app" + ssss);
                    paragraph2.Add(Environment.NewLine + "For Android" + Environment.NewLine);
                    //   paragraph2.Add(Environment.NewLine + Environment.NewLine + "https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en");
                    var c1 = new Chunk("https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en", titleFontBlue);

                    c1.SetAnchor("https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en");
                    paragraph2.Add(c1);

                    //----
                    var c11 = new Chunk("https://itunes.apple.com/in/app/morya-tools/id1315540530?mt=8", titleFontBlue);

                    c11.SetAnchor("https://itunes.apple.com/in/app/morya-tools/id1315540530?mt=8");

                    paragraph2.Add(Environment.NewLine + "For iOS" + Environment.NewLine);

                    paragraph2.Alignment = Element.ALIGN_CENTER;
                    paragraph2.Add(c11);

                    paragraph2.Alignment = Element.ALIGN_CENTER;



                    //---------


                    documnent.Add(paragraph);

                    documnent.Add(myImg);
                    documnent.Add(paragraph1);
                    documnent.Add(table);
                    documnent.Add(paragraph2);
                    mm.Add(ph1);
                    para.Add(mm);
                    documnent.Add(para);
                    Paragraph p2 = new Paragraph();
                    p2.SetLeading(5f, 5f);
                    documnent.Add(paragraph);
                    documnent.Add(p2);


                    // Save document




                    ////  var v = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "uploads\\" + PDFName + ".pdf"), FileMode.Create);
                    //  var w = PdfWriter.GetInstance(documnent,new FileStream((AppDomain.CurrentDomain.BaseDirectory + "uploads\\" + PDFName + ".pdf"), FileMode.Create));


                    //----------
                    documnent.Close();

                    DataTable dtPDFPath = new DataTable();
                    dtPDFPath.Columns.Add("PDFPath", typeof(string));
                    dtPDFPath.Rows.Add(@"http://moryaapp.moryatools.com/uploads/" + PDFName + ".pdf");

                    txtPath.Text = "http://moryaapp.moryatools.com/uploads/" + PDFName + ".pdf";
                    //  DownloadPDF(mstr, ddlCategory.SelectedItem.ToString());

                    //----------------------------

                    string[] a = new string[100];
                    for (int i = 0; i < 100; i++)
                    {
                        a[i] = " ";
                    }
                    for (int i = 0; i < 100; i = i + 4)
                    {
                        a[i] = " MORYATOOLS         MORYATOOLS          MORYATOOLS         MORYATOOLS         MORYATOOLS         MORYATOOLS ";
                    }
                    string srcpath = AppDomain.CurrentDomain.BaseDirectory + "uploads\\" + PDFName + ".pdf";
                    string destPath = AppDomain.CurrentDomain.BaseDirectory + "uploads\\WaterMarkPdf\\b.pdf";
                    AddWatermarkTextC(srcpath, destPath, a);
                    Response.ContentType = ContentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(ddlCategory.SelectedItem.ToString() + ".pdf"));
                    Response.WriteFile(destPath);
                    Response.End();



                }
                catch (Exception o)
                { }
                finally { }
                #endregion

            }
        }

    }
    public static void AddWatermarkTextC(string sourceFile, string outputFile, string[] watermarkText)
    {
        iTextSharp.text.pdf.BaseFont tWatermarkFont = null;
        float tWatermarkFontSize = 10F;
        iTextSharp.text.Color tWatermarkFontColor = null;
        float tWatermarkFontOpacity = 0.5f;
        //float tWatermarkRotation = 45.0F;
        float tWatermarkRotation = 20F;
        tWatermarkFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
        tWatermarkFontColor = iTextSharp.text.Color.GRAY;
        AddWatermarkTextC(sourceFile, outputFile, watermarkText, tWatermarkFont, tWatermarkFontSize, tWatermarkFontColor, tWatermarkFontOpacity, tWatermarkRotation);
    }//void AddWatermarkTextC(string sourceFile, string outputFile, string[] watermarkText )

    public static void AddWatermarkTextC(string sourceFile, string outputFile, string[] watermarkText, iTextSharp.text.pdf.BaseFont watermarkFont, float watermarkFontSize, iTextSharp.text.Color watermarkFontColor, float watermarkFontOpacity, float watermarkRotation)
    {

        iTextSharp.text.pdf.PdfReader reader = null;
        iTextSharp.text.pdf.PdfStamper stamper = null;
        iTextSharp.text.pdf.PdfGState gstate = null;
        iTextSharp.text.pdf.PdfContentByte underContent = null;
        iTextSharp.text.Rectangle rect = null;
        float currentY = 0.0F;
        float offset = 0.0F;
        int pageCount = 0;

        try
        {
            reader = new iTextSharp.text.pdf.PdfReader(sourceFile);
            rect = reader.GetPageSizeWithRotation(1);
            stamper = new iTextSharp.text.pdf.PdfStamper(reader, new System.IO.FileStream(outputFile, System.IO.FileMode.Create));
            if (watermarkFont == null)
            {
                watermarkFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            }//if (watermarkFont == null)

            if (watermarkFontColor == null)
            {
                watermarkFontColor = iTextSharp.text.Color.BLUE;
            }//if (watermarkFontColor == null)

            gstate = new iTextSharp.text.pdf.PdfGState();
            gstate.FillOpacity = watermarkFontOpacity;
            gstate.StrokeOpacity = watermarkFontOpacity;
            pageCount = reader.NumberOfPages;
            for (int i = 1; i <= pageCount; i++)
            {
                underContent = stamper.GetOverContent(i);
                underContent.SaveState();
                underContent.SetGState(gstate);
                underContent.SetColorFill(watermarkFontColor);
                underContent.BeginText();
                underContent.SetFontAndSize(watermarkFont, watermarkFontSize);
                underContent.SetTextMatrix(30, 30);
                currentY = rect.Height;
                //if (watermarkText.Length > 1)
                //{
                //    currentY = (rect.Height / 2) + ((watermarkFontSize * watermarkText.Length) / 2);
                //}//if (watermarkText.Length > 1)
                //else
                //{
                //    currentY = (rect.Height / 2);
                //}//else if (watermarkText.Length > 1)

                for (int j = 0; j < watermarkText.Length; j++)
                {
                    if (j > 0)
                    {
                        offset = (j * watermarkFontSize) + (watermarkFontSize / 4) * j;
                    }//if (j > 0)
                    else
                    {
                        offset = 0.0F;
                    }//else if (j > 0)

                    underContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_CENTER, watermarkText[j], rect.Width / 2, currentY - offset, watermarkRotation);
                }//for (int j = 0; j < watermarkText.Length; j++)

                underContent.EndText();
                underContent.RestoreState();
            }//for (int i = 1; i <= pageCount; i++)

            stamper.Close();
            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }//
    protected void DownloadPDF(System.IO.MemoryStream PDFData, String po)
    {
        // Clear response content & headers
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.Charset = string.Empty;
        HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
        HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.pdf", po));
        HttpContext.Current.Response.OutputStream.Write(PDFData.GetBuffer(), 0, PDFData.GetBuffer().Length);

        //  HttpContext.Current.Response.OutputStream.
        HttpContext.Current.Response.OutputStream.Flush();
        HttpContext.Current.Response.OutputStream.Close();
        HttpContext.Current.Response.End();

    }
    protected void IsActive_CheckedChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as CheckBox).Parent as RepeaterItem;
        Int64 CategoryId = int.Parse((item.FindControl("lblProductId") as Label).Text);
        bool chkSelected = Convert.ToBoolean((item.FindControl("IsActive") as CheckBox).Checked);
        bool yes = (new Cls_product_b().Product_IsActive(CategoryId, chkSelected));
        if (yes)
        {
            BindCategory();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Product Updated Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Product Not Updated";
        }
    }
    protected void isHotproduct_CheckedChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as CheckBox).Parent as RepeaterItem;
        Int64 CategoryId = int.Parse((item.FindControl("lblProductId") as Label).Text);
        bool chkSelected = Convert.ToBoolean((item.FindControl("isHotproduct") as CheckBox).Checked);
        bool yes = (new Cls_product_b().Product_IsHotProduct(CategoryId, chkSelected));
        if (yes)
        {
            BindCategory();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Product Updated Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Product Not Updated";
        }
    }
    protected void ddlCategory_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void ddlActiveStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        // BindProduct(Convert.ToInt64(ddlCategory.SelectedValue));
        search();
        ViewState["CategoryId"] = ddlCategory.SelectedValue;
    }
}