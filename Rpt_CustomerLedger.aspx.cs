using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Color = iTextSharp.text.Color;
using Font = iTextSharp.text.Font;

public partial class Rpt_CustomerLedger : System.Web.UI.Page
{
    readonly PagedDataSource _pgsource = new PagedDataSource();
    int _firstIndex, _lastIndex;
    private int _pageSize = 20;
    string pdfpath = string.Empty;




 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Customer Ledger";
            //GetLowStockProduct();
        }
        //if (Page.IsPostBack) return;
        //search();
    }
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

        repCustomerLedger.DataSource = null;
        repCustomerLedger.DataBind();
        // Bind data into repeater
        repCustomerLedger.DataSource = _pgsource;
        repCustomerLedger.DataBind();

        //------total--
        object sumTotal;
        sumTotal = dtData.Compute("Sum(total)", string.Empty);
        object sumPaid;
        sumPaid = dtData.Compute("Sum(paid)", string.Empty);
        object sumRemain;
        sumRemain = dtData.Compute("Sum(remain)", string.Empty);
        lblTotalAmount.Text = sumTotal.ToString();
        lblPaid.Text = sumPaid.ToString();
        lblRemaining.Text = sumRemain.ToString();


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
            cmd.CommandText = "ReportPaging_CustomerLedger";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@seachtext", txttsearch.Text.Trim());
            string dd = ddlUserType.SelectedValue.ToString().ToUpper().Trim();
            cmd.Parameters.AddWithValue("@userType", ddlUserType.SelectedValue.ToString().ToUpper().Trim());
            cmd.Connection = ConnectionString;
            ConnectionString.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
            {
                if (ds.Rows.Count > 0)
                {
                    Session["dtProduct"] = ds;
                }
            }
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

    //public void GetLowStockProduct()
    //{
    //    SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //    DataTable dtTable = new DataTable();
    //    SqlDataAdapter da = new SqlDataAdapter();

    //    SqlCommand cmd = new SqlCommand();
    //    cmd.CommandText = "getLowStockProduct";
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Connection = ConnectionString;
    //    ConnectionString.Open();
    //    try
    //    {
    //        da = new SqlDataAdapter(cmd);
    //        da.Fill(dtTable);
    //        if (dtTable != null)
    //        {
    //            if (dtTable.Rows.Count > 0)
    //            {
    //                gvLowProductList.DataSource = dtTable;
    //                gvLowProductList.DataBind();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.writeError(ex.Message, ex.StackTrace);
    //    }
    //    finally
    //    {
    //        ConnectionString.Close();
    //    }
    //}


    protected void btnpdf_Click(object sender, EventArgs e)
    {

        try
        {
            DataTable dt = new DataTable();
            if (Session["dtProduct"] != null)
            {
                dt = (DataTable)Session["dtProduct"];
            }
            StringBuilder sb = new StringBuilder();
            string s = "";


            //string finalResult = string.Empty;
            Paragraph paragraph = new Paragraph();
            paragraph.Add(("Date: " + DateTime.Now.ToString("dd/MM/yyyy")).Replace('-', '/'));
            paragraph.Alignment = Element.ALIGN_RIGHT;

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
            //PdfWriter writer = PdfWriter.GetInstance(documnent, new FileStream(path + "/" + PDFName + ".pdf", FileMode.Create));
            PdfWriter writer = PdfWriter.GetInstance(documnent, mstr);
            //PdfWriter writer = PdfWriter.GetInstance(documnent,mstr);


            Paragraph paragraph1 = new Paragraph();
            paragraph1.Add("------------------------------------------------------------------");
            paragraph1.Add("\n");
            paragraph1.Add(ddlUserType.SelectedItem.ToString()+" Ledger:");
            //paragraph1.Add("\n");
            //paragraph1.Add(ddlUserType.SelectedItem.ToString());
            paragraph1.Alignment = Element.ALIGN_CENTER;
            //paragraph1.Add("\n");
            //paragraph1.Add("Below mention price including GST");
            paragraph1.SpacingAfter = 12.5f;

            paragraph.SetLeading(1.0f, 3.0f);
            documnent.Open();
            paragraph.Add("");



            Phrase ph1 = new Phrase();

            Paragraph mm = new Paragraph();
            PdfPTable table = new PdfPTable(6);

            table.SetWidths(new float[] { 1f, 1f,9f,2f, 2f,2f });
            table.AddCell("sr");
            table.AddCell("ID");
            table.AddCell("Name");
            table.AddCell("Total");
            table.AddCell("Paid");
            table.AddCell("Balance");

        
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    table.AddCell(dt.Rows[i]["RowId"].ToString());
                    table.AddCell(dt.Rows[i]["id1"].ToString());
                    table.AddCell(dt.Rows[i]["name1"].ToString());
                    table.AddCell(dt.Rows[i]["total"].ToString());
                    table.AddCell(dt.Rows[i]["paid"].ToString());
                    table.AddCell(dt.Rows[i]["remain"].ToString());
                }
                catch (Exception)
                {
                }
            }
            table.AddCell("");
            table.AddCell("");
            table.AddCell("Total");
            table.AddCell(lblTotalAmount.Text.ToString());
            table.AddCell(lblPaid.Text.ToString());
            table.AddCell(lblRemaining.Text.ToString());



            //Paragraph paragraph2 = new Paragraph();
            //var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, Color.BLUE);
            //string ssss = " ''Morya Tools'' ";
            //paragraph2.Add("Above mention price including GST" + Environment.NewLine + "For iOS");
            //paragraph2.Add(Environment.NewLine + Environment.NewLine + "To Know the Prices,Please Download our app" + ssss);
            //paragraph2.Add(Environment.NewLine + "For Android" + Environment.NewLine);
            ////   paragraph2.Add(Environment.NewLine + Environment.NewLine + "https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en");
            //var c1 = new Chunk("https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en", titleFontBlue);

            //c1.SetAnchor("https://play.google.com/store/apps/details?id=in.co.vsys.moryatools&hl=en");
            //paragraph2.Add(c1);

            ////----
            //var c11 = new Chunk("https://itunes.apple.com/in/app/morya-tools/id1315540530?mt=8", titleFontBlue);

            //c11.SetAnchor("https://itunes.apple.com/in/app/morya-tools/id1315540530?mt=8");

            //paragraph2.Add(Environment.NewLine + "For iOS" + Environment.NewLine);

            //paragraph2.Alignment = Element.ALIGN_CENTER;
            //paragraph2.Add(c11);

            //paragraph2.Alignment = Element.ALIGN_CENTER;

            documnent.Add(paragraph);

            documnent.Add(myImg);
            documnent.Add(paragraph1);
            documnent.Add(table);
            //  documnent.Add(paragraph2);
            mm.Add(ph1);
            para.Add(mm);
            documnent.Add(para);
            Paragraph p2 = new Paragraph();
            p2.SetLeading(5f, 5f);
            documnent.Add(paragraph);
            documnent.Add(p2);
            documnent.Close();
            //DataTable dtPDFPath = new DataTable();
            //dtPDFPath.Columns.Add("PDFPath", typeof(string));
            //dtPDFPath.Rows.Add(@"http://moryaapp.moryatools.com/uploads/" + PDFName + ".pdf");

            DownloadPDF(mstr, "Customer Ledger");

            //String po = "PDFName";
            //DownloadPDF(mstr, po);

            ////-----
            //Response.ContentType = "application/pdf";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=MyFile.pdf");
            //Response.TransmitFile(Server.MapPath("~/uploads/" + PDFName + ".pdf"));
            //Response.End();
        }
        catch (Exception o)
        { }
        finally { }
    }
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
    protected void btnExcelExport_Click(object sender, EventArgs e)
    {
        try
        {

            if (Session["dtProduct"] != null)
            {
                Response.Redirect("ExcelExport.aspx?filename=LowStockProductList_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected void txttsearch_TextChanged(object sender, EventArgs e)
    {
        CurrentPage = 0;
        search();
    }

    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlUserType.SelectedIndex==0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert('Please Select User Type')", "", true);
        }
        else
        {
            CurrentPage = 0;
            search();
        }
       
    }
}
